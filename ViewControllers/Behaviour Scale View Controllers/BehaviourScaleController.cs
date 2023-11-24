using Fabic.Core.Helpers;
using Fabic.Data.Extensions;
using Fabic.iOS.ViewControllers.TableViewSources;
using System;
using UIKit;

namespace Fabic.iOS
{
    public partial class BehaviourScaleController : UIViewController, IDisposable, ICanCleanUpMyself
    {
        public BehaviourScaleController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ApplyLightInterface();

            // Perform any additional setup after loading the view, typically from a nib.
            tblMain.Source = new BehaviourScaleControllerTableViewSource();

            tblMain.ScrollEnabled = false;
            // backgroundFabicImageWidth.Constant = (nfloat)(((UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height) / 480) * 120); // 200;//120;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.View.Constraints[14].Constant = Convert.ToInt32(UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height - (UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height / 2 + 260));
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BackgroundColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Purple);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BarTintColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Purple);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.TintColor = UIColor.White;
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(false, true);
            this.Title = "Behaviour Scale";
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ((BehaviourScaleControllerTableViewSource)tblMain.Source).UnhightlightItems();
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(true, false);
            this.Title = "Back";
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        public void CleanUp()
        {

        }
    }
}