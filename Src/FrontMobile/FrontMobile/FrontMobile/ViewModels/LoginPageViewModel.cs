using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrontMobile.ViewModels
{
    using System.ComponentModel;
    using Acr.UserDialogs;
    using Business.DataModel;
    using Business.DTOs;
    using Business.Helpers.ManagerHelps;
    using Business.Services;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    public class LoginPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Account { get; set; }
        public string Password { get; set; }
        public DelegateCommand LoginCommand { get; set; }

        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;
        private readonly LoginManager loginManager;
        private readonly SystemStatusManager systemStatusManager;
        private readonly AppStatus appStatus;
        private readonly RecordCacheHelper recordCacheHelper;

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService,
            LoginManager loginManager, SystemStatusManager systemStatusManager,
            AppStatus appStatus, RecordCacheHelper recordCacheHelper)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.loginManager = loginManager;
            this.systemStatusManager = systemStatusManager;
            this.appStatus = appStatus;
            this.recordCacheHelper = recordCacheHelper;

            #region 登入按鈕命令
            LoginCommand = new DelegateCommand(async () =>
            {
                using (IProgressDialog fooIProgressDialog = UserDialogs.Instance.Loading($"請稍後，使用者登入驗證中...", null, null, true, MaskType.Black))
                {
                    LoginRequestDTO loginRequestDTO = new LoginRequestDTO()
                    {
                        Account = Account,
                        Password = Password,
                    };
                    var fooResult = await LoginUpdateTokenHelper.UserLoginAsync(dialogService, loginManager, systemStatusManager,
                        loginRequestDTO, appStatus);
                    if (fooResult == false)
                    {
                        //await dialogService.DisplayAlertAsync("登入驗證失敗", "登入成功", "OK");
                        return;
                    }
                    await recordCacheHelper.RefreshAsync(fooIProgressDialog);
                }

                await navigationService.NavigateAsync("/MDPage/NaviPage/HomePage");
            });
            #endregion
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
#if DEBUG
            Account = "user1";
            Password = "pw";
#endif
        }

    }
}
