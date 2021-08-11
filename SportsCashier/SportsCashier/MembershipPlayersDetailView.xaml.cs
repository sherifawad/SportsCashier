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
        private double currentHeight;
        const uint ExpandAnimationSpeed = 350;
        const uint CollapseAnimationSpeed = 250;

        public MembershipPlayersDetailView()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MembershipPlayersDetailViewModel();
            SizeChanged += MembershipPlayersDetailView_SizeChanged;

        }

        private void MembershipPlayersDetailView_SizeChanged(object sender, EventArgs e)
        {
            currentHeight = this.Height;
            payGrid.TranslationY = currentHeight - 40;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing(); 

            if (_viewModel != null)
                await _viewModel.InitializeAsync();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            if (_viewModel != null)
                await _viewModel.UninitializeAsync();
        }

        // Send message to close swipview
        private void OnCurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            MessagingCenter.Send<string>("App", "Close");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var frameLength = -(sportsFrame.Width - sportListToggleBtn.Width);

            if (sportsFrame.TranslationX == 0)
                sportsFrame.TranslateTo(frameLength, 0, ExpandAnimationSpeed, Easing.SinInOut);
            else
                sportsFrame.TranslateTo(0, 0, CollapseAnimationSpeed, Easing.SinInOut);
        }

        private async void OnCheckBoxChanged(object sender, CheckedChangedEventArgs e)
        {
            if (_viewModel == null)
                return;
            await _viewModel.CalculateAsync();
        }



        private void PayBtnClicked(object sender, EventArgs e)
        {
            var gridHeight = (payGrid.Height - ((Button)sender).Height);

            if (payGrid.TranslationY == 0)
            {
                payGrid.BackgroundColor = Color.Transparent;
                ((Button)sender).BackgroundColor = Color.Black;
                ((Button)sender).Text = "Pay";
                payGrid.TranslateTo(0, gridHeight, CollapseAnimationSpeed, Easing.SinInOut);
            }
            else
            {
                payGrid.BackgroundColor = Color.Beige;
                ((Button)sender).BackgroundColor = Color.Orange;
                ((Button)sender).Text = "Close";
                payGrid.TranslateTo(0, 0, ExpandAnimationSpeed, Easing.SinInOut);
            }
        }

    }
}