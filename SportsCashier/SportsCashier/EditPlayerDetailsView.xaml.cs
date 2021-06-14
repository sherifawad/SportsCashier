using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPlayerDetailsView : ContentPage
    {
        public ObservableRangeCollection<Grouping<string, HistoryGroup>> Histories { get; set; }

        public EditPlayerDetailsView()
        {
            InitializeComponent();
            Histories = new ObservableRangeCollection<Grouping<string, HistoryGroup>>();
            var pelPicks = new Grouping<string, HistoryGroup>("Date", new[] { new HistoryGroup(MockPlayer.Histories.OrderBy(x => x.Date).ToList()) });
            Histories.Add(new[] { pelPicks });
            BindingContext = this;
        }
        void OnFabTabTapped(object? sender, TabTappedEventArgs e) => DisplayAlert("FabTabGallery", "Tab Tapped.", "Ok");


        public MockPlayerData MockPlayer => new MockPlayerData
        {
            Name = "Sherif",
            Sports = new List<MockSportModel>
            {
                new MockSportModel {
                    Name = "FootBall", Price = 150
                } ,
                new MockSportModel {
                    Name = "HandBall", Price = 150
                } ,
                new MockSportModel {
                    Name = "Swimming", Price = 250
                } ,
                new MockSportModel {
                    Name = "BasketBall", Price = 150
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
                            Name = "FootBall", Price = 135, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=1003
                        } ,
                        new MockSportModel {
                            Name = "HandBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=1004
                        } ,
                        new MockSportModel {
                            Name = "Swimming", Price = 200, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=1500
                        } ,
                        new MockSportModel {
                            Name = "BasketBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(1), ReceiteNumber=0023
                        }
                    }
                },
                new History
                {
                    Date = DateTime.Now.AddMonths(5),
                    Sports = new List<MockSportModel>
                    {
                        new MockSportModel {
                            Name = "Swimming", Price = 225, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(2), ReceiteNumber=1523
                        } ,
                        new MockSportModel {
                            Name = "BasketBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(9), ReceiteNumber=0003
                        }
                    }
                },
            }

        };

    }
}