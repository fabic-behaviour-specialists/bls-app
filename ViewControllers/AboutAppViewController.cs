using Fabic.Core.Helpers;
using Fabic.Data.Extensions;
using Fabic.iOS.ViewControllers.TableViewSources;
using System;
using UIKit;

namespace Fabic.iOS
{
    public partial class AboutAppViewController : UIViewController, IDisposable, ICanCleanUpMyself
    {
        public AboutAppViewController(IntPtr handle) : base(handle)
        {
            this.ApplyLightInterface();
        }

        public void CleanUp()
        {

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(false, true);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetToolbarHidden(true, true);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BackgroundColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Gray);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BarTintColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Gray);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.TintColor = UIColor.White;

            tblMain.Source = new AboutAppViewControllerTableViewSource();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BackgroundColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Purple);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BarTintColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Purple);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.TintColor = UIColor.White;
        }
    }
}