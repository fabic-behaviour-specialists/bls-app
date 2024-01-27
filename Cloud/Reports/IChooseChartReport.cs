using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BodyLifeSkillsPlatform.Data.Reports
{
    public partial class IChooseChartReport : DevExpress.XtraReports.UI.XtraReport
    {
        IChooseChartReportItem1 _Item1;
        IChooseChartReportItem2 _Item2;
        XRSubreport _Item1Subreport;

        public IChooseChartReport()
        {
            InitializeComponent();
        }

        public void AddChartItem1(IChooseChartReportItem1 item)
        {
            _Item1 = item;
            XRSubreport subreport = new XRSubreport();
            subreport.ReportSource = item;
            _Item1Subreport = subreport;
            this.Detail.Controls.Add(subreport);
        }

        public void AddChartItem2(IChooseChartReportItem2 item)
        {
            _Item2 = item;
            XRSubreport subreport = new XRSubreport();
            subreport.ReportSource = item;
            subreport.TopF = _Item1Subreport.HeightF;
            this.Detail.Controls.Add(subreport);
        }
    }
}
