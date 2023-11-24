using Fabic.Data.Extensions;
using Foundation;
using System;
using UIKit;

namespace Fabic.iOS
{
    public partial class FabicTextCell : UITableViewCell
    {
        //		UILabel txtMain = new UILabel();
        //l
        private string stringToShow = "";
        private UITextView txtMain = new UITextView();

        /// <summary>
        /// Gets or sets the text to show.
        /// </summary>
        /// <value>The text to show.</value>
        public string TextToShow
        {
            get { return stringToShow; }
            set { stringToShow = value; LayoutSubviews(); }
        }

        /// <summary>
        /// Gets the width of the text box.
        /// </summary>
        /// <value>The width of the text box.</value>
        public nfloat TextBoxWidth
        {
            get { return txtMain.Frame.Width; }
        }

        /// <summary>
        /// Gets or sets the attributed text to show.
        /// </summary>
        /// <value>The attributed text to show.</value>
        public NSAttributedString AttributedTextToShow
        {
            get { return txtMain.AttributedText; }
            set { txtMain.AttributedText = value; }
        }

        public FabicTextCell(IntPtr handle) : base(handle)
        {

        }

        /// <summary>
        /// Draws the text view.
        /// </summary>
        public void ReDrawTextView()
        {
            txtMain.LayoutSubviews();//txtMain.re
        }

        public override void DrawRect(CoreGraphics.CGRect area, UIViewPrintFormatter formatter)
        {
            base.DrawRect(area, formatter);

        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (txtMain != null)
                txtMain.RemoveFromSuperview();

            // update the sizing of the text vie (txw
            txtMain = new UITextView();
            txtMain.Frame = new CoreGraphics.CGRect(25, 10, this.Frame.Width - 50, this.Frame.Height - 25);
            var htmlString = stringToShow;
            var error = new NSError();
            var docAttributes = new NSAttributedStringDocumentAttributes()
            {
                StringEncoding = NSStringEncoding.UTF8,
                DocumentType = NSDocumentType.HTML
            };
            txtMain.Editable = false;
            txtMain.ScrollEnabled = false;
            txtMain.BackgroundColor = UIColor.Clear;
            txtMain.TintColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple);
            txtMain.Selectable = true;
            txtMain.AttributedText = new NSAttributedString(htmlString, docAttributes, ref error);
            this.AddSubview(txtMain);
        }
    }
}
//		/// <summary>
//		/// Sets the attributed text.
//		/// </summary>
//		public void SetAttributedText()
//		{
//			var htmlString = @"<p>The Fabic Behaviour Change App offers a practical application via<a href='http://www.bodylifeskills.com/about-bls'>Fabic’s Body Life Skills program</a> to bring about lasting behaviour change for people of all ages.</p><br/><p>We all use behaviours we would prefer not to use … thus we all use unwanted behaviours; albeit in varying forms and intensity.</p><br/><p>In simplicity this app supports people to <b><em>""Understand and change unwanted behaviours used by self or any other person"".</em></b></p>";
//			var error = new NSError();
//			var docAttributes = new NSAttributedStringDocumentAttributes()
//			{
//				StringEncoding = NSStringEncoding.UTF8,
//				DocumentType = NSDocumentType.HTML
//			};
//			txtMain.AttributedText = new NSMutableAttributedString(htmlString, docAttributes.Dictionary);
//		}

