using Fabic.Core.Helpers;
using Fabic.Data.Extensions;
using Foundation;
using System;
using System.IO;
using UIKit;

namespace Fabic.iOS.ViewControllers
{
    public partial class TermsAndConditionsViewController : UIViewController
    {
        public TermsAndConditionsViewController(IntPtr handle) : base(handle)
        {
        }

        public event EventHandler Closed;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.ApplyLightInterface();
            if (this.NavigationController != null)
            {
                //this.View.Constraints[15].Constant = this.NavigationController.NavigationBar.Frame.Height - 40;
                //this.View.Constraints[8].Constant = this.NavigationController.NavigationBar.Frame.Height - 20;

                this.NavigationController.Toolbar.BarTintColor = UIColor.Blue.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                this.NavigationController.Toolbar.BackgroundColor = UIColor.Blue.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
            }
            else
            {
                //this.View.Constraints[15].Constant = 0;
                //this.View.Constraints[8].Constant = 0;
            }

            UIBarButtonItem closeButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (object sender, EventArgs e) => { this.NavigationController.DismissViewController(true, () => { Closed?.Invoke(this, null); }); });
            NavigationItem.RightBarButtonItem = closeButton;

            string fileName = "blsapp-terms-and-conditions.html";
            string localHtmlUrl = Path.Combine(NSBundle.MainBundle.BundlePath, fileName);

            webView.LoadRequest(new NSUrlRequest(new NSUrl(localHtmlUrl, false)));
            webView.ScalesPageToFit = true;
        }
    }
}