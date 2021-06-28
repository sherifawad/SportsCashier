using dotMorten.Xamarin.Forms;
using SportsCashier.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSportHistoryPopup : Popup<MockSportModel>, INotifyPropertyChanged
    {
        public MockSportModel MockSportModel { get; private set; }
        public EditSportHistoryPopup()
        {
            InitializeComponent();
            MockSportModel = new MockSportModel { ReceiteDate = DateTime.Now};
            BindingContext = this;

        }
        public EditSportHistoryPopup(MockSportModel mockSportModel)
        {
            InitializeComponent();
            MockSportModel = mockSportModel;
            BindingContext = this;
            //if (mockSportModel == default)
            //    return;
            //var matchsport = SportsData.GetSpoertsData.FirstOrDefault(x => x.Code == mockSportModel.Code);
            //if (matchsport != null)
            //{
            //    dmSuggestBox.PlaceholderText = matchsport.NamePath;
            //}
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var step = 10;
            var newStep = Math.Round(e.NewValue / step);

            ((Slider)sender).Value = newStep * step;
        }

        protected override MockSportModel GetLightDismissResult()
        {
            return null;
        }

        private void OnSaveBtnClicked(object sender, EventArgs e)
        {
            foreach (var property in MockSportModel.GetType().GetProperties())
            {
                object propertyObject = property.GetValue(MockSportModel);
                if (propertyObject is string)
                {
                    if (string.IsNullOrEmpty((string)propertyObject))
                        return;
                }
                else if (propertyObject is int)
                {
                    if ((int)propertyObject <= 0)
                        return;
                }
                else if (propertyObject is double && property.Name != "Discount")
                {
                    if ((double)propertyObject <= 0)
                        return;
                }

            }
            Dismiss(MockSportModel);
        }

        #region Sports dmSuggestBox

        private void OnSuggestBoxSuggestionChosen(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxSuggestionChosenEventArgs e)
        {

            if (e.SelectedItem != null)
            {
                var value = e.SelectedItem as SportsData;
                if (value != null)
                {
                    MockSportModel.Name = value.Name;
                    MockSportModel.Code = value.Code;
                    MockSportModel.Icon = value.Icon;

                    (sender as AutoSuggestBox).PlaceholderText = value.NamePath;
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
        }
        #endregion
    }
}