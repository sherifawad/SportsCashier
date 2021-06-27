using SportsCashier.Common.Services;
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

        //protected IGenericDbRepository<MemberModel> _membersRepository { get; }
        //protected IGenericDbRepository<PlayerModel> _playersRepository { get; }
        //protected IGenericDbRepository<Sport> _sportsRepository { get; }
        //protected IGenericDbRepository<PlayerSport> _ps { get; }
        //protected bool IsBusy { get; set; }
        //protected bool CommandRun { get; set; }

        #endregion

        public ViewModelBase()
        {
            _navigationService = DependencyService.Get<INavigationService>();
            _dialogService = DependencyService.Get<IDialogService>();
            _messagingService = DependencyService.Get<IMessagingService>();
            _dataStore = DependencyService.Get<IDataStore<MockPlayerData>>();
        }
        public virtual Task InitializeAsync() => Task.CompletedTask;

        public virtual Task UninitializeAsync() => Task.CompletedTask;
    }
}
