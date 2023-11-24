using CoreGraphics;
using Fabic.Data.Enums;
using Fabic.Data.Extensions;
using Foundation;
using System;
using System.Globalization;
using UIKit;

namespace Fabic.iOS
{
    public class TutorialOverlayItem : UIView, IDisposable, ICanCleanUpMyself
    {
        #region Variables
        private nfloat _angle = 50.8f;
        private nfloat _l2 = 21.5f;

        float cornerRadius = 20f;
        float borderWidth = 100f;
        float innerBorderWidth = 2f;
        UIViewContentMode imageContentMode = UIViewContentMode.ScaleToFill;

        CGColor innerBorderColour = UIColor.White.CGColor;
        UIColor descriptionLabelBackgroundColour = UIColor.Clear;

        UIView tut;
        UIView border;
        UIImageView tutView;
        UITextView tutLabel;
        FabicButton tutButton;
        EventHandler touchCallBackHandler;

        #endregion
        #region Properties
        public string DescriptionText
        {
            get { return tutLabel.Text; }
            set { tutLabel.Text = value; }
        }

        public NSAttributedString AttributedText
        {
            get { return tutLabel.AttributedText; }
            set { tutLabel.AttributedText = value; }
        }

        public CGRect TutorialLabelFrame
        {
            get
            {
                return tutLabel.Frame;
            }
        }

        public CGRect TutorialItemFrame
        {
            get;
            set;
        }

        public CGRect ArrowStartFrame
        {
            get;
            set;
        }

        public CGRect ArrowEndFrame
        {
            get;
            set;
        }

        public float ArrowCurveAdjustment
        {
            get;
            set;
        }

        public float ArrowAngleAdjustment
        {
            get;
            set;
        }

        public FabicColour ArrowColour
        {
            get;
            set;
        }

        public bool HasInnerBorder
        {
            get { return border.Layer.BorderWidth > 0; }
            set { if (value) { border.Layer.BorderWidth = innerBorderWidth; } else { border.Layer.BorderWidth = 0; } }
        }

        public float InnerBorderWidth
        {
            get { return innerBorderWidth; }
            set { innerBorderWidth = value; }
        }


        public float MainBorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; tut.Layer.BorderWidth = value; }
        }

        public float CornerRadius
        {
            get { return cornerRadius; }
            set { cornerRadius = value; tut.Layer.CornerRadius = value; border.Layer.CornerRadius = value; }
        }

        public UIFont DescriptionLabelFont
        {
            get { return tutLabel.Font; }
            set { tutLabel.Font = value; }
        }

        public CGColor InnerBorderColour
        {
            get { return innerBorderColour; }
            set { innerBorderColour = value; border.Layer.BorderColor = value; }
        }

        public UIColor DescriptionLabelBackgroundColour
        {
            get { return descriptionLabelBackgroundColour; }
            set { descriptionLabelBackgroundColour = value; if (tutLabel != null) { tutLabel.BackgroundColor = value; } }
        }

        public UIImage BackgroundSnapshot
        {
            get { return tutView.Image; }
            set { tutView.Image = value; }
        }

        public UIViewContentMode BackgroundSnapshotContentMode
        {
            get { return imageContentMode; }
            set { imageContentMode = value; if (tutView != null) { tutView.ContentMode = imageContentMode; } }
        }
        public UITextView Label
        {
            get
            {
                return tutLabel;
            }
        }
        public UIView Image
        {
            get
            {
                return border;
            }
        }
        #endregion

        public TutorialOverlayItem(UIImage snapshotImage, CGRect tutorialItemFrame, CGRect tutorialImageFrame, CGRect tutorialLabelFrame, string descriptionText, CGRect arrowStartFrame, CGRect arrowEndFrame, float arrowCurveAdjustment = 30, float arrowAngleAdjustment = 0, FabicColour arrowColour = FabicColour.Purple, EventHandler touchCallBack = null, bool useMask = true)
        {
            this.BackgroundColor = UIColor.Clear; this.UserInteractionEnabled = true;
            this.Frame = tutorialImageFrame;
            ArrowStartFrame = arrowStartFrame;
            ArrowEndFrame = arrowEndFrame;
            ArrowCurveAdjustment = arrowCurveAdjustment;
            ArrowAngleAdjustment = arrowAngleAdjustment;
            ArrowColour = arrowColour;

            // initialise the core components
            tut = new UIView(tutorialItemFrame);
            tutView = new UIImageView(this.Frame);
            tutLabel = new UITextView(tutorialLabelFrame);
            border = new UIView(tutorialItemFrame);

            // set up the subviews
            tut.Layer.CornerRadius = cornerRadius;
            tut.Layer.BorderWidth = borderWidth;
            tutView.Image = snapshotImage;
            if (useMask)
                tutView.MaskView = tut;
            tutView.ContentMode = imageContentMode;

            border.BackgroundColor = UIColor.Clear;
            border.Layer.BorderWidth = innerBorderWidth;
            border.Layer.CornerRadius = cornerRadius;
            border.Layer.BorderColor = UIColor.White.CGColor;
            tutView.AddSubview(border);

            AddSubview(tutView);

            // now set up the label
            tutLabel.Text = descriptionText;
            tutLabel.Editable = false;
            tutLabel.ScrollEnabled = false;
            tutLabel.Font = UIFont.FromName("American Typewriter", 26);
            tutLabel.TextColor = UIColor.White;
            tutLabel.TextAlignment = UITextAlignment.Center;
            tutLabel.BackgroundColor = descriptionLabelBackgroundColour;
            tutLabel.Layer.CornerRadius = 8f;
            tutLabel.Layer.ShadowOffset = new CGSize(1, 2);
            tutLabel.Layer.ShadowColor = UIColor.Black.ColorWithAlpha(0.88f).CGColor;
            AddSubview(tutLabel);

            // the button
            if (touchCallBack != null)
            {
                tutButton = new FabicButton();
                tutButton.WithShadow = false;
                tutButton.Frame = new CGRect(tutLabel.Frame.X + (tutLabel.Frame.Width / 2) - 40, tutLabel.Frame.Y + tutLabel.Frame.Height - 37, 80, 26); //
                tutButton.FabicColour = FabicColour.Gray;
                tutButton.SetTitle("Read More", UIControlState.Normal);
                tutButton.TitleLabel.Font = UIFont.SystemFontOfSize(11, UIFontWeight.Regular);
                tutButton.TouchDown += touchCallBack;
                touchCallBackHandler = touchCallBack;
                AddSubview(tutButton);
            }
        }

        public void DidTouchItem(NSSet touches, UIEvent evt, CGPoint location)
        {
            if (location.X >= tutButton.Frame.X && location.Y >= tutButton.Frame.Y && location.X <= tutButton.Frame.X + tutButton.Frame.Width && location.Y <= tutButton.Frame.Y + tutButton.Frame.Height)
            {
                tutButton.TouchesBegan(touches, evt);
                //if (touchCallBackHandler != null)
                //{
                //	touchCallBackHandler(this, new EventArgs());
                //}
            }
        }

        public void DidStopTouchingItem(NSSet touches, UIEvent evt, CGPoint location)
        {
            if (location.X >= tutButton.Frame.X && location.Y >= tutButton.Frame.Y && location.X <= tutButton.Frame.X + tutButton.Frame.Width && location.Y <= tutButton.Frame.Y + tutButton.Frame.Height)
            {
                tutButton.TouchesEnded(touches, evt);
                if (touchCallBackHandler != null)
                {
                    touchCallBackHandler(this, new EventArgs());
                }
            }
        }

        public void CleanUp()
        {

        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            // arrow
            //get graphics context
            using (CGContext cgContext = UIGraphics.GetCurrentContext())
            {
                nfloat x1 = ArrowStartFrame.X;
                nfloat y1 = ArrowStartFrame.Y;

                nfloat x2 = ArrowEndFrame.X;
                nfloat y2 = ArrowEndFrame.Y;

                nfloat xCurve = ((x1 + x2) / 2);
                nfloat yCurve = ((y1 + y2) / 2) - ArrowCurveAdjustment;
                nfloat xCurve2 = ((x1 + x2) / 2);

                //if (Math.Abs(y2 - y1) < 60 && Math.Abs(x2 - x1) < 60)
                //{
                xCurve2 = xCurve2 - ArrowAngleAdjustment;
                // _angle = 55;
                // }

                double l1Double = Math.Sqrt(Math.Pow(xCurve2 - x2, 2) + Math.Pow(yCurve - y2, 2));
                nfloat l1 = nfloat.Parse(l1Double.ToString(CultureInfo.InvariantCulture));

                double x3Double = x2 + _l2 / l1 * ((xCurve2 - x2) * Math.Cos(_angle) + (yCurve - y2) * Math.Sin(_angle));
                double y3Double = y2 + _l2 / l1 * ((yCurve - y2) * Math.Cos(_angle) - (xCurve2 - x2) * Math.Sin(_angle));
                double x4Double = x2 + _l2 / l1 * ((xCurve2 - x2) * Math.Cos(_angle) - (yCurve - y2) * Math.Sin(_angle));
                double y4Double = y2 + _l2 / l1 * ((yCurve - y2) * Math.Cos(_angle) + (xCurve2 - x2) * Math.Sin(_angle));

                nfloat x3 = nfloat.Parse(x3Double.ToString(CultureInfo.InvariantCulture));
                nfloat y3 = nfloat.Parse(y3Double.ToString(CultureInfo.InvariantCulture));
                nfloat x4 = nfloat.Parse(x4Double.ToString(CultureInfo.InvariantCulture));
                nfloat y4 = nfloat.Parse(y4Double.ToString(CultureInfo.InvariantCulture));

                //set up drawing attributes
                cgContext.SetLineWidth(5f);
                cgContext.SetLineCap(CGLineCap.Round);
                cgContext.SetStrokeColor(UIColor.Clear.FabicColour(ArrowColour).CGColor);

                //add lines to the touch points


                cgContext.MoveTo(x1, y1);
                cgContext.AddQuadCurveToPoint(xCurve, yCurve, x2, y2);
                cgContext.StrokePath();

                //  if (y3 < yCurve)
                //  y3 = y3 + _l2;
                //  y4 = y4 - _l2;

                cgContext.MoveTo(x2, y2);
                cgContext.AddLineToPoint(x3, y3);
                cgContext.StrokePath();

                cgContext.MoveTo(x2, y2);
                cgContext.AddLineToPoint(x4, y4);
                cgContext.StrokePath();
            }
        }
    }
}
