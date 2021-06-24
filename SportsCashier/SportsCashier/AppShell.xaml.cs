using SportsCashier.ViewModels;
using SportsCashier.ViewModels.PlayersPayment;
using SportsCashier.Views;
using SportsCashier.Views.PlayersPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(NewPaymentViewModel), typeof(NewPaymentView));
            Routing.RegisterRoute(nameof(PlayerViewModel), typeof(PlayerView));
            Routing.RegisterRoute(nameof(EditPlayerDetailsViewModel), typeof(EditPlayerDetailsView));
            //Routing.RegisterRoute(nameof(MembershipPlayersDetailViewModel), typeof(MembershipPlayersDetailView));
        }
    }
}