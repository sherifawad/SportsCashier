using SportsCashier.Common;
using SportsCashier.Extensions;
using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SportsCashier.ViewModels
{
    [QueryProperty("PlayerId", "PlayerId")]
    public class EditPlayerDetailsViewModel : ViewModelBase
    {
        #region Private Properties
        private string _PlayerId;
        private ObservableCollection<History> histories;
        private ObservableCollection<MockSportModel> sports;
        private SportsData _selectedSportsData;
        private string _Image;
        private string _Name;
        private MockPlayerData mockPlayer;
        #endregion

        #region Public Properties



        public ObservableCollection<History> Histories
        {
            get => histories;
            private set => SetProperty(ref histories, value);
        }
        public ObservableCollection<MockSportModel> Sports
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

        public IAsyncCommand<MockSportModel> SportHistoryDeleteCommand { get; }
        public IAsyncCommand<MockSportModel> SportHistoryEditCommand { get; }
        public IAsyncValueCommand<MockSportModel> SportEditCommand { get; }
        public IAsyncValueCommand<MockSportModel> SportEditCancelCommand { get; }
        public IAsyncValueCommand<MockSportModel> SportEditSubmitCommand { get; }
        public IAsyncValueCommand<MockSportModel> SportDeleteCommand { get; }
        public IAsyncValueCommand AddSportCommand { get; }
        public IAsyncValueCommand DoneCommand { get; }
        public IAsyncValueCommand ChangeImageCommand { get; }

        #endregion


        public EditPlayerDetailsViewModel()
        {
            Histories = new ObservableCollection<History>();
            Sports = new ObservableCollection<MockSportModel>();

            SportHistoryEditCommand = new AsyncCommand<MockSportModel>(SportHistoryEditAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportHistoryDeleteCommand = new AsyncCommand<MockSportModel>(SportHistoryDeleteAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportEditCommand = new AsyncValueCommand<MockSportModel>(SportEditAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportEditCancelCommand = new AsyncValueCommand<MockSportModel>(SportEditCancelAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportEditSubmitCommand = new AsyncValueCommand<MockSportModel>(SportEditSubmitAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            AddSportCommand = new AsyncValueCommand(AddSportAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            DoneCommand = new AsyncValueCommand(DoneAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            ChangeImageCommand = new AsyncValueCommand(ChangeImageAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportDeleteCommand = new AsyncValueCommand<MockSportModel>(SportDeleteAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            MessagingCenter.Subscribe<string, SportsData>(AppConstants.App, AppConstants.SelectedSport, (s, e) => _selectedSportsData = e as SportsData);

        }


        public override async Task InitializeAsync()
        {
            if (string.IsNullOrEmpty(PlayerId))
                return;
            mockPlayer = await _dataStore.GetItemAsync(PlayerId);

            if (mockPlayer == null)
                return;

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
            mockPlayer.Name = Name;
            mockPlayer.Image = Image;
            mockPlayer.Sports = sports.ToList();
            mockPlayer.Histories = Histories.ToList();
            await _dataStore.UpdateItemAsync(mockPlayer);
            //TODO save To dataBase
            await _navigationService.PopAsync();
        }

        private async Task SportHistoryDeleteAsync(MockSportModel arg)
        {
            History currentHistory = null;
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
            var sports = Sports ?? new ObservableCollection<MockSportModel>();
            sports.Add(new MockSportModel { EditMode = true, Name = "" });

            await Task.FromResult(true);

        }

        private async ValueTask SportEditAsync(MockSportModel arg)
        {
            arg.EditMode = true;
            var indx = Sports.IndexOf(arg);
            if (indx < 0)
                return;

            Sports[indx] = arg;
            await Task.FromResult(true);
        }


        private async ValueTask SportDeleteAsync(MockSportModel arg)
        {
            //var sport = MockPlayer.Sports.FirstOrDefault(x => x.code == arg.code);
            //if (sport != null)
            Sports.Remove(arg);
            await Task.FromResult(true);
        }

        private async ValueTask SportEditSubmitAsync(MockSportModel arg)
        {
            var indx = Sports.IndexOf(arg);
            if (indx < 0 || _selectedSportsData == null)
                return;
            var newSport = new MockSportModel
            {
                Name = _selectedSportsData.Name,
                EditMode = false,
                Alert = arg.Alert,
                Code = _selectedSportsData.Code,
                Icon = _selectedSportsData.Icon,
                Discount = arg.Discount,
                Price = arg.Price,
                ReceiteDate = arg.ReceiteDate,
                ReceiteNumber = arg.ReceiteNumber
            };
            Sports[indx] = newSport;
            _selectedSportsData = null;
            await Task.FromResult(true);

        }

        private async ValueTask SportEditCancelAsync(MockSportModel arg)
        {
            var indx = Sports.IndexOf(arg);
            if (indx < 0 || string.IsNullOrEmpty(arg.Name))
                return;
            arg.EditMode = false;
            Sports[indx] = arg;
            await Task.FromResult(true);

        }

        private async Task SportHistoryEditAsync(MockSportModel arg)
        {
            History currentHistory = null;
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
                    Histories.Add(new History { Date = popupResult.ReceiteDate, Sports = new List<MockSportModel> { popupResult } });
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
