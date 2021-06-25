using dotMorten.Xamarin.Forms;
using SportsCashier.Extensions;
using SportsCashier.Models;
using SportsCashier.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace SportsCashier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPlayerDetailsView : ContentPage, INotifyPropertyChanged
    {
        private readonly EditPlayerDetailsViewModel _viewModel;

        public EditPlayerDetailsView()
        {
            InitializeComponent();
            BindingContext = _viewModel = new EditPlayerDetailsViewModel();

            MessagingCenter.Subscribe<string>(AppConstants.App, AppConstants.ScrollSportsToBottom, (s) =>
            {
                // scroll to bottom
                Device.BeginInvokeOnMainThread(async () =>
                {
                    // Update your children here, add more or remove.
                    // todo

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        await Task.Delay(10); //UI will be updated by Xamarin
                        await sportsScrollView.ScrollToAsync(sportsGridView, ScrollToPosition.End, true);
                    }
                    else
                    {
                        await sportsScrollView.ScrollToAsync(0, sportsGridView.Height, true);

                    }

                });
            });


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

        void OnFabTabTapped(object? sender, TabTappedEventArgs e) => DisplayAlert("FabTabGallery", "Tab Tapped.", "Ok");


    }
}