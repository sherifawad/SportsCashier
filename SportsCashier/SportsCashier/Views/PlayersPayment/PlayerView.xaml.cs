using SportsCashier.Models;
using SportsCashier.Services.MessagingService;
using SportsCashier.ViewModels.PlayersPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier.Views.PlayersPayment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerView : ContentView
    {

        public PlayerView()
        {
            InitializeComponent();
            //BindingContext = new PlayerViewModel(new PlayerModel());
            //var sportListVm = sportsList.BindingContext as SportsListViewModel;
            //sportListVm.PropertyChanged += SportListVm_PropertyChanged;

        }

        //private void SportListVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    var vm = this.BindingContext as PlayerViewModel;
        //    var sportListVm = sportsList.BindingContext as SportsListViewModel;
        //    sportListVm.PropertyChanged += SportListVm_PropertyChanged;
        //    if (vm?.Sports != null)
        //    {
        //        if (sportListVm?.Sports != null)
        //        {
        //            vm.Sports = sportListVm.Sports;
        //            vm.PlayerPayment = sportListVm.SportsTotalPayments;
        //        }

        //    }
        //}

    }
}