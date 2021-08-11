using dotMorten.Xamarin.Forms;
using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier.Views.Template
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSportContentView : ContentView
    {
        public EditSportContentView()
        {
            InitializeComponent();
        }


        #region Sports dmSuggestBox

        private void OnSuggestBoxSuggestionChosen(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxSuggestionChosenEventArgs e)
        {

            if (e.SelectedItem != null)
            {
                var value = e.SelectedItem as SportsData;
                if (value != null)
                {
                    MessagingCenter.Send(AppConstants.App, AppConstants.SelectedSport, value);

                }
            }

            //Move focus to the next control or stop focus
            if (sender is AutoSuggestBox)
            {
                (sender as AutoSuggestBox).Unfocus();
                //(sender as AutoSuggestBox).Unfocused += StorageSuggestBox_Unfocused;
            }
        }

        private void OnSuggestBoxTextChanged(object sender, AutoSuggestBoxTextChangedEventArgs e)
        {
            if (e.CheckCurrent() && (sender as AutoSuggestBox).Text.Length >= 3)
            {
                var term = (sender as AutoSuggestBox).Text.ToLower();
                var results = SportsData.GetSpoertsData.Where(i => i.Name.ToLower().Contains(term)).ToList();
                (sender as AutoSuggestBox).ItemsSource = results;
            }

        }
        private void OnSuggestBoxFocused(object sender, FocusEventArgs e)
        {
            (sender as AutoSuggestBox).ItemsSource?.Clear();
            (sender as AutoSuggestBox).Text = "";
            MessagingCenter.Send<string>(AppConstants.App, AppConstants.ScrollSportsToBottom);

        }
        #endregion
    }
}