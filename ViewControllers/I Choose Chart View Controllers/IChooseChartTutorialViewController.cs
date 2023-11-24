using CoreGraphics;
using Fabic.Core.Helpers;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS
{
    public partial class IChooseChartTutorialViewController : UIViewController, IDisposable, ICanCleanUpMyself
    {
        public CGRect NavigationBarFrame
        {
            get; set;
        }

        public CGRect ToolBarFrame
        {
            get; set;
        }

        public IChooseChartTutorialViewController(IntPtr handle) : base(handle)
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
            UIGraphics.BeginImageContext(new CGSize(this.View.Frame.Width, this.View.Frame.Height));
            CGContext context = UIGraphics.GetCurrentContext();
            keyWindow.Layer.RenderInContext(context);
            UIImage capturedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            nfloat centerX = this.View.Frame.Width / 2;
            nfloat centerY = this.View.Frame.Height / 2;

            // Tutoriall
            List<TutorialOverlayItem> items = new List<TutorialOverlayItem>(); // help


            TutorialOverlayItem item2 = new TutorialOverlayItem(capturedImage, new CGRect(0, NavigationBarFrame.Y + 45, this.View.Frame.Width, 47), this.View.Frame, new CGRect(centerX - 120, NavigationBarFrame.Y + 100, 240, 120), "Click on the title of the I Choose Chart to edit it", new CGRect(centerX - 120, NavigationBarFrame.Y + 100 + 70, 240, 120), new CGRect(40, NavigationBarFrame.Y + 45 + 57, this.View.Frame.Width, 47), -30, 0, Data.Enums.FabicColour.Blue);
            items.Add(item2);

            TutorialOverlayItem item3 = new TutorialOverlayItem(capturedImage, new CGRect(0, NavigationBarFrame.Y + 140, this.View.Frame.Width, 80), this.View.Frame, new CGRect(centerX - 150, NavigationBarFrame.Y + 330, 300, 120), "Click on a I Choose Chart Option to view and edit its notes", new CGRect(centerX - 10, NavigationBarFrame.Y + 330, 300, 120), new CGRect(centerX, NavigationBarFrame.Y + 140 + 90, this.View.Frame.Width, 107), -5, 0, Data.Enums.FabicColour.Blue);
            item3.MainBorderWidth = 100f;
            items.Add(item3);

            TutorialOverlayItem item4 = new TutorialOverlayItem(capturedImage, new CGRect(this.View.Frame.Width - 42, NavigationBarFrame.Y + 44, 40, (ToolBarFrame.Y + 3) - (NavigationBarFrame.Y + 44)), this.View.Frame, new CGRect(this.View.Frame.Width - 220, centerY - 100, 180, 220), "Swipe the I Choose Chart up and down to see the different levels", new CGRect(this.View.Frame.Width - 220, centerY - 100, 180, 220), new CGRect(this.View.Frame.Width - 50, NavigationBarFrame.Y + 144, 40, (ToolBarFrame.Y + 3) - (NavigationBarFrame.Y + 44)), 40, 0, Data.Enums.FabicColour.Blue);
            items.Add(item4);

            TutorialOverlayItem item5 = new TutorialOverlayItem(capturedImage, new CGRect(8.5f, ToolBarFrame.Y + 3, 42, 42), this.View.Frame, new CGRect(8, this.View.Frame.Height - 300, 260, 260), "Click here to share and save the I Choose Chart as an image or PDF", new CGRect(130, this.View.Frame.Height - 140, 260, 260), new CGRect(8.5f + 42, ToolBarFrame.Y + 3, 42, 42), -40, -90, Data.Enums.FabicColour.Blue);
            items.Add(item5);

            TutorialOverlayItem item6 = new TutorialOverlayItem(capturedImage, new CGRect(this.View.Frame.Width - 51f, ToolBarFrame.Y + 3, 42, 42), this.View.Frame, new CGRect(centerX - 130, this.View.Frame.Height - 320, 260, 260), "Click here to archive this I Choose Chart if you decide you no longer need it to support you", new CGRect(centerX + 80, this.View.Frame.Height - 130, 260, 260), new CGRect(this.View.Frame.Width - 51f, ToolBarFrame.Y + 3, 42, 42), 0, 0, Data.Enums.FabicColour.Blue);
            items.Add(item6);

            TutorialOverlayItem item1 = new TutorialOverlayItem(capturedImage, new CGRect(this.View.Frame.Width - 53.5f, NavigationBarFrame.Y, 42, 42), this.View.Frame, new CGRect(this.View.Frame.Width - 170, NavigationBarFrame.Y + 40, 160, 120), "Click here for this help guide", new CGRect(this.View.Frame.Width - 130, NavigationBarFrame.Y + 40, 160, 120), new CGRect(this.View.Frame.Width - 53.5f, NavigationBarFrame.Y, 42, 42), 30, 90, Data.Enums.FabicColour.Blue);
            items.Add(item1);

            TutorialOverlay tut = new TutorialOverlay(new CoreGraphics.CGRect(0, 0, this.View.Frame.Width, this.View.Frame.Height), capturedImage, items, Data.Enums.FabicColour.Blue, Data.Enums.FabicColour.Purple, (int)NavigationBarFrame.Y);

            tut.CloseButtonPressed += Tut_CloseButtonPressed;
            tut.Show(this.View);
        }

        private void Tut_CloseButtonPressed(object sender, EventArgs e)
        {
            this.DismissModalViewController(true);
        }
    }
}