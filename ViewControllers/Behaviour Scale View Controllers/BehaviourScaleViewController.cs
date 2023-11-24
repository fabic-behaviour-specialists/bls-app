using CoreGraphics;
using Curse;
using Fabic.Core.Controllers;
using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Fabic.Data.Extensions;
using Fabic.iOS.Helpers;
using Fabic.iOS.UIControls;
using Fabic.iOS.ViewControllers.TableViewSources;
using Foundation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;

namespace Fabic.iOS
{
    public partial class BehaviourScaleViewController : UIViewController, IDisposable, ICanCleanUpMyself
    {
        ArchivedOverlay archived;
        BehaviourScaleViewSource source;
        UITapGestureRecognizer tableTapRecogniser;
        FabicButterflyAnimationLayer animationLayer;

        bool selectedBody = true;

        public BehaviourScaleViewController(IntPtr handle) : base(handle)
        {
        }

        public BehaviourScale BehaviourScale
        {
            get; set;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            //if ((NavigationController == null && IsMovingFromParentViewController) || (ParentViewController != null && ParentViewController.IsBeingDismissed))
            {
                //MemoryUtility.ReleaseUIViewWithChildren(this.View, true);
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.ApplyLightInterface();
            // Perform any additional setup after loading the view, typically from a nib.
            CoreGraphics.CGRect frame;
            if (this.NavigationController == null)
            {
                frame = new CoreGraphics.CGRect(0, 0, 0, 0);
            }
            else
            {
                frame = this.NavigationController.NavigationBar.Frame;
            }
            source = new BehaviourScaleViewSource(BehaviourScale, this.View, frame);
            mainTableView.BackgroundColor = UIColor.Clear;
            mainTableView.Source = source;
            mainTableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            mainTableView.SeparatorColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Purple).ColorWithAlpha(0.5f);
            source.UserDidSelectBehaviourLevelToViewEdit += Source_UserDidSelectBehaviourLevelToViewEdit;

            tableTapRecogniser = new UITapGestureRecognizer(() =>
            {
                if (tableTapRecogniser.State == UIGestureRecognizerState.Ended)
                {
                    var location = tableTapRecogniser.LocationInView(mainTableView);
                    var index = mainTableView.IndexPathForRowAtPoint(location);
                    selectedBody = !(location.X <= this.View.Frame.Width / 2);
                    source.RowSelected(mainTableView, index);
                }
            });
            //  tableTapRecogniser.Delegate = this;
            mainTableView.AddGestureRecognizer(tableTapRecogniser);

            if (this.NavigationController != null)
            {
                this.View.Constraints[15].Constant = this.NavigationController.NavigationBar.Frame.Height - 40;
                this.View.Constraints[8].Constant = this.NavigationController.NavigationBar.Frame.Height - 20;

                this.NavigationController.Toolbar.BarTintColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Purple);
                this.NavigationController.Toolbar.BackgroundColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Purple);
            }
            else
            {
                this.View.Constraints[15].Constant = 0;
                this.View.Constraints[8].Constant = 0;
            }

            nfloat originalTitleHeight = txtTitle.Frame.Height;
            if (BehaviourScale != null)
            {
                txtTitle.Text = BehaviourScale.Name;
            }

            txtTitle.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple);
            txtTitle.TextChanged += TxtTitle_TextChanged;
            txtTitle.WithLines = false;
            Title = "Behaviour Scale";
            txtTitle.SizeToFit();

            layoutTxtTitle.Constant = txtTitle.Frame.Height + 20;
            headerView.Frame = new CGRect(headerView.Frame.X, headerView.Frame.Y - originalTitleHeight + txtTitle.Frame.Height, headerView.Frame.Width, headerView.Frame.Height);
            mainTableView.Frame = new CGRect(mainTableView.Frame.X, mainTableView.Frame.Y - originalTitleHeight + txtTitle.Frame.Height, mainTableView.Frame.Width, mainTableView.Frame.Height);

            double width = ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[0].View.Frame.Width / 3;

            UILabel bodyHeader = new UILabel();
            bodyHeader.Frame = new CoreGraphics.CGRect((width * 2) + 7, 4, width - 12, 20);
            bodyHeader.Font = UIFont.BoldSystemFontOfSize(14);
            bodyHeader.BackgroundColor = UIColor.Clear;
            bodyHeader.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue);
            bodyHeader.TextAlignment = UITextAlignment.Center;
            bodyHeader.Text = "STEP 1: BODY";
            headerView.AddSubview(bodyHeader);

            UILabel bodySubHeader = new UILabel();
            bodySubHeader.Frame = new CoreGraphics.CGRect((width * 2) + 6, 18, width - 8, 45);
            bodySubHeader.Font = UIFont.SystemFontOfSize(8);
            bodySubHeader.BackgroundColor = UIColor.Clear;
            bodySubHeader.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Gray);
            bodySubHeader.TextAlignment = UITextAlignment.Center;
            bodySubHeader.Text = "What my body is doing, saying, thinking or feeling";
            bodySubHeader.Lines = 3;
            headerView.AddSubview(bodySubHeader);

            UILabel lifeHeader = new UILabel();
            lifeHeader.Frame = new CoreGraphics.CGRect(7, 4, width - 12, 20);
            lifeHeader.Font = UIFont.BoldSystemFontOfSize(14);
            lifeHeader.BackgroundColor = UIColor.Clear;
            lifeHeader.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue);
            lifeHeader.TextAlignment = UITextAlignment.Center;
            lifeHeader.Text = "STEP 2: LIFE";
            headerView.AddSubview(lifeHeader);

            UILabel lifeSubHeader = new UILabel();
            lifeSubHeader.Frame = new CoreGraphics.CGRect(7, 18, width - 8, 45);
            lifeSubHeader.Font = UIFont.SystemFontOfSize(8);
            lifeSubHeader.BackgroundColor = UIColor.Clear;
            lifeSubHeader.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Gray);
            lifeSubHeader.TextAlignment = UITextAlignment.Center;
            lifeSubHeader.Text = "S-I-T-A\rSetting - Interaction - Task - Automatic";
            lifeSubHeader.Lines = 3;
            headerView.AddSubview(lifeSubHeader);

            UIImage exportImg = UIImage.FromBundle("Export");
            exportImg = exportImg.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            UIBarButtonItem shareButton = new UIBarButtonItem(exportImg, UIBarButtonItemStyle.Bordered, ShareEventHandler);
            List<UIBarButtonItem> items = new List<UIBarButtonItem>();
            items.Add(shareButton);

            UIBarButtonItem space = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            items.Add(space);

            // put the overlay over the scale if it has been archive
            archived = new ArchivedOverlay(new CoreGraphics.CGRect(0, this.View.Frame.Height - 180, this.View.Frame.Width, 180));
            archived.UndoButtonPressed += Archived_UndoButtonPressed;

            if (this.BehaviourScale.Archived)
            {
                txtTitle.Editable = txtTitle.Selectable = false;
                if (this.NavigationController != null)
                    this.NavigationController.SetToolbarHidden(true, false);
                archived.Show(this.View);
            }
            else if (this.BehaviourScale.FabicExample)
            {
                archived.Frame = new CoreGraphics.CGRect(0, this.View.Frame.Height - 150, this.View.Frame.Width, 100);
                txtTitle.Editable = txtTitle.Selectable = false;
                if (this.NavigationController != null)
                    this.NavigationController.SetToolbarHidden(true, false);
                archived.Show(this.View, "This is an example only", false);
                layoutTableViewBottom.Constant = -50;
            }
            else
            {
                UIImage helpImg = UIImage.FromBundle("Help");
                helpImg = helpImg.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                UIBarButtonItem help = new UIBarButtonItem(helpImg, UIBarButtonItemStyle.Bordered, HelpButtonEventHandler);
                this.NavigationItem.RightBarButtonItem = help;
            }
            UIImage trashImg = UIImage.FromBundle("Trash");
            trashImg = trashImg.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            UIBarButtonItem trash = new UIBarButtonItem(trashImg, UIBarButtonItemStyle.Bordered, TrashButtonEventHandler);
            items.Add(trash);

            ToolbarItems = items.ToArray();

            animationLayer = new FabicButterflyAnimationLayer();
            this.Add(animationLayer);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (Controllers.SecurityController.FirstTimeUsingBehaviourScale && !this.BehaviourScale.FabicExample && !this.BehaviourScale.Archived)
            {
                HelpButtonEventHandler(this, null);
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (this.NavigationController != null)
            {
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(false, true);

                if (!BehaviourScale.Archived && !BehaviourScale.FabicExample)
                    ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetToolbarHidden(false, true);
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

        async void ShareEventHandler(object sender, EventArgs e)
        {
            // bring up the share menu
            //var item = this.PrintBehaviourScale();
            //var activityItems = new NSObject[] { item };
            //UIActivity[] applicationActivities = null;

            //var activityController = new UIActivityViewController(activityItems, applicationActivities);
            // save the behaviour scale first
            BigTed.BTProgressHUD.ShowContinuousProgress("Saving...", BigTed.ProgressHUD.MaskType.None);
            await Task.Run(() =>
            {
                try
                {
                    FabicDatabaseController.SaveOrUpdateBehaviourScale(BehaviourScale, false, true);
                }
                catch (Exception ex)
                {
                    UIApplication.SharedApplication.InvokeOnMainThread(delegate
                    {
                        BigTed.BTProgressHUD.Dismiss();
                        string err = "";
                        if (err.Length <= 0)
                            err = "Opps! Something went wrong and we could not save your behaviour scale for you. Please try again later";
                        UIAlertController alert = UIAlertController.Create("Unable to Print Behaviour Scale", err, UIAlertControllerStyle.Alert);
                        alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (UIAlertAction action) => { alert.DismissViewControllerAsync(true); }));
                        this.PresentModalViewController(alert, true);
                    });
                }
                UIApplication.SharedApplication.InvokeOnMainThread(delegate
                {
                    BigTed.BTProgressHUD.Dismiss();
                    BehaviourScalePrintController printController = (BehaviourScalePrintController)UIStoryboard.FromName("Main", null).InstantiateViewController("printBehaviourScaleVC");
                    printController.BehaviourScale = BehaviourScale;
                    printController.Type = Core.Enumerations.BehaviourScaleItemType.Body;
                    UINavigationController navigationController = new UINavigationController(printController);
                    PresentViewController(navigationController, true, null);
                });
            }
              );
        }

        void Archived_UndoButtonPressed(object sender, EventArgs e)
        {
            // has been unarchived - update item and display
            BehaviourScale.Archived = false;
            txtTitle.Editable = true;
            FabicDatabaseController.SaveOrUpdateBehaviourScale(BehaviourScale, false);

            if (archived != null)
            {
                archived.Hide();
                this.NavigationController.SetToolbarHidden(false, true);
            }
        }

        void HelpButtonEventHandler(object sender, EventArgs e)
        {
            BehaviourScaleTutorialViewController view = (BehaviourScaleTutorialViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("behaviourScaleTutorialView");
            view.ProvidesPresentationContextTransitionStyle = true;
            view.DefinesPresentationContext = true;
            view.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            view.NavigationBarFrame = this.NavigationController.NavigationBar.Frame;
            view.ToolBarFrame = this.NavigationController.Toolbar.Frame;
            view.Closed += View_Closed;
            view.ParentView = this.View;
            this.PresentViewController(view, true, null);
        }

        private void View_Closed(object sender, EventArgs e)
        {
            animationLayer.Animate(4);
        }

        void TrashButtonEventHandler(object sender, EventArgs e)
        {
            CRSAlertView alert = new CRSAlertView();
            alert.TintColor = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Purple);
            alert.Title = "Are you sure you want to archive this chart";
            alert.Message = "You cannot edit or view this Behaviour Scale when it is archived";
            alert.Image = new UIImage("fabic-logo.png");

            var action = new CRSAlertAction
            {
                Text = "No",
                Highlighted = false,
                TintColor = UIColor.Black,
                DidSelect = (alert2) =>
                {
                    // Do something here on press
                }
            };

            var action2 = new CRSAlertAction
            {
                Text = "Yes",
                Highlighted = true,
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Purple),
                DidSelect = DeleteHandler
            };

            alert.Actions = new CRSAlertAction[] { action, action2 };
            alert.Show();
        }

        void DeleteHandler(CRSAlertView e)
        {
            // has been archived - update item and displa
            BehaviourScale.Archived = true;
            txtTitle.Editable = false;
            FabicDatabaseController.SaveOrUpdateBehaviourScale(BehaviourScale);

            if (archived != null)
            {
                this.NavigationController.SetToolbarHidden(true, true);
                archived.Show(this.View);
            }
        }

        private void Source_UserDidSelectBehaviourLevelToViewEdit(object sender, ViewControllers.TableViewSources.UserDidSelectBehaviourLevelEventArgs args)
        {
            BehaviourScaleEditItemViewController view = (BehaviourScaleEditItemViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("behaviourScaleEditItemViewController");
            view.ProvidesPresentationContextTransitionStyle = true;
            view.DefinesPresentationContext = true;
            view.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            view.Scale = args.Scale;
            view.BehaviourScaleItems = args.ScaleItems;
            view.BehaviourScaleLevel = args.BehaviourScaleLevel;
            view.BehaviourScaleEditItemClose += BehaviourScaleEditItemClose;
            view.BodySelected = selectedBody;
            this.PresentViewController(view, true, null);
        }

        private void BehaviourScaleEditItemClose(object sender, EventArgs e)
        {
            // refresh the items
            source.Cells = null;
            mainTableView.ReloadData();
        }

        /// <summary>
        /// Prints the behaviour scale.
        /// </summary>
        public NSUrl PrintBehaviourScale()
        {
            string fileName = "BehaviourScaleExport-" + NSDate.Now.ToString() + ".pdf";
            string path = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.All, true)[0] + "/" + fileName;

            // first load the views...
            CGRect frame = new CGRect(0, -80, this.View.Frame.Width, this.View.Frame.Height);

            // header
            CGRect titleFrame = new CGRect(0, -30, this.txtTitle.Frame.Width, this.txtTitle.Frame.Height);
            UIImage titleImage = txtTitle.ExportUIViewAsImage();
            CGRect headerFrame = new CGRect(0, titleFrame.Height - 30, this.headerView.Frame.Width, this.headerView.Frame.Height);
            UIImage headerImage = headerView.ExportUIViewAsImage();

            // cells
            Dictionary<int, FabicBehaviourScaleCell> cells = source.Cells;

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

        void TxtTitle_TextChanged(object sender, EventArgs e)
        {
            this.BehaviourScale.Name = txtTitle.Text;
            FabicDatabaseController.SaveOrUpdateBehaviourScale(BehaviourScale);
        }

        public void CleanUp()
        {
            if (archived != null)
            {
                archived.CleanUp();
                archived.Dispose();
                archived = null;
            }
        }
    }
}