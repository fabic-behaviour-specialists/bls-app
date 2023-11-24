using CoreGraphics;
using Fabic.Data.Enums;
using Fabic.Data.Extensions;
using Foundation;
using System;
using UIKit;

namespace Fabic.iOS.UIControls
{
    public class FabicButton : UIButton, IDisposable, ICanCleanUpMyself
    {
        FabicColour selectedFabicColour = FabicColour.Purple;

        public FabicButton(FabicColour colour = FabicColour.Purple) : base()
        {
            selectedFabicColour = colour;
            this.SetTitle("test2", UIControlState.Normal);

            this.SetTitleColor(UIColor.White, UIControlState.Normal);
            this.BackgroundColor = this.BackgroundColor.FabicColour(colour);
            this.Layer.BorderColor = this.BackgroundColor.FabicColour(colour, true).CGColor;
        }

        public override void DrawRect(CGRect area, UIViewPrintFormatter formatter)
        {
            base.DrawRect(area, formatter);

            // lay the button out as needed
        }

        /// <summary>
        /// Unhighlight this button.
        /// </summary>
        public void Unhighlight()
        {
            // set the background colour a back
            this.BackgroundColor = this.BackgroundColor.FabicColour(selectedFabicColour, false);
            this.Layer.ShadowOffset = new CGSize(2f, 2f);
            this.Layer.ShadowColor = UIColor.Black.CGColor;
            this.Layer.ShadowOpacity = 0.8f;
            this.Layer.ShadowRadius = 6;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            // set the background colour a little darker
            this.BackgroundColor = this.BackgroundColor.FabicColour(selectedFabicColour, true);
            this.Layer.ShadowOffset = new CGSize(2f, 2f);
            this.Layer.ShadowColor = UIColor.Black.CGColor;
            this.Layer.ShadowOpacity = 1f;
            this.Layer.ShadowRadius = 9;
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            // set the background colour a back
            this.BackgroundColor = this.BackgroundColor.FabicColour(selectedFabicColour, false);
            this.Layer.ShadowOffset = new CGSize(2f, 2f);
            this.Layer.ShadowColor = UIColor.Black.CGColor;
            this.Layer.ShadowOpacity = 0.8f;
            this.Layer.ShadowRadius = 6;
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            // set the background colour a back
            this.BackgroundColor = this.BackgroundColor.FabicColour(selectedFabicColour, false);
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