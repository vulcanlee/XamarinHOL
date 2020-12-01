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
    using Business.Helpers.ManagerHelps;
    using Business.Services;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    public class HomePageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand OnlyAdministratorCommand { get; set; }
        public DelegateCommand OnlyUserCommand { get; set; }
        private readonly INavigationService navigationService;
        private readonly OnlyAdministratorManager onlyAdministratorManager;
        private readonly OnlyUserManager onlyUserManager;
        public string Message { get; set; }

        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService,
            OnlyAdministratorManager OnlyAdministratorManager, OnlyUserManager OnlyUserManager,
            RefreshTokenManager refreshTokenManager,
            SystemStatusManager systemStatusManager, AppStatus appStatus)
        {
            this.navigationService = navigationService;
            onlyAdministratorManager = OnlyAdministratorManager;
            onlyUserManager = OnlyUserManager;

            #region OnlyAdministratorCommand
            OnlyAdministratorCommand = new DelegateCommand(async () =>
            {
                using (IProgressDialog fooIProgressDialog = UserDialogs.Instance.Loading($"請稍後，執行中...", null, null, true, MaskType.Black))
                {
                    bool fooRefreshTokenResult = await RefreshTokenHelper.CheckAndRefreshToken(dialogService, refreshTokenManager, systemStatusManager, appStatus);
                    if (fooRefreshTokenResult == false)
                    {
                        return;
                    }
                    var fooResult = await OnlyAdministratorManager.GetAsync();
                    if(fooResult.Status ==false)
                    {
                        Message = fooResult.Message;
                    }
                    else
                    {
                        Message = fooResult.Payload.ToString();
                    }
                }
            });
            #endregion
            #region OnlyUserCommand
            OnlyUserCommand = new DelegateCommand(async () =>
            {
                using (IProgressDialog fooIProgressDialog = UserDialogs.Instance.Loading($"請稍後，執行中...", null, null, true, MaskType.Black))
                {
                    bool fooRefreshTokenResult = await RefreshTokenHelper.CheckAndRefreshToken(dialogService, refreshTokenManager, systemStatusManager, appStatus);
                    if (fooRefreshTokenResult == false)
                    {
                        return;
                    }
                    var fooResult = await OnlyUserManager.GetAsync();
                    if (fooResult.Status == false)
                    {
                        Message = fooResult.Message;
                    }
                    else
                    {
                        Message = fooResult.Payload.ToString();
                    }
                }
            });
            #endregion
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

    }
}
