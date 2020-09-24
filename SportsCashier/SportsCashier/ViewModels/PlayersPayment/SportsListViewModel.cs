using SportsCashier.Extensions;
using SportsCashier.Models;
using SportsCashier.Services.MessagingService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SportsCashier.ViewModels.PlayersPayment
{
    public class SportsListViewModel : BaseViewModel
    {
        #region Private Properties

        #endregion

        #region Public Property

        public ObservableCollection<Sport> Sports { get; set; }


        public double SportsTotalPayments { get; set; }

        public SportPickerViewModel PickerViewModel { get; set; }

        public ICommand AddSportCommand { get; set; }
        public ICommand RemoveSportCommand { get; set; }



        #endregion


        #region Constructor

        public SportsListViewModel(ObservableCollection<Sport> sports = null, double playerPayment = 0)
        {
            Sports = new ObservableCollection<Sport>();
            PickerViewModel = new SportPickerViewModel();


            AddSportCommand = new Command((parameter) => AddSport(parameter));
            RemoveSportCommand = new Command((parameter) => RemoveSport(parameter));

            if (sports != null)
            {
                Sports = sports;
                SportsTotalPayments = playerPayment;
            }


        }

        #endregion

        #region Private Method



        private void CalculateTotalPayment()
        {
            var t = (double)default;

            if (Sports != null && Sports.Count() > 0)
            {
                var priceList = Sports.Where(s => s?.SportCaegory?.SportPrice != null).OrderByDescending(p => p.SportCaegory.SportPrice).Select(s => s.SportCaegory.SportPrice).ToList();
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

            }

            SportsTotalPayments = t;

        }

        private void RemoveSport(object parameter)
        {
            if (parameter != null && parameter is Sport sport)
            {

                Sports.Remove(sport);

                CalculateTotalPayment();

            }

        }

        private void AddSport(object parameter)
        {
            if (Sports == null)
                return;

            if (parameter != null && parameter is Sport sport)
            {
                if (string.IsNullOrEmpty(sport?.SportCaegory?.SportType))
                    return;

                if (Sports.FirstOrDefault(s => s.SportName == sport.SportName) == null)
                {
                    try
                    {
                        Sports.Add(sport);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);

                    }

                    CalculateTotalPayment();
                }

            }
        }


        #endregion
    }
}
