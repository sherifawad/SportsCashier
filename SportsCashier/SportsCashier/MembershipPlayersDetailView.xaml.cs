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

        // Send message to close swipview
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