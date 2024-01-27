using Fabic.Core.Helpers;
using Fabic.Data.Extensions;
using Fabic.iOS.ViewControllers.TableViewSources;
using System;
using UIKit;

namespace Fabic.iOS
{
    public partial class IChooseChartController : UIViewController, IDisposable, ICanCleanUpMyself
    {
        public IChooseChartController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.ApplyLightInterface();
            // Perform any additional setup after loading the view, typically from a nib.
            tblMain.Source = new IChooseChartControllerTableViewSource();
            tblMain.ScrollEnabled = false;
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(false, true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.View.Constraints[16].Constant = Convert.ToInt32(UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height - (UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height / 2 + 260));
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BackgroundColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Blue);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BarTintColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Blue);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.TintColor = UIColor.White;
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(false, true);
            this.Title = "I Choose Chart";
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ((IChooseChartControllerTableViewSource)tblMain.Source).UnhightlightItems();
            //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(true, false);
            this.Title = "Back";
        }

        public void CleanUp()
        {

        }
    }
}