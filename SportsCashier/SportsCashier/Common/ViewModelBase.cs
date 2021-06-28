using DataBase.Models;
using SportsCashier.Common.Services;
using SportsCashier.Common.Services.UnitOfWork;
using SportsCashier.Models;
using SportsCashier.Services.DialogService;
using SportsCashier.Services.MessagingService;
using SportsCashier.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace SportsCashier.Common
{
    public class ViewModelBase : ObservableObject
    {
        #region Protected Members

        protected INavigationService _navigationService { get; }
        protected IDialogService _dialogService { get; }
        protected IMessagingService _messagingService { get; }
        protected IDataStore<MockPlayerData> _dataStore  { get; }
        protected IUnitOfWork _unitOfWork  { get; }

        #endregion

        public ViewModelBase()
        {
            _navigationService = DependencyService.Get<INavigationService>();
            _dialogService = DependencyService.Get<IDialogService>();
            _messagingService = DependencyService.Get<IMessagingService>();
            _dataStore = DependencyService.Get<IDataStore<MockPlayerData>>();
            _unitOfWork = DependencyService.Get<IUnitOfWork>();
        }
        public virtual Task InitializeAsync() => Task.CompletedTask;

        public virtual Task UninitializeAsync() => Task.CompletedTask;
    }
}
