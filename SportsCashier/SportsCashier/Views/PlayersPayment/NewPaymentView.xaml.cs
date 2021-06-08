using SportsCashier.Services.MessagingService;
using SportsCashier.ViewModels;
using SportsCashier.ViewModels.PlayersPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier.Views.PlayersPayment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPaymentView : BasePage
    {
        private readonly IMessagingService _messagingService;

        public NewPaymentView()
        {
            InitializeComponent();
            _messagingService = DependencyService.Get<IMessagingService>();
            sharebutton.IsVisible = false;

            //BindingContext = App.Container.Resolve<NewPaymentViewModel>();

            //BindingContext = new NewPaymentViewModel();
            //var sportListVm = sportsList.BindingContext as SportsListViewModel;
            //sportListVm.PropertyChanged += SportListVm_PropertyChanged;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            totalLabel.IsVisible = false;
            sharebutton.IsVisible = false;

            _messagingService.Subscribe(AppConstants.TotalPayment, x =>
            {
                var viewModel = BindingContext as NewPaymentViewModel;
                if (viewModel == null)
                    return;

                var t = (double)default;
                viewModel.Players.ToList().ForEach(x => t += x.PlayerPayment);
                totalLabel.Text = $"Total {t} ";
                if (t > 0)
                {
                    sharebutton.IsVisible = true;
                    totalLabel.IsVisible = true;
                }
                else
                {
                    sharebutton.IsVisible = false;
                    totalLabel.IsVisible = false;
                }
            });
        }

        //private void SportListVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    var vm = this.BindingContext as NewPaymentViewModel;
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