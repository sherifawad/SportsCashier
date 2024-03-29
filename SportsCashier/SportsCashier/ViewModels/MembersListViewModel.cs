﻿using Newtonsoft.Json;
using QRCoder;
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
using System.Diagnostics;
using System.IO;
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

        public ImageSource QrCodeImage { get; set; }

        public bool popupVisibility { get; set; }
        public bool QrGeneraation { get; set; }

        public ObservableCollection<MemberModel> Members
        {
            get => members;
            set
            {
                members = value;
                if (members == null)
                    return;

                if (members.Count > 0)
                    CollectionViewHeight = members.Count * 85;
            }
        }

        public double CollectionViewHeight { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand QRGenerationCommand { get; set; }
        public ICommand DoneCommand => new RelayCommand(() => popupVisibility = false);

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
            QRGenerationCommand = new RelayCommand(async (parameter) => await QRGenerationAsync(parameter));
        }

        #endregion

        #region Commands Methods

        private async Task EditAsync(object parameter)
        {
            if (parameter != null && parameter is MemberModel member)
            {
                await _navigationService.PushAsync<NewPaymentViewModel>($"id={member.Id}");
            }
        }

        private async Task DeleteAsync(object parameter)
        {
            await RunCommandAsync(() => IsBusy, async () =>
            {

                if (parameter != null && parameter is MemberModel member)
                {
                    var shouldDelete = await _dialogService.DisplayAlert("Confirm",
                        "Are you sure you want to Delete? Can't undo.", "Ok", "Canel");
                    if (shouldDelete)
                    {
                        var memberInDataBase = await _membersRepository.GetWithChildren(member.Id);
                        foreach (var player in memberInDataBase.MembershipNPlayers)
                        {
                            await _playersRepository.Delete(player);
                            var psRow = await _ps.Get(c => c.PlayerModelId == player.Id, c => c.OrderBy(y => y.Id));
                            foreach (var row in psRow)
                            {
                                await _ps.Delete(row);
                            }
                        }
                        await _membersRepository.Delete(memberInDataBase);
                        Members.Remove(member);
                    }
                }
            });
        }

        private async Task AddAsync()
        {

            await RunCommandAsync(() => IsBusy, async () =>
                await _navigationService.PushAsync<NewPaymentViewModel>()
            );

        }

        private async Task QRGenerationAsync(object parameter)
        {

            await RunCommandAsync(() => QrGeneraation, async () =>
            {
                if (parameter != null && parameter is MemberModel member)
                {
                    try
                    {

                        var memberwithPlayers = await _membersRepository.GetWithChildren(member.Id);
                        var playerList = new List<PlayerModel>();
                        foreach (var player in memberwithPlayers.MembershipNPlayers)
                        {
                            var p = await _playersRepository.GetWithChildren(player.Id);
                            playerList.Add(p);
                        }
                        member.MembershipNPlayers = playerList;
                        var serializedMember = JsonConvert.SerializeObject(member);
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(serializedMember, QRCodeGenerator.ECCLevel.H);
                        PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
                        byte[] qrCodeBytes = qRCode.GetGraphic(20);
                        QrCodeImage = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
                        popupVisibility = true;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }

            });
        }

        #endregion

        #region Private Method

        public override async Task InitializeAsync()
        {
            Members = (await _membersRepository.GetItemsAsync()).ToObservableCollection() ?? new ObservableCollection<MemberModel>();

        }

        public override Task UninitializeAsync()
        {
            IsBusy = false;
            return base.UninitializeAsync();
        }

        #endregion
    }
}
