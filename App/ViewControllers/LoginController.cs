using Fabic.Core.Helpers;
using Fabic.iOS.UIControls;
using System;
using System.Threading.Tasks;
using UIKit;

namespace Fabic.iOS
{
    public partial class LoginController : UIViewController
    {
        FabicButterflyAnimationLayer _AnimationLayer;
        bool disappearing = false;
        public FabicButterflyAnimationLayer AnimationLayer { get { return _AnimationLayer; } }
        public LoginController(IntPtr handle) : base(handle)
        {
            //tableTopConstraint.Constant = ((UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height / 736) * -3) - (112 * (1 - (UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height / 736))); // 42;//-42;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.ApplyLightInterface();
            this.WantsFullScreenLayout = true;
            tblMain.ScrollEnabled = false;
            ViewControllers.TableViewSources.LoginViewControllerTableViewSource viewSource = new ViewControllers.TableViewSources.LoginViewControllerTableViewSource();
            viewSource.ShowAlert += ViewSource_ShowAlert;
            viewSource.DismissAlert += ViewSource_DismissAlert;
            viewSource.Animate += ViewSource_Animate;
            tblMain.Source = viewSource;

            _AnimationLayer = new FabicButterflyAnimationLayer();
            this.Add(_AnimationLayer);
        }

        private async void ViewSource_Animate(object sender, EventArgs e)
        {
            while (!disappearing)
            {
                await _AnimationLayer.Animate(20);
                await Task.Delay(2000);
            }
        }

        private void ViewSource_DismissAlert(object sender, EventArgs e)
        {
            BigTed.BTProgressHUD.Dismiss();
        }

        private void ViewSource_ShowAlert(object sender, EventArgs e)
        {
            BigTed.BTProgressHUD.Show("Getting things set up...", -1, BigTed.MaskType.None);
        }

        public override void ViewWillAppear(bool animated)
        {
            disappearing = false;
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(false, false);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BackgroundColor = UIColor.Clear;
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BarTintColor = UIColor.Clear;
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.TintColor = UIColor.Gray;
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.Translucent = true;
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.Alpha = 1f;

            base.ViewWillAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            disappearing = true;
            base.ViewWillDisappear(animated);
        }
    }
}