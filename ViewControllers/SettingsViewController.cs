using Fabic.Core.Helpers;
using Fabic.Data.Extensions;
using Fabic.iOS.ViewControllers.TableViewSources;
using System;
using UIKit;

namespace Fabic.iOS
{
    public partial class SettingsViewController : UIViewController
    {
        partial void btnSettings_Tap(UIBarButtonItem sender)
        {
            this.NavigationController.DismissViewController(true, null);
        }

        public SettingsViewController(IntPtr handle) : base(handle)
        {
            //btnBack.Clicked += BtnBack_Clicked;
            this.ApplyLightInterface();
        }

        public override void ViewWillAppear(bool animated)
        {
            tblMain.Source = new SettingsControllerTableViewSource(this);
            base.ViewWillAppear(animated);

            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(false, true);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetToolbarHidden(true, true);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BackgroundColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Gray);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BarTintColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Gray);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.TintColor = UIColor.White;
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