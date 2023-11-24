using CoreGraphics;
using System;
using UIKit;

namespace Fabic.iOS
{
    public partial class FabicTextBox : UITextField, IDisposable, ICanCleanUpMyself
    {

        public FabicTextBox(IntPtr handle) : base(handle)
        {
            this.Layer.BorderWidth = 0;
            this.Layer.CornerRadius = 8;
            this.Layer.ShadowOffset = CGSize.Empty;
            this.Layer.ShadowColor = UIColor.White.CGColor;
            this.Layer.ShadowOpacity = 0.8f;
            this.Layer.ShadowRadius = 26;
            this.TextAlignment = UITextAlignment.Center;
        }

        public void CleanUp()
        {

        }
    }
}