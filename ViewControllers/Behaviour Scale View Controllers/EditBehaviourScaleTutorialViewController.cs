using CoreGraphics;
using Fabic.Core.Helpers;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS
{
    public partial class EditBehaviourScaleTutorialViewController : UIViewController, IDisposable, ICanCleanUpMyself
    {
        public bool BodySelected = false;
        public bool ForIChooseChart = false;

        public EditBehaviourScaleTutorialViewController(IntPtr handle) : base(handle)
        {
        }

        public void CleanUp()
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ApplyLightInterface();
            UIWindow keyWindow = UIApplication.SharedApplication.KeyWindow;
            UIGraphics.BeginImageContext(keyWindow.Bounds.Size);
            CGContext context = UIGraphics.GetCurrentContext();
            keyWindow.Layer.RenderInContext(context);
            UIImage capturedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            nfloat centerX = this.View.Frame.Width / 2;
            nfloat centerY = this.View.Frame.Height / 2;

            // Tutorial
            List<TutorialOverlayItem> items = new List<TutorialOverlayItem>(); // help
            if (!ForIChooseChart)
            {
                TutorialOverlayItem item2 = new TutorialOverlayItem(capturedImage, new CGRect(this.View.Frame.Width - 51, 54, 42, 42), this.View.Frame, new CGRect(centerX - 120, 100, 240, 160), "To add an item to your Behaviour Scale, click on the '+' button", new CGRect(centerX - 120 + 240, 100 + 70, 240, 120), new CGRect(this.View.Frame.Width - 45, 45 + 57, this.View.Frame.Width, 47), -30);  // new CGRect(centerX - 120, 100 + 70, 240, 120), new CGRect(40, 45 + 57, this.View.Frame.Width, 47), -30)
                items.Add(item2);

                TutorialOverlayItem item3 = new TutorialOverlayItem(capturedImage, new CGRect(6, 54, 42, 42), this.View.Frame, new CGRect(centerX - 120, 120, 240, 160), "Click on the '-' button to remove an item from your Behaviour Scale", new CGRect(centerX - 120, 100 + 70, 240, 120), new CGRect(28, 45 + 60, this.View.Frame.Width, 47), -30);
                items.Add(item3);

                if (!BodySelected)
                {
                    TutorialOverlayItem item4 = new TutorialOverlayItem(capturedImage, new CGRect(this.View.Frame.Width - 45, (this.View.Frame.Height / 2) - 40, 58, 92), this.View.Frame, new CGRect(20, centerY, this.View.Frame.Width - 200, 220), "Click on the '>' button to edit the body items on your Behaviour Scale", new CGRect(this.View.Frame.Width - 200, centerY, 180, 220), new CGRect(this.View.Frame.Width - 60, centerY, 40, 40));
                    items.Add(item4);
                }
                else
                {
                    TutorialOverlayItem item4 = new TutorialOverlayItem(capturedImage, new CGRect(0, (this.View.Frame.Height / 2) - 40, 58, 92), this.View.Frame, new CGRect(this.View.Frame.Width - 200, centerY, this.View.Frame.Width - 200, 220), "Click on the '<' button to edit the life items on your Behaviour Scale", new CGRect(this.View.Frame.Width - 200, centerY, 180, 220), new CGRect(60, centerY, 40, 40));
                    items.Add(item4);
                }

                TutorialOverlayItem item5 = new TutorialOverlayItem(capturedImage, new CGRect(centerX - 20, this.View.Frame.Height - 120, 42, 42), this.View.Frame, new CGRect(centerX - 120, centerY, 240, 240), "You can also swipe the screen left and right to navigate between the life and body items of your Behaviour Scale", new CGRect(centerX, centerY + 240, 260, 260), new CGRect(centerX, this.View.Frame.Height - 123, -40, -90));
                items.Add(item5);
                TutorialOverlay tut = new TutorialOverlay(new CoreGraphics.CGRect(0, 0, this.View.Frame.Width, this.View.Frame.Height), capturedImage, items, Data.Enums.FabicColour.Purple, Data.Enums.FabicColour.LightBlue, (int)0, "Get Started!");
                tut.CloseButtonPressed += Tut_CloseButtonPressed;
                tut.Show(this.View);
            }
            else
            {
                TutorialOverlayItem item2 = new TutorialOverlayItem(capturedImage, new CGRect(this.View.Frame.Width - 51, 54, 42, 42), this.View.Frame, new CGRect(centerX - 120, 100, 240, 160), "To add an item to your I Choose Chart, click on the '+' button", new CGRect(centerX - 120 + 240, 100 + 70, 240, 120), new CGRect(this.View.Frame.Width - 45, 45 + 57, this.View.Frame.Width, 47), -30);  // new CGRect(centerX - 120, 100 + 70, 240, 120), new CGRect(40, 45 + 57, this.View.Frame.Width, 47), -30)
                items.Add(item2);

                TutorialOverlayItem item3 = new TutorialOverlayItem(capturedImage, new CGRect(6, 54, 42, 42), this.View.Frame, new CGRect(centerX - 120, 120, 240, 160), "Click on the '-' button to remove an item from your I Choose Chart", new CGRect(centerX - 120, 100 + 70, 240, 120), new CGRect(28, 45 + 60, this.View.Frame.Width, 47), -30);
                items.Add(item3);

                if (!BodySelected)
                {
                    TutorialOverlayItem item4 = new TutorialOverlayItem(capturedImage, new CGRect(this.View.Frame.Width - 45, (this.View.Frame.Height / 2) - 40, 58, 92), this.View.Frame, new CGRect(20, centerY, this.View.Frame.Width - 200, 220), "Click on the '>' button to edit the body items on your I Choose Chart", new CGRect(this.View.Frame.Width - 200, centerY, 180, 220), new CGRect(this.View.Frame.Width - 60, centerY, 40, 40));
                    items.Add(item4);
                }
                else
                {
                    TutorialOverlayItem item4 = new TutorialOverlayItem(capturedImage, new CGRect(0, (this.View.Frame.Height / 2) - 40, 58, 92), this.View.Frame, new CGRect(this.View.Frame.Width - 200, centerY, this.View.Frame.Width - 200, 220), "Click on the '<' button to edit the life items on your I Choose Chart", new CGRect(this.View.Frame.Width - 200, centerY, 180, 220), new CGRect(60, centerY, 40, 40));
                    items.Add(item4);
                }

                TutorialOverlayItem item5 = new TutorialOverlayItem(capturedImage, new CGRect(centerX - 20, this.View.Frame.Height - 120, 42, 42), this.View.Frame, new CGRect(centerX - 120, centerY, 240, 240), "You can also swipe the screen left and right to navigate between the option 1 and 2 items of your I Choose Chart", new CGRect(centerX, centerY + 240, 260, 260), new CGRect(centerX, this.View.Frame.Height - 123, -40, -90));
                items.Add(item5);
                TutorialOverlay tut = new TutorialOverlay(new CoreGraphics.CGRect(0, 0, this.View.Frame.Width, this.View.Frame.Height), capturedImage, items, Data.Enums.FabicColour.LightBlue, Data.Enums.FabicColour.Purple, (int)0, "Get Started!");
                tut.CloseButtonPressed += Tut_CloseButtonPressed;
                tut.Show(this.View);
            }
        }

        private void Tut_CloseButtonPressed(object sender, EventArgs e)
        {
            this.DismissModalViewController(true);
        }
    }
}