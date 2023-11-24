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
    public class EditBehaviourScaleViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        string CellIdentifier = "FabicBehaviourScaleCell";
        List<BehaviourScaleItem> ScaleItems = new List<BehaviourScaleItem>();
        Dictionary<string, nfloat> ScaleItemHeights = new Dictionary<string, nfloat>();
        UITableView TableView;
        UIView SuperView;
        UILabel label;

        public UITextView ViewForRefocus
        {
            get; set;
        }

        public EditBehaviourScaleViewSource(List<BehaviourScaleItem> scaleItems, UIView superView)
        {
            ScaleItems = scaleItems;
            SuperView = superView;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            TableView = tableView;
            FabicTextEditOverlay overlay = new FabicTextEditOverlay(ScaleItems[indexPath.Row].Name, ScaleItems[indexPath.Row]);
            overlay.FabicColour = Data.Enums.FabicColour.Purple;
            overlay.FinishedEditingItem += Overlay_FinishedEditingItem;
            overlay.Show(SuperView);//((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers.Count()-1].View);
        }

        async void Overlay_FinishedEditingItem(object sender, EventArgs e)
        {
            // refresh the item in the list and save it
            FabicTextEditOverlay overlay = (FabicTextEditOverlay)sender;
            BehaviourScaleItem item = (BehaviourScaleItem)overlay.EditingItem;
            item.Name = overlay.Text;

            await FabicDatabaseController.SaveOrUpdateBehaviourScaleItem(item);
            int index = ScaleItems.IndexOf(item);
            ScaleItems.Remove(item);
            ScaleItems.Insert(index, item);

            TableView.ReloadData();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            FabicBehaviourScaleCell cell = (FabicBehaviourScaleCell)tableView.DequeueReusableCell(CellIdentifier);

            //---- if there are no cells to reuse, create a new one
            if (cell == null || indexPath.Row == 0)
            {
                UITableViewCell header = new UITableViewCell();
                double width = ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[0].View.Frame.Width;
                double height = (ScaleItemHeights.ContainsKey(indexPath.Row.ToString())) ? ScaleItemHeights[indexPath.Row.ToString()] : 45;
                if (height < 50)
                {
                    height = 50;
                }

                UILabel text = new UILabel();
                text.Frame = new CGRect(10, 3, width - 110, height);
                //text.AutosizesSubviews = true;
                //text.AutoresizingMask = UIViewAutoresizing.All;
                text.Text = " •  " + ScaleItems[indexPath.Row].Name;
                text.Font = UIFont.FromName("AvenirNext-Regular", 17);
                text.Lines = 100;
                text.BackgroundColor = UIColor.Clear;
                text.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue);
                header.BackgroundColor = UIColor.Clear;
                header.ContentView.AddSubview(text);

                return header;
            }

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (ScaleItems.Count <= 0)
            {
                if (label != null)
                {
                    label.RemoveFromSuperview();
                    label = null;
                }

                if (tableview.BackgroundView == null)
                    tableview.BackgroundView = new UIView(tableview.Frame);

                label = new UILabel();
                label.Text = "No items have been added here yet. Click the '+' button to start adding items.";
                label.Font = UIFont.BoldSystemFontOfSize(22);
                label.Lines = 3;
                label.TextColor = UIColor.DarkGray;
                label.Frame = new CGRect(15, 0, tableview.Frame.Width - 30, tableview.Frame.Height);
                label.TextAlignment = UITextAlignment.Center;
                tableview.BackgroundView.AddSubview(label);
            }
            else
            {
                if (label != null)
                {
                    label.RemoveFromSuperview();
                    label = null;
                }
            }

            return ScaleItems.Count;
        }


        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            // if the row is the first row, return a smaller height;
            // now work out based on the number of items we have and their estimated heights, the best height for the row
            double width = ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[0].View.Frame.Width;
            width = width - 110;

            if (ScaleItems.Count > indexPath.Row)
            {
                UILabel label = new UILabel();
                label.Frame = new CGRect(0, 0, width, 1000);
                label.Font = UIFont.FromName("AvenirNext-Regular", 20);
                label.Text = ScaleItems[indexPath.Row].Name;
                label.Lines = 100;
                label.PreferredMaxLayoutWidth = (nfloat)width;

                label.SizeToFit();

                if (label.Frame.Height < 50)
                {
                    if (!ScaleItemHeights.ContainsKey(indexPath.Row.ToString()))
                        ScaleItemHeights.Add(indexPath.Row.ToString(), 50);
                    return 50;
                }
                else
                {
                    if (!ScaleItemHeights.ContainsKey(indexPath.Row.ToString()))
                        ScaleItemHeights.Add(indexPath.Row.ToString(), label.Frame.Height);
                    return label.Frame.Height;
                }
            }

            return 50;

        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 0;
        }

        public override nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            return 0;
        }

        public override UITableViewRowAction[] EditActionsForRow(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewRowAction hiButton = UITableViewRowAction.Create(
                  UITableViewRowActionStyle.Normal,
                  "Delete",
                  delegate
                  {
                      List<NSIndexPath> indexes = new List<NSIndexPath>();
                      indexes.Add(indexPath);
                      ScaleItems[indexPath.Row].Archived = true;
                      FabicDatabaseController.SaveOrUpdateBehaviourScaleItem(ScaleItems[indexPath.Row]);
                      ScaleItemHeights.Remove(ScaleItems[indexPath.Row].Id);
                      ScaleItems.RemoveAt(indexPath.Row);
                      tableView.DeleteRows(indexes.ToArray(), UITableViewRowAnimation.Left);
                  });
            hiButton.BackgroundColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Red);
            return new UITableViewRowAction[] { hiButton };
        }

        public override bool ShouldIndentWhileEditing(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public void CleanUp()
        {

        }
    }
}
