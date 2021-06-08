using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SportsCashier.ViewModels.PlayersPayment
{
    public class PlayerListItemViewModel : BaseViewModel
    {
        #region Private Properties

        private bool playerChecked;
        #endregion

        #region Public properties

        public bool PlayerChecked
        {
            get => playerChecked;
            set
            {
                playerChecked = value;
                if (value)
                {
                    if (Sports != null && Sports.Count > 0)
                    {
                        CalculateTotalPayment();
                    }

                }
            }
        }

        public ObservableCollection<Sport> Sports { get; set; }
        public string PlayerName { get; set; }
        public double PlayerPayment { get; set; }

        #endregion

        #region Constructor

        public PlayerListItemViewModel()
        {
            Sports = new ObservableCollection<Sport>();
            _messagingService.Subscribe(AppConstants.SportChecked, x =>
            {
                CalculateTotalPayment();
            });
        }

        #endregion

        #region Private Methods

        private void CalculateTotalPayment()
        {
            var t = (double)default;

            if (Sports != null && Sports.Count > 0)
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

            PlayerPayment = t;

        }

        #endregion
    }
}
