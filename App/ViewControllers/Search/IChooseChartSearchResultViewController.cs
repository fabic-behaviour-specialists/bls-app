using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS.ViewControllers.Search
{
    public class IChooseChartSearchResultViewController : UITableViewController
    {
        public List<IChooseChart> FilteredIChooseCharts { get; set; }

        public IChooseChartSearchResultViewController()
        {
            this.ApplyLightInterface();
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return FilteredIChooseCharts.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            IChooseChart scale = FilteredIChooseCharts[indexPath.Row];
            UITableViewCell cell = new UITableViewCell();//tableView.DequeueReusableCell(cellIdentifier);
            ConfigureCell(cell, scale);
            return cell;
        }
        protected void ConfigureCell(UITableViewCell cell, IChooseChart scale)
        {
            cell.TextLabel.Text = scale.Name;
            //string detailedStr = string.Format("{0:C} | {1}", scale.IntroPrice, scale.YearIntroduced);
            //cell.DetailTextLabel.Text = detailedStr;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            // navigate to the behaviour scale
            IChooseChartViewController controller = (IChooseChartViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("IChooseChartViewIdentifier");
            controller.Chart = FilteredIChooseCharts[indexPath.Row];
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(controller, true);
        }
    }
}