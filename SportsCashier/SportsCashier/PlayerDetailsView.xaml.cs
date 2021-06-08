using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerDetailsView : ContentPage
    {
        const uint ExpandAnimationSpeed = 350;
        const uint CollapseAnimationSpeed = 250;
        public PlayerDetailsView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            historyGrid.TranslationY = 400;
            //CartPopupFade.IsVisible = false;
            //CartPopupFade.Opacity = 0;
        }

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
                    Date = DateTime.Now.AddMonths(3),
                    Sports = new List<MockSportModel>
                    {
                        new MockSportModel {
                            Name = "FootBall", Price = 135
                        } ,
                        new MockSportModel {
                            Name = "HandBall", Price = 150
                        } ,
                        new MockSportModel {
                            Name = "Swimming", Price = 200
                        } ,
                        new MockSportModel {
                            Name = "BasketBall", Price = 150
                        }
                    }
                },
                new History
                {
                    Date = DateTime.Now.AddMonths(5),
                    Sports = new List<MockSportModel>
                    {
                        new MockSportModel {
                            Name = "Swimming", Price = 225
                        } ,
                        new MockSportModel {
                            Name = "BasketBall", Price = 150
                        }
                    }
                },
            }

        };

        public ObservableCollection<MockSportModel> Items => new ObservableCollection<MockSportModel> {
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

        };

        private void historyBtn_Clicked(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "History")
            {
                //CartPopupFade.IsVisible = true;
                bookmarkGrid.FadeTo(0, ExpandAnimationSpeed, Easing.SinInOut);
                ((Button)sender).Text = "X";
                historyGrid.TranslateTo(0, 0, ExpandAnimationSpeed, Easing.SinInOut);
                ((Button)sender).WidthRequest = 40;
                ((Button)sender).HeightRequest = 40;
                ((Button)sender).CornerRadius = 20;

            }
            else
            {
                ((Button)sender).Text = "History";
                ((Button)sender).HeightRequest = 40;
                ((Button)sender).WidthRequest = 90;
                ((Button)sender).CornerRadius = 0;
                historyGrid.TranslateTo(0, 400, CollapseAnimationSpeed, Easing.SinInOut);
                bookmarkGrid.FadeTo(1, CollapseAnimationSpeed, Easing.SinInOut);
                //CartPopupFade.IsVisible = false;
            }
        }
    }


    public class MockPlayerData
    {
        public string Name { get; set; }
        public List<MockSportModel> Sports { get; set; }
        public List<History> Histories { get; set; }
    }

    public class MockSportModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class History
    {
        public DateTime Date { get; set; }
        public List<MockSportModel> Sports { get; set; }

    }
}