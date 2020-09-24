using Autofac;
using SportsCashier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : BasePage
    {
        public HomeView()
        {
            InitializeComponent();
            //BindingContext = App.Container.Resolve<HomeViewModel>();
        }

    }
}