using App1;
using BodyLifeSkillsPlatform.iOS;
using Fabic.Core.Controllers;
using Fabic.Core.Helpers;
using Fabic.iOS.Controllers;
using Fabic.iOS.UIControls;
using Fabic.iOS.ViewControllers.TableViewSources;
using System;
using System.Threading.Tasks;
using UIKit;
using Xamarin.LockScreen;
using Xamarin.LockScreen.Security;

namespace Fabic.iOS
{
    public partial class HomeController : MainLockScreenController, ICanCleanUpMyself, IDisposable
    {
        FabicButterflyAnimationLayer _AnimationLayer;
        TutorialOverlay _TutorialLayer;
        bool loaded = false;
        public FabicButterflyAnimationLayer AnimationLayer { get { return _AnimationLayer; } }

        public HomeController(IntPtr handle) : base(handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.ApplyLightInterface();

            bool initialised = FabicDatabaseController.Initialised;

            if (!FabicDatabaseController.Initialised)
            {
                await FabicDatabaseController.InitialiseDatabase(false);
            }

            // Perform any additional setup after loading the view, typically from a nib.
            HomeViewControllerTableViewSource source = new HomeViewControllerTableViewSource();
            source.Animate += Source_Animate;
            tblMain.AllowsSelection = false;
            tblMain.Source = source;
            if (UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Width < 360)
            {
                tableTopConstraint.Constant = 17;//((UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height / 736) * -3) - (112 * (1 -(UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height / 736))); // 42;//-42;
                                                 //tblMain.ScrollEnabled = true;
                imgHomeBLSHeight.Constant = 134;
                homeTop.Constant = -26;
            }
            else if (UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height < 800)
            {
                tableTopConstraint.Constant = 5;//((UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height / 736) * -3) - (112 * (1 -(UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height / 736))); // 42;//-42;
                                                //tblMain.ScrollEnabled = true;
                imgHomeBLSHeight.Constant = 170;
                homeTop.Constant = -8;
            }
            else
            {
                tableTopConstraint.Constant = -20;//((UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height / 736) * -3) - (112 * (1 -(UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height / 736))); // 42;//-42;
                                                  //tblMain.ScrollEnabled = true;
                imgHomeBLSHeight.Constant = 170;
                imgBLSLeft.Constant = 20;
                imgBLSRight.Constant = 20;
                homeTop.Constant = -8;
            }

            //char c = '\u2699';
            UIImage settingsImg = UIImage.FromBundle("Settings");
            settingsImg = settingsImg.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            UIBarButtonItem settings = new UIBarButtonItem(settingsImg, UIBarButtonItemStyle.Plain, HandleEventHandler);

            NavigationItem.LeftBarButtonItem = settings;

            _AnimationLayer = new FabicButterflyAnimationLayer();
            this.Add(_AnimationLayer);


            if (!initialised)
            {
                await FabicDatabaseController.SyncTablesAsync();
            }
        }

        private async void Source_Animate(object sender, EventArgs e)
        {
            // await _AnimationLayer.Animate(3);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewWillAppear(bool animated)
        {
            try
            {
                tblMain.Hidden = false;
                base.Initialize(3, new LockHandler(this), UIColor.White);
                bool cont = true;

                if (Keychain.IsPasswordSet())
                {
                    if (!IsLocked)
                    {
                        tblMain.Hidden = true;
                        Lock();
                        cont = false;
                    }
                }
                if (!SecurityController.FirstTimeUsingApp)
                {
                    if (!loaded && cont)
                    {
                        _AnimationLayer.Animate(10);
                        loaded = true;
                    }
                    else if (loaded && cont)
                    {
                        _AnimationLayer.Animate(2);
                    }
                }
               ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(false, false);
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BackgroundColor = UIColor.Clear;
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BarTintColor = UIColor.Clear;
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.TintColor = UIColor.Gray;
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.Translucent = true;
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.Alpha = 1f;

                base.ViewWillAppear(animated);
            }
            catch (Exception ex)
            {

            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.ShadowImage = new UIImage();
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.Translucent = false;
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.Alpha = 1f;
        }

        public override void ViewDidDisappear(bool animated)
        {
            if ((NavigationController == null && IsMovingFromParentViewController) || (ParentViewController != null && ParentViewController.IsBeingDismissed))
            {

            }
            base.ViewDidDisappear(animated);
        }

        public async override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            bool cont = true;
            if (Keychain.IsPasswordSet())
            {
                if (!IsLocked)
                {
                    cont = false;
                }
            }
            if (SecurityController.FirstTimeUsingApp && cont)
            {
                await Task.Delay(300);
                ShowTutorial();
                loaded = true;
                SecurityController.FirstTimeUsingApp = false;
            }
        }

        private void ShowTutorial()
        {
            HomeTutorialViewController view = (HomeTutorialViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("homeTutorialView");
            view.ProvidesPresentationContextTransitionStyle = true;
            view.DefinesPresentationContext = true;
            view.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            view.NavigationBarFrame = this.NavigationController.NavigationBar.Frame;
            view.ParentView = this.View;
            //view.ToolBarFrame = this.NavigationController.Toolbar.Frame;
            //  view += View_Closed;
            this.PresentViewController(view, true, null);
        }

        private void Tut_CloseButtonPressed(object sender, EventArgs e)
        {
            _TutorialLayer.Hide();
            //this.DismissModalViewController(true);
            // this.Closed?.Invoke(sender, e);
        }

        void HandleEventHandler(object sender, EventArgs e)
        {
            var vc = UIStoryboard.FromName("Main", null).InstantiateViewController("settingsNav");
            //UINavigationController controller = new UINavigationController(vc);
            this.PresentViewController(vc, true, null);
        }

        public void CleanUp()
        {

        }
    }
}