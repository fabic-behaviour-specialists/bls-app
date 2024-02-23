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
    public partial class BehaviourScalePrintController : UIViewController, IDisposable, ICanCleanUpMyself
    {
        private BehaviourScalePrintControllerViewSource viewSource;

        public BehaviourScale BehaviourScale
        {
            get; set;
        }

        public List<BehaviourScaleItem> BehaviourScaleItemsToPrint
        {
            get;
            set;
        }

        public Fabic.Core.Enumerations.BehaviourScaleItemType Type
        {
            get; set;
        }

        public BehaviourScalePrintController(IntPtr handle) : base(handle)
        {
            BehaviourScaleItemsToPrint = new List<BehaviourScaleItem>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ApplyLightInterface();
            // Perform any additional setup after loading the view, typically from a nib.
            tblMain.Source = viewSource = new BehaviourScalePrintControllerViewSource(BehaviourScale, Type);
            tblMain.Editing = false;
            barBtnCancel.Clicked += BarBtnCancel_Clicked;
            barBtnDone.Clicked += BarBtnDone_Clicked;
            if (Type == Core.Enumerations.BehaviourScaleItemType.Body)
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
            if (Type == Core.Enumerations.BehaviourScaleItemType.Body)
            {
                BehaviourScalePrintController printController = (BehaviourScalePrintController)UIStoryboard.FromName("Main", null).InstantiateViewController("printBehaviourScaleVC");
                printController.BehaviourScale = BehaviourScale;
                printController.Type = Core.Enumerations.BehaviourScaleItemType.Life;
                printController.BehaviourScaleItemsToPrint = viewSource.SelectedItems.Values.ToList();

                this.NavigationController.PushViewController(printController, true);
            }
            else
            {
                foreach (BehaviourScaleItem item in viewSource.SelectedItems.Values)
                {
                    if (!this.BehaviourScaleItemsToPrint.Contains(item))
                        this.BehaviourScaleItemsToPrint.Add(item);
                }
                
                BigTed.BTProgressHUD.ShowContinuousProgress("Printing...", BigTed.MaskType.None);
                byte[] result = await printBehaviourScale();

                UIApplication.SharedApplication.InvokeOnMainThread(delegate
                {

                    BigTed.BTProgressHUD.Dismiss();
                    if (result != null)
                    {
                        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        var filePath = Path.Combine(documentsPath, DateTime.Now.ToFileTime().ToString() + ".pdf");
                        File.WriteAllBytes(filePath, result);
                        QLPreviewController previewController = new QLPreviewController
                        {
                            DataSource = new PDFPreviewControllerDataSource(new QLItem("Behaviour Scale Export", NSUrl.FromFilename(filePath)))
                        };
                        previewController.DidDismiss += PreviewController_DidDismiss;
                        this.PresentViewController(previewController, true, null);
                    }
                    else
                    {
                        string err = error;
                        if (err.Length <= 0)
                            err = "Oops! Something went wrong and we could not print your behaviour scale for you. Please try again later";
                        UIAlertController alert = UIAlertController.Create("Unable to Print Behaviour Scale", err, UIAlertControllerStyle.Alert);
                        alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (UIAlertAction action) => { alert.DismissViewControllerAsync(true); }));
                        this.PresentModalViewController(alert, true);
                    }
                });
            }
        }

        private void PreviewController_DidDismiss(object sender, EventArgs e)
        {
            this.DismissViewController(true, () => { });
        }

        string error = string.Empty;
        private async Task<byte[]> printBehaviourScale()
        {
            error = string.Empty;
            try
            {
                BehaviourScaleReport reportBS = new BehaviourScaleReport();
                reportBS.Archived = BehaviourScale.Archived;
                reportBS.CreatedAt = BehaviourScale.CreatedAt;
                reportBS.Description = BehaviourScale.Description;
                reportBS.FabicExample = BehaviourScale.FabicExample;
                reportBS.Id = BehaviourScale.Id;
                reportBS.Name = BehaviourScale.Name;
                reportBS.UserID = BehaviourScale.UserID;
                reportBS.Items = BehaviourScaleItemsToPrint.Select(x => x).ToList();
                if (Fabic.iOS.Controllers.SecurityController.AccessTokenExpiry < DateTime.Now)
                    Fabic.iOS.Controllers.SecurityController.RefreshAccessToken();

                var client = new RestClient(FabicDatabaseController.MOBILE_APP_URL + "behaviourscale/report");
                var request = new RestRequest();
                request.AddHeader("access_token", SecurityController.AccessToken);
                request.AddHeader("userId", SecurityController.CurrentUser.UserID);
                request.AddHeader("X-API-KEY", FabicDatabaseController.APIKey);
                request.AddHeader("id", reportBS.Id);
                request.AddJsonBody(reportBS);
                request.Method = Method.Get;

                var result = await client.GetAsync(request);
                string responseString = result.Content.Replace("\"", "");

                byte[] response = Convert.FromBase64String(responseString);

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
            if (Type == Core.Enumerations.BehaviourScaleItemType.Body)
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
            this.NavigationController.NavigationBar.BackgroundColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Purple);
            this.NavigationController.NavigationBar.BarTintColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Purple);
            this.NavigationController.NavigationBar.TintColor = UIColor.White;
            UIColor white = UIColor.White;

            UIStringAttributes myTextAttrib = new UIStringAttributes() { StrokeColor = white, ForegroundColor = white, Font = UIFont.FromName("Avenir-Heavy", 20) };
            this.NavigationController.NavigationBar.TitleTextAttributes = myTextAttrib;
            if (Type == Core.Enumerations.BehaviourScaleItemType.Body)
                this.Title = "Body Items to Print";
            else
                this.Title = "Life Items to Print";
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

    public class PDFPreviewControllerDataSource : QLPreviewControllerDataSource
    {
        private IQLPreviewItem _Item;
        public PDFPreviewControllerDataSource(QLPreviewItem previewItem)
        {
            _Item = previewItem;
        }

        public override nint PreviewItemCount(QLPreviewController controller)
        {
            return 1;
        }

        public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
        {
            return _Item;
        }
    }
    public class QLItem : QLPreviewItem
    {
        string title;
        NSUrl uri;

        public QLItem(string title, NSUrl uri)
        {
            this.title = title;
            this.uri = uri;
        }

        public override string ItemTitle
        {
            get { return title; }
        }

        public override NSUrl ItemUrl
        {
            get { return uri; }
        }
    }

}