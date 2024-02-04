using DevExpress.XtraReports.UI;
using System.Drawing;

/// <summary>
/// Summary description for BodyLifeSkillsChart
/// </summary>
public class BodyLifeSkillsChart : DevExpress.XtraReports.UI.XtraReport
{
    public DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    public XRLabel lblTitle;
    private PageFooterBand PageFooter;
    private XRPictureBox xrPictureBox1;
    private PageHeaderBand PageHeader;
    private XRPanel xrPanel2;
    public XRLabel lblStep1Description;
    public XRLabel lblStep1;
    private XRPanel xrPanel1;
    public XRLabel lblStep2;
    public XRLabel lblStep2Description;
    private float runningHeightTotal = 0f;
    private XRControlStyle xrControlStyle1;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public BodyLifeSkillsChart()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        lblTitle.ForeColor = Color.FromArgb(102, 45, 145);
        lblStep1.ForeColor = Color.FromArgb(0, 133, 205);
        lblStep2.ForeColor = Color.FromArgb(0, 133, 205);
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

    public float AddBodyLifeSkillsChartItem(BodyLifeSkillsChartItem item, int height)
    {
        XRSubreport subreport = new XRSubreport();
        subreport.ReportSource = item;
        subreport.TopF = height;
        this.Detail.Controls.Add(subreport);
        return subreport.HeightF;
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BodyLifeSkillsChart));
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.lblTitle = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.lblStep1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.lblStep1Description = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.lblStep2 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblStep2Description = new DevExpress.XtraReports.UI.XRLabel();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.HeightF = 102.0833F;
        this.Detail.KeepTogether = true;
        this.Detail.KeepTogetherWithDetailReports = true;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 25.79167F;
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
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTitle});
        this.ReportHeader.HeightF = 58.33333F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // lblTitle
        // 
        this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblTitle.ForeColor = System.Drawing.SystemColors.WindowText;
        this.lblTitle.LocationFloat = new DevExpress.Utils.PointFloat(12.00002F, 0F);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblTitle.SizeF = new System.Drawing.SizeF(738.9999F, 54.25001F);
        this.lblTitle.StylePriority.UseFont = false;
        this.lblTitle.StylePriority.UseForeColor = false;
        this.lblTitle.StylePriority.UseTextAlignment = false;
        this.lblTitle.Text = "Fabic Behaviour/Anxiety Scale";
        this.lblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
        this.PageFooter.HeightF = 106.3753F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.StyleName = "xrControlStyle1";
        this.PageFooter.AfterPrint += new System.EventHandler(this.PageFooter_AfterPrint);
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleLeft;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(750.9999F, 106.3753F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblStep1,
            this.xrPanel2,
            this.xrPanel1});
        this.PageHeader.HeightF = 77.08334F;
        this.PageHeader.Name = "PageHeader";
        // 
        // lblStep1
        // 
        this.lblStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
        this.lblStep1.ForeColor = System.Drawing.Color.Turquoise;
        this.lblStep1.LocationFloat = new DevExpress.Utils.PointFloat(510.9999F, 0F);
        this.lblStep1.Name = "lblStep1";
        this.lblStep1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblStep1.SizeF = new System.Drawing.SizeF(239.9999F, 31.25F);
        this.lblStep1.StylePriority.UseFont = false;
        this.lblStep1.StylePriority.UseForeColor = false;
        this.lblStep1.StylePriority.UseTextAlignment = false;
        this.lblStep1.Text = "STEP 1: BODY";
        this.lblStep1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrPanel2
        // 
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblStep1Description});
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(510.9999F, 0F);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.SizeF = new System.Drawing.SizeF(240F, 75F);
        // 
        // lblStep1Description
        // 
        this.lblStep1Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
        this.lblStep1Description.ForeColor = System.Drawing.Color.Gray;
        this.lblStep1Description.LocationFloat = new DevExpress.Utils.PointFloat(0.0001220703F, 39.66667F);
        this.lblStep1Description.Multiline = true;
        this.lblStep1Description.Name = "lblStep1Description";
        this.lblStep1Description.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblStep1Description.SizeF = new System.Drawing.SizeF(240F, 35.33333F);
        this.lblStep1Description.StylePriority.UseFont = false;
        this.lblStep1Description.StylePriority.UseForeColor = false;
        this.lblStep1Description.StylePriority.UseTextAlignment = false;
        this.lblStep1Description.Text = "What my body is doing, saying, \r\nthinking or feeling";
        this.lblStep1Description.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblStep2,
            this.lblStep2Description});
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(240F, 75F);
        // 
        // lblStep2
        // 
        this.lblStep2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
        this.lblStep2.ForeColor = System.Drawing.Color.Turquoise;
        this.lblStep2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.lblStep2.Name = "lblStep2";
        this.lblStep2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblStep2.SizeF = new System.Drawing.SizeF(240F, 27.16667F);
        this.lblStep2.StylePriority.UseFont = false;
        this.lblStep2.StylePriority.UseForeColor = false;
        this.lblStep2.StylePriority.UseTextAlignment = false;
        this.lblStep2.Text = "STEP 2: LIFE";
        this.lblStep2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // lblStep2Description
        // 
        this.lblStep2Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
        this.lblStep2Description.ForeColor = System.Drawing.Color.Gray;
        this.lblStep2Description.LocationFloat = new DevExpress.Utils.PointFloat(0F, 39.66667F);
        this.lblStep2Description.Multiline = true;
        this.lblStep2Description.Name = "lblStep2Description";
        this.lblStep2Description.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblStep2Description.SizeF = new System.Drawing.SizeF(240F, 35.33333F);
        this.lblStep2Description.StylePriority.UseFont = false;
        this.lblStep2Description.StylePriority.UseForeColor = false;
        this.lblStep2Description.StylePriority.UseTextAlignment = false;
        this.lblStep2Description.Text = "S-I-T-A\r\nSetting-Interaction-Task-Automatic";
        this.lblStep2Description.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        // 
        // BodyLifeSkillsChart
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageFooter,
            this.PageHeader});
        this.Margins = new System.Drawing.Printing.Margins(32, 57, 26, 0);
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.Version = "18.1";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void PageFooter_AfterPrint(object sender, System.EventArgs e)
    {

    }
}
