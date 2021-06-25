using SportsCashier.Common;
using SportsCashier.Extensions;
using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
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
            SportDeleteCommand = new AsyncValueCommand<MockSportModel>(SportDeleteAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            MessagingCenter.Subscribe<string, SportsData>(AppConstants.App, AppConstants.SelectedSport, (s, e) => _selectedSportsData = e as SportsData);

        }



        public override Task InitializeAsync()
        {

            var mockPlayer = new MockPlayerData
            {
                Name = "Sherif",
                Sports = new List<MockSportModel>
                {
                    new MockSportModel {
                        Name = "FootBall", Price = 150, Code = 5000300, EditMode = true
                    } ,
                    new MockSportModel {
                        Name = "HandBall", Price = 150, Code = 5000900
                    } ,
                    new MockSportModel {
                        Name = "Swimming", Price = 250, Code = 5002103
                    } ,
                    new MockSportModel {
                        Name = "BasketBall", Price = 150, Code = 5000500
                    }
                },
                Histories = new List<History>
            {
                new History
                {
                    Date = DateTime.Now.AddMonths(1),
                    Sports = new List<MockSportModel>
                    {
                        new MockSportModel {
                            Name = "FootBall", Price = 135, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=1003, Code = 5000300, Discount = 10
                        } ,
                        new MockSportModel {
                            Name = "HandBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=1004, Code = 5000900, Discount = 0
                        } ,
                        new MockSportModel {
                            Name = "Swimming", Price = 200, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=1500, Code = 5002103, Discount = 20
                        } ,
                        new MockSportModel {
                            Name = "BasketBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(1), ReceiteNumber=0023, Code = 5000500, Discount = 0
                        }
                    }
                },
                new History
                {
                    Date = DateTime.Now.AddMonths(5),
                    Sports = new List<MockSportModel>
                    {
                        new MockSportModel {
                            Name = "Swimming", Price = 225, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(2), ReceiteNumber=1523, Code = 5002102, Discount = 10
                        } ,
                        new MockSportModel {
                            Name = "BasketBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(9), ReceiteNumber=0003, Code = 5000500, Discount = 0
                        }
                    }
                },
            }


            };

            Histories = mockPlayer.Histories.ToObservableCollection();
            Sports = mockPlayer.Sports.ToObservableCollection();

            return base.InitializeAsync();
        }

        #region Commands Methods

        private async ValueTask DoneAsync()
        {
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

    }
}
