using Fabic.Core.Enumerations;
using Fabic.Core.Models;
using Fabic.Data.Extensions;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS
{
    public partial class FabicIChooseChartCell : UITableViewCell, IDisposable, ICanCleanUpMyself
    {
        public IChooseChartItemType IChooseChartType
        {
            get; set;
        }

        public List<IChooseChartItem> IChooseChartItems
        {
            get; set;
        }

        public IChooseChart IChooseChart
        {
            get; set;
        }

        public FabicIChooseChartCell(IntPtr handle) : base(handle)
        {
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            // here we must draw the actual cell. This involves the actual laying out of the three different columns including lines to make them visible defined.
            // first work out how big each column can be based on the width of the main view

            double width = ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[0].View.Frame.Width / 2;
            double height = this.Frame.Height;
            double borderIndent = 13;
            double arrowHeight = 22;
            // outer border

            // inner heading label
            foreach (UIView view in Subviews)
                view.RemoveFromSuperview();

            UIView chartOption1View = new UIView();
            chartOption1View.Layer.BorderWidth = 1;
            chartOption1View.Layer.BorderColor = UIColor.Black.FabicColour(Data.Enums.FabicColour.Gray).CGColor;
            chartOption1View.BackgroundColor = UIColor.Clear;
            AddSubview(chartOption1View);

            UIView chartOption2View = new UIView();
            chartOption2View.Layer.BorderWidth = 1;
            chartOption2View.Layer.BorderColor = UIColor.Black.FabicColour(Data.Enums.FabicColour.Gray).CGColor;
            chartOption2View.BackgroundColor = UIColor.Clear;
            AddSubview(chartOption2View);

            // now draw the text
            // option 1
            int count = 0;
            double lastY = 4;

            if (IChooseChartType == IChooseChartItemType.Behaviour)
            {
                UILabel labelBevaiour = new UILabel();
                labelBevaiour.Frame = new CoreGraphics.CGRect(5, lastY + 3, width - (borderIndent * 2) - 10, 20);
                labelBevaiour.Font = UIFont.BoldSystemFontOfSize(12);
                labelBevaiour.TextColor = UIColor.Black.FabicColour(Data.Enums.FabicColour.Purple);
                labelBevaiour.TextAlignment = UITextAlignment.Center;
                labelBevaiour.Text = "STEP 3: NEW SKILLS";
                labelBevaiour.Lines = 100;
                chartOption1View.AddSubview(labelBevaiour);

                UILabel labelReaction = new UILabel();
                labelReaction.Frame = new CoreGraphics.CGRect(5, lastY + 3, width - (borderIndent * 2) - 10, 20);
                labelReaction.Font = UIFont.BoldSystemFontOfSize(12);
                labelReaction.TextColor = UIColor.Black;
                labelReaction.TextAlignment = UITextAlignment.Center;
                labelReaction.Text = "Possible Reactions";
                labelReaction.Lines = 100;
                chartOption2View.AddSubview(labelReaction);
                lastY += 24;
            }
            else
            {
                UILabel labelBevaiour = new UILabel();
                labelBevaiour.Frame = new CoreGraphics.CGRect(5, lastY + 3, width - (borderIndent * 2) - 10, 20);
                labelBevaiour.Font = UIFont.BoldSystemFontOfSize(9);
                labelBevaiour.TextColor = UIColor.Black;
                labelBevaiour.TextAlignment = UITextAlignment.Center;
                labelBevaiour.Text = "Likely Natural Consequence 1";
                labelBevaiour.Lines = 100;
                chartOption1View.AddSubview(labelBevaiour);

                UILabel labelReaction = new UILabel();
                labelReaction.Frame = new CoreGraphics.CGRect(5, lastY + 3, width - (borderIndent * 2) - 10, 20);
                labelReaction.Font = UIFont.BoldSystemFontOfSize(9);
                labelReaction.TextColor = UIColor.Black;
                labelReaction.TextAlignment = UITextAlignment.Center;
                labelReaction.Text = "Likely Natural Consequence 2";
                labelReaction.Lines = 100;
                chartOption2View.AddSubview(labelReaction);
                lastY += 20;
            }

            foreach (IChooseChartItem item in IChooseChartItems)
            {
                // check to see if the item relates to life or body - make sure it is only life
                if ((Core.Enumerations.IChooseChartItemType)item.ChartType == IChooseChartType && (Core.Enumerations.IChooseChartOption)item.ChartOption == IChooseChartOption.Option1)
                {
                    UILabel bullet = new UILabel();
                    bullet.Frame = new CoreGraphics.CGRect(4, 4 + (20 * count), 10, 20);
                    bullet.Font = UIFont.SystemFontOfSize(9);
                    bullet.Text = "•";
                    chartOption1View.AddSubview(bullet);

                    UILabel label = new UILabel();
                    label.Frame = new CoreGraphics.CGRect(12, lastY + 3, width - (borderIndent * 2) - 14, 20);
                    label.Font = UIFont.SystemFontOfSize(9);
                    label.AttributedText = item.MutableText;
                    label.Lines = 100;
                    label.SizeToFit();
                    bullet.Frame = new CoreGraphics.CGRect(4, label.Frame.Y, label.Frame.Width, label.Frame.Height);
                    chartOption1View.AddSubview(label);
                    count++;
                    lastY += label.Frame.Height + 6;
                }
            }
            chartOption1View.Frame = new CoreGraphics.CGRect(borderIndent, borderIndent, width - (borderIndent * 2), lastY + (borderIndent * 2) - arrowHeight);

            // option 2
            count = 0;
            lastY = 28;
            foreach (IChooseChartItem item in IChooseChartItems)
            {
                // check to see if the item relates to life or body - make sure it is only body
                if ((Core.Enumerations.IChooseChartItemType)item.ChartType == IChooseChartType && (Core.Enumerations.IChooseChartOption)item.ChartOption == IChooseChartOption.Option2)
                {
                    UILabel bullet = new UILabel();
                    bullet.Frame = new CoreGraphics.CGRect(4, 4 + (20 * count), 10, 20);
                    bullet.Font = UIFont.SystemFontOfSize(9);
                    bullet.Text = "•";
                    chartOption2View.AddSubview(bullet);

                    UILabel label = new UILabel();
                    label.Frame = new CoreGraphics.CGRect(12, lastY + 3, width - (borderIndent * 2) - 14, 20);
                    label.Font = UIFont.SystemFontOfSize(9);
                    //label.Text = item.ItemText;
                    label.AttributedText = item.MutableText;
                    label.Lines = 100;
                    label.SizeToFit();
                    bullet.Frame = new CoreGraphics.CGRect(4, label.Frame.Y, label.Frame.Width, label.Frame.Height);
                    chartOption2View.AddSubview(label);
                    count++;
                    lastY += label.Frame.Height + 6;
                }
            }
            chartOption2View.Frame = new CoreGraphics.CGRect(width + borderIndent, borderIndent, width - (borderIndent * 2), lastY + (borderIndent * 2) - arrowHeight);

            if (chartOption2View.Frame.Height > chartOption1View.Frame.Height)
                chartOption1View.Frame = new CoreGraphics.CGRect(chartOption1View.Frame.X, chartOption1View.Frame.Y, chartOption1View.Frame.Width, chartOption2View.Frame.Height);
            else if (chartOption1View.Frame.Height > chartOption2View.Frame.Height)
                chartOption2View.Frame = new CoreGraphics.CGRect(chartOption2View.Frame.X, chartOption2View.Frame.Y, chartOption2View.Frame.Width, chartOption1View.Frame.Height);


            // arrows
            UIImageView arrowLeft = new UIImageView();
            arrowLeft.Image = new UIImage("i-choose-chart-arrow.png");
            arrowLeft.Frame = new CoreGraphics.CGRect((width / 2) - 15, chartOption1View.Frame.Height + 15, 30, 30);
            AddSubview(arrowLeft);

            UIImageView arrowRight = new UIImageView();
            arrowRight.Image = new UIImage("i-choose-chart-arrow.png");
            arrowRight.Frame = new CoreGraphics.CGRect(width + (width / 2) - 15, chartOption2View.Frame.Height + 15, 30, 30);
            AddSubview(arrowRight);
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