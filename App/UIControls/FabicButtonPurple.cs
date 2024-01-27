using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace Fabic.iOS.UIControls
{
    public class FabicButtonPurple : UIButton, IDisposable, ICanCleanUpMyself
    {
        public FabicButtonPurple() : base()
        {
            this.SetTitle("test", UIControlState.Normal);
            this.Frame = new CoreGraphics.CGRect(0, 0, 170, 50);
            this.SetTitleColor(UIColor.White, UIControlState.Normal);
            this.BackgroundColor = new UIColor((nfloat)0.39, (nfloat)0.1765, (nfloat)0.5333, 1);
            this.Layer.BorderColor = new CGColor((nfloat)0.25, (nfloat)0.2, (nfloat)0.5333, 1);
            this.Layer.BorderWidth = 2;
            this.Layer.CornerRadius = 12;
            this.Layer.ShadowOffset = new CGSize(2f, 2f);
            this.Layer.ShadowColor = UIColor.Black.CGColor;
            this.Layer.ShadowOpacity = 0.8f;
            this.Layer.ShadowRadius = 6;
        }

        public override void DrawRect(CGRect area, UIViewPrintFormatter formatter)
        {
            base.DrawRect(area, formatter);

            // lay the button out as needed
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            // set the background colour a little darker
            this.BackgroundColor = new UIColor((nfloat)(0.2529), (nfloat)(0.1568), (nfloat)(0.5333), 1);
            this.Layer.ShadowOffset = new CGSize(2f, 2f);
            this.Layer.ShadowColor = UIColor.Black.CGColor;
            this.Layer.ShadowOpacity = 1f;
            this.Layer.ShadowRadius = 9;
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            // set the background colour a back
            this.BackgroundColor = new UIColor((nfloat)(0.39215), (nfloat)(0.1568), (nfloat)(0.5333), 1);
            this.Layer.ShadowOffset = new CGSize(2f, 2f);
            this.Layer.ShadowColor = UIColor.Black.CGColor;
            this.Layer.ShadowOpacity = 0.8f;
            this.Layer.ShadowRadius = 6;
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            // set the background colour a back
            this.BackgroundColor = new UIColor((nfloat)(0.39215), (nfloat)(0.1568), (nfloat)(0.5333), 1);
            this.Layer.ShadowOffset = new CGSize(2f, 2f);
            this.Layer.ShadowColor = UIColor.Black.CGColor;
            this.Layer.ShadowOpacity = 0.8f;
            this.Layer.ShadowRadius = 6;
        }

        public void CleanUp()
        {

        }
    }
}