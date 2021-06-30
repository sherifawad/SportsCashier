using DataBase.Models;
using SportsCashier.Common;
using SportsCashier.Common.Extensions;
using SportsCashier.Common.Models;
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

        private ObservableCollection<PlayerDto> _Players;
        public ObservableCollection<PlayerDto> Players
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
        public IAsyncValueCommand<PlayerDto> HidePalyerCommand { get; set; }
        public IAsyncValueCommand<PlayerDto> EditPalyerCommand { get; set; }
        public IAsyncValueCommand<List<SportHistoryDto>> BookmarkAlertCommand { get; set; }
        public IAsyncValueCommand AddPlayerCommand { get; set; }

        #endregion

        public MembershipPlayersDetailViewModel()
        {
            Players = new ObservableCollection<PlayerDto>();
            FilterPlayers = true;
            BookmarkAlertCommand = new AsyncValueCommand<List<SportHistoryDto>>(BookmarkAlertAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            HidePalyerCommand = new AsyncValueCommand<PlayerDto>(HidePalyerAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            EditPalyerCommand = new AsyncValueCommand<PlayerDto>(EditPalyerAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            AddPlayerCommand = new AsyncValueCommand(AddPlayerAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
        }


        public override async Task InitializeAsync()
        {
            var list = await _unitOfWork.Repository<Player>().GetAllAsync();
            Players = list.ToPlayerDtoList().ToObservableCollection();
            //Players = (await _unitOfWork.Repository<DataBasePlayer>().GetAllAsync()).ToObservableCollection();
            //Players = (await _dataStore.GetItemsAsync()).ToObservableCollection();
        }

        #region Commands Methods

        private async ValueTask HidePalyerAsync(PlayerDto arg)
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
            var newPlayer = new Player { Name = result };
            await _unitOfWork.Repository<Player>().AddItemAsync(newPlayer, true);
            //await _dataStore.AddItemAsync(newPlayer);
            Players.Add(newPlayer.ToPlayerDto());
        }

        private async ValueTask EditPalyerAsync(PlayerDto arg)
        {
            if (arg.Id <= 0)
                return;
            await _navigationService.PushAsync<EditPlayerDetailsViewModel>($"{nameof(EditPlayerDetailsViewModel.PlayerId)}={arg.Id}");
        }

        private async ValueTask BookmarkAlertAsync(List<SportHistoryDto> arg)
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
