ParallaxViewController is a UIViewController container designed for presenting multiple images in a single view.

#Features
* Present images in a slider fashion with support for auto/manual scrolling;
* Define the time spent on each image displayed;
* Add images dynamically;
* Capture events related to image tapping and image changing.

---

#Usage

####You can create a ParallaxViewController very easily

	ParallaxViewController = new ParallaxViewController();

	//Setting a fixed image height
	ParallaxViewController.SetImageHeight (400);

	//Content view can be any type of UIView
	var view = new UIView (new RectangleF (0, 0, window.Frame.Size.Width, 1000));
	view.BackgroundColor = UIColor.White;

	//Setting the main content
	ParallaxViewController.SetupFor (view);

	//Setting the images
	var images = new List<UIImageView> ();
	images.Add (UIImage.FromBundle("image1.png"));
	ParallaxViewController.SetImages (images);

####Capture image tapped and image changed events

	ParallaxViewController.ImageTaped = (int index) =>
	{
		//Do custom action when image is touched
	};

	ParallaxViewController.ImageChange = (int newImageIndex) =>
	{
		//Do custom action when image changed
	};

---

#Requirements

* iOS 7.0+

#Release Notes

####Version 1.3.2.1

* Support for iOS 8
