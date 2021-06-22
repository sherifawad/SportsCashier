using CoreGraphics;
using Foundation;
using SportsCashier.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(CustomFrameRenderer))]
namespace SportsCashier.iOS
{
    class CustomFrameRenderer : FrameRenderer
    {

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            base.LayoutSubviews();
            this.Layer.ShadowRadius = 2.0f;
            this.Layer.ShadowColor = UIColor.Red.CGColor;
            this.Layer.ShadowOffset = new CGSize(2, 2);
            this.Layer.ShadowOpacity = 1.0f;
            this.Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            this.Layer.MasksToBounds = false;
        }
    }
}