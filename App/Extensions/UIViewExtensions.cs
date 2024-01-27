using CoreGraphics;
using Foundation;
using UIKit;
namespace Fabic.iOS.Helpers
{
    public static class UIViewExtensions
    {
        /// <summary>
        /// Exports the UIView as an image.
        /// </summary>
        /// <returns>The UIView as an image.</returns>
        /// <param name="view">View.</param>
        public static UIImage ExportUIViewAsImage(this UIView view)
        {
            UIGraphics.BeginImageContextWithOptions(view.Frame.Size, false, 0.0f);
            view.Layer.RenderInContext(UIGraphics.GetCurrentContext());
            var img = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return img;
        }

        /// <summary>
        /// Exports the UIView as a pdf.
        /// </summary>
        /// <returns>The UIView as a pdf.</returns>
        /// <param name="view">View.</param>
        public static NSData ExportUIViewAsPDF(this UIView view)
        {
            string fileName = "BehaviourScaleExport-" + NSDate.Now.ToString();
            UIGraphics.BeginPDFContext(fileName, view.Frame, new Foundation.NSDictionary());

            CGRect frame = new CGRect(0, -80, view.Frame.Width, view.Frame.Height);
            CGRect footerFrame = new CGRect(0, 0, view.Frame.Width, 80);

            UIImage footerImage = new UIImage("fabic-footer");
            UIImageView footerView = new UIImageView(footerImage); footerView.Frame = footerFrame;

            UIGraphics.BeginPDFPage(view.Frame, new Foundation.NSDictionary());
            footerView.ExportUIViewAsImage().Draw(footerFrame);
            footerImage.Draw(footerFrame);

            UIGraphics.BeginPDFPage(view.Frame, new Foundation.NSDictionary());
            view.ExportUIViewAsImage().Draw(frame);
            footerView.ExportUIViewAsImage().Draw(footerFrame);

            UIGraphics.EndPDFContent();
            return NSData.FromFile(fileName);
        }
    }
}