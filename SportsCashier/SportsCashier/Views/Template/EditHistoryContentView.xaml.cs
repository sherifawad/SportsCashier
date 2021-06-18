using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier.Views.Template
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditHistoryContentView : ContentView
    {
        public EditHistoryContentView()
        {
            InitializeComponent();
        }

        private void OnFrameTapped(object sender, EventArgs e)
        {
            var parent = ((View)sender).Parent;
            if(parent is SwipeView swipeView)
                swipeView.Open(OpenSwipeItem.RightItems);
        }

    }
}