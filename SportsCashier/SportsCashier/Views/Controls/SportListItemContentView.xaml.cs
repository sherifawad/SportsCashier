using SportsCashier.Services.MessagingService;
using SportsCashier.ViewModels.PlayersPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SportListItemContentView : ContentView
    {
        private readonly IMessagingService _messagingService;

        public SportListItemContentView()
        {
            InitializeComponent();
            _messagingService = DependencyService.Get<IMessagingService>();

        }

        private void checkSport_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _messagingService.SendMessage(AppConstants.SportChecked);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            checkSport.IsChecked =! checkSport.IsChecked;
        }
    }
}