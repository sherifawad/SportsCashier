using DataBase.Models;
using SportsCashier.Common;
using SportsCashier.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace SportsCashier.ViewModels
{
    public class MembershipPlayersDetailViewModel : ViewModelBase
    {
        #region Public properties

        private ObservableCollection<MockPlayerData> _Players;
        public ObservableCollection<MockPlayerData> Players
        {
            get => _Players; 
            set => SetProperty(ref _Players, value);
        }

        private bool _FilterPlayers;
        public bool FilterPlayers
        {
            get => _FilterPlayers; 
            set => SetProperty(ref _FilterPlayers, value);
        }


        #endregion


        #region Public Command
        public IAsyncValueCommand<MockPlayerData> HidePalyerCommand { get; set; }
        public IAsyncValueCommand<MockPlayerData> EditPalyerCommand { get; set; }
        public IAsyncValueCommand<List<MockSportModel>> BookmarkAlertCommand { get; set; }
        public IAsyncValueCommand AddPlayerCommand { get; set; }

        #endregion

        public MembershipPlayersDetailViewModel()
        {
            Players = new ObservableCollection<MockPlayerData>();
            FilterPlayers = true;
            BookmarkAlertCommand = new AsyncValueCommand<List<MockSportModel>>(BookmarkAlertAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            HidePalyerCommand = new AsyncValueCommand<MockPlayerData>(HidePalyerAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            EditPalyerCommand = new AsyncValueCommand<MockPlayerData>(EditPalyerAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            AddPlayerCommand = new AsyncValueCommand(AddPlayerAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
        }


        public override async Task InitializeAsync()
        {
            Players = (await _dataStore.GetItemsAsync()).ToObservableCollection();
        }

        #region Commands Methods

        private async ValueTask HidePalyerAsync(MockPlayerData arg)
        {
           var indx = Players.IndexOf(arg);
            arg.Hide = !arg.Hide;
            var editedPlayer = arg;
            Players[indx] = editedPlayer;
            await Task.FromResult(true);
        }
        private async ValueTask AddPlayerAsync()
        {
            var result = await _dialogService.DisplayPrompt("New Player", "Add Player Name", "OK", "Cancel");
            if (string.IsNullOrWhiteSpace(result))
                return;
            var newPlayer = new MockPlayerData { Name = result };
            await _dataStore.AddItemAsync(newPlayer);
            Players.Add(newPlayer);
        }

        private async ValueTask EditPalyerAsync(MockPlayerData arg)
        {
            if (arg.Id <= 0)
                return;
            await _navigationService.PushAsync<EditPlayerDetailsViewModel>($"{nameof(EditPlayerDetailsViewModel.PlayerId)}={arg.Id}");
        }

        private async ValueTask BookmarkAlertAsync(List<MockSportModel> arg)
        {
            string message = string.Empty;
            if (arg != null && arg.Count > 0)
            {
                foreach (var sport in arg)
                {
                    message += $"{sport.Name}\n";
                }
                await _dialogService.DisplayAlert("Sports", message, "Ok");
            }

        }

        #endregion
    }
}
