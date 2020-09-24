using Newtonsoft.Json;
using SportsCashier.Extensions;
using SportsCashier.Models;
using SportsCashier.Services.MessagingService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SportsCashier.ViewModels.PlayersPayment
{
    public class PlayerViewModel : BaseViewModel
    {
        #region Private Methods

        private string playerName;

        private int playerID;
        #endregion

        #region public Property

        public bool IsEnabled { get; set; }

        public SportsListViewModel ListViewModel { get; set; }

        public PlayerModel Player { get; set; }


        public string PlayerName
        {
            get => playerName;
            set
            {

                if (playerName != value)
                {
                    playerName = value;
                    CheckVisability();
                }
            }
        }

        public double PlayerPayment { get; set; }

        public ObservableCollection<Sport> Sports { get; set; }

        #endregion

        #region Constructor


        public PlayerViewModel(PlayerModel player = null)
        {
            Sports = new ObservableCollection<Sport>();
            Player = new PlayerModel();

            if (player != null)
            {
                Player = player;
                playerID = player.Id;
                Sports = player.Sports.ToObservableCollection();
                PlayerPayment = player.PlayerPayment;
                playerName = player.PlayerName;
                ListViewModel = new SportsListViewModel(Sports, PlayerPayment);
            }
            else
                ListViewModel = new SportsListViewModel(null, 0);

            ListViewModel.PropertyChanged += ListViewModel_PropertyChanged;

        }

        #endregion

        #region Private Method

        private void ListViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var vm = sender as SportsListViewModel;
            Sports = vm.Sports;
            PlayerPayment = vm.SportsTotalPayments;
            CheckVisability();
        }

        private void CheckVisability()
        {

            if (!string.IsNullOrEmpty(PlayerName) && PlayerPayment != default && Sports != null && Sports.Count > 0)
            {
                Player = new PlayerModel
                {
                    Id = playerID,
                    PlayerName = PlayerName,
                    Sports = Sports.ToList(),
                    PlayerPayment = PlayerPayment
                };
                IsEnabled = true;
            }
            else
                IsEnabled = false;
        }

        #endregion


    }
}
