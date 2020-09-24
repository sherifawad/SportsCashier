using SportsCashier.DataBase;
using SportsCashier.Extensions;
using SportsCashier.Helpers;
using SportsCashier.Models;
using SportsCashier.Services.DialogService;
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
    public class MembersListViewModel : BaseViewModel
    {
        #region Private Method

        private ObservableCollection<MemberModel> members;
        #endregion

        #region Public Property

        public ObservableCollection<MemberModel> Members 
        {
            get => members;
            set
            {
                members = value;
                if (members == null)
                    return;

                if (members.Count == 0)
                    CollectionViewHeight = 0;
                else
                    CollectionViewHeight = members.Count * 85;
            }
        }

        public int CollectionViewHeight { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand QRGenerationCommand { get; set; }

        #endregion

        #region Constructor

        //public MembersListViewModel()
        //{
        //    Members = new ObservableCollection<MemberModel>();
        //    AddCommand = new RelayCommand(async () => await AddAsync());
        //    DeleteCommand = new RelayCommand(async (parameter) => await DeleteAsync(parameter));
        //    EditCommand = new RelayCommand(async (parameter) => await EditAsync(parameter));
        //}

        public MembersListViewModel()
        {

            Members = new ObservableCollection<MemberModel>();
            AddCommand = new RelayCommand(async () => await AddAsync());
            DeleteCommand = new RelayCommand(async (parameter) => await DeleteAsync(parameter));
            EditCommand = new RelayCommand(async (parameter) => await EditAsync(parameter));
        }

        private async Task EditAsync(object parameter)
        {
            if (parameter != null && parameter is MemberModel member)
            {
                await _navigationService.PushAsync<NewPaymentViewModel>($"id={member.Id}");
            }
        }

        private async Task DeleteAsync(object parameter)
        {
            if (parameter != null && parameter is MemberModel member)
            {
                await _membersRepository.Delete(member);
            }
        }

        private async Task AddAsync()
        {

            await RunCommandAsync(() => IsBusy, async () =>
                await _navigationService.PushAsync<NewPaymentViewModel>()
            );

        }

        #endregion

        #region Private Method

        public override async Task InitializeAsync()
        {
            await RunCommandAsync(() => IsBusy, async () =>
            {
                Members = (await _membersRepository.GetItemsAsync()).ToObservableCollection() ?? new ObservableCollection<MemberModel>();

            });
        }

        #endregion
    }
}
