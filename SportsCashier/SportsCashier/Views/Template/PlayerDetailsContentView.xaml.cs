using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier.Views.Template
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerDetailsContentView : ContentView
    {
        const uint ExpandAnimationSpeed = 350;
        const uint CollapseAnimationSpeed = 250;
        public PlayerDetailsContentView()
        {
            InitializeComponent();
            // Close the swip on parent carousle view swipe
            MessagingCenter.Subscribe<string>("App", "Close", (x) => swipView.Close());
        }

        private void historyBtn_Clicked(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "History")
            {
                //CartPopupFade.IsVisible = true;
                bookmarkGrid.FadeTo(0, ExpandAnimationSpeed, Easing.SinInOut);
                ((Button)sender).Text = "X";
                historyGrid.TranslateTo(0, 0, ExpandAnimationSpeed, Easing.SinInOut);
                ((Button)sender).WidthRequest = 40;
                ((Button)sender).HeightRequest = 40;
                ((Button)sender).CornerRadius = 20;

            }
            else
            {
                ((Button)sender).Text = "History";
                ((Button)sender).HeightRequest = 40;
                ((Button)sender).WidthRequest = 90;
                ((Button)sender).CornerRadius = 0;
                historyGrid.TranslateTo(0, 400, CollapseAnimationSpeed, Easing.SinInOut);
                bookmarkGrid.FadeTo(1, CollapseAnimationSpeed, Easing.SinInOut);
                //CartPopupFade.IsVisible = false;
            }
        }

    }

}