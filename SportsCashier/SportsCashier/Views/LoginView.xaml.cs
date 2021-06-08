using Plugin.Fingerprint.Abstractions;
using SportsCashier.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        private bool _initialized;
        private readonly INavigationService _navigationService;

        public LoginView()
        {
            InitializeComponent();
            _navigationService = DependencyService.Get<INavigationService>();
        }

        protected override  void OnAppearing()
        {
            base.OnAppearing();

            if (!_initialized)
            {
                _initialized = true;
                //lblAuthenticationType.Text = "Auth Type: " + await Plugin.Fingerprint.CrossFingerprint.Current.GetAuthenticationTypeAsync();
            }
        }

        private async void OnAuthenticate(object sender, EventArgs e)
        {
            await AuthenticateAsync("To LogIn!");
        }

        private async void OnAuthenticateLocalized(object sender, EventArgs e)
        {
            await AuthenticateAsync("Beweise, dass du Finger hast!", "Abbrechen", "Anders!", "Viel zu schnell!");
        }

        private async Task AuthenticateAsync(string reason, string cancel = null, string fallback = null, string tooFast = null)
        {
            //_cancel = swAutoCancel.IsToggled ? new CancellationTokenSource(TimeSpan.FromSeconds(10)) : new CancellationTokenSource();
            lblStatus.Text = "";

            var dialogConfig = new AuthenticationRequestConfiguration("LogIn ", reason)
            { // all optional
                //CancelTitle = cancel,
                //FallbackTitle = fallback,
                //AllowAlternativeAuthentication = swAllowAlternative.IsToggled,
                //ConfirmationRequired = swConfirmationRequired.IsToggled
                
                AllowAlternativeAuthentication = true
            };

            // optional
            dialogConfig.HelpTexts.MovedTooFast = tooFast;

            //var result = await Plugin.Fingerprint.CrossFingerprint.Current.AuthenticateAsync(dialogConfig, _cancel.Token);
            var result = await Plugin.Fingerprint.CrossFingerprint.Current.AuthenticateAsync(dialogConfig);

            await SetResultAsync(result);
        }

        private async Task SetResultAsync(FingerprintAuthenticationResult result)
        {
            if (result.Authenticated)
            {
                _navigationService.GoToMainFlow();
                //await Shell.Current.Navigation.PushAsync(new AppShell());
            }
            else
            {
                lblStatus.Text = $"{result.Status}: {result.ErrorMessage}";
                await DisplayAlert($"{result.Status}", $"{result.ErrorMessage}", "Ok");
            }
        }
    }
}