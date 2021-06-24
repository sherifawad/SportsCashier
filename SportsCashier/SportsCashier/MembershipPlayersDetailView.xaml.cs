using SportsCashier.Common.Helpers.Commands;
using SportsCashier.Models;
using SportsCashier.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MembershipPlayersDetailView : ContentPage, INotifyPropertyChanged
    {
        private readonly MembershipPlayersDetailViewModel _viewModel;


        public MembershipPlayersDetailView()
        {
            InitializeComponent();
             
            BindingContext = _viewModel = new MembershipPlayersDetailViewModel();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel != null)
                _viewModel.InitializeAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (_viewModel != null)
                _viewModel.UninitializeAsync();
        }


        //public MockPlayerData MockPlayer { get; set; } = new MockPlayerData
        //{
        //    Name = "Sherif",
        //    Sports = new List<MockSportModel>
        //    {
        //        new MockSportModel {
        //            Name = "FootBall", Price = 150
        //        } ,
        //        new MockSportModel {
        //            Name = "HandBall", Price = 150
        //        } ,
        //        new MockSportModel {
        //            Name = "Swimming", Price = 250
        //        } ,
        //        new MockSportModel {
        //            Name = "BasketBall", Price = 150
        //        }
        //    },
        //    Histories = new List<History>
        //    {
        //        new History
        //        {
        //            Date = DateTime.Now.AddMonths(1),
        //            Sports = new List<MockSportModel>
        //            {
        //                new MockSportModel {
        //                    Name = "FootBall", Price = 135, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=1003
        //                } ,
        //                new MockSportModel {
        //                    Name = "HandBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=1004
        //                } ,
        //                new MockSportModel {
        //                    Name = "Swimming", Price = 200, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=1500
        //                } ,
        //                new MockSportModel {
        //                    Name = "BasketBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(1), ReceiteNumber=0023
        //                }
        //            }
        //        },
        //        new History
        //        {
        //            Date = DateTime.Now.AddMonths(5),
        //            Sports = new List<MockSportModel>
        //            {
        //                new MockSportModel {
        //                    Name = "Swimming", Price = 225, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(2), ReceiteNumber=1523
        //                } ,
        //                new MockSportModel {
        //                    Name = "BasketBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(9), ReceiteNumber=0003
        //                }
        //            }
        //        },
        //    }

        //};

        //public ObservableCollection<MockSportModel> Items => new ObservableCollection<MockSportModel> {
        //    new MockSportModel {
        //        Name = "FootBall", Price = 150
        //    } ,
        //    new MockSportModel {
        //        Name = "HandBall", Price = 150
        //    } ,
        //    new MockSportModel {
        //        Name = "Swimming", Price = 250
        //    } ,
        //    new MockSportModel {
        //        Name = "BasketBall", Price = 150
        //    }

        //};

        private void OnCurrentItemChanged(object sender, CurrentItemChangedEventArgs e) 
        { 
            MessagingCenter.Send<string>("App", "Close"); 
        }
    }

    //public class HistoryGroup
    //{
    //    public HistoryGroup(List<History> histories)
    //    {
    //        Histories = histories;
    //    }
    //    public List<History> Histories { get; set; }
    //}
}