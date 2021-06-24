using SportsCashier.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;

namespace SportsCashier.Services.NavigationService
{
    public interface INavigationService
    {
        Task PushAsync<TViewModel>(string parameters = null) where TViewModel : class;
        Task PushAsync(Type viewModelType, string parameters = null);
        Task PopAsync();
        Task InsertAsRoot<TViewModel>(string parameters = null) where TViewModel : class;
        Task InsertAsRoot(Type viewModelType, string parameters = null);
        Task GoBackAsync();
        void GoToMainFlow();
        void GoToLoginFlow();
        Task<T?> PopUp<T>(Popup<T?> popup);
    }
}
