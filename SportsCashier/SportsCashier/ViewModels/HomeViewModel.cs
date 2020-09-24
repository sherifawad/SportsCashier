using SportsCashier.Helpers;
using SportsCashier.Models;
using SportsCashier.Services.NavigationService;
using SportsCashier.ViewModels.PlayersPayment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SportsCashier.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public ObservableCollection<HomeItemsModel> HomeItemsCollection { get; set; }

        public ICommand TappingCommand { get; set; }

        public HomeViewModel()
        {

            //TappingCommand = new Command(async (Parmaeter) =>  await Shell.Current.GoToAsync((string)Parmaeter));
            TappingCommand = new RelayCommand(async (Parmaeter) => await NavigateAsync(Parmaeter));

            HomeItemsCollection = new ObservableCollection<HomeItemsModel>
            {
                new HomeItemsModel
                {
                    IconText = "\uf0e2",
                    Name = "New Payment",
                    ViewModel = typeof(NewPaymentViewModel)
                },

                new HomeItemsModel
                {
                    IconText = "\uf0e2",
                    Name = "Members List",
                    ViewModel = typeof(MembersListViewModel)
                },

                new HomeItemsModel
                {
                    IconText = "\uf0e2",
                    Name = "Scann",
                    ViewModel = typeof(ScannViewModel)
                },

            };
        }

        private async Task NavigateAsync(object parmaeter)
        {
            if (parmaeter != null && parmaeter is Type viewModel){

                await RunCommandAsync(() => IsBusy, async () =>  
                    await _navigationService.PushAsync(viewModel)
                );
            }
        }
    }
}
