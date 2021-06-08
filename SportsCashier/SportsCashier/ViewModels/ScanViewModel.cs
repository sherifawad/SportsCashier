using SportsCashier.Helpers;
using SportsCashier.ViewModels.PlayersPayment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

namespace SportsCashier.ViewModels
{
    public class ScanViewModel : BaseViewModel
    {
        #region Public Properties

        public MobileBarcodeScanningOptions ScannerOptions { get; set; }

        public bool ScanMember { get; set; }
        public bool IsScanning { get; set; }

        public bool IsAnalyzing { get; set; }

        public Result Result { get; set; }

        public ICommand ScanResultCommand { get; set; }

        #endregion

        #region Constructor

        public ScanViewModel()
        {
            IsScanning = true;
            IsAnalyzing = true;
            ScannerOptions = new MobileBarcodeScanningOptions
            {
                AutoRotate = true,
                PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE },
                DisableAutofocus = false,
                TryHarder = true
            };

            ScanResultCommand = new RelayCommand( async () =>   await ScanResultAsync());
        }

        #endregion

        #region Command Methods

        private  async Task ScanResultAsync()
        {
            IsBusy = false;

            await RunCommandAsync(() => IsBusy, async() => { 
                IsScanning = false;
                Device.BeginInvokeOnMainThread(
                    async () =>
                    {
                        IsAnalyzing = false;
                        await _navigationService.PushAsync<NewPaymentViewModel>($"member={Result.Text}");
                    });
                await Task.FromResult(true);
            });
        }

        #endregion

        #region Private Methods

        public override Task InitializeAsync()
        {
            IsScanning = true;
            IsAnalyzing = true;
            ScannerOptions = new MobileBarcodeScanningOptions
            {
                AutoRotate = true,
                PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE },
                DisableAutofocus = false,
                TryHarder = true
            };
            return base.InitializeAsync();
        }
        public override Task UninitializeAsync()
        {
            IsScanning = false;
            return base.UninitializeAsync();    
        }

        #endregion
    }

}
