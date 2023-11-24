using CoreGraphics;
using Fabic.Data.Enums;
using Fabic.Data.Extensions;
using Foundation;
using System;
using UIKit;

namespace Fabic.iOS
{
    public partial class FabicButton : UIButton, IDisposable, ICanCleanUpMyself
    {
        bool withShadow = true;
        FabicColour SelectedFabicColour = FabicColour.Purple;

        public FabicColour FabicColour
        {
            get { return SelectedFabicColour; }
            set
            {
                SelectedFabicColour = value;
                this.BackgroundColor = this.BackgroundColor.FabicColour(SelectedFabicColour);
                this.Layer.BorderColor = this.BackgroundColor.FabicColour(SelectedFabicColour, true).CGColor;
            }
        }

        public bool WithShadow
        {
            get { return withShadow; }
            set
            {
                withShadow = value;
                if (withShadow)
                {
                    this.Layer.ShadowOffset = new CGSize(2f, 2f);
                    this.Layer.ShadowColor = UIColor.Black.CGColor;
                    this.Layer.ShadowOpacity = 0.8f;
                    this.Layer.ShadowRadius = 6;
                }
                else
                {

                    this.Layer.ShadowOffset = new CGSize(0f, 0f);
                    this.Layer.ShadowColor = UIColor.Black.CGColor;
                    this.Layer.ShadowOpacity = 0f;
                    this.Layer.ShadowRadius = 0;
                }
            }
        }

        public FabicButton() : base()
        {
            this.SetTitleColor(UIColor.White, UIControlState.Normal);
            this.BackgroundColor = this.BackgroundColor.FabicColour(SelectedFabicColour);
            this.Layer.BorderColor = this.BackgroundColor.FabicColour(SelectedFabicColour, true).CGColor;
            this.Layer.BorderWidth = 2;
            this.Layer.CornerRadius = 12;
            if (withShadow)
            {
                this.Layer.ShadowOffset = new CGSize(2f, 2f);
                this.Layer.ShadowColor = UIColor.Black.CGColor;
                this.Layer.ShadowOpacity = 0.8f;
                this.Layer.ShadowRadius = 6;
            }
            this.Frame = new CoreGraphics.CGRect(0, 0, 170, 50);
        }

        public FabicButton(IntPtr handle) : base(handle)
        {
            this.SetTitleColor(UIColor.White, UIControlState.Normal);
            this.BackgroundColor = this.BackgroundColor.FabicColour(SelectedFabicColour);
            this.Layer.BorderColor = this.BackgroundColor.FabicColour(SelectedFabicColour, true).CGColor;
            this.Layer.BorderWidth = 2;
            this.Layer.CornerRadius = 12;
            if (withShadow)
            {
                this.Layer.ShadowOffset = new CGSize(2f, 2f);
                this.Layer.ShadowColor = UIColor.Black.CGColor;
                this.Layer.ShadowOpacity = 0.8f;
                this.Layer.ShadowRadius = 6;
            }
        }

        /// <summary>
        /// Unhighlight this button.
        /// </summary>
        public void Unhighlight()
        {
            // set the background colour a back
            this.BackgroundColor = this.BackgroundColor.FabicColour(SelectedFabicColour, false);
            if (withShadow)
            {
                this.Layer.ShadowOffset = new CGSize(2f, 2f);
                this.Layer.ShadowColor = UIColor.Black.CGColor;
                this.Layer.ShadowOpacity = 0.8f;
                this.Layer.ShadowRadius = 6;
            }
            else
            {
                this.Layer.ShadowOffset = new CGSize(0f, 0f);
                this.Layer.ShadowColor = UIColor.Black.CGColor;
                this.Layer.ShadowOpacity = 0f;
                this.Layer.ShadowRadius = 0;
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            // set the background colour a little darker
            this.BackgroundColor = this.BackgroundColor.FabicColour(SelectedFabicColour, true);
            this.Layer.ShadowOffset = new CGSize(2f, 2f);
            this.Layer.ShadowColor = UIColor.Black.CGColor;
            this.Layer.ShadowOpacity = 1f;
            this.Layer.ShadowRadius = 9;
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            // set the background colour a back
            this.BackgroundColor = this.BackgroundColor.FabicColour(SelectedFabicColour, false);
            if (withShadow)
            {
                this.Layer.ShadowOffset = new CGSize(2f, 2f);
                this.Layer.ShadowColor = UIColor.Black.CGColor;
                this.Layer.ShadowOpacity = 0.8f;
                this.Layer.ShadowRadius = 6;
            }
            else
            {
                this.Layer.ShadowOffset = new CGSize(0f, 0f);
                this.Layer.ShadowColor = UIColor.Black.CGColor;
                this.Layer.ShadowOpacity = 0f;
                this.Layer.ShadowRadius = 0;
            }
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            // set the background colour a back
            this.BackgroundColor = this.BackgroundColor.FabicColour(SelectedFabicColour, false);
            if (withShadow)
            {
                this.Layer.ShadowOffset = new CGSize(2f, 2f);
                this.Layer.ShadowColor = UIColor.Black.CGColor;
                this.Layer.ShadowOpacity = 0.8f;
                this.Layer.ShadowRadius = 6;
            }
            else
            {

                this.Layer.ShadowOffset = new CGSize(0f, 0f);
                this.Layer.ShadowColor = UIColor.Black.CGColor;
                this.Layer.ShadowOpacity = 0f;
                this.Layer.ShadowRadius = 0;
            }
        }

        public void CleanUp()
        {

        }
    }
}