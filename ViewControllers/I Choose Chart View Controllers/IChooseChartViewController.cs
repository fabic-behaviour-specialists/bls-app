using CoreGraphics;
using Curse;
using Fabic.Core.Controllers;
using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Fabic.Data.Extensions;
using Fabic.iOS.Helpers;
using Fabic.iOS.ViewControllers.TableViewSources;
using Foundation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;

namespace Fabic.iOS
{
    public partial class IChooseChartViewController : UIViewController, IDisposable, ICanCleanUpMyself
    {
        ArchivedOverlay archived;
        IChooseChartViewSource source;
        UITapGestureRecognizer tableTapRecogniser;

        bool selectedOption2 = true;

        public IChooseChartViewController(IntPtr handle) : base(handle)
        {
        }

        public IChooseChart Chart
        {
            get; set;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ApplyLightInterface();

            // Perform any additional setup after loading the view, typically from a nib.
            if (this.NavigationController != null)
            {
                this.View.Constraints[15].Constant = this.NavigationController.NavigationBar.Frame.Height - 30;
                this.View.Constraints[8].Constant = this.NavigationController.NavigationBar.Frame.Height - 20;

                this.NavigationController.Toolbar.BarTintColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Blue);
                this.NavigationController.Toolbar.BackgroundColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Blue);
            }
            else
            {
                this.View.Constraints[15].Constant = 0;
                this.View.Constraints[8].Constant = 0;
            }

            nfloat originalTitleHeight = txtTitle.Frame.Height;

            txtTitle.Text = Chart.Name;
            txtTitle.WithLines = false;
            txtTitle.TextChanged += TxtTitle_TextChanged;
            txtTitle.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple);

            txtTitle.SizeToFit();
            layoutTxtTitleICC.Constant = txtTitle.Frame.Height + 20;
            headerView.Frame = new CGRect(headerView.Frame.X, headerView.Frame.Y - originalTitleHeight + txtTitle.Frame.Height, headerView.Frame.Width, headerView.Frame.Height);
            mainTableView.Frame = new CGRect(mainTableView.Frame.X, mainTableView.Frame.Y - originalTitleHeight + txtTitle.Frame.Height, mainTableView.Frame.Width, mainTableView.Frame.Height);

            CoreGraphics.CGRect frame;
            if (this.NavigationController != null)
            {
                frame = this.NavigationController.NavigationBar.Frame;
            }
            else
            {
                frame = new CGRect(0, 0, 0, 0);
            }

            mainTableView.BackgroundColor = UIColor.Clear;
            source = new IChooseChartViewSource(Chart, this.View, frame);
            mainTableView.Source = source;
            source.UserDidSelectIChooseChartLevelToViewEdit += Source_UserDidSelectIChooseChartLevelToViewEdit;
            mainTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

            tableTapRecogniser = new UITapGestureRecognizer(() =>
            {
                if (tableTapRecogniser.State == UIGestureRecognizerState.Ended)
                {
                    var location = tableTapRecogniser.LocationInView(mainTableView);
                    var index = mainTableView.IndexPathForRowAtPoint(location);
                    selectedOption2 = !(location.X <= this.View.Frame.Width / 2);
                    source.RowSelected(mainTableView, index);
                }
            });
            //  tableTapRecogniser.Delegate = this;
            mainTableView.AddGestureRecognizer(tableTapRecogniser);

            Title = "I Choose Chart";

            double width = ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[0].View.Frame.Width / 2;

            UILabel bodyHeader = new UILabel();
            bodyHeader.Frame = new CoreGraphics.CGRect(width + (width / 2) - 70, 5, 140, 30);
            bodyHeader.Font = UIFont.BoldSystemFontOfSize(16);
            bodyHeader.BackgroundColor = UIColor.Clear;
            bodyHeader.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue);
            bodyHeader.TextAlignment = UITextAlignment.Center;
            bodyHeader.Text = "Option 2";
            headerView.AddSubview(bodyHeader);

            UIImageView thumbsDown = new UIImageView();
            thumbsDown.Image = UIImage.FromBundle("ThumbsDown");
            thumbsDown.Frame = new CGRect(width + (width / 2) + 40, 0, 30, 30);
            headerView.AddSubview(thumbsDown);

            UILabel lifeHeader = new UILabel();
            lifeHeader.Frame = new CoreGraphics.CGRect((width / 2) - 70, 5, 140, 30);
            lifeHeader.Font = UIFont.BoldSystemFontOfSize(16);
            lifeHeader.BackgroundColor = UIColor.Clear;
            lifeHeader.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue);
            lifeHeader.TextAlignment = UITextAlignment.Center;
            lifeHeader.Text = "Option 1";
            headerView.AddSubview(lifeHeader);

            UIImageView thumbsUp = new UIImageView();
            thumbsUp.Image = UIImage.FromBundle("ThumbsUp");
            thumbsUp.Frame = new CGRect((width / 2) + 40, 0
                                        , 30, 30);
            headerView.AddSubview(thumbsUp);
            headerView.BackgroundColor = UIColor.Clear;

            UIImage exportBtn = UIImage.FromBundle("Export-Blue");
            exportBtn = exportBtn.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            UIBarButtonItem shareButton = new UIBarButtonItem(exportBtn, UIBarButtonItemStyle.Bordered, ShareHandleEventHandler);
            List<UIBarButtonItem> items = new List<UIBarButtonItem>();
            items.Add(shareButton);

            UIBarButtonItem space = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            items.Add(space);

            // put the overlay over the scale if it has been archive
            archived = new ArchivedOverlay(new CoreGraphics.CGRect(0, this.View.Frame.Height - 180, this.View.Frame.Width, 180), Data.Enums.FabicColour.Blue);
            archived.UndoButtonPressed += Archived_UndoButtonPressed; ;

            if (this.Chart.Archived)
            {
                txtTitle.Editable = txtTitle.Selectable = false;
                if (this.NavigationController != null)
                    this.NavigationController.SetToolbarHidden(true, false);
                archived.Show(this.View);
            }
            else if (this.Chart.FabicExample)
            {
                archived.Frame = new CoreGraphics.CGRect(0, this.View.Frame.Height - 150, this.View.Frame.Width, 100);
                txtTitle.Editable = txtTitle.Selectable = false;
                if (this.NavigationController != null)
                    this.NavigationController.SetToolbarHidden(true, false);
                archived.Show(this.View, "This is an example only", false);
                layoutTableBottom.Constant = -65;
            }
            else
            {
                UIImage helpBtn = UIImage.FromBundle("Help-Blue");
                helpBtn = helpBtn.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                UIBarButtonItem help = new UIBarButtonItem(helpBtn, UIBarButtonItemStyle.Bordered, HelpHandleEventHandler);
                this.NavigationItem.RightBarButtonItem = help;
            }

            UIImage trashBtn = UIImage.FromBundle("Trash-Blue");
            trashBtn = trashBtn.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            UIBarButtonItem trash = new UIBarButtonItem(trashBtn, UIBarButtonItemStyle.Bordered, TrashHandleEventHandler);
            items.Add(trash);

            ToolbarItems = items.ToArray();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (this.NavigationController != null)
            {
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(false, true);

                if (!Chart.Archived && !Chart.FabicExample)
                    ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetToolbarHidden(false, true);
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (Controllers.SecurityController.FirstTimeUsingIChooseChart && !this.Chart.FabicExample && !this.Chart.Archived)
            {
                HelpHandleEventHandler(this, null);
                Controllers.SecurityController.FirstTimeUsingIChooseChart = false;
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            if (this.NavigationController != null)
            {
                //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(true, false);
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetToolbarHidden(true, true);
            }
        }

        void TxtTitle_TextChanged(object sender, EventArgs e)
        {
            this.Chart.Name = txtTitle.Text;
            FabicDatabaseController.SaveOrUpdateIChooseChart(Chart);
        }

        private void ShareHandleEventHandler(object sender, EventArgs e)
        {
            BigTed.BTProgressHUD.ShowContinuousProgress("Saving...", BigTed.ProgressHUD.MaskType.None);
            Task.Run(() =>
            {
                try
                {
                    FabicDatabaseController.SaveOrUpdateIChooseChart(Chart, false, true);
                }
                catch (Exception ex)
                {
                    UIApplication.SharedApplication.InvokeOnMainThread(delegate
                    {
                        BigTed.BTProgressHUD.Dismiss();
                        string err = "";
                        if (err.Length <= 0)
                            err = "Opps! Something went wrong and we could not save your I Choose Chart for you. Please try again later";
                        UIAlertController alert = UIAlertController.Create("Unable to Print I Choose Chart", err, UIAlertControllerStyle.Alert);
                        alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (UIAlertAction action) => { alert.DismissViewControllerAsync(true); }));
                        this.PresentModalViewController(alert, true);
                    });
                }
                UIApplication.SharedApplication.InvokeOnMainThread(delegate
                {
                    BigTed.BTProgressHUD.Dismiss();
                    IChooseChartPrintController printController = (IChooseChartPrintController)UIStoryboard.FromName("Main", null).InstantiateViewController("printIChooseChartVC");
                    printController.Chart = Chart;
                    printController.Option = Core.Enumerations.IChooseChartOption.Option2;
                    UINavigationController navigationController = new UINavigationController(printController);
                    PresentViewController(navigationController, true, null);
                });
            }
              );
        }

        private void HelpHandleEventHandler(object sender, EventArgs e)
        {
            IChooseChartTutorialViewController view = (IChooseChartTutorialViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("iChooseChartTutorialView");
            view.ProvidesPresentationContextTransitionStyle = true;
            view.DefinesPresentationContext = true;
            view.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            view.NavigationBarFrame = this.NavigationController.NavigationBar.Frame;
            view.ToolBarFrame = this.NavigationController.Toolbar.Frame;
            this.PresentViewController(view, true, null);
        }

        private void TrashHandleEventHandler(object sender, EventArgs e)
        {
            CRSAlertView alert = new CRSAlertView();
            alert.TintColor = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Purple);
            alert.Title = "Are you sure you want to archive this chart";
            alert.Message = "You cannot edit or view this I Choose Chart when it is archived";
            alert.Image = new UIImage("fabic-logo.png");

            var action = new CRSAlertAction
            {
                Text = "No",
                Highlighted = false,
                TintColor = UIColor.Black,
                DidSelect = (alert2) =>
                {
                    // Do something here on pres
                }
            };

            var action2 = new CRSAlertAction
            {
                Text = "Yes",
                Highlighted = true,
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Blue),
                DidSelect = DeleteHandler
            };

            alert.Actions = new CRSAlertAction[] { action, action2 };
            alert.Show();
        }

        private async void Archived_UndoButtonPressed(object sender, EventArgs e)
        {
            // has been unarchived - update item and displ
            Chart.Archived = false;
            txtTitle.Editable = true;
            FabicDatabaseController.SaveOrUpdateIChooseChart(Chart);

            if (archived != null)
            {
                archived.Hide();
                this.NavigationController.SetToolbarHidden(false, true);
            }
        }

        async void DeleteHandler(CRSAlertView e)
        {
            // has been archived - update item and displa
            Chart.Archived = true;
            txtTitle.Editable = false;
            FabicDatabaseController.SaveOrUpdateIChooseChart(Chart);

            if (archived != null)
            {
                this.NavigationController.SetToolbarHidden(true, true);
                archived.Show(this.View);
            }
        }

        private void Source_UserDidSelectIChooseChartLevelToViewEdit(object sender, ViewControllers.TableViewSources.UserDidSelectIChooseChartLevelEventArgs args)
        {
            IChooseChartEditItemViewController view = (IChooseChartEditItemViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("iChooseChartEditItemViewController");
            view.ProvidesPresentationContextTransitionStyle = true;
            view.DefinesPresentationContext = true;
            view.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            view.Chart = args.Chart;
            view.ChartItems = args.ScaleItems;
            view.ChartType = args.BehaviourScaleLevel;
            view.UserDidEditIChooseChart += View_UserDidEditIChooseChart;
            view.Option2Selected = selectedOption2;
            this.PresentViewController(view, true, null);
        }

        void View_UserDidEditIChooseChart(object sender, EventArgs args)
        {
            source.Cells = null;
            mainTableView.ReloadData();
        }

        /// <summary>
        /// Prints the chart.
        /// </summary>
        public NSUrl PrintIChooseChart()
        {
            string fileName = "IChooseChartExport-" + NSDate.Now.ToString() + ".pdf";
            string path = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.All, true)[0] + "/" + fileName;

            // first load the views...
            CGRect frame = new CGRect(0, -80, this.View.Frame.Width, this.View.Frame.Height);

            // header
            CGRect titleFrame = new CGRect(0, -30, this.txtTitle.Frame.Width, this.txtTitle.Frame.Height);
            UIImage titleImage = txtTitle.ExportUIViewAsImage();
            CGRect headerFrame = new CGRect(0, titleFrame.Height - 30, this.headerView.Frame.Width, this.headerView.Frame.Height);
            UIImage headerImage = headerView.ExportUIViewAsImage();

            // cells
            Dictionary<int, FabicIChooseChartCell> cells = source.Cells;

            // footer
            CGRect footerFrame = new CGRect(0, this.View.Frame.Height - 119, this.View.Frame.Width, 55);
            UIImage footerImage = new UIImage("fabic-footer-export");
            UIImageView footerView = new UIImageView(footerImage); footerView.Frame = footerFrame;

            // begin drawing
            nfloat currentHeight = 0;
            UIGraphics.BeginPDFContext(path, this.View.Frame, new Foundation.NSDictionary());
            UIGraphics.BeginPDFPage(this.View.Frame, new Foundation.NSDictionary());
            titleImage.Draw(titleFrame);
            headerImage.Draw(headerFrame);
            footerImage.Draw(footerFrame);
            currentHeight += headerFrame.Height;
            currentHeight += titleFrame.Height - 30;

            for (int i = 0; i < cells.Count; i++)
            {
                CGRect cellFrame = new CGRect(0, currentHeight, cells[i].Frame.Width, cells[i].Frame.Height);
                UIImage cellImage = cells[i].ExportUIViewAsImage();

                if (currentHeight + cellFrame.Height > frame.Height - footerFrame.Height - 30)
                {
                    UIGraphics.BeginPDFPage(this.View.Frame, new Foundation.NSDictionary());
                    currentHeight = 0;
                    titleImage.Draw(titleFrame);
                    headerImage.Draw(headerFrame);
                    footerImage.Draw(footerFrame);
                    currentHeight += headerFrame.Height;
                    currentHeight += titleFrame.Height - 30;
                    cellFrame = new CGRect(0, currentHeight, cells[i].Frame.Width, cells[i].Frame.Height);
                }

                cellImage.Draw(cellFrame);
                currentHeight += cellFrame.Height;
            }

            UIGraphics.EndPDFContent();
            NSData data = NSData.FromFile(path);
            NSError err;
            data.Save(path, NSDataWritingOptions.Atomic, out err);
            return NSUrl.FromFilename(path);
        }

        public void CleanUp()
        {

        }
    }
}