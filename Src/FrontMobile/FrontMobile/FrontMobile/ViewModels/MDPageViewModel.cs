using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrontMobile.ViewModels
{
    using System.ComponentModel;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    public class MDPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;

        public DelegateCommand LogoutCommand { get; set; }
        public MDPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;

            #region 登出命令
            LogoutCommand = new DelegateCommand(async () =>
            {
                var isLogout = await dialogService.DisplayAlertAsync("警告",
                    "你確定要登出嗎？", "確定", "取消");
                if(isLogout== true)
                {
                    await navigationService.NavigateAsync("/LoginPage");
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
