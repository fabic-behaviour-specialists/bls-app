using CoreGraphics;
using Fabic.Core.Helpers;
using Fabic.iOS;
using System;
using System.Collections.Generic;
using UIKit;

namespace BodyLifeSkillsPlatform.iOS
{
    public partial class HomeTutorialViewController : UIViewController
    {
        public CGRect NavigationBarFrame
        {
            get; set;
        }

        public UIImage BackgroundImage
        {
            get; set;
        }

        public UIView ParentView
        {
            get; set;
        }

        public HomeTutorialViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.ApplyLightInterface();
            // grab reference to the view you'd like to capture
            //            UIView* wholeScreen = self.splitViewController.view;

            //            // define the size and grab a UIImage from it
            UIGraphics.BeginImageContextWithOptions(ParentView.Bounds.Size, ParentView.Opaque, 0.0f);
            ParentView.Layer.RenderInContext(UIGraphics.GetCurrentContext());
            UIImage capturedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            UIImage settingsImg = UIImage.FromBundle("Settings");
            settingsImg = settingsImg.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            UIGraphics.BeginImageContextWithOptions(capturedImage.Size, false, 0.0f);
            capturedImage.Draw(new CGRect(0, 0, capturedImage.Size.Width, capturedImage.Size.Height));
            settingsImg.Draw(new CGRect(16, NavigationBarFrame.Y + 6, settingsImg.Size.Width, settingsImg.Size.Height));
            capturedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            //UIWindow keyWindow = UIApplication.SharedApplication.KeyWindow;
            //    //UIGraphics.EndImageContext();
            //    UIGraphics.BeginImageContextWithOptions(keyWindow.Bounds.Size, false, UIScreen.MainScreen.Scale);
            //    CGContext context = UIGraphics.GetCurrentContext();
            //    keyWindow.Layer.RenderInContext(context);
            //    UIImage capturedImage = UIGraphics.GetImageFromCurrentImageContext();

            nfloat centerX = this.View.Frame.Width / 2;
            nfloat centerY = this.View.Frame.Height / 2;

            // Tutorial
            List<TutorialOverlayItem> items = new List<TutorialOverlayItem>(); // help



            TutorialOverlayItem item3 = new TutorialOverlayItem(capturedImage, new CGRect(9, NavigationBarFrame.Y, 42, 42), this.View.Frame, new CGRect(centerX - 120, 120, 240, 160), "Click here to edit your settings and account info", new CGRect(centerX - 120, 100 + 70, 240, 120), new CGRect(28, NavigationBarFrame.Y + 47, this.View.Frame.Width, 47), -30);
            items.Add(item3);

            if (UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height < 600)
            {
                TutorialOverlayItem item2 = new TutorialOverlayItem(capturedImage, new CGRect(centerX - 110, NavigationBarFrame.Y + 238, 220, 120), this.View.Frame, new CGRect(centerX - 120, NavigationBarFrame.Y + 0, 240, 240), "Click on these buttons to make, edit and print Behaviour Scales and I Choose Charts", new CGRect(centerX, NavigationBarFrame.Y + 200, 260, 260), new CGRect(centerX, NavigationBarFrame.Y + 235, -40, -90));
                items.Add(item2);
                TutorialOverlayItem item5 = new TutorialOverlayItem(capturedImage, new CGRect(centerX - 110, NavigationBarFrame.Y + 360, 220, 200), this.View.Frame, new CGRect(centerX - 120, NavigationBarFrame.Y + 60, 240, 240), "Click here for help, resources, support and more info about the BLS Program", new CGRect(centerX, NavigationBarFrame.Y + 260, 260, 260), new CGRect(centerX, NavigationBarFrame.Y + 325, -40, -90));
                items.Add(item5);
            }
            else
            if (UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height > 600 && UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height < 670)
            {
                TutorialOverlayItem item2 = new TutorialOverlayItem(capturedImage, new CGRect(centerX - 110, NavigationBarFrame.Y + 303, 220, 120), this.View.Frame, new CGRect(centerX - 120, NavigationBarFrame.Y + 0, 240, 240), "Click on these buttons to make, edit and print Behaviour Scales and I Choose Charts", new CGRect(centerX, NavigationBarFrame.Y + 200, 260, 260), new CGRect(centerX, NavigationBarFrame.Y + 300, -40, -90));
                items.Add(item2);
                TutorialOverlayItem item5 = new TutorialOverlayItem(capturedImage, new CGRect(centerX - 110, NavigationBarFrame.Y + 430, 220, 200), this.View.Frame, new CGRect(centerX - 120, NavigationBarFrame.Y + 60, 240, 240), "Click here for help, resources, support and more info about the BLS Program", new CGRect(centerX, NavigationBarFrame.Y + 260, 260, 260), new CGRect(centerX, NavigationBarFrame.Y + 395, -40, -90));
                items.Add(item5);
            }
            else if (UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height > 670 && UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height < 800)
            {
                TutorialOverlayItem item2 = new TutorialOverlayItem(capturedImage, new CGRect(centerX - 110, NavigationBarFrame.Y + 303, 220, 120), this.View.Frame, new CGRect(centerX - 120, NavigationBarFrame.Y + 0, 240, 240), "Click on these buttons to make, edit and print Behaviour Scales and I Choose Charts", new CGRect(centerX, NavigationBarFrame.Y + 200, 260, 260), new CGRect(centerX, NavigationBarFrame.Y + 300, -40, -90));
                items.Add(item2);
                TutorialOverlayItem item5 = new TutorialOverlayItem(capturedImage, new CGRect(centerX - 110, NavigationBarFrame.Y + 430, 220, 200), this.View.Frame, new CGRect(centerX - 120, NavigationBarFrame.Y + 60, 240, 240), "Click here for help, resources, support and more info about the BLS Program", new CGRect(centerX, NavigationBarFrame.Y + 260, 260, 260), new CGRect(centerX, NavigationBarFrame.Y + 395, -40, -90));
                items.Add(item5);
            }
            else
            {
                TutorialOverlayItem item2 = new TutorialOverlayItem(capturedImage, new CGRect(centerX - 110, NavigationBarFrame.Y + 328, 220, 120), this.View.Frame, new CGRect(centerX - 120, NavigationBarFrame.Y + 50, 240, 240), "Click on these buttons to make, edit and print Behaviour Scales and I Choose Charts", new CGRect(centerX, NavigationBarFrame.Y + 250, 260, 260), new CGRect(centerX, NavigationBarFrame.Y + 325, -40, -90));
                items.Add(item2);
                TutorialOverlayItem item5 = new TutorialOverlayItem(capturedImage, new CGRect(centerX - 110, NavigationBarFrame.Y + 450, 220, 200), this.View.Frame, new CGRect(centerX - 120, NavigationBarFrame.Y + 60, 240, 240), "Click here for help, resources, support and more info about the BLS Program", new CGRect(centerX, NavigationBarFrame.Y + 230, 260, 260), new CGRect(centerX, NavigationBarFrame.Y + 425, -40, -90));
                items.Add(item5);
            }


            TutorialOverlay tut = new TutorialOverlay(new CoreGraphics.CGRect(0, 0, this.View.Frame.Width, this.View.Frame.Height), capturedImage, items, Fabic.Data.Enums.FabicColour.LightBlue, Fabic.Data.Enums.FabicColour.Purple, 0, "Get Started!");
            tut.CloseButtonPressed += Tut_CloseButtonPressed;
            tut.Show(this.View);
        }

        private void Tut_CloseButtonPressed(object sender, EventArgs e)
        {
            this.DismissModalViewController(true);
        }
    }
}