using DevExpress.XtraReports.UI;
using System;
using System.Linq;

namespace BodyLifeSkillsPlatform.Data.Helpers
{
    public static class XRControlExtensions
    {
        public static XRPanel Copy(this XRPanel panel)
        {
            XRPanel newPanel = new XRPanel();
            newPanel.AnchorHorizontal = panel.AnchorHorizontal;
            newPanel.AnchorVertical = panel.AnchorVertical;
            newPanel.BackColor = panel.BackColor;
            newPanel.Bookmark = panel.Bookmark;
            newPanel.BookmarkParent = panel.BookmarkParent;
            newPanel.BorderColor = panel.BorderColor;
            newPanel.BorderDashStyle = panel.BorderDashStyle;
            newPanel.Borders = panel.Borders;
            newPanel.BorderWidth = panel.BorderWidth;
            newPanel.BoundsF = panel.BoundsF;
            newPanel.CanPublish = panel.CanPublish;
            newPanel.CanGrow = panel.CanGrow;
            newPanel.CanShrink = panel.CanShrink;
            newPanel.EvenStyleName = panel.EvenStyleName;
            newPanel.HeightF = panel.HeightF;
            newPanel.KeepTogether = panel.KeepTogether;
            newPanel.LeftF = panel.LeftF;
            newPanel.LocationF = panel.LocationF;
            newPanel.Padding = panel.Padding;
            newPanel.RightToLeft = panel.RightToLeft;
            newPanel.SizeF = panel.SizeF;
            newPanel.SnapLineMargin = panel.SnapLineMargin;
            newPanel.SnapLinePadding = panel.SnapLinePadding;
            newPanel.StyleName = panel.StyleName;
            newPanel.TextFormatString = panel.TextFormatString;
            newPanel.TopF = panel.TopF;
            newPanel.Visible = panel.Visible;
            newPanel.WidthF = panel.WidthF;
            return newPanel;
        }

        public static XRLabel Copy(this XRLabel label)
        {
            XRLabel newLabel = new XRLabel();
            newLabel.AnchorHorizontal = label.AnchorHorizontal;
            newLabel.AnchorVertical = label.AnchorVertical;
            newLabel.BackColor = label.BackColor;
            newLabel.Bookmark = label.Bookmark;
            newLabel.BookmarkParent = label.BookmarkParent;
            newLabel.BorderColor = label.BorderColor;
            newLabel.BorderDashStyle = label.BorderDashStyle;
            newLabel.Borders = label.Borders;
            newLabel.BorderWidth = label.BorderWidth;
            newLabel.BoundsF = label.BoundsF;
            newLabel.CanPublish = label.CanPublish;
            newLabel.CanGrow = label.CanGrow;
            newLabel.CanShrink = label.CanShrink;
            newLabel.EvenStyleName = label.EvenStyleName;
            newLabel.HeightF = label.HeightF;
            newLabel.KeepTogether = label.KeepTogether;
            newLabel.LeftF = label.LeftF;
            newLabel.LocationF = label.LocationF;
            newLabel.Padding = label.Padding;
            newLabel.RightToLeft = label.RightToLeft;
            newLabel.SizeF = label.SizeF;
            newLabel.SnapLineMargin = label.SnapLineMargin;
            newLabel.SnapLinePadding = label.SnapLinePadding;
            newLabel.StyleName = label.StyleName;
            newLabel.TextFormatString = label.TextFormatString;
            newLabel.TopF = label.TopF;
            newLabel.Visible = label.Visible;
            newLabel.WidthF = label.WidthF;
            newLabel.WordWrap = label.WordWrap;
            newLabel.TextTrimming = label.TextTrimming;
            newLabel.TextFormatString = label.TextFormatString;
            newLabel.TextAlignment = label.TextAlignment;
            newLabel.Text = label.Text;
            newLabel.Summary = label.Summary;
            newLabel.Multiline = label.Multiline;
            newLabel.Lines = label.Lines;
            newLabel.ForeColor = label.ForeColor;
            newLabel.Font = label.Font;
            newLabel.Angle = label.Angle;
            return newLabel;
        }
    }
}