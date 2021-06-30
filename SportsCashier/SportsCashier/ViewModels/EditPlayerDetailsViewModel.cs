using DataBase.Models;
using SportsCashier.Common;
using SportsCashier.Common.Models;
using SportsCashier.Extensions;
using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using SportsCashier.Common.Extensions;

namespace SportsCashier.ViewModels
{
    [QueryProperty("PlayerId", "PlayerId")]
    public class EditPlayerDetailsViewModel : ViewModelBase
    {
        #region Private Properties
        private string _PlayerId;
        private ObservableCollection<HistoryDto> histories;
        private ObservableCollection<PlayerSport> sports;
        private SportsData _selectedSportsData;
        private string _Image;
        private string _Name;
        private PlayerDto mockPlayer;
        private Player dataBasePlayer;
        #endregion

        #region Public Properties

        public ObservableCollection<HistoryDto> Histories
        {
            get => histories;
            private set => SetProperty(ref histories, value);
        }
        public ObservableCollection<PlayerSport> Sports
        {
            get => sports;
            private set => SetProperty(ref sports, value);
        }

        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }
        public string Image
        {
            get => _Image;
            private set => SetProperty(ref _Image, value);
        }



        public string PlayerId
        {
            get => _PlayerId;
            set
            {
                _PlayerId = Uri.UnescapeDataString(value);
            }
        }
        #endregion

        #region Public Commands

        public IAsyncCommand<SportHistoryDto> SportHistoryDeleteCommand { get; }
        public IAsyncCommand<SportHistoryDto> SportHistoryEditCommand { get; }
        public IAsyncValueCommand<PlayerSport> SportEditCommand { get; }
        public IAsyncValueCommand<PlayerSport> SportEditCancelCommand { get; }
        public IAsyncValueCommand<PlayerSport> SportEditSubmitCommand { get; }
        public IAsyncValueCommand<PlayerSport> SportDeleteCommand { get; }
        public IAsyncValueCommand AddSportCommand { get; }
        public IAsyncValueCommand DoneCommand { get; }
        public IAsyncValueCommand ChangeImageCommand { get; }

        #endregion


        public EditPlayerDetailsViewModel()
        {
            Histories = new ObservableCollection<HistoryDto>();
            Sports = new ObservableCollection<PlayerSport>();

            SportHistoryEditCommand = new AsyncCommand<SportHistoryDto>(SportHistoryEditAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportHistoryDeleteCommand = new AsyncCommand<SportHistoryDto>(SportHistoryDeleteAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportEditCommand = new AsyncValueCommand<PlayerSport>(SportEditAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportEditCancelCommand = new AsyncValueCommand<PlayerSport>(SportEditCancelAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportEditSubmitCommand = new AsyncValueCommand<PlayerSport>(SportEditSubmitAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            AddSportCommand = new AsyncValueCommand(AddSportAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            DoneCommand = new AsyncValueCommand(DoneAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            ChangeImageCommand = new AsyncValueCommand(ChangeImageAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportDeleteCommand = new AsyncValueCommand<PlayerSport>(SportDeleteAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            MessagingCenter.Subscribe<string, SportsData>(AppConstants.App, AppConstants.SelectedSport, (s, e) => _selectedSportsData = e as SportsData);

        }


        public override async Task InitializeAsync()
        {
            if (string.IsNullOrEmpty(PlayerId))
                return;
            dataBasePlayer = await _dataStore.GetItemAsync(int.Parse(PlayerId));
            if (dataBasePlayer == null)
                return;

            mockPlayer = dataBasePlayer.ToPlayerDto();



            Name = mockPlayer.Name;
            Image = mockPlayer.Image;
            if (mockPlayer.Histories != null)
                Histories = mockPlayer.Histories.ToObservableCollection();
            if (mockPlayer.Sports != null)
                Sports = mockPlayer.Sports.ToObservableCollection();
        }

        #region Commands Methods


        private async ValueTask ChangeImageAsync()
        {
            try
            {
                FileResult photo = null;
                var choiceResult = await _dialogService.DisplayActionSheet("Change Image", "Cancel", null,  "Camera", "Gallery");
                switch (choiceResult)
                {
                    case "Camera":
                        photo = await MediaPicker.CapturePhotoAsync();
                        break;
                    case "Gallery":
                        photo = await MediaPicker.PickPhotoAsync();
                        break;
                    default:
                        return;
                }
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {Image}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is now supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        private async ValueTask DoneAsync()
        {
            dataBasePlayer.Name = Name;
            dataBasePlayer.Image = Image;
            //await _dataStore.UpdateItemAsync(mockPlayer);
            List<int> sports = new List<int>();
            foreach (var item in Sports)
            {
                sports.Add(item.Code);
            }
            dataBasePlayer.Sports = sports;
            dataBasePlayer.Histories = Histories.ToList().ToHistoryList();
            await _unitOfWork.CommitAsync();
            await _navigationService.PopAsync();
        }

        private async Task SportHistoryDeleteAsync(SportHistoryDto arg)
        {
            HistoryDto currentHistory = null;
            int historyIndex = -1;
            foreach (var history in Histories)
            {
                var sport = history.Sports.FirstOrDefault(y => y.ReceiteDate == arg.ReceiteDate && arg.ReceiteNumber == y.ReceiteNumber);
                if (sport != null)
                {
                    history.Sports.Remove(sport);
                    currentHistory = history;
                    historyIndex = Histories.IndexOf(history);
                    Histories.Remove(history);
                    break;
                }
            }
            Histories[historyIndex] = currentHistory;
            await Task.FromResult(true);

        }

        private async ValueTask AddSportAsync()
        {
            var sportInEditMode = Sports?.FirstOrDefault(x => x.EditMode == true);
            if (sportInEditMode != null)
                return;
            var sports = Sports ?? new ObservableCollection<PlayerSport>();
            sports.Add(new PlayerSport { EditMode = true });

            await Task.FromResult(true);

        }

        private async ValueTask SportEditAsync(PlayerSport arg)
        {
            arg.EditMode = true;
            var indx = Sports.IndexOf(arg);
            if (indx < 0)
                return;
            Sports[indx] = arg;
            await Task.FromResult(true);
        }


        private async ValueTask SportDeleteAsync(PlayerSport arg)
        {
            //var sport = MockPlayer.Sports.FirstOrDefault(x => x.code == arg.code);
            //if (sport != null)
            Sports.Remove(arg);
            await Task.FromResult(true);
        }

        private async ValueTask SportEditSubmitAsync(PlayerSport arg)
        {
            var indx = Sports.IndexOf(arg);
            if (indx < 0 || _selectedSportsData == null)
                return;
            var newSport = new PlayerSport
            {
                Code = _selectedSportsData.Code
            };
            Sports[indx] = newSport;
            _selectedSportsData = null;
            await Task.FromResult(true);

        }

        private async ValueTask SportEditCancelAsync(PlayerSport arg)
        {
            var indx = Sports.IndexOf(arg);
            if (indx < 0 || string.IsNullOrEmpty(arg.Name))
                return;
            arg.EditMode = false;
            Sports[indx] = arg;
            await Task.FromResult(true);

        }

        private async Task SportHistoryEditAsync(SportHistoryDto arg)
        {
            HistoryDto currentHistory = null;
            int histroryIndex = -1;
            

            if (arg == null)
            {
                var sportHistoryPopup = new EditSportHistoryPopup();

                var popupResult = await _navigationService.PopUp(sportHistoryPopup);

                if (popupResult == null)
                    return;

                var result = Histories.FirstOrDefault(x => x.Date.Year == popupResult.ReceiteDate.Year && x.Date.Month == popupResult.ReceiteDate.Month);

                if (result == null)
                {
                    Histories.Add(new HistoryDto { Date = popupResult.ReceiteDate, Sports = new List<SportHistoryDto> { popupResult } });
                }
                else
                {
                    histroryIndex = Histories.IndexOf(result);
                    result.Sports.Add(popupResult);
                    currentHistory = result;
                    Histories.Remove(result);

                }
            }
            else
            {
                var sportHistoryPopup = new EditSportHistoryPopup(arg);

                var popupResult = await _navigationService.PopUp(sportHistoryPopup);

                if (popupResult == null)
                    return;

                foreach (var history in Histories)
                {
                    var result = history.Sports.FirstOrDefault(y => y.ReceiteDate == arg.ReceiteDate && arg.ReceiteNumber == y.ReceiteNumber);
                    if (result != null)
                    {
                        histroryIndex = Histories.IndexOf(history);
                        result = popupResult;
                        currentHistory = history;
                        Histories.Remove(history);
                        //sportIndex = history.Sports.IndexOf(result);
                        //sport = result;
                        break;
                    }
                };
            }
            if (histroryIndex > 0)
                Histories.Insert(histroryIndex, currentHistory);
        }

        #endregion

        #region Private Methods

        private async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                Image = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            // Delete Previous Stored File
            if (File.Exists(Image))
                File.Delete(Image);

            Image = newFile;
        }

        #endregion

    }
}
