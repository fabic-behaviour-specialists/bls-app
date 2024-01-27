using CoreGraphics;
using Fabic.Data.Enums;
using Fabic.Data.Extensions;
using System;
using UIKit;

namespace Fabic.iOS
{
    public class FabicTextEditOverlay : UIView, IDisposable, ICanCleanUpMyself
    {
        FabicTextView textView;
        FabicButton confirmButton;
        UIButton cancelButton;
        UILabel titleLabel;
        Object editingItem;
        UIImageView butterfly;

        public string Text
        {
            get { return textView.Text; }
        }

        public object EditingItem
        {
            get { return editingItem; }
        }

        public FabicColour FabicColour
        {
            get; set;
        }

        public event EventHandler FinishedEditingItem;

        public FabicTextEditOverlay(string text = "", object item = null)
        {
            editingItem = item;

            this.Frame = ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[0].View.Frame;
            this.BackgroundColor = UIColor.Clear.ColorWithAlpha(0f);

            UIBlurEffect effect = UIBlurEffect.FromStyle(UIBlurEffectStyle.Regular);
            UIVisualEffectView effectView = new UIVisualEffectView(effect);
            effectView.Frame = this.Frame;
            this.AddSubview(effectView);

            UIImageView backgroundButterflies = new UIImageView(UIImage.FromFile("Waves.png"));
            backgroundButterflies.Frame = this.Frame;
            backgroundButterflies.ContentMode = UIViewContentMode.ScaleAspectFill;
            backgroundButterflies.Alpha = 1f;
            AddSubview(backgroundButterflies);

            titleLabel = new UILabel();
            titleLabel.Frame = new CoreGraphics.CGRect(20, 30, this.Frame.Width - 40, 20);
            titleLabel.Font = UIFont.FromName("AvenirNext-Medium", 20);
            titleLabel.TextAlignment = UITextAlignment.Center;
            titleLabel.TextColor = UIColor.Black.FabicColour(Data.Enums.FabicColour.Purple);
            titleLabel.Text = (text.Length == 0) ? "Add Item" : "Edit Item";
            this.AddSubview(titleLabel);

            textView = new FabicTextView();
            textView.Text = text;
            textView.BarColour = FabicColour;
            textView.Frame = new CoreGraphics.CGRect(20, 60, this.Frame.Width - 40, this.Frame.Height - 135);
            textView.Layer.CornerRadius = 14;
            textView.Font = UIFont.FromName("AvenirNext-Regular", 17);
            textView.BackgroundColor = UIColor.Clear;
            textView.DoneClicked += TextView_DoneClicked;
            this.Alpha = 0;
            this.AddSubview(textView);

            // create the new confirm button
            confirmButton = new FabicButton();
            confirmButton.Frame = new CGRect(20, this.Frame.Height - 120, this.Frame.Width - 40, 40);
            confirmButton.SetTitle("Save", UIControlState.Normal);
            confirmButton.TouchDown += ConfirmButton_TouchDown; ;
            this.AddSubview(confirmButton);

            // Add the additional butterfly
            butterfly = new UIImageView(new UIImage("butterfly.png"));
            butterfly.ContentMode = UIViewContentMode.ScaleAspectFill;
            butterfly.Frame = new CGRect(Frame.Width - 44, Frame.Height - 140, 35, 35);
            butterfly.Transform = CGAffineTransform.MakeRotation((nfloat)Math.PI / 12);
            butterfly.Layer.ShadowOffset = new CGSize(6f, 6f);
            butterfly.Layer.ShadowColor = UIColor.White.CGColor;
            AddSubview(butterfly);

            // need cancel button
            cancelButton = new UIButton(UIButtonType.Custom);
            cancelButton.SetTitle("Cancel", UIControlState.Normal);
            cancelButton.Font = UIFont.FromName("AvenirNext-Bold", 17); ;
            cancelButton.SetTitleColor(UIColor.Blue.FabicColour(Data.Enums.FabicColour.Purple), UIControlState.Normal);
            cancelButton.Frame = new CGRect(20, this.Frame.Height - 60, this.Frame.Width - 40, 30);
            cancelButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            cancelButton.TouchDown += CancelButton_TouchDown;
            this.AddSubview(cancelButton);
        }

        private void TextView_DoneClicked(object sender, EventArgs e)
        {
            ConfirmButton_TouchDown(sender, e);
        }

        public override void DrawRect(CoreGraphics.CGRect area, UIViewPrintFormatter formatter)
        {
            base.DrawRect(area, formatter);
        }

        void ConfirmButton_TouchDown(object sender, EventArgs e)
        {
            UIView.Animate(
                1, // duration
                () => { Alpha = 0; },
            () => { this.RemoveFromSuperview(); if (FinishedEditingItem != null) { FinishedEditingItem(this, new EventArgs()); } }
            );
        }

        /// <summary>
        /// Show the overlay with a fade animation, adding it to specified View.
        /// </summary>
        /// <param name="subView">Sub view.</param>
        public void Show(UIView subView)
        {
            subView.AddSubview(this);

            UIView.Animate(
                1, // duration
                () => { Alpha = 1; },
                () => { }
            );
        }

        void CancelButton_TouchDown(object sender, EventArgs e)
        {
            UIView.Animate(
                1, // duration
                () => { Alpha = 0; },
                () => { this.RemoveFromSuperview(); }
            );
        }

        public void CleanUp()
        {

        }
    }
}
