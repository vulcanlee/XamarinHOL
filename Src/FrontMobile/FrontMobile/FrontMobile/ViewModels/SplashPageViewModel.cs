﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrontMobile.ViewModels
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Acr.UserDialogs;
    using Business.DataModel;
    using Business.DTOs;
    using Business.Helpers.ManagerHelps;
    using Business.Services;
    using CommonLibrary.Helpers.Utilities;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    public class SplashPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;
        private readonly SystemStatusManager systemStatusManager;
        private readonly SystemEnvironmentsManager systemEnvironmentsManager;
        private readonly RecordCacheHelper recordCacheHelper;
        private readonly AppStatus appStatus;
        private readonly ExceptionRecordsManager exceptionRecordsManager;
        private readonly AppExceptionsManager appExceptionsManager;

        public SplashPageViewModel(INavigationService navigationService, IPageDialogService dialogService,
            SystemStatusManager systemStatusManager, SystemEnvironmentsManager systemEnvironmentsManager,
            RecordCacheHelper recordCacheHelper, AppStatus appStatus,
            ExceptionRecordsManager exceptionRecordsManager, AppExceptionsManager appExceptionsManager)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.systemStatusManager = systemStatusManager;
            this.systemEnvironmentsManager = systemEnvironmentsManager;
            this.recordCacheHelper = recordCacheHelper;
            this.appStatus = appStatus;
            this.exceptionRecordsManager = exceptionRecordsManager;
            this.appExceptionsManager = appExceptionsManager;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            await AppStatusHelper.ReadAndUpdateAppStatus(systemStatusManager, appStatus);
            await Task.Delay(3000);
            if (appStatus.SystemStatus.IsLogin == false)
            {
                // 使用者尚未成功登入，切換到登入頁面
                await navigationService.NavigateAsync("/LoginPage");
                return;
            }

            #region 使用者已經成功登入了，接下來要更新相關資料
            if (UtilityHelper.IsConnected() == false)
            {
                await dialogService.DisplayAlertAsync("警告", "無網路連線可用，請檢查網路狀態", "確定");
                return;
            }

            using (IProgressDialog fooIProgressDialog = UserDialogs.Instance.Loading($"請稍後，更新資料中...", null, null, true, MaskType.Black))
            {
                await recordCacheHelper.RefreshAsync(fooIProgressDialog);

                #region 上傳例外異常
                fooIProgressDialog.Title = "請稍後，上傳例外異常";
                await appExceptionsManager.ReadFromFileAsync();
                if (appExceptionsManager.Items.Count > 0)
                {
                    await appExceptionsManager.ReadFromFileAsync();
                    var fooResult = await exceptionRecordsManager.PostAsync(appExceptionsManager.Items);
                    if (fooResult.Status == true)
                    {
                        appExceptionsManager.Items.Clear();
                        await appExceptionsManager.WriteToFileAsync();
                    }
                }
                #endregion
            }

            // 使用者尚已經功登入，切換到首頁頁面
            await navigationService.NavigateAsync("/MDPage/NaviPage/HomePage");
            #endregion
        }

    }
}
