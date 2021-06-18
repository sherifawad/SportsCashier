﻿using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPlayerDetailsView : ContentPage
    {
        public IAsyncCommand<MockSportModel> SportHistoryEditCommand { get;}
        public EditPlayerDetailsView()
        {
            InitializeComponent();
            //Histories = new ObservableRangeCollection<Grouping<string, HistoryGroup>>();
            //var pelPicks = new Grouping<string, HistoryGroup>("Date", new[] { new HistoryGroup(MockPlayer.Histories.OrderBy(x => x.Date).ToList()) });
            //Histories.Add(new[] { pelPicks });
            BindingContext = this;
            SportHistoryEditCommand = new AsyncCommand<MockSportModel>(SportHistoryEditAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
        }

        private async Task SportHistoryEditAsync(MockSportModel arg)
        {
            var sportHistoryPopup = new EditSportHistoryPopup(arg);
            await Navigation.ShowPopupAsync(sportHistoryPopup);
        }

        void OnFabTabTapped(object? sender, TabTappedEventArgs e) => DisplayAlert("FabTabGallery", "Tab Tapped.", "Ok");


        public MockPlayerData MockPlayer => new MockPlayerData
        {
            Name = "Sherif",
            Sports = new List<MockSportModel>
            {
                new MockSportModel {
                    Name = "FootBall", Price = 150, code = 5000300
                } ,
                new MockSportModel {
                    Name = "HandBall", Price = 150, code = 5000900
                } ,
                new MockSportModel {
                    Name = "Swimming", Price = 250, code = 5002103
                } ,
                new MockSportModel {
                    Name = "BasketBall", Price = 150, code = 5000500
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
                            Name = "FootBall", Price = 135, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=1003, code = 5000300, Discount = 10
                        } ,
                        new MockSportModel {
                            Name = "HandBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=1004, code = 5000900, Discount = 0
                        } ,
                        new MockSportModel {
                            Name = "Swimming", Price = 200, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=1500, code = 5002103, Discount = 20
                        } ,
                        new MockSportModel {
                            Name = "BasketBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(1), ReceiteNumber=0023, code = 5000500, Discount = 0
                        }
                    }
                },
                new History
                {
                    Date = DateTime.Now.AddMonths(5),
                    Sports = new List<MockSportModel>
                    {
                        new MockSportModel {
                            Name = "Swimming", Price = 225, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(2), ReceiteNumber=1523, code = 5002102, Discount = 10
                        } ,
                        new MockSportModel {
                            Name = "BasketBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(9), ReceiteNumber=0003, code = 5000500, Discount = 0
                        }
                    }
                },
            }

        };

    }
}