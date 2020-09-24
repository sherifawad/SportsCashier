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

            Routing.RegisterRoute("NewPaymentViewModel", typeof(NewPaymentView));
            Routing.RegisterRoute("MembersListViewModel", typeof(MembersListView));
            Routing.RegisterRoute("PlayerViewModel", typeof(PlayerView));
            Routing.RegisterRoute("SportsListViewModel", typeof(SportsListView));
            Routing.RegisterRoute("EmptyViewModel", typeof(EmptyView));
        }
    }
}