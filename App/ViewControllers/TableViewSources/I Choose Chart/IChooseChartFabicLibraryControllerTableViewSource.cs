using CoreGraphics;
using Fabic.Core.Controllers;
using Fabic.Core.Models;
using Fabic.Data.Extensions;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public class IChooseChartFabicLibraryTableViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        string CellIdentifier = "TableCell";
        List<IChooseChart> IChooseCharts;

        public IChooseChartFabicLibraryTableViewSource(List<IChooseChart> charts)
        {
            IChooseCharts = charts;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
            }

            //if (cell.Tag != 200)
            //{
            if (IChooseCharts == null)
                IChooseCharts = FabicDatabaseController.FetchFabicIChooseChartsTemplates().Result;

            if (IChooseCharts.Count > indexPath.Row)
                cell.TextLabel.Text = IChooseCharts[indexPath.Row].Name;

            UIView selectedBackgroundView = new UIView();
            selectedBackgroundView.Frame = cell.Frame;
            selectedBackgroundView.BackgroundColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue);

            cell.BackgroundColor = UIColor.Clear;
            cell.TextLabel.Lines = 2;
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            cell.SelectedBackgroundView = selectedBackgroundView;
            cell.TextLabel.HighlightedTextColor = UIColor.White;
            cell.TextLabel.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple, true);

            //cell.Tag = 200; // mark as loaded;
            //}

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (IChooseCharts == null)
            {
                IChooseCharts = FabicDatabaseController.FetchFabicIChooseChartsTemplates().Result;
                if (IChooseCharts == null || IChooseCharts.Count <= 0)
                {
                    UILabel label = new UILabel();
                    label.Text = "No Charts have been Archived Yet";
                    label.Font = UIFont.BoldSystemFontOfSize(20);
                    label.Lines = 3;
                    label.TextColor = UIColor.DarkGray;
                    label.Frame = new CGRect(0, 0, tableview.Frame.Width, tableview.Frame.Height);
                    label.TextAlignment = UITextAlignment.Center;
                    tableview.BackgroundView.AddSubview(label);
                }
            }

            return IChooseCharts.Count;
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

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            // navigate to the behaviour scalee
            UIViewController controller = UIStoryboard.FromName("Main", null).InstantiateViewController("IChooseChartViewIdentifier");
            ((IChooseChartViewController)controller).Chart = IChooseCharts[indexPath.Row];
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(controller, true);
        }

        public void CleanUp()
        {

        }
    }
}
