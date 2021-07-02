using DataBase.Models;
using SportsCashier.Common;
using SportsCashier.Common.Extensions;
using SportsCashier.Common.Models;
using SportsCashier.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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

        private ObservableCollection<PlayerDto> _PlayersToPaid = new ObservableCollection<PlayerDto>();
        public ObservableCollection<PlayerDto> PlayersToPaid
        {
            get => _PlayersToPaid;
            set => SetProperty(ref _PlayersToPaid, value);
        }

        private bool _FilterPlayers;
        public bool FilterPlayers
        {
            get => _FilterPlayers;
            set
            {
                SetProperty(ref _FilterPlayers, value);
                GetPalyers();
            }
        }


        private double _Total;
        public double Total
        {
            get => _Total;
            set
            {
                SetProperty(ref _Total, value);
            }
        }

        #endregion


        #region Public Command
        public IAsyncValueCommand<PlayerDto> HidePalyerCommand { get; set; }
        public IAsyncValueCommand<PlayerDto> EditPalyerCommand { get; set; }
        public IAsyncValueCommand<List<SportHistoryDto>> BookmarkAlertCommand { get; set; }
        public IAsyncValueCommand AddPlayerCommand { get; set; }
        public IAsyncValueCommand PayCommand { get; set; }

        #endregion

        public MembershipPlayersDetailViewModel()
        {
            Players = new ObservableCollection<PlayerDto>();
            BookmarkAlertCommand = new AsyncValueCommand<List<SportHistoryDto>>(BookmarkAlertAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            HidePalyerCommand = new AsyncValueCommand<PlayerDto>(HidePalyerAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            EditPalyerCommand = new AsyncValueCommand<PlayerDto>(EditPalyerAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            AddPlayerCommand = new AsyncValueCommand(AddPlayerAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            PayCommand = new AsyncValueCommand(PayAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
        }

        private async ValueTask PayAsync()
        {
            PlayersToPaid.Clear();
            var playerList = Players.Where(x => x.Hide == false);
            foreach (var player in playerList)
            {
                // get history of pay in the current month then select the sports finally get the sport kode to compare.
                var keyToCompare = player.Histories.FirstOrDefault(x => x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year)?
                    .Sports?
                    .Select(x => new { x.Code});
                if(keyToCompare != null)
                    player.Sports.RemoveAll(x => keyToCompare.Any(k => k.Code == x.Code));

                PlayersToPaid.Add(player);
            }
            await Task.FromResult(true);
        }

        public override Task InitializeAsync()
        {
            GetPalyers();
            return base.InitializeAsync();
        }

        internal async Task CalculateAsync()
        {
            var t = (double)default;
            foreach (var player in PlayersToPaid)
            {
                var priceList = player.Sports.Where(s => s?.IsChecked == true).OrderByDescending(p => p.Price).Select(s => s.Price).ToList();
                var listCount = priceList.Count();
                if (listCount >= 3)
                {
                    t = (priceList[0] * 0.8) + (priceList[1] * 0.9);
                    for (int i = 2; i < priceList.Count(); i++)
                    {
                        t += priceList[i];
                    }
                }

                else if (listCount == 2)
                {
                    t += (priceList[0] * 0.9) + priceList[1];
                }
                else
                {
                    t += priceList.Sum();
                }
            }
            Total = t;
            await Task.FromResult(true);
        }

        //public override Task InitializeAsync()
        //{
        //    GetPalyers();
        //    //var list = await _unitOfWork.Repository<Player>().Get(x => x.Hide == false);
        //    //Players = list.ToPlayerDtoList().ToObservableCollection();
        //    //Players = (await _unitOfWork.Repository<DataBasePlayer>().GetAllAsync()).ToObservableCollection();
        //    //Players = (await _dataStore.GetItemsAsync()).ToObservableCollection();
        //}

        #region Commands Methods

        private async ValueTask HidePalyerAsync(PlayerDto arg)
        {
            var dataBasePlayer = await _unitOfWork.Repository<Player>().FindAsync(arg.Id);
            dataBasePlayer.Hide = !dataBasePlayer.Hide;
            var indx = Players.IndexOf(arg);
            arg.Hide = !arg.Hide;
            var editedPlayer = arg;
            Players[indx] = editedPlayer;
            await _unitOfWork.CommitAsync();
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

        #region Private Methods
        private async void GetPalyers()
        {
            List<Player> list = null;
            await Task.Delay(500);
            if (FilterPlayers)
                list = await _unitOfWork.Repository<Player>().Get(null, $"{nameof(Player.Histories)}.{nameof(History.Sports)}");
            else
                list = await _unitOfWork.Repository<Player>().Get(x => x.Hide == false, $"{nameof(Player.Histories)}.{nameof(History.Sports)}");

            if (list != null)
            {
                Players.Clear();
                Players = list.ToPlayerDtoList().ToObservableCollection();
            }
        }

        // check if the same month of the year
        private bool IsSamemonth(DateTime src) => src.Month == DateTime.Now.Month && src.Year == DateTime.Now.Year;
        #endregion
    }
}
