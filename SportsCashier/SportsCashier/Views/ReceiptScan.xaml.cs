using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceiptScan : ContentPage
    {
        //private readonly ITesseractApi _tesseract;
        public ReceiptScan()
        {
            InitializeComponent();
            //_tesseract = DependencyService.Resolve<ITesseractApi>();
        }

        private async void LoadImageButton_OnClicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync();
            await Recognise((await result.OpenReadAsync()));
        }

        private async void GetPhotoButton_OnClicked(object sender, EventArgs e)
        {
            if (!MediaPicker.IsCaptureSupported)
                return;
            var result = await MediaPicker.CapturePhotoAsync();
            using Stream stream = await result.OpenReadAsync();
            await Recognise(stream);
        }

        async Task Recognise(Stream result)
        {
            if (result == null)
                return;
            try
            {
                activityIndicator.IsRunning = true;
                //if (!_tesseract.Initialized)
                //{
                //    var initialised = await _tesseract.Init("eng", OcrEngineMode.TesseractOnly);
                //    if (!initialised)
                //        return;
                //}
                ////_tesseract.SetVariable("tessedit_char_whitelist", "0123456789-.");
                ////_tesseract.SetPageSegmentationMode(PageSegmentationMode.SingleBlockVertText);
                //_tesseract.SetWhitelist("0123456789");
                ////_tesseract.SetBlacklist("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789:<>.,$-/#&=()\"':?");
                ////_tesseract.SetVariable("tessedit_char_whitelist", "1234567890");
                //if (!await _tesseract.SetImage(result))
                //    return;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Cancel");
            }
            finally
            {
                activityIndicator.IsRunning = false;
            }
            //TextLabel.Text = _tesseract.Text;
            //var words = _tesseract.Results(PageIteratorLevel.Word);
            //var symbols = _tesseract.Results(PageIteratorLevel.Symbol);
            //var blocks = _tesseract.Results(PageIteratorLevel.Block);
            //var paragraphs = _tesseract.Results(PageIteratorLevel.Paragraph);
            //var lines = _tesseract.Results(PageIteratorLevel.Textline);
        }

        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            string PhotoPath;
            // canceled
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
        }
    }
}