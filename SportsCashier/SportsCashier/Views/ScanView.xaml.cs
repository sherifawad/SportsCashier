using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace SportsCashier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanView : BasePage
    {
        public ScanView()
        {
            InitializeComponent();
        }
    }
}