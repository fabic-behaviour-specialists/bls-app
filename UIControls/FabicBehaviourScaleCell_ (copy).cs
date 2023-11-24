using Fabic.Core.Models;
using Fabic.Data.Extensions;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS
{
    public partial class FabicBehaviourScaleCell : UITableViewCell, IDisposable, ICanCleanUpMyself
    {
        private bool drawn = false;

        public int BehaviourScaleLevel
        {
            get; set;
        }

        public List<BehaviourScaleItem> BehaviourScaleItems
        {
            get; set;
        }

        public BehaviourScale BehaviourScale
        {
            get; set;
        }

        public FabicBehaviourScaleCell(IntPtr handle) : base(handle)
        {

        }

        public override void LayoutSubviews()
        {
            if (drawn)
            {
                foreach (UIView subView in this.Subviews)
                {
                    subView.RemoveFromSuperview();
                    subView.Dispose();
                }
            }

            base.LayoutSubviews();

            // here we must draw the actual cell. This involves the actual laying out of the three different columns including lines to make them visible defined.
            // first work out how big each column can be based on the width of the main view

            double width = ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[0].View.Frame.Width / 3;
            double height = this.Frame.Height;


            // now draw the text
            // life
            int count = 0;
            double lastY = 4;
            foreach (BehaviourScaleItem item in BehaviourScaleItems)
            {
                // check to see if the item relates to life or body - make sure it is only life
                if ((Core.Enumerations.BehaviourScaleItemType)item.BehaviourScaleType == Core.Enumerations.BehaviourScaleItemType.Life && item.BehaviourScaleLevel == BehaviourScaleLevel)
                {
                    UILabel bullet = new UILabel();
                    bullet.Frame = new CoreGraphics.CGRect(8, 4 + (20 * count), 10, 20);
                    bullet.Font = UIFont.SystemFontOfSize(8);
                    bullet.Text = "•";
                    AddSubview(bullet);

                    UILabel label = new UILabel();
                    label.Frame = new CoreGraphics.CGRect(16, lastY + 3, width - 20, 20);
                    label.Font = UIFont.SystemFontOfSize(8);
                    label.Text = item.Name.Trim();
                    label.Lines = 100;
                    label.SizeToFit();
                    bullet.Frame = new CoreGraphics.CGRect(8, label.Frame.Y, label.Frame.Width, label.Frame.Height);
                    AddSubview(label);
                    count++;
                    lastY += label.Frame.Height + 6;
                }
            }

            // body
            count = 0;
            lastY = 4;
            foreach (BehaviourScaleItem item in BehaviourScaleItems)
            {
                // check to see if the item relates to life or body - make sure it is only body
                if ((Core.Enumerations.BehaviourScaleItemType)item.BehaviourScaleType == Core.Enumerations.BehaviourScaleItemType.Body && item.BehaviourScaleLevel == BehaviourScaleLevel)
                {
                    UILabel bullet = new UILabel();
                    bullet.Frame = new CoreGraphics.CGRect(width * 2 + 10, 4 + (20 * count), 10, 20);
                    bullet.Font = UIFont.SystemFontOfSize(8);
                    bullet.Text = "•";
                    AddSubview(bullet);

                    UILabel label = new UILabel();
                    label.Frame = new CoreGraphics.CGRect(width * 2 + 18, lastY + 3, width - 24, 20);
                    label.Font = UIFont.SystemFontOfSize(8);
                    label.Text = item.Name;
                    label.Lines = 100;
                    label.SizeToFit();
                    bullet.Frame = new CoreGraphics.CGRect(width * 2 + 10, label.Frame.Y, label.Frame.Width, label.Frame.Height);
                    AddSubview(label);
                    count++;
                    lastY += label.Frame.Height + 6;
                }
            }

            // middle
            UIView middleView = new UIView();
            middleView.Frame = new CoreGraphics.CGRect(width, 0, width, height - 2);
            middleView.Layer.BorderWidth = 3;
            AddSubview(middleView);

            UILabel middleViewHeaderLabel = new UILabel();
            middleViewHeaderLabel.Frame = new CoreGraphics.CGRect(8, 4, width - 12, 20);
            middleViewHeaderLabel.Font = UIFont.FromName("AvenirNext-Medium", 8);
            middleViewHeaderLabel.BackgroundColor = UIColor.Clear;
            middleView.AddSubview(middleViewHeaderLabel);

            UIView middleViewSummarySubView = new UIView();
            middleViewSummarySubView.Frame = new CoreGraphics.CGRect(7, 23, width - 21, 21);
            middleViewSummarySubView.BackgroundColor = UIColor.White;
            //middleViewSummarySubView.
            middleView.AddSubview(middleViewSummarySubView);

            UILabel middleViewSummaryLabel = new UILabel();
            middleViewSummaryLabel.Frame = new CoreGraphics.CGRect(7, 0, width - 21, 21);
            middleViewSummaryLabel.Font = UIFont.SystemFontOfSize(7);
            middleViewSummaryLabel.Lines = 2;
            middleViewSummaryLabel.TextAlignment = UITextAlignment.Natural;
            middleViewSummaryLabel.BackgroundColor = UIColor.White;
            middleViewSummarySubView.AddSubview(middleViewSummaryLabel);

            UILabel middleViewSubSummaryLabel = new UILabel();
            middleViewSubSummaryLabel.Frame = new CoreGraphics.CGRect(12, 38, width - 12, 50);
            middleViewSubSummaryLabel.Lines = 3;
            middleViewSubSummaryLabel.Font = UIFont.SystemFontOfSize(7);
            middleViewSubSummaryLabel.BackgroundColor = UIColor.Clear;
            middleView.AddSubview(middleViewSubSummaryLabel);

            UIImageView face = new UIImageView();
            face.Frame = new CoreGraphics.CGRect(width - 31, height - 35, 26, 26);
            middleView.AddSubview(face);

            // side labels
            UILabel sideLabel = new UILabel();
            sideLabel.Frame = new CoreGraphics.CGRect(width + (width / 2) + 15, height / 2, 105, 12);
            sideLabel.Font = UIFont.FromName("AvenirNext-Regular", 7);
            sideLabel.BackgroundColor = UIColor.Clear;
            sideLabel.Transform = CoreGraphics.CGAffineTransform.MakeRotation((nfloat)Math.PI / 2);
            //sideLabel.Frame = new CoreGraphics.CGRect(width + (width / 2) + 15, height / 2, height, 12);
            sideLabel.Hidden = true;
            sideLabel.TextAlignment = UITextAlignment.Center;
            AddSubview(sideLabel);

            switch (BehaviourScaleLevel)
            {
                case 1: // blue
                    middleView.BackgroundColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.DarkBlue, true);
                    middleView.Layer.BorderColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.DarkBlue).CGColor;
                    middleViewHeaderLabel.Text = "Level 1 – Code Blue";
                    middleViewSummaryLabel.Text = "Calm and wanted body";
                    middleViewSubSummaryLabel.Text = "Perceived 80 - 100% equipped to manage life";
                    face.Image = new UIImage("Faces1.png");
                    break;
                case 2: // green
                    middleView.BackgroundColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Green, true);
                    middleView.Layer.BorderColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Green).CGColor;
                    middleViewHeaderLabel.Text = "Level 2 – Code Green";
                    middleViewSummaryLabel.Text = "Low intensity body change";
                    middleViewSubSummaryLabel.Text = "Perceived 60 - 80% equipped to manage life";
                    face.Image = new UIImage("Faces2.png");
                    sideLabel.Hidden = false;
                    sideLabel.Text = "a little bit";
                    break;
                case 3: // yellow
                    middleView.BackgroundColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Yellow, true);
                    middleView.Layer.BorderColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Yellow).CGColor;
                    middleViewHeaderLabel.Text = "Level 3 – Code Yellow";
                    middleViewSummaryLabel.Text = "Increased intensity body change";
                    middleViewSubSummaryLabel.Text = "Perceived 40 - 60% equipped to manage life";
                    face.Image = new UIImage("Faces3.png");
                    sideLabel.Hidden = false;
                    sideLabel.Text = "more";
                    break;
                case 4: // orange
                    middleView.BackgroundColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Orange, true);
                    middleView.Layer.BorderColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Orange).CGColor;
                    middleViewHeaderLabel.Text = "Level 4 – Code Orange";
                    middleViewSummaryLabel.Text = "High intensity body change";
                    middleViewSubSummaryLabel.Text = "Perceived 20 - 40% equipped to manage life";
                    face.Image = new UIImage("Faces4.png");
                    sideLabel.Hidden = false;
                    sideLabel.Text = "a lot";
                    break;
                case 5: // red
                    middleView.BackgroundColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Red, true);
                    middleView.Layer.BorderColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Red).CGColor;
                    middleViewHeaderLabel.Text = "Level 5 – Code Red";
                    middleViewSummaryLabel.Text = "Meltdown body";
                    middleViewSubSummaryLabel.Text = "Perceived 0 - 20% equipped to manage life";
                    face.Image = new UIImage("Faces5.png");
                    break;
            }
            drawn = true;
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public override void DrawRect(CoreGraphics.CGRect area, UIViewPrintFormatter formatter)
        {
            base.DrawRect(area, formatter);
        }

        public void CleanUp()
        {

        }
    }
}