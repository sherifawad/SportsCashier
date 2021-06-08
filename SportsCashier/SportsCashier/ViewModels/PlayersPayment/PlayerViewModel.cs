using Newtonsoft.Json;
using SportsCashier.Extensions;
using SportsCashier.Helpers;
using SportsCashier.Models;
using SportsCashier.Services.MessagingService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SportsCashier.ViewModels.PlayersPayment
{
    [QueryProperty("PlayerId", "pId")]
    [QueryProperty("MemberId", "mId")]
    public class PlayerViewModel : BaseViewModel
    {
        #region Private Methods

        private string selectedSport;
        private string pId;
        private string mId;
        private int memberId;
        private int playerId;
        private List<Sport> storedSports;
        #endregion

        #region public Property

        public string PlayerId
        {
            get => pId;
            set
            {
                pId = Uri.UnescapeDataString(value);
            }
        }
        public string MemberId
        {
            get => mId;
            set
            {
                mId = Uri.UnescapeDataString(value);
            }
        }

        public string PlayerName { get; set; }

        public ObservableCollection<Sport> Sports { get; set; }

        public ObservableCollection<string> SportsList { get; private set; }

        public ObservableCollection<SportCaegory> SportCaegoriesList { get; private set; }

        public SportCaegory SelectedCaegory { get; set; }

        public string SelectedSport
        {
            get => selectedSport;
            set
            {
                selectedSport = value;
                if(!string.IsNullOrEmpty(value) || storedSports != null)
                    SportCaegoriesList = storedSports.Where(s => s.SportName == value).Select(x => x.SportCaegory).ToObservableCollection();

            }
        }
        #endregion

        #region Public Commands

        public ICommand SavePlayerCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ICommand AddSportCommand { get; set; }
        public ICommand RemoveSportCommand { get; set; }

        #endregion

        #region Constructor
        public PlayerViewModel()
        {
            Sports = new ObservableCollection<Sport>();
            SportsList = new ObservableCollection<string>();

            AddSportCommand = new Command(() => AddSport());
            RemoveSportCommand = new Command((parameter) => RemoveSport(parameter));
            CancelCommand = new RelayCommand(async() => await Cancel());
            SavePlayerCommand = new RelayCommand(async () => await SavePlayerAsync());
        }

        #endregion

        #region Private Method

        public override async Task InitializeAsync()
        {
           storedSports = await _sportsRepository.GetItemsAsync();
            storedSports.ForEach(x =>
            {
                var exist = SportsList.Any(y => y == x.SportName);
                if (!exist) SportsList.Add(x.SportName);
            });

            if (!string.IsNullOrEmpty(MemberId))
            {
                if (!int.TryParse(MemberId, out memberId))
                    await _navigationService.GoBackAsync();
            }

            if (!string.IsNullOrEmpty(PlayerId))
            {
                if (int.TryParse(PlayerId, out playerId))
                {
                    var player = await _playersRepository.GetWithChildren(playerId);
                    if (player != null)
                    {
                        PlayerName = player.PlayerName;
                        Sports = player.Sports.ToObservableCollection();
                    }
                }
            }
        }


        #endregion

        #region Commands Methods

        private void RemoveSport(object parameter)
        {
            if (parameter != null && parameter is Sport sport)
            {

                Sports.Remove(sport);

            }
        }
        private void AddSport()
        {
            if (Sports == null)
                Sports = new ObservableCollection<Sport>();

            if (SelectedSport == null || SelectedCaegory == null)
            {
                _dialogService.DisplayAlert("Error", "EmptyValues", "Ok");
                return;
            }
            if (Sports.FirstOrDefault(s => s.SportName == SelectedSport) == null)
            {
                try
                {
                    var sport = storedSports.FirstOrDefault(x => x.SportName == SelectedSport && x.SportType == SelectedCaegory.SportType);
                    if (sport == null)
                        return;
                    Sports.Add(sport);

                    SelectedSport = default;
                    SelectedCaegory = default;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);

                }


            }
        }


        private async Task SavePlayerAsync()
        {
            await RunCommandAsync(() => IsBusy, async () =>
            {
                try
                {
                    if(string.IsNullOrEmpty(PlayerName) || Sports.Count <= 0)
                    {
                        await _dialogService.DisplayAlert("Error", "EmptyValues", "Ok");

                        return;
                    }
                    var name = PlayerName.Trim().ToLower();

                    var playerExist = await _playersRepository.GetFirstOrDefault(
                        x => x.PlayerName.ToLower() == name &&
                        x.MemberModelId == memberId &&
                        (x.Id != playerId)
                        );
                    if (playerExist != null)
                    {
                        await _dialogService.DisplayAlert("Alert", "Name Dublication", "Ok");
                        return;
                    }
                    var player = new PlayerModel
                    {
                        Id = playerId,
                        PlayerName = PlayerName.Trim(),
                        Sports = new List<Sport>(),
                        MemberModelId = memberId
                    };

                    if (playerId == 0)
                        await _playersRepository.Insert(player);

                    foreach (var sport in Sports)
                    {
                        Sport _sport = null;
                        // Check for Sport Is Existing In the data Base
                        var dataBasesport = await _sportsRepository.GetFirstOrDefault(s => s.SportName == sport.SportName && s.SportType == sport.SportCaegory.SportType);
                        if (dataBasesport == null)
                            continue;

                        _sport = await _sportsRepository.GetWithChildren(dataBasesport.Id);
                        // Add The player to the sport
                        _sport.Players.Add(player);

                        await _sportsRepository.UpdateWithChildren(_sport);
                        // Add the sport to local Sports List
                        player.Sports.Add(_sport);

                    }
                    await _playersRepository.SaveWithChildrenAsync(player);
                    Sports.Clear();
                    PlayerName = string.Empty;
                    await _navigationService.GoBackAsync();

                    //_messagingService.SendMessage(AppConstants.PopupStatus);
                }
                catch (Exception ex)
                {

                    await _dialogService.DisplayAlert("Error", $"{ex.Message}", "Ok");
                }
            });

        }

        private async Task Cancel()
        {
            Sports.Clear();
            PlayerName = string.Empty;
            var shouldGoBack = await _dialogService.DisplayAlert("Confirm", 
                "Are you sure you want to navigate back? Any unsaved changes will be lost.", "Ok", "Canel");
            if (shouldGoBack)
            {
                await _navigationService.GoBackAsync();

            }
            //_messagingService.SendMessage(AppConstants.PopupStatus);

        }

        #endregion

    }
}
