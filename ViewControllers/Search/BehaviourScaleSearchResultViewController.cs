using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS.ViewControllers.Search
{
    public class BehaviourScaleSearchResultViewController : UITableViewController
    {
        public List<BehaviourScale> FilteredBehaviourScales { get; set; }

        public BehaviourScaleSearchResultViewController()
        {
            this.ApplyLightInterface();
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return FilteredBehaviourScales.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            BehaviourScale scale = FilteredBehaviourScales[indexPath.Row];
            UITableViewCell cell = new UITableViewCell();//tableView.DequeueReusableCell(cellIdentifier);
            ConfigureCell(cell, scale);
            return cell;
        }
        protected void ConfigureCell(UITableViewCell cell, BehaviourScale scale)
        {
            cell.TextLabel.Text = scale.Name;
            //string detailedStr = string.Format("{0:C} | {1}", scale.IntroPrice, scale.YearIntroduced);
            //cell.DetailTextLabel.Text = detailedStr;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            // navigate to the behaviour scale
            BehaviourScaleViewController controller = (BehaviourScaleViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("BehaviourScaleViewIdentifier");
            controller.BehaviourScale = FilteredBehaviourScales[indexPath.Row];
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(controller, true);
        }
    }
}