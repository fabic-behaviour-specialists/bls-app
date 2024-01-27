using Fabic.Core.Controllers;
using Fabic.Core.Models;
using Fabic.Data.Extensions;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public class BehaviourScalePrintControllerViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        Fabic.Core.Enumerations.BehaviourScaleItemType _Type = Core.Enumerations.BehaviourScaleItemType.Body;
        BehaviourScale Scale = null;
        List<BehaviourScaleItem> ScaleItems = new List<BehaviourScaleItem>();
        Dictionary<string, BehaviourScaleItem> ScaleItemsDict = new Dictionary<string, BehaviourScaleItem>();
        Dictionary<int, List<BehaviourScaleItem>> SectionsDict = new Dictionary<int, List<BehaviourScaleItem>>();
        Dictionary<string, UITableViewCell> cells = new Dictionary<string, UITableViewCell>();
        Dictionary<string, BehaviourScaleItem> selectedItems = new Dictionary<string, BehaviourScaleItem>();

        public Dictionary<string, UITableViewCell> Cells
        {
            get
            {
                return cells;
            }
            set
            {
                cells = new Dictionary<string, UITableViewCell>();
                initialiseLists();
            }
        }

        public Dictionary<string, BehaviourScaleItem> SelectedItems
        {
            get
            {
                return selectedItems;
            }
        }

        public BehaviourScalePrintControllerViewSource(BehaviourScale scale, Fabic.Core.Enumerations.BehaviourScaleItemType type)
        {
            _Type = type;
            Scale = scale;
            initialiseLists();
        }

        public void initialiseLists()
        {
            ScaleItems = FabicDatabaseController.FetchActiveBehaviourScalesItems(Scale);
            ScaleItemsDict = ScaleItems.Where(x => x.BehaviourScaleType == (int)_Type).ToDictionary(x => x.Id);

            foreach (BehaviourScaleItem item in ScaleItems)
            {
                if (item.BehaviourScaleType == (int)_Type)
                {
                    if (!SectionsDict.ContainsKey(item.BehaviourScaleLevel + 1))
                        SectionsDict.Add(item.BehaviourScaleLevel + 1, new List<BehaviourScaleItem>());
                    SectionsDict[item.BehaviourScaleLevel + 1].Add(item);
                }
            }
        }

        //      public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
        //      {
        //          if (SectionsDict[indexPath.Section + 1].Count >= indexPath.Row)
        //          {
        //              if (!unselectedItems.ContainsKey(SectionsDict[indexPath.Section + 1][indexPath.Row].Id))
        //              {
        //                  unselectedItems.Add(SectionsDict[indexPath.Section +1][indexPath.Row].Id, SectionsDict[indexPath.Section][indexPath.Row]);
        //              }
        //          }
        //      }

        //      public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        //{
        //          if (SectionsDict[indexPath.Section + 1].Count >= indexPath.Row)
        //          {
        //              if (unselectedItems.ContainsKey(SectionsDict[indexPath.Section + 1][indexPath.Row].Id))
        //              {
        //                  unselectedItems.Remove(SectionsDict[indexPath.Section + 1][indexPath.Row].Id);
        //              }
        //          }
        //      }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (selectedItems.ContainsKey(SectionsDict[indexPath.Section + 1][indexPath.Row].Id))
            {
                // unselect
                selectedItems.Remove(SectionsDict[indexPath.Section + 1][indexPath.Row].Id);
                Cells[SectionsDict[indexPath.Section + 1][indexPath.Row].Id].Accessory = UITableViewCellAccessory.None;
                Cells[SectionsDict[indexPath.Section + 1][indexPath.Row].Id].BackgroundColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple).ColorWithAlpha(0.3f);
            }
            else
            {
                // select
                selectedItems.Add(SectionsDict[indexPath.Section + 1][indexPath.Row].Id, SectionsDict[indexPath.Section + 1][indexPath.Row]);
                Cells[SectionsDict[indexPath.Section + 1][indexPath.Row].Id].Accessory = UITableViewCellAccessory.Checkmark;
                Cells[SectionsDict[indexPath.Section + 1][indexPath.Row].Id].BackgroundColor = UIColor.Clear;
            }
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (SectionsDict.ContainsKey(indexPath.Section + 1))
            {
                if (SectionsDict[indexPath.Section + 1].Count >= indexPath.Row)
                {
                    if (!Cells.ContainsKey(SectionsDict[indexPath.Section + 1][indexPath.Row].Id))
                    {
                        UITableViewCell cell = new UITableViewCell(UITableViewCellStyle.Default, null);
                        cell.Accessory = UITableViewCellAccessory.Checkmark;
                        cell.TintColor = UIColor.White;
                        cell.BackgroundColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple).ColorWithAlpha(0.3f);
                        cell.TextLabel.Text = SectionsDict[indexPath.Section + 1][indexPath.Row].Name;
                        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                        // cell.DetailTextLabel.Text = SectionsDict[indexPath.Section + 1][indexPath.Row].Id;
                        cells.Add(SectionsDict[indexPath.Section + 1][indexPath.Row].Id, cell);
                        selectedItems.Add(SectionsDict[indexPath.Section + 1][indexPath.Row].Id, SectionsDict[indexPath.Section + 1][indexPath.Row]);

                        return cell;
                    }
                    else
                    {
                        return cells[SectionsDict[indexPath.Section + 1][indexPath.Row].Id];
                    }
                }
            }
            return new UITableViewCell();
        }

        public override UITableViewRowAction[] EditActionsForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return base.EditActionsForRow(tableView, indexPath);
        }

        public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return UITableViewCellEditingStyle.None;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 6;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (SectionsDict.ContainsKey((int)section + 1))
                return SectionsDict[(int)section + 1].Count;
            else
                return 0;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 60;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 0;
        }

        public override nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            return 0;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            switch (section)
            {
                case 1:
                    return "Code Blue";
                case 2:
                    return "Code Green";
                case 3:
                    return "Code Yellow";
                case 4:
                    return "Code Orange";
                case 5:
                    return "Code Red";
            }
            return "";
        }

        public void CleanUp()
        {

        }
    }
}
