using SportsCashier.Models;
using SportsCashier.Services.MessagingService;
using SportsCashier.Services.NavigationService;
using SportsCashier.ViewModels.PlayersPayment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerListItemContentView : ContentView
    {
        private readonly PlayerListItemViewModel _viewModel;
        private readonly IMessagingService _messagingService;
        //private readonly INavigationService _navigationService;
        public PlayerListItemContentView()
        {
            InitializeComponent();
            //BindingContext = _viewModel = new PlayerListItemViewModel();
            _messagingService = DependencyService.Get<IMessagingService>();
            //_navigationService = DependencyService.Get<INavigationService>();
            _messagingService.Subscribe(AppConstants.SportChecked, x =>
            {
                CalculateTotalPayment();
            });
        }
        //private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        //{
        //    if (_viewModel == null)
        //        return;

        //    _viewModel.PlayerChecked = true;
        //    var isChecked = ((CheckBox)sender).IsChecked;
        //    if (isChecked)
        //    {
        //        var Sports = BindableLayout.GetItemsSource(fixedSportscollection) as List<Sport>;
        //        if (Sports != null && Sports.Count > 0)
        //        {
        //            Sports.ForEach(x => x.Checked = true);
        //            BindableLayout.SetItemsSource(fixedSportscollection, Sports);
        //            CalculateTotalPayment();
        //        }

        //    }
        //    else
        //        payment.Text = "0";

        //}

        private void CalculateTotalPayment()
        {
            var t = (double)default;
            var Sports = BindableLayout.GetItemsSource(fixedSportscollection) as List<Sport>;

            if (Sports != null && Sports.Count > 0)
            {
                var priceList = Sports.Where(s => s?.SportCaegory?.SportPrice != null && s.Checked == true).OrderByDescending(p => p.SportCaegory.SportPrice).Select(s => s.SportCaegory.SportPrice).ToList();
                var listCount = priceList.Count();
                if (listCount >= 3)
                {
                    t = (priceList[0] * 0.8) + (priceList[1] * 0.9);
                    for (int i = 2; i < priceList.Count(); i++)
                    {
                        t += priceList[i];
                    }
                }

                else if (listCount == 2)
                {
                    t = (priceList[0] * 0.9) + priceList[1];
                }
                else
                {
                    t = priceList.Sum();
                }
                payment.Text = t.ToString();

            }
            else
                payment.Text = t.ToString();

            _messagingService.SendMessage(AppConstants.TotalPayment);

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}