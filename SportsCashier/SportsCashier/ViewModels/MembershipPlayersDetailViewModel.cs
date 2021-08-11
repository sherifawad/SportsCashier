using DataBase.Models;
using Forms9Patch;
using Newtonsoft.Json;
using QRCoder;
using SportsCashier.Common;
using SportsCashier.Common.Extensions;
using SportsCashier.Common.Models;
using SportsCashier.Extensions;
using SportsCashier.RazorTemplates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Debug = System.Diagnostics.Debug;

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


        private bool _EditCode;
        public bool EditCode
        {
            get => _EditCode;
            set
            {
                SetProperty(ref _EditCode, value);
            }
        }


        private decimal _Total;
        public decimal Total
        {
            get => _Total;
            set
            {
                SetProperty(ref _Total, value);
            }
        }

        private string _FullMembershipCode;
        public string FullMembershipCode
        {
            get => _FullMembershipCode;
            set
            {
                SetProperty(ref _FullMembershipCode, value);
            }
        }


        private string _MembershipCode;
        public string MembershipCode
        {
            get => _MembershipCode;
            set
            {
                SetProperty(ref _MembershipCode, value);
            }
        }

        private string _MembershipYear;
        public string MembershipYear
        {
            get => _MembershipYear;
            set
            {
                SetProperty(ref _MembershipYear, value);
            }
        }

        #endregion


        #region Public Command
        public IAsyncCommand CancelCodeCommand => new AsyncCommand(() => { EditCode = false; return Task.FromResult(true); }, allowsMultipleExecutions: false);
        public IAsyncCommand EditCodeCommand => new AsyncCommand(() => { EditCode = true; return Task.FromResult(true); }, allowsMultipleExecutions: false);
        public IAsyncCommand SaveCodeCommand { get; }
        public IAsyncCommand<PlayerDto> HidePalyerCommand { get; }
        public IAsyncCommand<PlayerDto> EditPalyerCommand { get; }
        public IAsyncCommand<List<SportHistoryDto>> BookmarkAlertCommand { get; }
        public IAsyncCommand AddPlayerCommand { get; }
        public IAsyncCommand PayCommand { get; }
        public IAsyncCommand QrCommand { get; }
        public IAsyncCommand PDFCommand { get; }

        #endregion

        public MembershipPlayersDetailViewModel()
        {
            Players = new ObservableCollection<PlayerDto>();
            BookmarkAlertCommand = new AsyncCommand<List<SportHistoryDto>>(BookmarkAlertAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            HidePalyerCommand = new AsyncCommand<PlayerDto>(HidePalyerAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            EditPalyerCommand = new AsyncCommand<PlayerDto>(EditPalyerAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            AddPlayerCommand = new AsyncCommand(AddPlayerAsync);
            PayCommand = new AsyncCommand(PayAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SaveCodeCommand = new AsyncCommand(SaveCodeAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            QrCommand = new AsyncCommand(QrAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            PDFCommand = new AsyncCommand(PDFAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SetFullMembershipCode();
        }


        private Task SaveCodeAsync()
        {
            Preferences.Set(AppConstants.Membership_Code, MembershipCode);
            Preferences.Set(AppConstants.Membership_Year, MembershipYear);
            SetFullMembershipCode(MembershipCode, MembershipYear);
            EditCode = false;
            return Task.FromResult(true);
        }

        private async Task PayAsync()
        {
            PlayersToPaid.Clear();
            var playerList = Players.Where(x => x.Hide == false);
            foreach (var player in playerList)
            {
                // get history of pay in the current month then select the sports finally get the sport kode to compare.
                var keyToCompare = player.Histories.FirstOrDefault(x => x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year)?
                    .Sports?
                    .Select(x => new { x.Code });
                if (keyToCompare != null)
                    player.Sports.RemoveAll(x => keyToCompare.Any(k => k.Code == x.Code));

                PlayersToPaid.Add(player);
            }
            await Task.FromResult(true);
        }

        private void SetFullMembershipCode(string membershipcode = "", string membershipYear = "")
        {
            if (string.IsNullOrEmpty(membershipcode))
                membershipcode = Preferences.Get(AppConstants.Membership_Code, string.Empty);
            if (string.IsNullOrEmpty(membershipYear))
                membershipYear = Preferences.Get(AppConstants.Membership_Year, string.Empty);
            FullMembershipCode = $"{membershipYear}/{membershipcode}";
        }

        public override Task InitializeAsync()
        {
            GetPalyers();
            return base.InitializeAsync();
        }

        public async Task<Payoutput> PayOutPutResult()
        {
            var playerPayoutput = new List<PlayerPayoutput>();

            foreach (var player in PlayersToPaid)
            {
                var priceList = player.Sports.Where(s => s?.IsChecked == true).OrderByDescending(p => p.Price).ToList();
                List<int> spoertsCodeList = new List<int>();
                priceList.ForEach(x => spoertsCodeList.Add(x.Code));
                if(spoertsCodeList.Count > 0)
                    playerPayoutput.Add(new PlayerPayoutput { Name = player.Name, SportsCodeList = spoertsCodeList });
            }

            if(playerPayoutput.Count > 0)
                return await Task.FromResult(new Payoutput { MemberShipCode = FullMembershipCode, PlayerPayoutput = playerPayoutput });

            return null;
        }
        public async Task CalculateAsync()
        {
            var t = (decimal)default;
            foreach (var player in PlayersToPaid)
            {
                //var priceList = player.Sports.Where(s => s?.IsChecked == true).OrderByDescending(p => p.Price).Select(s => s.Price).ToList();
                var priceList = player.Sports.Where(s => s?.IsChecked == true).OrderByDescending(p => p.Price).ToList();
                var unCheckedList = player.Sports.Where(s => s?.IsChecked == false).ToList();
                var listCount = priceList.Count();


                for (int i = 0; i < unCheckedList?.Count(); i++)
                {
                    unCheckedList[i].Discount = 0;
                    unCheckedList[i].Total = 0;
                }
                t = (decimal)default;
                if (listCount >= 3)
                {
                    priceList[0].Discount = 20;
                    priceList[0].Total = priceList[0].Price * 0.8m;
                    priceList[1].Discount = 10;
                    priceList[1].Total = priceList[1].Price * 0.9m;
                    t = (priceList[0].Total) + (priceList[1].Total);
                    for (int i = 2; i < priceList.Count(); i++)
                    {
                        priceList[i].Discount = 0;
                        priceList[i].Total = priceList[i].Price;
                        t += priceList[i].Total;
                    }
                }

                else if (listCount == 2)
                {
                    priceList[0].Discount = 10;
                    priceList[0].Total = priceList[0].Price * 0.9m;
                    priceList[1].Discount = 0;
                    priceList[1].Total = priceList[1].Price;
                    t += (priceList[0].Total) + priceList[1].Total;
                }
                else
                {
                    //t += priceList.Sum(x => x.Price);
                    for (int i = 0; i < priceList?.Count(); i++)
                    {
                        priceList[i].Discount = 0;
                        priceList[i].Total = priceList[i].Price;
                        t += priceList[i].Total;
                    }
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

        private async Task PDFAsync()
        {
            var output = await PayOutPutResult();
            if (output == null)
                return;
            var billTemplate = new PayoutPdf();
            billTemplate.Model = output;
            var htmlString = billTemplate.GenerateString();
            if (!string.IsNullOrEmpty(htmlString) && PrintService.CanPrint)
            {
                await htmlString.PrintAsync(Guid.NewGuid().ToString());
            }
        }

        private async Task QrAsync()
        {
            var output = await PayOutPutResult();
            if (output == null)
                return;
            var qrPopup = new QrPopup(output);

            await _navigationService.PopUp(qrPopup);
        
         }


        private async Task HidePalyerAsync(PlayerDto arg)
        {
            var dataBasePlayer = await _unitOfWork.Repository<Player>().FindAsync(arg.Id);
            dataBasePlayer.Hide = !dataBasePlayer.Hide;
            var indx = Players.IndexOf(arg);
            arg.Hide = !arg.Hide;
            var editedPlayer = arg;
            Players[indx] = editedPlayer;
            await _unitOfWork.CommitAsync();
            GetPalyers();
        }
        private async Task AddPlayerAsync()
        {
            var result = await _dialogService.DisplayPrompt("New Player", "Add Player Name", "OK", "Cancel");
            if (string.IsNullOrWhiteSpace(result))
                return;
            var newPlayer = new Player { Name = result };
            await _unitOfWork.Repository<Player>().AddItemAsync(newPlayer, true);
            //await _dataStore.AddItemAsync(newPlayer);
            Players.Add(newPlayer.ToPlayerDto());
        }

        private async Task EditPalyerAsync(PlayerDto arg)
        {
            if (arg.Id <= 0)
                return;
            await _navigationService.PushAsync<EditPlayerDetailsViewModel>($"{nameof(EditPlayerDetailsViewModel.PlayerId)}={arg.Id}");
        }

        private async Task BookmarkAlertAsync(List<SportHistoryDto> arg)
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
