using BLS.Cloud.Helpers;
using DevExpress.XtraReports.UI;
using System.Drawing;

/// <summary>
/// Summary description for BodyLifeSkillsChartItem
/// </summary>
public class BodyLifeSkillsChartItem : DevExpress.XtraReports.UI.XtraReport
{
    public DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRPanel panelItem;
    private XRPanel panelLifeItems;
    private XRPanel panelInfo;
    private XRLabel lblBullet;
    private XRLabel lblItem;
    private XRLabel lblLevelDescription;
    private XRLabel lblIntensity;
    private XRLabel lblBehaviourLevel;
    private XRPictureBox picLevel;
    private XRLabel lblAnxiety;
    private XRPanel panelBodyItems;
    private float runningHeightTotalBody = 0f;
    private float runningHeightTotalLife = 0f;
    private int count = 0;

    public int Height
    {
        get
        {
            int height = 30;
            if (count > 4)
            {
                height = 30 + ((count - 4) * 0);
            }
            return height;
        }
    }

    public float EstimatedHeight
    {
        get
        {
            if (runningHeightTotalBody > runningHeightTotalLife)
                return runningHeightTotalBody;
            return runningHeightTotalLife;
        }
    }

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private int Level = 1;

    public BodyLifeSkillsChartItem(IWebHostEnvironment environment, int behaviourScaleLevel)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        switch (behaviourScaleLevel)
        {
            case 5:
                lblBehaviourLevel.Text = "Level 5 - Code Red";
                lblIntensity.Text = "Meltdown body";
                lblLevelDescription.Text = "Perceived 0-20% equipped to manage life";
                lblAnxiety.Visible = false;
                picLevel.Image = Image.FromFile(Path.Combine(environment.WebRootPath, "~/Resources/Faces5.png"));
                panelInfo.BorderColor = Color.FromArgb(207, 29, 36);
                panelInfo.BackColor = Color.FromArgb(245, 150, 164);
                break;
            case 4:
                lblBehaviourLevel.Text = "Level 4 - Code Orange";
                lblIntensity.Text = "High intensity body change";
                lblLevelDescription.Text = "Perceived 20-40% equipped to manage life";
                lblAnxiety.Text = "a lot";
                lblAnxiety.Visible = true;
                picLevel.Image = Image.FromFile(Path.Combine(environment.WebRootPath, "~/Resources/Faces4.png"));
                panelInfo.BorderColor = Color.FromArgb(237, 106, 35);
                panelInfo.BackColor = Color.FromArgb(247, 193, 154);
                break;
            case 3:
                lblBehaviourLevel.Text = "Level 3 - Code Yellow";
                lblIntensity.Text = "Increased intensity body change";
                lblLevelDescription.Text = "Perceived 40-60% equipped to manage life";
                lblAnxiety.Text = "more";
                lblAnxiety.Visible = true;
                picLevel.Image = Image.FromFile(Path.Combine(environment.WebRootPath, "~/Resources/Faces3.png"));
                panelInfo.BorderColor = Color.FromArgb(253, 212, 5);
                panelInfo.BackColor = Color.FromArgb(250, 236, 159);
                break;
            case 2:
                lblBehaviourLevel.Text = "Level 2 - Code Green";
                lblIntensity.Text = "Low intensity body change";
                lblLevelDescription.Text = "Perceived 60-80% equipped to manage life";
                lblAnxiety.Text = "a little bit";
                lblAnxiety.Visible = true;
                picLevel.Image = Image.FromFile(Path.Combine(environment.WebRootPath, "~/Resources/Faces2.png"));
                panelInfo.BorderColor = Color.FromArgb(0, 166, 81);
                panelInfo.BackColor = Color.FromArgb(147, 209, 172);
                break;
            case 1:
                lblBehaviourLevel.Text = "Level 1 - Code Blue";
                lblIntensity.Text = "Calm and wanted body";
                lblLevelDescription.Text = "Perceived 80-100% equipped to manage life";
                lblAnxiety.Visible = false;
                picLevel.Image = Image.FromFile(Path.Combine(environment.WebRootPath, "~/Resources/Faces1.png"));
                panelInfo.BorderColor = Color.FromArgb(42, 75, 160);
                panelInfo.BackColor = Color.FromArgb(138, 172, 218);
                break;
        }
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

    public void AddChartItemText(BLS.Cloud.Models.BehaviourScaleItem item)
    {
        count++;
        XRPanel itemPanel = new XRPanel();
        XRLabel itemText = lblItem.Copy();
        XRLabel itemBullet = lblBullet.Copy();

        itemText.Text = item.Name;

        //System.Windows.Forms.Label label = new System.Windows.Forms.Label();
        //label.Size = new Size(Convert.ToInt32(itemText.WidthF), Convert.ToInt32(itemPanel.HeightF));
        //Graphics graphics = label.CreateGraphics();
        //SizeF size = graphics.MeasureString(item.Name, itemText.Font);
        //itemPanel.HeightF = size.Height;
        //itemBullet.HeightF = size.Height;
        itemPanel.Visible = true;
        itemText.Visible = true;
        itemBullet.Visible = true;

        switch (item.BehaviourScaleType)
        {
            case 0: // body
                itemPanel.TopF = runningHeightTotalBody;
                runningHeightTotalBody += itemText.HeightF;
                itemPanel.LeftF = panelBodyItems.LeftF;
                itemPanel.WidthF -= 70f;
                break;
            case 1: // life
                itemPanel.TopF = runningHeightTotalLife;
                runningHeightTotalLife += itemText.HeightF;
                break;
        }
        itemPanel.Controls.Add(itemBullet);
        itemPanel.Controls.Add(itemText);
        this.Detail.Controls.Add(itemPanel);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BodyLifeSkillsChartItem));
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.panelBodyItems = new DevExpress.XtraReports.UI.XRPanel();
        this.panelItem = new DevExpress.XtraReports.UI.XRPanel();
        this.lblItem = new DevExpress.XtraReports.UI.XRLabel();
        this.lblBullet = new DevExpress.XtraReports.UI.XRLabel();
        this.panelLifeItems = new DevExpress.XtraReports.UI.XRPanel();
        this.panelInfo = new DevExpress.XtraReports.UI.XRPanel();
        this.lblLevelDescription = new DevExpress.XtraReports.UI.XRLabel();
        this.lblIntensity = new DevExpress.XtraReports.UI.XRLabel();
        this.lblBehaviourLevel = new DevExpress.XtraReports.UI.XRLabel();
        this.picLevel = new DevExpress.XtraReports.UI.XRPictureBox();
        this.lblAnxiety = new DevExpress.XtraReports.UI.XRLabel();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.panelBodyItems,
            this.panelItem,
            this.panelLifeItems,
            this.panelInfo,
            this.lblAnxiety});
        this.Detail.HeightF = 152.4583F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // panelBodyItems
        // 
        this.panelBodyItems.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.panelBodyItems.CanGrow = false;
        this.panelBodyItems.LocationFloat = new DevExpress.Utils.PointFloat(519.9999F, 0F);
        this.panelBodyItems.Name = "panelBodyItems";
        this.panelBodyItems.SizeF = new System.Drawing.SizeF(234.0002F, 108.2499F);
        this.panelBodyItems.Draw += new DevExpress.XtraReports.UI.DrawEventHandler(this.panelBodyItems_Draw);
        // 
        // panelItem
        // 
        this.panelItem.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblItem,
            this.lblBullet});
        this.panelItem.LocationFloat = new DevExpress.Utils.PointFloat(519.9999F, 108.2499F);
        this.panelItem.Name = "panelItem";
        this.panelItem.SizeF = new System.Drawing.SizeF(234.0002F, 35.875F);
        this.panelItem.Visible = false;
        // 
        // lblItem
        // 
        this.lblItem.AutoWidth = true;
        this.lblItem.CanShrink = true;
        this.lblItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
        this.lblItem.LocationFloat = new DevExpress.Utils.PointFloat(21.875F, 0F);
        this.lblItem.Multiline = true;
        this.lblItem.Name = "lblItem";
        this.lblItem.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblItem.SizeF = new System.Drawing.SizeF(208.7499F, 35.87498F);
        this.lblItem.StylePriority.UseFont = false;
        this.lblItem.StylePriority.UseTextAlignment = false;
        this.lblItem.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        this.lblItem.TextTrimming = System.Drawing.StringTrimming.None;
        // 
        // lblBullet
        // 
        this.lblBullet.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.lblBullet.CanGrow = false;
        this.lblBullet.Font = new System.Drawing.Font("Times New Roman", 14F);
        this.lblBullet.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.lblBullet.Name = "lblBullet";
        this.lblBullet.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblBullet.SizeF = new System.Drawing.SizeF(21.875F, 35.875F);
        this.lblBullet.StylePriority.UseFont = false;
        this.lblBullet.StylePriority.UseTextAlignment = false;
        this.lblBullet.Text = "•";
        this.lblBullet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        // 
        // panelLifeItems
        // 
        this.panelLifeItems.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.panelLifeItems.CanGrow = false;
        this.panelLifeItems.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.panelLifeItems.Name = "panelLifeItems";
        this.panelLifeItems.SizeF = new System.Drawing.SizeF(240.625F, 152.4583F);
        // 
        // panelInfo
        // 
        this.panelInfo.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.panelInfo.BackColor = System.Drawing.Color.LightGray;
        this.panelInfo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
        | DevExpress.XtraPrinting.BorderSide.Right)
        | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.panelInfo.BorderWidth = 2.5F;
        this.panelInfo.CanGrow = false;
        this.panelInfo.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblLevelDescription,
            this.lblIntensity,
            this.lblBehaviourLevel,
            this.picLevel});
        this.panelInfo.LocationFloat = new DevExpress.Utils.PointFloat(250F, 0F);
        this.panelInfo.Name = "panelInfo";
        this.panelInfo.SizeF = new System.Drawing.SizeF(248.9583F, 152.4583F);
        this.panelInfo.StylePriority.UseBackColor = false;
        this.panelInfo.StylePriority.UseBorders = false;
        this.panelInfo.StylePriority.UseBorderWidth = false;
        // 
        // lblLevelDescription
        // 
        this.lblLevelDescription.BackColor = System.Drawing.Color.Transparent;
        this.lblLevelDescription.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblLevelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
        this.lblLevelDescription.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 56.00001F);
        this.lblLevelDescription.Name = "lblLevelDescription";
        this.lblLevelDescription.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblLevelDescription.SizeF = new System.Drawing.SizeF(228.9583F, 39.66667F);
        this.lblLevelDescription.StylePriority.UseBackColor = false;
        this.lblLevelDescription.StylePriority.UseBorders = false;
        this.lblLevelDescription.StylePriority.UseFont = false;
        this.lblLevelDescription.Text = "Perceived 0-30% euipped to manage life";
        // 
        // lblIntensity
        // 
        this.lblIntensity.BackColor = System.Drawing.Color.White;
        this.lblIntensity.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblIntensity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
        this.lblIntensity.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 33.00001F);
        this.lblIntensity.Name = "lblIntensity";
        this.lblIntensity.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblIntensity.SizeF = new System.Drawing.SizeF(228.9583F, 23F);
        this.lblIntensity.StylePriority.UseBackColor = false;
        this.lblIntensity.StylePriority.UseBorders = false;
        this.lblIntensity.StylePriority.UseFont = false;
        this.lblIntensity.Text = "Meltdown body";
        // 
        // lblBehaviourLevel
        // 
        this.lblBehaviourLevel.BackColor = System.Drawing.Color.Transparent;
        this.lblBehaviourLevel.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.lblBehaviourLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
        this.lblBehaviourLevel.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 10F);
        this.lblBehaviourLevel.Name = "lblBehaviourLevel";
        this.lblBehaviourLevel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblBehaviourLevel.SizeF = new System.Drawing.SizeF(228.9583F, 23F);
        this.lblBehaviourLevel.StylePriority.UseBackColor = false;
        this.lblBehaviourLevel.StylePriority.UseBorders = false;
        this.lblBehaviourLevel.StylePriority.UseFont = false;
        this.lblBehaviourLevel.Text = "Level 5 - Code Red";
        // 
        // picLevel
        // 
        this.picLevel.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
        this.picLevel.BackColor = System.Drawing.Color.Transparent;
        this.picLevel.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.picLevel.Image = ((System.Drawing.Image)(resources.GetObject("picLevel.Image")));
        this.picLevel.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleRight;
        this.picLevel.LocationFloat = new DevExpress.Utils.PointFloat(148.3332F, 98.79169F);
        this.picLevel.Name = "picLevel";
        this.picLevel.SizeF = new System.Drawing.SizeF(90.625F, 48.375F);
        this.picLevel.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
        this.picLevel.StylePriority.UseBackColor = false;
        this.picLevel.StylePriority.UseBorders = false;
        // 
        // lblAnxiety
        // 
        this.lblAnxiety.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
        this.lblAnxiety.Angle = 270F;
        this.lblAnxiety.CanGrow = false;
        this.lblAnxiety.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
        this.lblAnxiety.LocationFloat = new DevExpress.Utils.PointFloat(500F, 0F);
        this.lblAnxiety.Name = "lblAnxiety";
        this.lblAnxiety.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblAnxiety.SizeF = new System.Drawing.SizeF(14.54163F, 152.4583F);
        this.lblAnxiety.StylePriority.UseFont = false;
        this.lblAnxiety.StylePriority.UseTextAlignment = false;
        this.lblAnxiety.Text = "a little bit";
        this.lblAnxiety.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
        this.lblAnxiety.Visible = false;
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 7F;
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
        // BodyLifeSkillsChartItem
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
        this.Margins = new System.Drawing.Printing.Margins(31, 55, 7, 0);
        this.Version = "18.1";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void panelBodyItems_Draw(object sender, DrawEventArgs e)
    {

    }
}
