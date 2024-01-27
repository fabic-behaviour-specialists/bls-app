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
    public class BehaviourScaleArchivedTableViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        string CellIdentifier = "TableCell";
        List<BehaviourScale> BehaviourScales;
        UILabel label;

        public BehaviourScaleArchivedTableViewSource(List<BehaviourScale> scales)
        {
            BehaviourScales = scales;
        }

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
                if (BehaviourScales == null)
                    BehaviourScales = FabicDatabaseController.FetchArchivedBehaviourScales();

                if (BehaviourScales.Count > indexPath.Row)
                    cell.TextLabel.Text = BehaviourScales[indexPath.Row].Name;

                UIView selectedBackgroundView = new UIView();
                selectedBackgroundView.Frame = cell.Frame;
                selectedBackgroundView.BackgroundColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue);

                cell.BackgroundColor = UIColor.Clear;
                cell.TextLabel.Lines = 2;
                cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                cell.SelectedBackgroundView = selectedBackgroundView;
                cell.TextLabel.HighlightedTextColor = UIColor.White;
                cell.TextLabel.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue, true);

                cell.Tag = 200; // mark as loaded;
            }

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (BehaviourScales == null)
                BehaviourScales = FabicDatabaseController.FetchArchivedBehaviourScales();

            if (BehaviourScales.Count <= 0)
            {
                if (label != null)
                {
                    label.RemoveFromSuperview();
                    label = null;
                }

                label = new UILabel();
                label.Text = "No Charts have been Archived Yet";
                label.Font = UIFont.BoldSystemFontOfSize(22);
                label.Lines = 3;
                label.TextColor = UIColor.DarkGray;
                label.Frame = new CGRect(15, 0, tableview.Frame.Width - 30, tableview.Frame.Height);
                label.TextAlignment = UITextAlignment.Center;
                tableview.BackgroundView.AddSubview(label);
            }

            return BehaviourScales.Count;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            Console.WriteLine("row - " + (((UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height / 480) * 65)));
            return 65; // (((UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height - 480) * 65)); //80;//65;
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
            // navigate to the behaviour scale
            BehaviourScaleViewController controller = (BehaviourScaleViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("BehaviourScaleViewIdentifier");
            controller.BehaviourScale = BehaviourScales[indexPath.Row];
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(controller, true);
        }

        public override UITableViewRowAction[] EditActionsForRow(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewRowAction hiButton = UITableViewRowAction.Create(
                  UITableViewRowActionStyle.Normal,
                  "Unarchive",
                  delegate
                  {
                      List<NSIndexPath> indexes = new List<NSIndexPath>();
                      indexes.Add(indexPath);
                      BehaviourScales[indexPath.Row].Archived = false;
                      FabicDatabaseController.SaveOrUpdateBehaviourScale(BehaviourScales[indexPath.Row]);
                      BehaviourScales.RemoveAt(indexPath.Row);
                      tableView.DeleteRows(indexes.ToArray(), UITableViewRowAnimation.Left);

                      if (BehaviourScales.Count <= 0)
                      {
                          if (label != null)
                          {
                              label.RemoveFromSuperview();
                              label = null;
                          }

                          label = new UILabel();
                          label.Text = "No Charts have been Archived Yet";
                          label.Font = UIFont.BoldSystemFontOfSize(22);
                          label.Lines = 3;
                          label.TextColor = UIColor.DarkGray;
                          label.Frame = new CGRect(15, 0, tableView.Frame.Width - 30, tableView.Frame.Height);
                          label.TextAlignment = UITextAlignment.Center;
                          tableView.BackgroundView.AddSubview(label);

                      }
                  });
            hiButton.BackgroundColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Blue);
            return new UITableViewRowAction[] { hiButton };
        }

        public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return UITableViewCellEditingStyle.Delete;
        }

        public void CleanUp()
        {

        }
    }
}
