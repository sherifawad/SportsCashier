using Newtonsoft.Json;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SportsCashier.ViewModels.PlayersPayment
{
    [QueryProperty("Id", "id")]
    [QueryProperty("Member", "member")]
    public class NewPaymentViewModel : BaseViewModel
    {
        #region Private Property

        private IList<PlayerModel> RemovePlayersList;
        private IList<Sport> SportsList;

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

        public bool popupVisibility { get; set; }

        public bool SavePlayerButtonIsEnabled { get; set; }

        public string MemberShipCode { get; set; }

        public string MemberShipYear { get; set; }

        public PlayerViewModel NewPlayerViewModel { get; set; }

        public PlayerModel SelectedPlayer { get; set; }

        public double Total { get; set; }

        public ObservableCollection<PlayerModel> Players { get; set; }

        public ICommand AddPlayerCommand { get; set; }
        public ICommand SavePlayerCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand RemovePlayerCommand { get; set; }
        public ICommand EditPlayerCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand BackCommand { get; set; }

        #endregion

        #region Constructor

        public NewPaymentViewModel()
        {

            Players = new ObservableCollection<PlayerModel>();
            NewPlayerViewModel = new PlayerViewModel();
            RemovePlayersList = new List<PlayerModel>();
            SportsList = new List<Sport>();

            AddPlayerCommand = new RelayCommand(AddPlayer);
            RemovePlayerCommand = new RelayCommand(async (parameter) => await RemovePlayerAsync(parameter));
            CancelCommand = new RelayCommand(async () => await CancelAsync());
            SavePlayerCommand = new RelayCommand(async () => await SavePlayerAsync());
            EditPlayerCommand = new RelayCommand(async (parameter) => await EditAsync(parameter));
            BackCommand = new RelayCommand(async () => await GoBackAsync());
            SaveCommand = new RelayCommand(async () => await SaveMemberAsync());
        }

        #endregion

        #region Commands Methods


        private async Task SaveMemberAsync()
        {
            if (string.IsNullOrEmpty(MemberShipCode) || string.IsNullOrEmpty(MemberShipYear) || Players?.Count == 0)
                return;

            await RunCommandAsync(() => IsBusy, async () => {

                foreach (var player in RemovePlayersList)
                {
                    await _playersRepository.Delete(player);
                }



                // Save Member to DataBase Without Players
                var member = new MemberModel
                {
                    Id = string.IsNullOrEmpty(Id) ? 0 : int.Parse(Id),
                    MemberShipCode = MemberShipCode,
                    MemberShipYear = MemberShipYear,
                    MembershipNPlayers = Players.ToList()
                };
                await _membersRepository.SaveWithChildrenAsync(member);


                // save sport with children
                foreach (var sport in SportsList)
                {
                    await _sportsRepository.SaveWithChildrenAsync(sport);
                };

                // Save each Member Player to DataBase without Sports
                foreach (var player in Players)
                {
                    var intersect = SportsList.Where(x => player.Sports.Any(y => y.SportName == x.SportName) &&
                    SportsList.Any(y => y.SportCaegory.SportType == x.SportCaegory.SportType)).ToList();

                    //var sp = player.Sports.Intersect(SportsList).ToList();
                    player.Sports = intersect;
                    await _playersRepository.SaveWithChildrenAsync(player);
                    //await _playersRepository.SaveWithChildrenAsync(new PlayerModel { 

                    //    Id = player.Id == 0 ? 0 : player.Id,
                    //    MemberModelId = member.Id,
                    //    PlayerName = player.PlayerName,
                    //    PlayerPayment = player.PlayerPayment,
                    //    Sports = intersect
                    //});
                }
                //// Save Each Sport To DataBase
                //foreach (var sport in Players.SelectMany(p => p.Sports))
                //{
                //    await _sportsRepository.SaveItemAsync(new Sport { 
                //        Id = sport.Id == 0 ? 0 : sport.Id,
                //        SportName = sport.SportName,
                //        SportType = sport.SportCaegory.SportType,
                //        SportPrice = sport.SportCaegory.SportPrice
                //    });
                //};

                ////// Add Players to Member and save with children
                ////member.MembershipNPlayers = Players.ToList();
                ////await _membersRepository.SaveWithChildrenAsync(member);


                //// Save with children
                //foreach (var player in Players)
                //{
                //    await _playersRepository.SaveWithChildrenAsync(player);

                //};





            });

            await _navigationService.InsertAsRoot<HomeViewModel>();

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

        private async Task RemovePlayerAsync(object parameter)
        {

            if (parameter != null && parameter is PlayerModel player)
            {
                var delete = await _dialogService.DisplayAlert("Confirm",
                                    "Are you sure you want to Delete Player? Any unsaved changes will be lost.", "Ok", "Canel");
                if (delete)
                {
                    Players.Remove(player);
                    CalculateTotalPayment();
                    RemovePlayersList.Add(player);
                }

            }
        }

        private void AddPlayer()
        {
            NewPlayerViewModel = new PlayerViewModel();
            popupVisibility = true;
            NewPlayerViewModel.PropertyChanged += NewPlayerViewModel_PropertyChanged;
        }

        private async Task EditAsync(object parameter)
        {
            if (parameter != null && parameter is PlayerModel player)
            {

                //try
                //{
                NewPlayerViewModel = new PlayerViewModel(player);

                //}
                //catch (Exception e)
                //{
                //    if (e.InnerException != null)
                //    {
                //        string err = e.InnerException.Message;
                //    }
                //}
                popupVisibility = true;
                NewPlayerViewModel.PropertyChanged += NewPlayerViewModel_PropertyChanged;

                await Task.CompletedTask;
            }
        }

        private async Task SavePlayerAsync()
        {
            PlayerModel player = null;
            if(SelectedPlayer.Id == 0)
                player = Players.FirstOrDefault(p => p.PlayerName.ToLower() == SelectedPlayer.PlayerName.ToLower());
            else
                player = Players.FirstOrDefault(p => p.Id == SelectedPlayer.Id);

            if (Players.IndexOf(player) == -1)
                Players.Add(SelectedPlayer);
            else
                Players[Players.IndexOf(player)] = SelectedPlayer;

            foreach (var sport in SelectedPlayer.Sports)
            {
                int sportIndex = 0;

                Sport _sport = null;

                // check for sport existance in the local list
                foreach (var localsport in SportsList)
                {
                    _sport = SportsList.FirstOrDefault(s => s.SportName == sport.SportName && s.SportCaegory.SportType == sport.SportCaegory.SportType);
                }

                sportIndex = SportsList.IndexOf(_sport);

            // Check for Sport Is Existing In the data Base
            var dataBasesport = await _sportsRepository.Get(s => s.SportName == sport.SportName &&  s.SportType == sport.SportCaegory.SportType);
                if (dataBasesport != null) 
                    _sport = await _sportsRepository.GetWithChildren(dataBasesport.Id);
                else
                    _sport = new Sport
                    {
                        Id = 0,
                        SportName = sport.SportName,
                        SportCaegory = sport.SportCaegory,
                        Players = new List<PlayerModel>()
                    };

                // Add The player to the sport
                _sport.Players.Add(new PlayerModel { 
                    PlayerName = SelectedPlayer.PlayerName,
                    PlayerPayment = SelectedPlayer.PlayerPayment
                });

                // Add the sport to local Sports List
                if (sportIndex == -1)
                    SportsList.Add(_sport);
                else
                    SportsList[sportIndex] = _sport;

            }

            NewPlayerViewModel.PropertyChanged -= NewPlayerViewModel_PropertyChanged;
            popupVisibility = false;
            CalculateTotalPayment();
            await Task.CompletedTask;
        }

        private async Task CancelAsync()
        {
            NewPlayerViewModel.PropertyChanged -= NewPlayerViewModel_PropertyChanged;
            popupVisibility = false;
            await Task.CompletedTask;
        }


        #endregion

        #region Private Methods

        public override async Task InitializeAsync()
        {
            MemberModel member = null;

            if (!string.IsNullOrEmpty(Id))
            {
                if (!int.TryParse(Id, out int MemberId))
                    await GoBackAsync();

                member = await _membersRepository.GetWithChildren(MemberId);


                var MemberPlayers = member.MembershipNPlayers;

                foreach (var player in MemberPlayers)
                {
                    var p = await _playersRepository.GetWithChildren(player.Id);
                    Players.Add(p);
                }
                MemberShipCode = member.MemberShipCode;
                MemberShipYear = member.MemberShipYear;
            }

            if (!string.IsNullOrEmpty(Member))
            {
                try
                {
                    member = JsonConvert.DeserializeObject<MemberModel>(Member);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    await GoBackAsync();
                }

                foreach (var player in member.MembershipNPlayers)
                {
                    foreach (var sport in player.Sports)
                    {
                        Sport _sport = null;
                        // Check for Sport Is Existing In the data Base
                        var dataBasesport = await _sportsRepository.Get(s => s.SportName == sport.SportName && s.SportType == sport.SportCaegory.SportType);
                        if (dataBasesport != null)
                            _sport = await _sportsRepository.GetWithChildren(dataBasesport.Id);
                        else
                            _sport = new Sport
                            {
                                Id = 0,
                                SportName = sport.SportName,
                                SportCaegory = sport.SportCaegory,
                                Players = new List<PlayerModel>()
                            };

                        // Add The player to the sport
                        _sport.Players.Add(new PlayerModel
                        {
                            PlayerName = player.PlayerName,
                            PlayerPayment = player.PlayerPayment
                        });

                        // Add the sport to local Sports List
                         SportsList.Add(_sport);
                    }
                }

                Players = member.MembershipNPlayers.ToObservableCollection();
                MemberShipCode = member.MemberShipCode;
                MemberShipYear = member.MemberShipYear;


            }

        }


        private void NewPlayerViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var vm = sender as PlayerViewModel;
            SelectedPlayer = vm.Player;
            SavePlayerButtonIsEnabled = vm.IsEnabled;
        }


        private void CalculateTotalPayment()
        {
            var t = (double)default;
            if (Players?.Count == 0)
                return;
            foreach (var player in Players)
            {
                //t += player.Sports.SelectMany(s => s.Caegories).Select(c => c.SportPrice).Sum();
                t += player.PlayerPayment;
            }

            Total = t;
        }



        #endregion


    }
}
