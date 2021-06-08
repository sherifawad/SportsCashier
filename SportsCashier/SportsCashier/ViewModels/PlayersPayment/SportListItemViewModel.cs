using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCashier.ViewModels.PlayersPayment
{
    public class SportListItemViewModel : BaseViewModel
    {
        private bool sportChecked;
        public SportCaegory SportCaegory { get; set; }

        public string SportName { get; set; }
        public bool Checked
        {
            get => sportChecked;
            set
            {
                sportChecked = value;
                _messagingService.SendMessage(AppConstants.SportChecked);

            }
        }
    }
}
