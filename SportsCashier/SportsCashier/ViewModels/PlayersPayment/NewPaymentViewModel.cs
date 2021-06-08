using Newtonsoft.Json;
using QRCoder;
using SportsCashier.DataBase;
using SportsCashier.Extensions;
using SportsCashier.Helpers;
using SportsCashier.Models;
using SportsCashier.Services.DialogService;
using SportsCashier.Services.MessagingService;
using SportsCashier.Services.NavigationService;
using SportsCashier.Views.PlayersPayment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace SportsCashier.ViewModels.PlayersPayment
{
    [QueryProperty("Id", "id")]
    [QueryProperty("Member", "member")]
    public class NewPaymentViewModel : BaseViewModel
    {
        #region Private Property

        private MemberModel _Member;
        private string _id;
        private string _member;
        #endregion

        #region Public Property

        public string Id
        {
            get => _id;
            set
            {
                _id = Uri.UnescapeDataString(value);
            }
        }

        public string Member
        {
            get => _member;
            set
            {
                _member = Uri.UnescapeDataString(value);
            }
        }

        public bool CanSave { get; private set; }
        public bool QrGeneraation { get; private set; }
        public bool MemberShipCodeVisibility { get; private set; }
        public bool popupVisibility { get; set; }
        public ImageSource QrCodeImage { get; set; }

        public string MemberShipCode { get; set; }

        public string MemberShipYear { get; set; }

        public PlayerViewModel NewPlayerViewModel { get; set; }

        public PlayerModel SelectedPlayer { get; set; }

        public ObservableCollection<PlayerModel> Players { get; set; }

        #endregion

        #region Public Commands
        public ICommand AddPlayerCommand { get; set; }
        public ICommand RemovePlayerCommand { get; set; }
        public ICommand EditPlayerCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand ShareCommand { get; set; }
        public ICommand DoneCommand => new RelayCommand(() => popupVisibility = false);

        #endregion

        #region Constructor

        public NewPaymentViewModel()
        {

            Players = new ObservableCollection<PlayerModel>();
            NewPlayerViewModel = new PlayerViewModel();
            MemberShipCodeVisibility = true;
            AddPlayerCommand = new RelayCommand(async () => await AddPlayerAsync());
            RemovePlayerCommand = new RelayCommand(async (parameter) => await RemovePlayerAsync(parameter));
            EditPlayerCommand = new RelayCommand(async (parameter) => await EditAsync(parameter));
            BackCommand = new RelayCommand(async () => await GoBackAsync());
            SaveCommand = new RelayCommand(async () => await SaveMemberAsync());
            ShareCommand = new RelayCommand(async () => await ShareAsync());
            //_messagingService.Subscribe(AppConstants.PopupStatus, z =>
            //{
            //    PlayerListRefresh().SafeFireAndForget(false);
            //    //popupVisibility = false;
            //});
        }

        #endregion

        #region Commands Methods

        private async Task SaveMemberAsync()
        {
            await RunCommandAsync(() => CommandRun, async () =>
            {
                try
                {
                    MemberModel memberExist = null;
                    var code = MemberShipCode.Trim().ToLower();
                    var year = MemberShipYear.Trim().ToLower();
                    if (_Member == null)
                    {
                        memberExist = await _membersRepository.GetFirstOrDefault(x =>
                            x.MemberShipCode == code &&
                            x.MemberShipYear == year);
                    }
                    else
                    {
                        memberExist = await _membersRepository.GetFirstOrDefault(x =>
                            x.MemberShipCode == code &&
                            x.MemberShipYear == year && x.Id != _Member.Id);
                    }

                    if (memberExist != null)
                    {
                        await _dialogService.DisplayAlert("Alert", "MemberShip Number Dublication", "Ok");
                        return;
                    }

                    _Member = new MemberModel
                    {
                        MemberShipCode = code,
                        MemberShipYear = year,
                        MembershipNPlayers = new List<PlayerModel>()
                    };

                    if (_Member.Id == 0)
                    {
                        await _membersRepository.Insert(_Member);
                        if (Players != null && Players.Count > 0)
                        {
                            Players.ToList().ForEach(x =>
                            {
                                x.MemberModelId = _Member.Id;
                                _playersRepository.Insert(x);
                            });
                        }

                        CanSave = false;

                    }
                    else
                    {
                        await _membersRepository.Update(_Member);
                        Players.ToList().ForEach(x =>
                        {
                            x.MemberModelId = _Member.Id;
                            _playersRepository.Update(x);
                        });
                    }

                    MemberShipCodeVisibility = false;
                }
                catch (Exception ex)
                {

                    await _dialogService.DisplayAlert("Error", $"{ex.Message}", "Ok");
                }
            });

            //await _navigationService.InsertAsRoot<HomeViewModel>();

        }
        private async Task GoBackAsync()
        {
            var shouldGoBack = await _dialogService.DisplayAlert("Confirm",
                "Are you sure you want to navigate back? Any unsaved changes will be lost.", "Ok", "Canel");
            if (shouldGoBack)
            {
                await _navigationService.GoBackAsync();

            }
        }
        private async Task ShareAsync()
        {
            await RunCommandAsync(() => QrGeneraation, async () =>
            {
                if (CanSave)
                    return;
                try
                {
                    var exportMember = new MemberModel
                    {
                        MemberShipCode = MemberShipCode,
                        MemberShipYear = MemberShipYear,
                        MembershipNPlayers = new List<PlayerModel>()
                    };
                    foreach (var player in Players)
                    {
                        if (player.PlayerPayment <= 0)
                            continue;
                        player.Id = 0;
                        var exportSports = new List<Sport>();
                        foreach (var sport in player.Sports)
                        {
                            if (!sport.Checked)
                                continue;
                                sport.Id = 0;
                                exportSports.Add(sport);
                        }
                        player.Sports = exportSports;
                        exportMember.MembershipNPlayers.Add(player);
                    }
                    if (exportMember.MembershipNPlayers.Count <= 0)
                        return;
                    var serializedMember = JsonConvert.SerializeObject(exportMember);
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(serializedMember, QRCodeGenerator.ECCLevel.H);
                    PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
                    byte[] qrCodeBytes = qRCode.GetGraphic(20);
                    QrCodeImage = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
                    popupVisibility = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
        }
        private async Task RemovePlayerAsync(object parameter)
        {
            await RunCommandAsync(() => CommandRun, async () =>
            {
                try
                {
                    if (parameter != null && parameter is PlayerModel player)
                    {
                        var delete = await _dialogService.DisplayAlert("Confirm",
                                            "Are you sure you want to Delete Player? Can't undo", "Ok", "Canel");
                        if (delete)
                        {
                            await _playersRepository.Delete(player);
                            Players.Remove(player);
                        }
                    }
                }
                catch (Exception ex)
                {

                    await _dialogService.DisplayAlert("Error", $"{ex.Message}", "Ok");
                }
            });

        }
        private async Task AddPlayerAsync()
        {
            await RunCommandAsync(() => CommandRun, async () =>
            {
                try
                {
                    if (_Member == null)
                        return;
                    await _navigationService.PushAsync<PlayerViewModel>($"mId={_Member.Id}");

                    //_messagingService.SendMessage<int>(AppConstants.MemberId, _Member.Id);
                    //popupVisibility = true;
                }
                catch (Exception ex)
                {

                    await _dialogService.DisplayAlert("Error", $"{ex.Message}", "Ok");
                }
            });
        }
        private async Task EditAsync(object parameter)
        {
            await RunCommandAsync(() => CommandRun, async () =>
            {
                try
                {
                    if (parameter != null && parameter is PlayerModel player)
                    {
                        await _navigationService.PushAsync<PlayerViewModel>($"pId={player.Id}&mId={_Member.Id}");
                        //_messagingService.SendMessage<int>(AppConstants.MemberId, _Member.Id);
                        //_messagingService.SendMessage<int>(AppConstants.PlayerId, player.Id);
                        //popupVisibility = true;
                    }
                }
                catch (Exception ex)
                {

                    await _dialogService.DisplayAlert("Error", $"{ex.Message}", "Ok");
                }
            });
        }

        #endregion

        #region Private Methods

        public override async Task InitializeAsync()
        {

            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    MemberShipCodeVisibility = false;
                    if (!int.TryParse(Id, out int MemberId))
                        await GoBackAsync();

                    _Member = await _membersRepository.GetItemByIdAsync(MemberId);

                    await PlayerListRefresh();
                    MemberShipCode = _Member.MemberShipCode;
                    MemberShipYear = _Member.MemberShipYear;
                }
                if (!string.IsNullOrEmpty(Member))
                {
                    MemberShipCodeVisibility = false;

                    _Member = JsonConvert.DeserializeObject<MemberModel>(Member);

                    _Member.Id = 0;

                    var memberDublication = await _membersRepository.GetFirstOrDefault(x =>
                    x.MemberShipCode == _Member.MemberShipCode &&
                    x.MemberShipYear == _Member.MemberShipYear);

                    if (memberDublication != null)
                    {
                        await _dialogService.DisplayAlert("Dublication", "MemberShip Dublication", "Ok");
                        await _navigationService.GoBackAsync();
                    }


                    foreach (var player in _Member.MembershipNPlayers)
                    {
                        var playersport = new List<Sport>();
                        foreach (var sport in player.Sports)
                        {
                            Sport _sport = null;
                            // Check for Sport Is Existing In the data Base
                            var dataBasesport = await _sportsRepository.GetFirstOrDefault(s => s.SportName == sport.SportName && s.SportType == sport.SportCaegory.SportType);
                            if (dataBasesport != null)
                                _sport = await _sportsRepository.GetWithChildren(dataBasesport.Id);
                            else
                                continue;

                            // Add The player to the sport
                            _sport.Players.Add(new PlayerModel
                            {
                                PlayerName = player.PlayerName,
                                PlayerPayment = player.PlayerPayment
                            });
                            _sport.Checked = false;
                            playersport.Add(_sport);

                        }

                        Players.Add(new PlayerModel
                        {
                            PlayerName = player.PlayerName,
                            PlayerPayment = player.PlayerPayment,
                            Sports = playersport
                        });
                    }

                    MemberShipCode = _Member.MemberShipCode;
                    MemberShipYear = _Member.MemberShipYear;

                    CanSave = true;

                }
                else
                    await PlayerListRefresh();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await _navigationService.GoBackAsync();
            }
        }

        public override Task UninitializeAsync()
        {
            IsBusy = false;
            return base.UninitializeAsync();
        }
        private async Task PlayerListRefresh()
        {
            try
            {
                if (_Member == null)
                    return;
                var playersList = await _playersRepository.GetChildrens(x => x.MemberModelId == _Member.Id);
                if (playersList == null || playersList.Count <= 0)
                    return;
                Players.Clear();
                playersList.ForEach(x => Players.Add(x));
            }
            catch (Exception ex)
            {

                await _dialogService.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }

        #endregion


    }
}
