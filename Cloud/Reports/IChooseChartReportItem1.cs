using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BLS.Cloud.Helpers;

/// <summary>
/// Summary description for BodyLifeSkillsChartItem
/// </summary>
public class IChooseChartReportItem1 : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRPanel panelNewSkillsItems;
    private XRPanel panelPossibleReactionsItems;
    private float runningHeightTotalReaction = 45f;
    private float runningHeightTotalSkill = 45f;

    public float EstimatedHeight
    {
        get
        {
            if (runningHeightTotalReaction > runningHeightTotalSkill)
                return runningHeightTotalReaction;
            return runningHeightTotalSkill;
        }
    }

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    public XRLabel xrLabel2;
    public XRLabel xrLabel1;
    private XRPictureBox xrPictureBox2;
    private XRPictureBox xrPictureBox1;
    private XRPanel panelSkillsItem;
    private XRLabel lblItemSkills;
    private XRLabel lblBulletSkills;
    private XRPanel panelReactionItem;
    private XRLabel lblItemReaction;
    private XRLabel lblBulletReaction;
    public XRPanel panelArrow;
    private int Level = 1;

    public IChooseChartReportItem1()
    {
        InitializeComponent();
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    public void AddChartItemText(BLS.Cloud.Models.IChooseChartItemReport item)
    {
        XRPanel itemPanel = null; 
        XRLabel itemText = null;
        XRLabel itemBullet = null;

        switch (item.ChartOption)
        {
            case 0:
                itemPanel = panelSkillsItem.Copy();
                itemText = lblItemSkills.Copy();
                itemBullet = lblBulletSkills.Copy();
                break;
            case 1:
                itemPanel = panelReactionItem.Copy();
                itemText = lblItemReaction.Copy();
                itemBullet = lblBulletReaction.Copy();
                break;
        }

        itemText.Text = item.ItemText;
        itemPanel.Controls.Add(itemBullet);
        itemPanel.Controls.Add(itemText);
        itemPanel.Visible = true;

        System.Windows.Forms.Label label = new System.Windows.Forms.Label();
        label.Size = new Size(Convert.ToInt32(itemText.WidthF), Convert.ToInt32(itemText.HeightF));
        Graphics graphics = label.CreateGraphics();
        SizeF size = graphics.MeasureString(item.ItemText, itemText.Font);
        if (size.Height > 35)
        {
            itemText.HeightF = size.Height + 4;
            itemPanel.HeightF = size.Height + 4;
        }

        switch (item.ChartOption)
        {
            case 0: // skill
                itemPanel.TopF = runningHeightTotalSkill;
                runningHeightTotalSkill += itemPanel.HeightF;
                if (panelNewSkillsItems.HeightF < runningHeightTotalSkill)
                    panelNewSkillsItems.HeightF = runningHeightTotalSkill;
                panelNewSkillsItems.Controls.Add(itemPanel);
                break;
            case 1: // reaction
                itemPanel.TopF = runningHeightTotalReaction;
                runningHeightTotalReaction += itemPanel.HeightF;
                if (panelPossibleReactionsItems.HeightF < runningHeightTotalReaction)
                    panelPossibleReactionsItems.HeightF = runningHeightTotalReaction;
                panelPossibleReactionsItems.Controls.Add(itemPanel);
                break;
        }
        if (panelNewSkillsItems.SizeF.Height > panelPossibleReactionsItems.SizeF.Height)
            panelPossibleReactionsItems.SizeF = new SizeF(panelNewSkillsItems.SizeF.Width, panelNewSkillsItems.SizeF.Height);
        else if (panelPossibleReactionsItems.SizeF.Height > panelNewSkillsItems.SizeF.Height)
            panelNewSkillsItems.SizeF = new SizeF(panelNewSkillsItems.SizeF.Width, panelPossibleReactionsItems.SizeF.Height);
        panelArrow.LocationF = new PointF(panelArrow.LocationF.X, panelNewSkillsItems.SizeF.Height);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IChooseChartReportItem1));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.panelPossibleReactionsItems = new DevExpress.XtraReports.UI.XRPanel();
            this.panelReactionItem = new DevExpress.XtraReports.UI.XRPanel();
            this.lblItemReaction = new DevExpress.XtraReports.UI.XRLabel();
            this.lblBulletReaction = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.panelNewSkillsItems = new DevExpress.XtraReports.UI.XRPanel();
            this.panelSkillsItem = new DevExpress.XtraReports.UI.XRPanel();
            this.lblItemSkills = new DevExpress.XtraReports.UI.XRLabel();
            this.lblBulletSkills = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.panelArrow = new DevExpress.XtraReports.UI.XRPanel();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.panelPossibleReactionsItems,
            this.panelNewSkillsItems,
            this.panelArrow});
            this.Detail.HeightF = 207.2499F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // panelPossibleReactionsItems
            // 
            this.panelPossibleReactionsItems.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.panelPossibleReactionsItems.BorderWidth = 1.5F;
            this.panelPossibleReactionsItems.CanGrow = false;
            this.panelPossibleReactionsItems.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.panelReactionItem,
            this.xrLabel2});
            this.panelPossibleReactionsItems.LocationFloat = new DevExpress.Utils.PointFloat(426.0001F, 0F);
            this.panelPossibleReactionsItems.Name = "panelPossibleReactionsItems";
            this.panelPossibleReactionsItems.SizeF = new System.Drawing.SizeF(340.625F, 141.6249F);
            this.panelPossibleReactionsItems.StylePriority.UseBorders = false;
            this.panelPossibleReactionsItems.StylePriority.UseBorderWidth = false;
            this.panelPossibleReactionsItems.Draw += new DevExpress.XtraReports.UI.DrawEventHandler(this.panelBodyItems_Draw);
            // 
            // panelReactionItem
            // 
            this.panelReactionItem.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.panelReactionItem.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblItemReaction,
            this.lblBulletReaction});
            this.panelReactionItem.LocationFloat = new DevExpress.Utils.PointFloat(10.0001F, 45.33332F);
            this.panelReactionItem.Name = "panelReactionItem";
            this.panelReactionItem.SizeF = new System.Drawing.SizeF(320.6249F, 35.87499F);
            this.panelReactionItem.StylePriority.UseBorders = false;
            this.panelReactionItem.Visible = false;
            // 
            // lblItemReaction
            // 
            this.lblItemReaction.AutoWidth = true;
            this.lblItemReaction.CanShrink = true;
            this.lblItemReaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblItemReaction.LocationFloat = new DevExpress.Utils.PointFloat(23.58348F, 0F);
            this.lblItemReaction.Multiline = true;
            this.lblItemReaction.Name = "lblItemReaction";
            this.lblItemReaction.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblItemReaction.SizeF = new System.Drawing.SizeF(297.0415F, 35.875F);
            this.lblItemReaction.StylePriority.UseFont = false;
            this.lblItemReaction.StylePriority.UseTextAlignment = false;
            this.lblItemReaction.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblItemReaction.TextTrimming = System.Drawing.StringTrimming.None;
            // 
            // lblBulletReaction
            // 
            this.lblBulletReaction.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.lblBulletReaction.CanGrow = false;
            this.lblBulletReaction.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.lblBulletReaction.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblBulletReaction.Name = "lblBulletReaction";
            this.lblBulletReaction.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblBulletReaction.SizeF = new System.Drawing.SizeF(23.5836F, 35.87499F);
            this.lblBulletReaction.StylePriority.UseFont = false;
            this.lblBulletReaction.StylePriority.UseTextAlignment = false;
            this.lblBulletReaction.Text = "•";
            this.lblBulletReaction.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 9.999998F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(320.6251F, 35.33332F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseForeColor = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Possible Reactions";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // panelNewSkillsItems
            // 
            this.panelNewSkillsItems.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.panelNewSkillsItems.BorderWidth = 1.5F;
            this.panelNewSkillsItems.CanGrow = false;
            this.panelNewSkillsItems.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.panelSkillsItem,
            this.xrLabel1});
            this.panelNewSkillsItems.LocationFloat = new DevExpress.Utils.PointFloat(35.37483F, 0F);
            this.panelNewSkillsItems.Name = "panelNewSkillsItems";
            this.panelNewSkillsItems.SizeF = new System.Drawing.SizeF(340.625F, 141.6249F);
            this.panelNewSkillsItems.StylePriority.UseBorders = false;
            this.panelNewSkillsItems.StylePriority.UseBorderWidth = false;
            // 
            // panelSkillsItem
            // 
            this.panelSkillsItem.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.panelSkillsItem.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblItemSkills,
            this.lblBulletSkills});
            this.panelSkillsItem.LocationFloat = new DevExpress.Utils.PointFloat(10.0001F, 45.33332F);
            this.panelSkillsItem.Name = "panelSkillsItem";
            this.panelSkillsItem.SizeF = new System.Drawing.SizeF(320.6248F, 35.87499F);
            this.panelSkillsItem.StylePriority.UseBorders = false;
            this.panelSkillsItem.Visible = false;
            // 
            // lblItemSkills
            // 
            this.lblItemSkills.AutoWidth = true;
            this.lblItemSkills.CanShrink = true;
            this.lblItemSkills.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblItemSkills.LocationFloat = new DevExpress.Utils.PointFloat(23.58348F, 0F);
            this.lblItemSkills.Multiline = true;
            this.lblItemSkills.Name = "lblItemSkills";
            this.lblItemSkills.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblItemSkills.SizeF = new System.Drawing.SizeF(297.0413F, 35.875F);
            this.lblItemSkills.StylePriority.UseFont = false;
            this.lblItemSkills.StylePriority.UseTextAlignment = false;
            this.lblItemSkills.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblItemSkills.TextTrimming = System.Drawing.StringTrimming.None;
            // 
            // lblBulletSkills
            // 
            this.lblBulletSkills.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.lblBulletSkills.CanGrow = false;
            this.lblBulletSkills.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.lblBulletSkills.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblBulletSkills.Name = "lblBulletSkills";
            this.lblBulletSkills.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblBulletSkills.SizeF = new System.Drawing.SizeF(23.5836F, 35.87499F);
            this.lblBulletSkills.StylePriority.UseFont = false;
            this.lblBulletSkills.StylePriority.UseTextAlignment = false;
            this.lblBulletSkills.Text = "•";
            this.lblBulletSkills.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(45)))), ((int)(((byte)(145)))));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 9.999998F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(320.6251F, 35.33332F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseForeColor = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "STEP 3: NEW SKILLS";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // panelArrow
            // 
            this.panelArrow.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2,
            this.xrPictureBox1});
            this.panelArrow.LocationFloat = new DevExpress.Utils.PointFloat(35.37483F, 141.6249F);
            this.panelArrow.Name = "panelArrow";
            this.panelArrow.SizeF = new System.Drawing.SizeF(731.2502F, 65.62498F);
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(520.1251F, 1.624982F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(107.2917F, 64F);
            this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(116F, 1.624982F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(107.2917F, 64F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // IChooseChartReportItem1
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Margins = new System.Drawing.Printing.Margins(23, 30, 0, 0);
            this.Version = "18.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void panelBodyItems_Draw(object sender, DrawEventArgs e)
    {

    }
}
