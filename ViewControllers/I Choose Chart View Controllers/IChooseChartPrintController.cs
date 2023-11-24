using Fabic.Core.Controllers;
using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Fabic.Data.Extensions;
using Fabic.iOS.Controllers;
using Fabic.iOS.ViewControllers.TableViewSources;
using Foundation;
using QuickLook;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UIKit;

namespace Fabic.iOS
{
    public partial class IChooseChartPrintController : UIViewController
    {
        private IChooseChartPrintControllerViewSource viewSource;

        public IChooseChart Chart
        {
            get; set;
        }

        public List<IChooseChartItem> IChooseChartItemsToPrint
        {
            get;
            set;
        }

        public Fabic.Core.Enumerations.IChooseChartOption Option
        {
            get; set;
        }

        public IChooseChartPrintController(IntPtr handle) : base(handle)
        {
            IChooseChartItemsToPrint = new List<IChooseChartItem>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ApplyLightInterface();
            // Perform any additional setup after loading the view, typically from a nib.
            tblMain.Source = viewSource = new IChooseChartPrintControllerViewSource(Chart, Option);
            tblMain.Editing = false;
            tblMain.AllowsMultipleSelection = false;
            barBtnCancel.Clicked += BarBtnCancel_Clicked;
            barBtnDone.Clicked += BarBtnDone_Clicked;
            if (Option == Core.Enumerations.IChooseChartOption.Option2)
            {
                barBtnDone = new UIBarButtonItem("Next", UIBarButtonItemStyle.Done, BarBtnDone_Clicked);
                this.NavigationItem.RightBarButtonItem = barBtnDone;
            }
            else
            {
                barBtnCancel = new UIBarButtonItem("Back", UIBarButtonItemStyle.Plain, BarBtnCancel_Clicked);
                this.NavigationItem.LeftBarButtonItem = barBtnCancel;

                barBtnDone = new UIBarButtonItem("Print", UIBarButtonItemStyle.Done, BarBtnDone_Clicked);
                this.NavigationItem.RightBarButtonItem = barBtnDone;
            }
        }

        private async void BarBtnDone_Clicked(object sender, EventArgs e)
        {
            if (Option == Core.Enumerations.IChooseChartOption.Option2)
            {
                IChooseChartPrintController printController = (IChooseChartPrintController)UIStoryboard.FromName("Main", null).InstantiateViewController("printIChooseChartVC");
                printController.Chart = Chart;
                printController.Option = Core.Enumerations.IChooseChartOption.Option1;
                printController.IChooseChartItemsToPrint = viewSource.SelectedItems.Values.ToList();

                this.NavigationController.PushViewController(printController, true);
            }
            else
            {
                foreach (IChooseChartItem item in viewSource.SelectedItems.Values)
                {
                    if (!this.IChooseChartItemsToPrint.Contains(item))
                        this.IChooseChartItemsToPrint.Add(item);
                }

                BigTed.BTProgressHUD.ShowContinuousProgress("Printing...", BigTed.ProgressHUD.MaskType.None);
                byte[] result = null;
                Task.Run(() =>
                {
                    result = printBehaviourScale();
                    UIApplication.SharedApplication.InvokeOnMainThread(delegate
                    {
                        BigTed.BTProgressHUD.Dismiss();
                        if (result != null)
                        {
                            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            var filePath = Path.Combine(documentsPath, DateTime.Now.ToFileTime().ToString() + ".pdf");
                            File.WriteAllBytes(filePath, result);
                            QLPreviewController previewController = new QLPreviewController();
                            previewController.DataSource = new PDFPreviewControllerDataSource(new QLItem("I Choose Chart Export", NSUrl.FromFilename(filePath)));
                            previewController.DidDismiss += PreviewController_DidDismiss;
                            this.PresentViewController(previewController, true, null);
                        }
                        else
                        {
                            string err = error;
                            if (err.Length <= 0)
                                err = "Opps! Something went wrong and we could not print your I Choose Chart for you. Please try again later";
                            UIAlertController alert = UIAlertController.Create("Unable to Print I Choose Chart", err, UIAlertControllerStyle.Alert);
                            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (UIAlertAction action) => { alert.DismissViewControllerAsync(true); }));
                            this.PresentModalViewController(alert, true);
                        }
                    });
                });
            }
        }

        private void PreviewController_DidDismiss(object sender, EventArgs e)
        {
            this.DismissViewController(true, () => { });
        }

        string error = string.Empty;
        private byte[] printBehaviourScale()
        {
            error = string.Empty;
            try
            {
                //   Chart.Items = IChooseChartItemsToPrint.Select(x => x).ToList();
                IChooseChartReport chartReport = new IChooseChartReport();
                chartReport.Archived = Chart.Archived;
                chartReport.Description1 = Chart.Description1;
                chartReport.Description2 = Chart.Description2;
                chartReport.Id = Chart.Id;
                chartReport.Name = Chart.Name;
                chartReport.Keywords1 = new List<ItemHighlight>();
                chartReport.Keywords2 = new List<ItemHighlight>();
                chartReport.Items = new List<IChooseChartItemReport>();

                foreach (IChooseChartItem icci in IChooseChartItemsToPrint)
                {
                    IChooseChartItemReport item = new IChooseChartItemReport();
                    item.ChartOption = icci.ChartOption;
                    item.ChartType = icci.ChartType;
                    item.IChooseChart = icci.IChooseChart;
                    item.Id = icci.Id;
                    item.ItemText = icci.ItemText;
                    item.UserID = icci.UserID;
                    chartReport.Items.Add(item);
                }

                if (Fabic.iOS.Controllers.SecurityController.AccessTokenExpiry < DateTime.Now)
                    Fabic.iOS.Controllers.SecurityController.RefreshAccessToken();

                var client = new RestClient(FabicDatabaseController.MOBILE_APP_URL + "api/ichoosechartreport");
                var request = new RestRequest();
                request.AddHeader("access_token", SecurityController.AccessToken);
                request.AddHeader("userId", SecurityController.CurrentUser.UserID);
                request.AddHeader("ZUMO-API-VERSION", "2.0.0");
                request.AddHeader("id", chartReport.Id);
                request.AddJsonBody(chartReport);
                request.Method = Method.Post;

                byte[] response = client.DownloadData(request);

                if (response != null)
                    return response;
                else
                {
                    Exception exception = new Exception();
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                error = ex.Message;
            }
            return null;
        }

        private void BarBtnCancel_Clicked(object sender, EventArgs e)
        {
            if (Option == Core.Enumerations.IChooseChartOption.Option2)
                this.DismissViewController(true, null);
            else
                this.NavigationController.PopViewController(true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.View.Constraints[14].Constant = Convert.ToInt32(UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height - (UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height / 2 + 260));
            this.NavigationController.NavigationBar.BackgroundColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Blue);
            this.NavigationController.NavigationBar.BarTintColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Blue);
            this.NavigationController.NavigationBar.TintColor = UIColor.White;
            UIColor white = UIColor.White;

            UIStringAttributes myTextAttrib = new UIStringAttributes() { StrokeColor = white, ForegroundColor = white, Font = UIFont.FromName("Avenir-Heavy", 20) };
            this.NavigationController.NavigationBar.TitleTextAttributes = myTextAttrib;
            if (Option == Core.Enumerations.IChooseChartOption.Option1)
                this.Title = "Option 1 Items to Print";
            else
                this.Title = "Option 2 Items to Print";
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            foreach (var item in tblMain.VisibleCells)
            {
                item.Selected = true;
            }
        }

        public void CleanUp()
        {

        }
    }
}