using Newtonsoft.Json;
using QRCoder;
using SportsCashier.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrPopup : Popup
    {
        private readonly Payoutput _payoutput;

        //public QrPopup()
        //{
        //    InitializeComponent();
        //}
        public QrPopup(Payoutput payoutput)
        {
            InitializeComponent();

            var serializedMember = JsonConvert.SerializeObject(payoutput);
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(serializedMember, QRCodeGenerator.ECCLevel.H);
            PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(20);
            qrImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
        }
    }
}