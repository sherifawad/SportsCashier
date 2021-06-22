using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SportsCashier.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Frame), typeof(CustomFrameRenderer))]
namespace SportsCashier.Droid
{

    public class CustomFrameRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        public CustomFrameRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement.HasShadow)
            {
                //SetOutlineSpotShadowColor(Android.Graphics.Color.Red);
                Elevation = 90.0f;
                TranslationZ = 0.0f;
                SetZ(90f);

            }
        }
    }
}