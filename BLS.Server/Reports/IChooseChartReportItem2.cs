using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BLS.Cloud.Helpers;

/// <summary>
/// Summary description for BodyLifeSkillsChartItem
/// </summary>
public class IChooseChartReportItem2 : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRPanel panelNaturalConsequence1Items;
    private XRPanel panelNaturalConsequence2Items;
    private float runningHeightTotalConsequence1 = 45f;
    private float runningHeightTotalConsequence2 = 45f;

    public float EstimatedHeight
    {
        get
        {
            if (runningHeightTotalConsequence1 > runningHeightTotalConsequence2)
                return runningHeightTotalConsequence1;
            return runningHeightTotalConsequence2;
        }
    }

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    public XRLabel xrLabel1;
    private XRPictureBox xrPictureBox2;
    private XRPictureBox xrPictureBox1;
    public XRLabel xrLabel2;
    private XRPanel xrPanel1;
    public XRLabel xrLabel3;
    private XRPanel xrPanel2;
    public XRLabel xrLabel5;
    public XRLabel xrLabel4;
    private XRPictureBox xrPictureBox3;
    private XRPictureBox xrPictureBox4;
    public XRLabel xrLabel8;
    public XRLabel xrLabel7;
    public XRLabel xrLabel6;
    private XRPanel panelConsequence2Item;
    private XRLabel lblConsequence2Item;
    private XRLabel lblBulletConsequence2;
    private XRPanel panelConsequence1Item;
    private XRLabel lblConsequence1Item;
    private XRLabel lblBulletConsequence1;
    public XRPanel panelSummary;
    private int Level = 1;

    public IChooseChartReportItem2()
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
            case 0: //1
                itemPanel = panelConsequence1Item.Copy();
                itemText = lblConsequence1Item.Copy();
                itemBullet = lblBulletConsequence1.Copy();
                break;
            case 1: //2
                itemPanel = panelConsequence2Item.Copy();
                itemText = lblConsequence2Item.Copy();
                itemBullet = lblBulletConsequence2.Copy();
                break;
        }

        itemText.Text = item.ItemText;
        itemPanel.Controls.Add(itemBullet);
        itemPanel.Controls.Add(itemText);
        itemPanel.Visible = true;

        //System.Windows.Forms.Label label = new System.Windows.Forms.Label();
        //label.Size = new Size(Convert.ToInt32(itemText.WidthF), Convert.ToInt32(itemText.HeightF));
        //Graphics graphics = label.CreateGraphics();
        SizeF size = new SizeF();//graphics.MeasureString(item.ItemText, itemText.Font);

        if (size.Height > 35)
        {
            itemText.HeightF = size.Height + 4;
            itemPanel.HeightF = size.Height + 4;
        }

        switch (item.ChartOption)
        {
            case 0: // 1
                itemPanel.TopF = runningHeightTotalConsequence1;
                runningHeightTotalConsequence1 += itemPanel.HeightF;
                if (panelNaturalConsequence1Items.HeightF < runningHeightTotalConsequence1)
                    panelNaturalConsequence1Items.HeightF = runningHeightTotalConsequence1;
                panelNaturalConsequence1Items.Controls.Add(itemPanel);
                break;
            case 1: // 2
                itemPanel.TopF = runningHeightTotalConsequence2;
                runningHeightTotalConsequence2 += itemPanel.HeightF;
                if (panelNaturalConsequence2Items.HeightF < runningHeightTotalConsequence2)
                    panelNaturalConsequence2Items.HeightF = runningHeightTotalConsequence2;
                panelNaturalConsequence2Items.Controls.Add(itemPanel);
                break;
        }
        if (panelNaturalConsequence1Items.SizeF.Height > panelNaturalConsequence2Items.SizeF.Height)
            panelNaturalConsequence2Items.SizeF = new SizeF(panelNaturalConsequence2Items.SizeF.Width, panelNaturalConsequence1Items.SizeF.Height);
        else if (panelNaturalConsequence2Items.SizeF.Height > panelNaturalConsequence1Items.SizeF.Height)
            panelNaturalConsequence1Items.SizeF = new SizeF(panelNaturalConsequence1Items.SizeF.Width, panelNaturalConsequence2Items.SizeF.Height);
        panelSummary.LocationF = new PointF(panelSummary.LocationF.X, panelNaturalConsequence1Items.SizeF.Height);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IChooseChartReportItem2));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.panelNaturalConsequence2Items = new DevExpress.XtraReports.UI.XRPanel();
            this.panelConsequence2Item = new DevExpress.XtraReports.UI.XRPanel();
            this.lblConsequence2Item = new DevExpress.XtraReports.UI.XRLabel();
            this.lblBulletConsequence2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.panelNaturalConsequence1Items = new DevExpress.XtraReports.UI.XRPanel();
            this.panelConsequence1Item = new DevExpress.XtraReports.UI.XRPanel();
            this.lblConsequence1Item = new DevExpress.XtraReports.UI.XRLabel();
            this.lblBulletConsequence1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.panelSummary = new DevExpress.XtraReports.UI.XRPanel();
            this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrPictureBox4 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.panelNaturalConsequence2Items,
            this.panelNaturalConsequence1Items,
            this.panelSummary});
            this.Detail.HeightF = 325.4168F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // panelNaturalConsequence2Items
            // 
            this.panelNaturalConsequence2Items.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.panelNaturalConsequence2Items.BorderWidth = 1.5F;
            this.panelNaturalConsequence2Items.CanGrow = false;
            this.panelNaturalConsequence2Items.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.panelConsequence2Item,
            this.xrLabel2});
            this.panelNaturalConsequence2Items.LocationFloat = new DevExpress.Utils.PointFloat(426.0001F, 0F);
            this.panelNaturalConsequence2Items.Name = "panelNaturalConsequence2Items";
            this.panelNaturalConsequence2Items.SizeF = new System.Drawing.SizeF(340.625F, 139.5417F);
            this.panelNaturalConsequence2Items.StylePriority.UseBorders = false;
            this.panelNaturalConsequence2Items.StylePriority.UseBorderWidth = false;
            this.panelNaturalConsequence2Items.Draw += new DevExpress.XtraReports.UI.DrawEventHandler(this.panelBodyItems_Draw);
            // 
            // panelConsequence2Item
            // 
            this.panelConsequence2Item.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.panelConsequence2Item.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblConsequence2Item,
            this.lblBulletConsequence2});
            this.panelConsequence2Item.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 45.33333F);
            this.panelConsequence2Item.Name = "panelConsequence2Item";
            this.panelConsequence2Item.SizeF = new System.Drawing.SizeF(320.6249F, 35.875F);
            this.panelConsequence2Item.StylePriority.UseBorders = false;
            this.panelConsequence2Item.Visible = false;
            // 
            // lblConsequence2Item
            // 
            this.lblConsequence2Item.AutoWidth = true;
            this.lblConsequence2Item.CanShrink = true;
            this.lblConsequence2Item.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblConsequence2Item.LocationFloat = new DevExpress.Utils.PointFloat(34.0004F, 0F);
            this.lblConsequence2Item.Multiline = true;
            this.lblConsequence2Item.Name = "lblConsequence2Item";
            this.lblConsequence2Item.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblConsequence2Item.SizeF = new System.Drawing.SizeF(286.6245F, 35.87501F);
            this.lblConsequence2Item.StylePriority.UseFont = false;
            this.lblConsequence2Item.StylePriority.UseTextAlignment = false;
            this.lblConsequence2Item.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblConsequence2Item.TextTrimming = System.Drawing.StringTrimming.None;
            // 
            // lblBulletConsequence2
            // 
            this.lblBulletConsequence2.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.lblBulletConsequence2.CanGrow = false;
            this.lblBulletConsequence2.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.lblBulletConsequence2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblBulletConsequence2.Name = "lblBulletConsequence2";
            this.lblBulletConsequence2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblBulletConsequence2.SizeF = new System.Drawing.SizeF(34.00014F, 35.875F);
            this.lblBulletConsequence2.StylePriority.UseFont = false;
            this.lblBulletConsequence2.StylePriority.UseTextAlignment = false;
            this.lblBulletConsequence2.Text = "•";
            this.lblBulletConsequence2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 10F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(320.6251F, 35.33332F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseForeColor = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Likely Natural Consequence 2";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // panelNaturalConsequence1Items
            // 
            this.panelNaturalConsequence1Items.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.panelNaturalConsequence1Items.BorderWidth = 1.5F;
            this.panelNaturalConsequence1Items.CanGrow = false;
            this.panelNaturalConsequence1Items.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.panelConsequence1Item,
            this.xrLabel1});
            this.panelNaturalConsequence1Items.LocationFloat = new DevExpress.Utils.PointFloat(35.37483F, 0F);
            this.panelNaturalConsequence1Items.Name = "panelNaturalConsequence1Items";
            this.panelNaturalConsequence1Items.SizeF = new System.Drawing.SizeF(340.625F, 139.5417F);
            this.panelNaturalConsequence1Items.StylePriority.UseBorders = false;
            this.panelNaturalConsequence1Items.StylePriority.UseBorderWidth = false;
            // 
            // panelConsequence1Item
            // 
            this.panelConsequence1Item.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.panelConsequence1Item.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblConsequence1Item,
            this.lblBulletConsequence1});
            this.panelConsequence1Item.LocationFloat = new DevExpress.Utils.PointFloat(4.270808F, 45.33332F);
            this.panelConsequence1Item.Name = "panelConsequence1Item";
            this.panelConsequence1Item.SizeF = new System.Drawing.SizeF(326.3542F, 35.87501F);
            this.panelConsequence1Item.StylePriority.UseBorders = false;
            this.panelConsequence1Item.Visible = false;
            // 
            // lblConsequence1Item
            // 
            this.lblConsequence1Item.AutoWidth = true;
            this.lblConsequence1Item.CanShrink = true;
            this.lblConsequence1Item.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblConsequence1Item.LocationFloat = new DevExpress.Utils.PointFloat(34.00014F, 0F);
            this.lblConsequence1Item.Multiline = true;
            this.lblConsequence1Item.Name = "lblConsequence1Item";
            this.lblConsequence1Item.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblConsequence1Item.SizeF = new System.Drawing.SizeF(292.3541F, 35.875F);
            this.lblConsequence1Item.StylePriority.UseFont = false;
            this.lblConsequence1Item.StylePriority.UseTextAlignment = false;
            this.lblConsequence1Item.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblConsequence1Item.TextTrimming = System.Drawing.StringTrimming.None;
            // 
            // lblBulletConsequence1
            // 
            this.lblBulletConsequence1.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.lblBulletConsequence1.CanGrow = false;
            this.lblBulletConsequence1.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.lblBulletConsequence1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblBulletConsequence1.Name = "lblBulletConsequence1";
            this.lblBulletConsequence1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblBulletConsequence1.SizeF = new System.Drawing.SizeF(34.00014F, 35.87501F);
            this.lblBulletConsequence1.StylePriority.UseFont = false;
            this.lblBulletConsequence1.StylePriority.UseTextAlignment = false;
            this.lblBulletConsequence1.Text = "•";
            this.lblBulletConsequence1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(4.270808F, 9.999998F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(326.3542F, 35.33332F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseForeColor = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Likely Natural Consequence 1";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // panelSummary
            // 
            this.panelSummary.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2,
            this.xrPanel1,
            this.xrPictureBox2,
            this.xrPictureBox1});
            this.panelSummary.LocationFloat = new DevExpress.Utils.PointFloat(35.37483F, 139.5417F);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.SizeF = new System.Drawing.SizeF(731.2502F, 185.8752F);
            // 
            // xrPanel2
            // 
            this.xrPanel2.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(150)))), ((int)(((byte)(164)))));
            this.xrPanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(29)))), ((int)(((byte)(36)))));
            this.xrPanel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel2.BorderWidth = 2.5F;
            this.xrPanel2.CanGrow = false;
            this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox4,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6});
            this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(390.6252F, 66.08337F);
            this.xrPanel2.Name = "xrPanel2";
            this.xrPanel2.SizeF = new System.Drawing.SizeF(340.625F, 119.7918F);
            this.xrPanel2.StylePriority.UseBackColor = false;
            this.xrPanel2.StylePriority.UseBorderColor = false;
            this.xrPanel2.StylePriority.UseBorders = false;
            this.xrPanel2.StylePriority.UseBorderWidth = false;
            // 
            // xrPictureBox4
            // 
            this.xrPictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.xrPictureBox4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox4.Image")));
            this.xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(274.5832F, 18.99999F);
            this.xrPictureBox4.Name = "xrPictureBox4";
            this.xrPictureBox4.SizeF = new System.Drawing.SizeF(66.04184F, 47.49994F);
            this.xrPictureBox4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBox4.StylePriority.UseBackColor = false;
            this.xrPictureBox4.StylePriority.UseBorders = false;
            // 
            // xrLabel8
            // 
            this.xrLabel8.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(9.999954F, 78F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(320.6251F, 24.95824F);
            this.xrLabel8.StylePriority.UseBorderColor = false;
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseForeColor = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Perceived 0-20% equipped to manage life";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel7
            // 
            this.xrLabel7.BackColor = System.Drawing.Color.White;
            this.xrLabel7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel7.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(62.5F, 44F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(215.625F, 24.95826F);
            this.xrLabel7.StylePriority.UseBackColor = false;
            this.xrLabel7.StylePriority.UseBorderColor = false;
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseForeColor = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "MELTDOWN BODY";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.BackColor = System.Drawing.Color.Transparent;
            this.xrLabel6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(54.6875F, 11.42F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(231.25F, 24.95826F);
            this.xrLabel6.StylePriority.UseBackColor = false;
            this.xrLabel6.StylePriority.UseBorderColor = false;
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseForeColor = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "Closer to Level 5 - Code Red";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrPanel1
            // 
            this.xrPanel1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(172)))), ((int)(((byte)(218)))));
            this.xrPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel1.BorderWidth = 2.5F;
            this.xrPanel1.CanGrow = false;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrPictureBox3});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 66.08337F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(340.625F, 119.7918F);
            this.xrPanel1.StylePriority.UseBackColor = false;
            this.xrPanel1.StylePriority.UseBorderColor = false;
            this.xrPanel1.StylePriority.UseBorders = false;
            this.xrPanel1.StylePriority.UseBorderWidth = false;
            // 
            // xrLabel5
            // 
            this.xrLabel5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(9.999954F, 78.00001F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(320.6251F, 24.95824F);
            this.xrLabel5.StylePriority.UseBorderColor = false;
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseForeColor = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Perceived 80-100% equipped to manage life";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.BackColor = System.Drawing.Color.White;
            this.xrLabel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(62.5F, 41.54164F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(215.625F, 24.95827F);
            this.xrLabel4.StylePriority.UseBackColor = false;
            this.xrLabel4.StylePriority.UseBorderColor = false;
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseForeColor = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "CALM AND WANTED BODY";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.BackColor = System.Drawing.Color.Transparent;
            this.xrLabel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(54.6875F, 11.42F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(231.25F, 24.95826F);
            this.xrLabel3.StylePriority.UseBackColor = false;
            this.xrLabel3.StylePriority.UseBorderColor = false;
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseForeColor = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Closer to Level 1 - Code Blue";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrPictureBox3
            // 
            this.xrPictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.xrPictureBox3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox3.Image")));
            this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(274.583F, 18.99999F);
            this.xrPictureBox3.Name = "xrPictureBox3";
            this.xrPictureBox3.SizeF = new System.Drawing.SizeF(66.04187F, 47.49994F);
            this.xrPictureBox3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBox3.StylePriority.UseBackColor = false;
            this.xrPictureBox3.StylePriority.UseBorders = false;
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(507.8129F, 2.083344F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(107.2917F, 64F);
            this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(106.5002F, 2.083344F);
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
            this.BottomMargin.HeightF = 0.6248474F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // IChooseChartReportItem2
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Margins = new System.Drawing.Printing.Margins(23, 30, 0, 1);
            this.Version = "18.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void panelBodyItems_Draw(object sender, DrawEventArgs e)
    {

    }
}
