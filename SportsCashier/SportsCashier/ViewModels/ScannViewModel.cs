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
    public class ScannViewModel : BaseViewModel
    {
        #region Public Properties

        public MobileBarcodeScanningOptions ScannerOptions { get; set; }

        public bool IsScanning { get; set; }

        public bool IsAnalyzing { get; set; }

        public Result Result { get; set; }

        public ICommand ScanResultCommand { get; set; }

        #endregion

        #region Constructor

        public ScannViewModel()
        {
            ScannerOptions = new MobileBarcodeScanningOptions
            {
                AutoRotate = false,
                PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE },
                TryHarder = true
            };

            IsScanning = true;
            IsAnalyzing = true;

            ScanResultCommand = new RelayCommand(() =>  ScanResultAsync());
        }

        #endregion

        #region Command Methods

        private  void ScanResultAsync()
        {
            IsAnalyzing = false;
            IsScanning = false;
             Device.BeginInvokeOnMainThread(
                async () =>
                {
                    IsAnalyzing = false;

                    await _navigationService.PushAsync<NewPaymentViewModel> ($"member={Result.Text}");
                });
        }

        #endregion
    }
}
