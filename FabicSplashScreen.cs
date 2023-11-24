using CoreGraphics;
using System;
using UIKit;

namespace Fabic.iOS
{
    public partial class FabicSplashScreen : UIView
    {
        public FabicSplashScreen(IntPtr handle) : base(handle)
        {
            if ((int)UIScreen.MainScreen.NativeBounds.Size.Height >= 2436) // iPhone X
                heightConstraint.Constant = 40;
        }

        public override void DrawRect(CGRect area, UIViewPrintFormatter formatter)
        {
            base.DrawRect(area, formatter);
        }
    }
}