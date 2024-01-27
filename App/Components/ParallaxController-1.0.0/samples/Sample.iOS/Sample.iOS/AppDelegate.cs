using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using CoreGraphics;
using XpandItComponents;

namespace Sample.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Foundation.Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;

        ParallaxViewController ParallaxViewController { get; set; }

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // create a new window instance based on the screen size
            window = new UIWindow((CGRect)UIScreen.MainScreen.Bounds);

            //Creating a ParallaxViewController 
            ParallaxViewController = new ParallaxViewController();

            //Setting a fixed image height
            ParallaxViewController.SetImageHeight(400);

            // Creting a list UIImages to present in the ParallaxViewController
            var images = new List<UIImage>();
            images.Add(UIImage.FromBundle("image1"));
            images.Add(UIImage.FromBundle("image2"));
            images.Add(UIImage.FromBundle("image3"));
            images.Add(UIImage.FromBundle("image4"));

            //View will be the ContentView of ParallaxViewController
            var view = new UIView(new CGRect(0, 0, window.Frame.Size.Width, 1000));
            view.BackgroundColor = UIColor.White;
            view.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin |
            UIViewAutoresizing.FlexibleRightMargin |
            UIViewAutoresizing.FlexibleWidth;

            //You can check if the image is tapped by set the ImageTapped property
            ParallaxViewController.ImageTaped = (i) =>
            {
                UIAlertView alertView = new UIAlertView("Image tapped", "Image at index " + i, null, "Ok", null);
                alertView.Show();
            };

            //Label that displays the index of current image
            var label = new UILabel(new CGRect(40, 0, window.Frame.Size.Width, 40));
            label.Text = "Displaying image at index 0";

            //You can listen when a image switches by setting the 
            ParallaxViewController.ImageChange = (i) =>
            {
                label.Text = "Displaying image at index " + i + ".";
            };
            view.AddSubview(label);

            UIButton startAutoScroll = new UIButton(new CGRect(40, label.Frame.Bottom, 280, 40));
            startAutoScroll.SetTitle("Click to Start Auto Scroll", UIControlState.Normal);
            startAutoScroll.SetTitleColor(UIColor.Black, UIControlState.Normal);
            startAutoScroll.TouchUpInside += (sender, e) => ParallaxViewController.StartAutomaticScroll();
            view.AddSubview(startAutoScroll);

            UIButton endAutoScroll = new UIButton(new CGRect(40, startAutoScroll.Frame.Bottom, 280, 40));
            endAutoScroll.SetTitle("Click to Stop Auto Scroll", UIControlState.Normal);
            endAutoScroll.SetTitleColor(UIColor.Black, UIControlState.Normal);
            endAutoScroll.TouchUpInside += (sender, e) => ParallaxViewController.StopAutomaticScroll();
            view.AddSubview(endAutoScroll);

            var sliderLabel = new UILabel(new CGRect(40, endAutoScroll.Frame.Bottom, window.Frame.Size.Width, 40));
            const string str = "Set the content offset: ";
            sliderLabel.Text = str + ParallaxViewController.CurrentIndex;
            view.AddSubview(sliderLabel);

            UISlider contentViewOffsetSlider = new UISlider(new CGRect(0, sliderLabel.Frame.Bottom, window.Frame.Size.Width, 40));
            contentViewOffsetSlider.MinValue = -100;
            contentViewOffsetSlider.MaxValue = 100;
            view.AddSubview(contentViewOffsetSlider);
            contentViewOffsetSlider.ValueChanged += (sender, e) =>
            {
                var value = contentViewOffsetSlider.Value;
                sliderLabel.Text = str + value;
                ParallaxViewController.SetContentViewOffsetY(value);
            };

            //			var view = new UIWebView (new RectangleF (0, 0, window.Frame.Size.Width, 1000));
            //			view.LoadRequest (new NSUrlRequest (new NSUrl ("http://www.xpand-it.com/pt/")));
            ParallaxViewController.SetupFor(view);
            ParallaxViewController.SetImages(images);
            var navigation = ParallaxViewController;
            window.RootViewController = navigation;

            // make the window visible
            window.MakeKeyAndVisible();

            return true;
        }
    }
}