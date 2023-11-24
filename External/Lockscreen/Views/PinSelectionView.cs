using CoreGraphics;
using Fabic.Data.Extensions;
using System;
using System.Drawing;
using UIKit;

namespace Xamarin.LockScreen.Views
{
    public class PinSelectionView : UIView
    {
        private const float _AnimationLength = 0.15f;
        internal UIColor SelectedColor { get; set; }
        internal UIView SelectedView { get; set; }
        public static float PinSelectionViewWidth { get { return 10; } }
        public static float PinSelectionViewHeight { get { return 10; } }

        public PinSelectionView(RectangleF frame) : base(frame)
        {
            SetDefaultStyles();
            Layer.BorderWidth = 1.5f;
            SelectedView = new UIView(RectangleF.Empty);
            SelectedView.Alpha = 0.0f;
            SelectedView.BackgroundColor = SelectedColor;
        }

        private void SetDefaultStyles()
        {
            SelectedColor = UIColor.White.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
        }
        private void PrepareApperance()
        {
            SelectedView.BackgroundColor = SelectedColor;
            Layer.BorderColor = SelectedColor.CGColor;
            BackgroundColor = UIColor.Clear;
        }
        private void PerformLayout()
        {
            SelectedView.Frame = new RectangleF(0, 0, (float)Frame.Size.Width, (float)Frame.Size.Height);
            AddSubview(SelectedView);
        }
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            PerformLayout();
            PrepareApperance();
        }
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            PrepareApperance();
        }
        internal async void SetSelected(bool selected, bool animated, Action completion)
        {
            float length = animated ? _AnimationLength : 0.0f;
            float alpha = selected ? 1.0f : 0.0f;
            await UIView.AnimateAsync(length, new Action(() => SelectedView.Alpha = alpha));

            /*UIView.Animate (length, 0, UIViewAnimationOptions.CurveEaseInOut,
				new NSAction (() => SelectedView.Alpha = alpha),
				new NSAction (completion));*/
        }
    }
}
