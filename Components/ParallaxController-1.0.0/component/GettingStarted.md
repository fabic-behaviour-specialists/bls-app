ParallaxViewController is free to use but presents a watermark with the Xpand-IT logo visible in all the images. To remove this watermark you must acquired a license key.

#Acquiring a license key
To acquire your license key email us at <xamarin.components@xpand-it.com> with the subject "ParallaxViewController - Request Key". An email will be sent back to you containing your license key for ParallaxViewController.

#Usage

####You can create a ParallaxViewController very easily

	ParallaxViewController = new ParallaxViewController();

###A second constructor enables you to present a license key for watermark removal

	ParallaxViewController = new ParallaxViewController("EMAIL", "LICENSE_KEY");

####Initialize the controller height, view and set of initial images

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

####Add images dynamically
	var images = new List ();
	images.Add (UIImage.FromBundle("image2.png"));
	images.Add (UIImage.FromBundle("image3.png"));
	ParallaxViewController.AddImages (images);

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

Feel free to create a subclass of ParallaxViewController for further customization as all of the components are exposed and available to tweak as you wish.

#Requirements

* iOS 7.0+

#Release Notes

####Version 1.3.2.1

* Support for iOS 8
