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

namespace SportsCashier.ViewModels.PlayersPayment
{
    public class SportsListViewModel : BaseViewModel
    {

        #region Private Property
        private SportModel selectedSport;
        #endregion

        #region Public Property

        public ObservableCollection<Sport> Sports { get; set; }

        public ICollection<Sport> SportsList;

        public ICollection<SportCaegory> SportCaegoriesList { get; set; }

        public SportCaegory SelectedCaegory { get; set; }

        public SportModel SelectedSport
        {
            get => selectedSport;
            set
            {
                selectedSport = value;
                SportCaegoriesList = SportsList.Where(s => s.SportName == value.SportName).Select(x => x.SportCaegory).ToList();
            }
        }
        #endregion


        #region Public Commands

        public ICommand AddSportCommand { get; set; }
        public ICommand RemoveSportCommand { get; set; }

        #endregion




        #region Constructor

        public SportsListViewModel()
        {
            Sports = new ObservableCollection<Sport>();
            SportsList = new List<Sport>();
            AddSportCommand = new Command((parameter) => AddSport(parameter));
            RemoveSportCommand = new Command((parameter) => RemoveSport(parameter));
        }

        #endregion

        #region Commands Methods

        private void RemoveSport(object parameter)
        {
            if (parameter != null && parameter is Sport sport)
            {

                Sports.Remove(sport);

            }

        }

        private void AddSport(object parameter)
        {
            if (Sports == null)
                Sports = new ObservableCollection<Sport>();

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

                }

            }
        }

        #endregion

        #region Private Method

        public override async Task InitializeAsync()
        {
            await _sportsRepository.GetItemsAsync();
        }

        #endregion
    }
}
