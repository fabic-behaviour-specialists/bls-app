using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Foundation;
using UIKit;
//using VideoSplash;

namespace Fabic.iOS.ViewControllers
{
    public partial class FabicVideoViewController : UIViewController
    {
        public FabicVideo Video
        {
            get; set;
        }

        public FabicVideoViewController(FabicVideo video)
        {
            Video = video;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.ApplyLightInterface();
            var bundle = NSBundle.MainBundle;

            //// optionally crop the video
            //StartTime = 12.0f;
            //Duration = 4.0f;

            // make the video translucent
            //  Alpha = 0.7f;
        }

        //public override void ViewDidAppear(bool animated)
        //{
        //    if (Video != null)
        //        VideoUrl = new NSUrl(Video.URL, false);
        //    base.PlayVideo();
        //}

        //public override void OnVideoComplete()
        //{
        //    base.OnVideoComplete();
        //    this.DismissViewController(true, () => { });
        //}
    }
}