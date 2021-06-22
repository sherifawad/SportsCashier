using dotMorten.Xamarin.Forms;
using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace SportsCashier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPlayerDetailsView : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<History> Histories { get; private set; } = new ObservableCollection<History>();
        public ObservableCollection<MockSportModel> Sports { get; private set; } = new ObservableCollection<MockSportModel>();

        private SportsData _selectedSportsData = null;
        public MockPlayerData MockPlayer { get; private set; }
        public IAsyncCommand<MockSportModel> SportHistoryDeleteCommand { get; }
        public IAsyncCommand<MockSportModel> SportHistoryEditCommand { get; }
        public IAsyncValueCommand<MockSportModel> SportEditCommand { get; }
        public IAsyncValueCommand<MockSportModel> SportEditCancelCommand { get; }
        public IAsyncValueCommand<MockSportModel> SportEditSubmitCommand { get; }
        public IAsyncValueCommand<MockSportModel> SportDeleteCommand { get; }
        public IAsyncValueCommand AddSportCommand { get; }

        public EditPlayerDetailsView(MockPlayerData mockPlayer)
        {
            InitializeComponent();
            MockPlayer = mockPlayer;
            Histories = mockPlayer.Histories;
            Sports = mockPlayer.Sports;
            BindingContext = this;
            SportHistoryEditCommand = new AsyncCommand<MockSportModel>(SportHistoryEditAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportHistoryDeleteCommand = new AsyncCommand<MockSportModel>(SportHistoryDeleteAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportEditCommand = new AsyncValueCommand<MockSportModel>(SportEditAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportEditCancelCommand = new AsyncValueCommand<MockSportModel>(SportEditCancelAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportEditSubmitCommand = new AsyncValueCommand<MockSportModel>(SportEditSubmitAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            AddSportCommand = new AsyncValueCommand(AddSportAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportDeleteCommand = new AsyncValueCommand<MockSportModel>(SportDeleteAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);

            MessagingCenter.Subscribe<string, SportsData>(AppConstants.App, AppConstants.SelectedSport, (s, e) => _selectedSportsData = e as SportsData);
            MessagingCenter.Subscribe<string>(AppConstants.App, AppConstants.ScrollSportsToBottom, (s) =>
            {
                // scroll to bottom
                Device.BeginInvokeOnMainThread(async () =>
                {
                    // Update your children here, add more or remove.
                    // todo

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        await Task.Delay(10); //UI will be updated by Xamarin
                        await sportsScrollView.ScrollToAsync(sportsGridView, ScrollToPosition.End, true);
                    }
                    else
                    {
                        await sportsScrollView.ScrollToAsync(0, sportsGridView.Height, true);

                    }

                });
            });
        }

        public EditPlayerDetailsView()
        {
            InitializeComponent();
            //Histories = new ObservableRangeCollection<Grouping<string, HistoryGroup>>();
            //var pelPicks = new Grouping<string, HistoryGroup>("Date", new[] { new HistoryGroup(MockPlayer.Histories.OrderBy(x => x.Date).ToList()) });
            //Histories.Add(new[] { pelPicks });
            BindingContext = this;
            SportHistoryEditCommand = new AsyncCommand<MockSportModel>(SportHistoryEditAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportHistoryDeleteCommand = new AsyncCommand<MockSportModel>(SportHistoryDeleteAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportEditCommand = new AsyncValueCommand<MockSportModel>(SportEditAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportEditCancelCommand = new AsyncValueCommand<MockSportModel>(SportEditCancelAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportEditSubmitCommand = new AsyncValueCommand<MockSportModel>(SportEditSubmitAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            AddSportCommand = new AsyncValueCommand(AddSportAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            SportDeleteCommand = new AsyncValueCommand<MockSportModel>(SportDeleteAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);

            MessagingCenter.Subscribe<string, SportsData>(AppConstants.App, AppConstants.SelectedSport, (s, e) => _selectedSportsData = e as SportsData);
            MessagingCenter.Subscribe<string>(AppConstants.App, AppConstants.ScrollSportsToBottom, (s) =>
            {
                // scroll to bottom
                Device.BeginInvokeOnMainThread(async () =>
                {
                    // Update your children here, add more or remove.
                    // todo

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        await Task.Delay(10); //UI will be updated by Xamarin
                        await sportsScrollView.ScrollToAsync(sportsGridView, ScrollToPosition.End, true);
                    }
                    else
                    {
                        await sportsScrollView.ScrollToAsync(0, sportsGridView.Height, true);

                    }

                });
            });

            MockPlayer = new MockPlayerData
            {
                Name = "Sherif",
                Sports = new ObservableCollection<MockSportModel>
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
                Histories = new ObservableCollection<History>
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

            Histories = MockPlayer.Histories;
            Sports = MockPlayer.Sports;
        }

        private async Task SportHistoryDeleteAsync(MockSportModel arg)
        {
            foreach (var history in Histories)
            {
                var sport = history.Sports.FirstOrDefault(y => y.ReceiteDate == arg.ReceiteDate && arg.ReceiteNumber == y.ReceiteNumber);
                if (sport != null)
                {
                    history.Sports.Remove(sport);
                    break;
                }
            }
            await Task.FromResult(true);

        }

        private async ValueTask AddSportAsync()
        {
            var sports = Sports ?? new ObservableCollection<MockSportModel>();
            sports.Add(new MockSportModel { EditMode = true });

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
            var sportHistoryPopup = new EditSportHistoryPopup(arg);
            arg = await Navigation.ShowPopupAsync(sportHistoryPopup);
        }

        void OnFabTabTapped(object? sender, TabTappedEventArgs e) => DisplayAlert("FabTabGallery", "Tab Tapped.", "Ok");

    }
}