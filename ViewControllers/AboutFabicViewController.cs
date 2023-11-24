using BigTed;
using CoreGraphics;
using Fabic.Core.Controllers;
using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Fabic.Data.Extensions;
using Fabic.iOS.ViewControllers.TableViewSources;
using Foundation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;
using XpandItComponents;

namespace Fabic.iOS
{
    public partial class AboutFabicViewController : ParallaxViewController, IDisposable, ICanCleanUpMyself
    {
        List<AboutFabicVideo> Videos;

        public AboutFabicViewController() : base("josh@fabic.com.au", "qvzoGmhipj5jvt5hA")
        {
            //Setting a fixed image height
            this.SetImagesContent(UIViewContentMode.ScaleAspectFill);
            this.SetImageHeight(750);
        }

        public void CleanUp()
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.ApplyLightInterface();
            BTProgressHUD.Show();
            Task.Run(() => { Videos = FabicDatabaseController.FetchAboutFabicVideos().Result; }).Wait();
            BTProgressHUD.Dismiss();

            // Creting a list UIImages to present in the ParallaxViewController
            var images = new List<UIImage>();
            images.Add(UIImage.FromBundle("Welcome.jpg"));
            //images.Add(UIImage.FromBundle("Clients.jpg"));
            images.Add(UIImage.FromBundle("Events.jpg"));
            images.Add(UIImage.FromBundle("Presenting.jpg"));
            images.Add(UIImage.FromBundle("Tanya.jpg"));
            images.Add(UIImage.FromBundle("Children.jpg"));

            //View will be the ContentView of ParallaxViewController
            var view = new UIView(new CGRect(0, 0, this.View.Frame.Width, (108 * (Videos.Count + 1)) + (360 + 190)));
            view.BackgroundColor = UIColor.White;
            view.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin |
            UIViewAutoresizing.FlexibleRightMargin |
            UIViewAutoresizing.FlexibleWidth;

            UIImage backgroundImage = UIImage.FromBundle("Background");
            UIImageView backgroundView = new UIImageView(backgroundImage);
            backgroundView.ContentMode = UIViewContentMode.ScaleAspectFill;
            backgroundView.Frame = new CGRect(0, 0, this.View.Frame.Width, (108 * (Videos.Count + 1)) + (360 + 190));
            UITableView table = new UITableView(new CGRect(0, 0, this.View.Frame.Width, (108 * (Videos.Count + 1)) + (360 + 190)), UITableViewStyle.Plain);
            AboutFabicViewControllerTableViewSource viewSource = new AboutFabicViewControllerTableViewSource();
            table.Source = viewSource;
            table.BackgroundView = backgroundView;
            table.ScrollEnabled = false;
            view.AddSubview(table);

            //You can check if the image is tapped by set the ImageTapped property
            this.ImageTaped = (i) =>
            {
                //UIAlertView alertView = new UIAlertView("Image tapped", "Image at index " + i, null, "Ok", null);
                //alertView.Show();
            };

            this.SetupFor(view);
            this.SetImages(images);
            this.View.BackgroundColor = UIColor.White;
            this.StartAutomaticScroll();
            this.ShouldAutorotate();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(false, true);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetToolbarHidden(true, true);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BackgroundColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Gray);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BarTintColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Gray);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.TintColor = UIColor.White;

            this.Title = "About Fabic";
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BackgroundColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Purple);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BarTintColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Purple);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.TintColor = UIColor.White;
        }

        void Item1EventHandler(object sender, EventArgs e)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.fabic.com.au/"));
        }

        void Item2EventHandler(object sender, EventArgs e)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.fabic.com.au/"));
        }

        void Item3EventHandler(object sender, EventArgs e)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.fabic.com.au/"));
        }

        void Item4EventHandler(object sender, EventArgs e)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.fabic.com.au/"));
        }

        void Item5EventHandler(object sender, EventArgs e)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.fabic.com.au/"));
        }

        void Item6EventHandler(object sender, EventArgs e)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.fabic.com.au/"));
        }
    }
}