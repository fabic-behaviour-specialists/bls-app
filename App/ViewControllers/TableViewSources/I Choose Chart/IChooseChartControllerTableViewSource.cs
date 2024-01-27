using CoreGraphics;
using Curse;
using Fabic.Core.Controllers;
using Fabic.Core.Models;
using Fabic.Data.Extensions;
using Foundation;
using System;
using UIKit;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public class IChooseChartControllerTableViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        string CellIdentifier = "TableCell";
        FabicButton addChart; FabicButton iChooseChart; FabicButton fabicsLibrary; FabicButton archived;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
            }

            if (cell.Tag != 200)
            {
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                cell.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0);
                cell.TranslatesAutoresizingMaskIntoConstraints = false;

                double centerX = tableView.Frame.Width / 2;
                double centerY = 60 / 2;

                switch (indexPath.Row)
                {
                    case 0:
                        FabicButton aboutApp = new FabicButton();
                        aboutApp.FabicColour = Data.Enums.FabicColour.Gray;
                        aboutApp.SetTitle("About I Choose Chart", UIControlState.Normal);
                        aboutApp.Frame = new CGRect(centerX - (aboutApp.Frame.Width / 2), centerY - (aboutApp.Frame.Height / 2), aboutApp.Frame.Width, aboutApp.Frame.Height);
                        aboutApp.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
                        aboutApp.TitleLabel.Lines = 0;
                        aboutApp.TitleLabel.TextAlignment = UITextAlignment.Center;
                        aboutApp.TouchDown += AboutApp_TouchDown;
                        cell.ContentView.Add(aboutApp);
                        break;
                    case 1:
                        addChart = new FabicButton();
                        addChart.FabicColour = Data.Enums.FabicColour.Blue;
                        addChart.SetTitle("Add Chart", UIControlState.Normal);
                        addChart.Frame = new CGRect(centerX - (addChart.Frame.Width / 2), centerY - (addChart.Frame.Height / 2), addChart.Frame.Width, addChart.Frame.Height);
                        addChart.TouchDown += AddIChooseChart_TouchDown;
                        cell.ContentView.Add(addChart);
                        break;
                    case 2:
                        iChooseChart = new FabicButton();
                        iChooseChart.FabicColour = Data.Enums.FabicColour.Blue;
                        iChooseChart.SetTitle("My Library", UIControlState.Normal);
                        iChooseChart.Frame = new CGRect(centerX - (iChooseChart.Frame.Width / 2), centerY - (iChooseChart.Frame.Height / 2), iChooseChart.Frame.Width, iChooseChart.Frame.Height);
                        iChooseChart.TouchDown += Library_TouchDown;
                        cell.ContentView.Add(iChooseChart);
                        break;
                    case 3:
                        fabicsLibrary = new FabicButton();
                        fabicsLibrary.FabicColour = Data.Enums.FabicColour.Blue;
                        fabicsLibrary.SetTitle("Fabic Library", UIControlState.Normal);
                        fabicsLibrary.Frame = new CGRect(centerX - (fabicsLibrary.Frame.Width / 2), centerY - (fabicsLibrary.Frame.Height / 2), fabicsLibrary.Frame.Width, fabicsLibrary.Frame.Height);
                        fabicsLibrary.TouchDown += Fabic_TouchDown;
                        cell.ContentView.Add(fabicsLibrary);
                        break;
                    case 4:
                        archived = new FabicButton();
                        archived.FabicColour = Data.Enums.FabicColour.Blue;
                        archived.SetTitle("Archived Charts", UIControlState.Normal);
                        archived.Frame = new CGRect(centerX - (archived.Frame.Width / 2), centerY - (archived.Frame.Height / 2), archived.Frame.Width, archived.Frame.Height);
                        archived.TouchDown += Archived_TouchDown;
                        cell.ContentView.Add(archived);
                        break;
                    case 5:
                        FabicButton help = new FabicButton();
                        help.FabicColour = Data.Enums.FabicColour.Gray;
                        help.SetTitle("Help", UIControlState.Normal);
                        help.Frame = new CGRect(centerX - (help.Frame.Width / 2), centerY - (help.Frame.Height / 2), help.Frame.Width, help.Frame.Height);
                        cell.ContentView.Add(help);
                        break;
                }

                cell.Tag = 200; // mark as loaded;
            }

            return cell;
        }

        private void AboutApp_TouchDown(object sender, EventArgs e)
        {
            UIViewController vc = UIStoryboard.FromName("Main", null).InstantiateViewController("iChooseChartAboutVC");
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(vc, true);
        }

        private void Archived_TouchDown(object sender, EventArgs e)
        {
            UIViewController vc = UIStoryboard.FromName("Main", null).InstantiateViewController("iChooseChartArchivedTableVC");
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(vc, true);
        }

        private void Fabic_TouchDown(object sender, EventArgs e)
        {
            UIViewController vc = UIStoryboard.FromName("Main", null).InstantiateViewController("iChooseChartFabicTableVC");
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(vc, true);
        }

        private void Library_TouchDown(object sender, EventArgs e)
        {
            UIViewController vc = UIStoryboard.FromName("Main", null).InstantiateViewController("iChooseChartLibraryTableVC");
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(vc, true);
        }

        private void AddIChooseChart_TouchDown(object sender, EventArgs e)
        {
            CRSAlertView alert = new CRSAlertView();
            alert.TintColor = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Blue);
            alert.Title = "Add an I Choose Chart";
            alert.Message = "What would you like to call your I Choose Chart?";
            alert.Image = new UIImage("butterfly.png");

            var action = new CRSAlertAction
            {
                Text = "Cancel",
                Highlighted = false,
                TintColor = UIColor.Black,
                DidSelect = (alert2) =>
                {
                    // Do something here on press
                }
            };

            var input2 = new CRSAlertInput
            {
                Placeholder = "Name of Chart",
                Text = "",
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Blue),
                OpenAutomatically = true
            };

            var action2 = new CRSAlertAction
            {
                Text = "Save",
                Highlighted = true,
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Blue),
                DidSelect = HandleAction
            };

            alert.Input = input2;
            alert.Actions = new CRSAlertAction[] { action, action2 };
            alert.Show();
        }

        async void HandleAction(Curse.CRSAlertView obj)
        {
            // Do something here on pre
            string input = obj.Input.Text;

            // check to make sure the entered text is not blank
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                // if it is, make the user know

                CRSAlertView alert = new CRSAlertView();
                alert.TintColor = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Red);
                alert.Title = "Add an I Choose Chart";
                alert.Message = "What would you like to call your I Choose Chart?";
                alert.Image = new UIImage("butterfly.png");

                var action = new CRSAlertAction
                {
                    Text = "Cancel",
                    Highlighted = false,
                    TintColor = UIColor.Black,
                    DidSelect = (alert2) =>
                    {
                        // Do something here on press
                    }
                };

                var input2 = new CRSAlertInput
                {
                    Placeholder = "A Title is Required",
                    Text = "",
                    TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Red),
                    OpenAutomatically = true
                };

                var action2 = new CRSAlertAction
                {
                    Text = "Save",
                    Highlighted = true,
                    TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Red),
                    DidSelect = HandleAction
                };

                alert.Input = input2;
                alert.Actions = new CRSAlertAction[] { action, action2 };
                alert.Show();
            }
            else
            {
                IChooseChart icc = new IChooseChart();
                icc.Archived = false;
                icc.Description1 = "";
                icc.FabicExample = false;
                icc.Name = input;

                FabicDatabaseController.SaveOrUpdateIChooseChart(icc);
                UIViewController controller = UIStoryboard.FromName("Main", null).InstantiateViewController("IChooseChartViewIdentifier");
                ((IChooseChartViewController)controller).Chart = icc;
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(controller, true);
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 5;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 65;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 0;
        }

        public override nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            return 0;
        }

        /// <summary>
        /// Unhightlights the items.
        /// </summary>
        public void UnhightlightItems()
        {
            addChart.Unhighlight();
            iChooseChart.Unhighlight();
            fabicsLibrary.Unhighlight();
            archived.Unhighlight();
            //aboutApp.Unhighlight();
            //help3.Highlighted = false;
        }

        public void CleanUp()
        {

        }
    }
}
