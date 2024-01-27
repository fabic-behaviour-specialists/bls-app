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
    public class IChooseChartPrintControllerViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        Fabic.Core.Enumerations.IChooseChartOption _Option = Core.Enumerations.IChooseChartOption.Option1;
        IChooseChart Chart = null;
        List<IChooseChartItem> ScaleItems = new List<IChooseChartItem>();
        Dictionary<string, IChooseChartItem> ScaleItemsDict = new Dictionary<string, IChooseChartItem>();
        Dictionary<int, List<IChooseChartItem>> SectionsDict = new Dictionary<int, List<IChooseChartItem>>();
        Dictionary<string, UITableViewCell> cells = new Dictionary<string, UITableViewCell>();
        Dictionary<string, IChooseChartItem> selectedItems = new Dictionary<string, IChooseChartItem>();

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

        public Dictionary<string, IChooseChartItem> SelectedItems
        {
            get
            {
                return selectedItems;
            }
        }

        public IChooseChartPrintControllerViewSource(IChooseChart chart, Fabic.Core.Enumerations.IChooseChartOption option)
        {
            _Option = option;
            Chart = chart;
            initialiseLists();
        }

        public void initialiseLists()
        {
            ScaleItems = FabicDatabaseController.FetchActiveIChooseChartItems(Chart).Result;
            ScaleItemsDict = ScaleItems.Where(x => x.ChartOption == (int)_Option).ToDictionary(x => x.Id);

            foreach (IChooseChartItem item in ScaleItems)
            {
                if (item.ChartOption == (int)_Option)
                {
                    if (!SectionsDict.ContainsKey(item.ChartType + 1))
                        SectionsDict.Add(item.ChartType + 1, new List<IChooseChartItem>());
                    SectionsDict[item.ChartType + 1].Add(item);
                }
            }
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (selectedItems.ContainsKey(SectionsDict[indexPath.Section][indexPath.Row].Id))
            {
                // select again
                selectedItems.Remove(SectionsDict[indexPath.Section][indexPath.Row].Id);
                Cells[SectionsDict[indexPath.Section][indexPath.Row].Id].Accessory = UITableViewCellAccessory.None;
                Cells[SectionsDict[indexPath.Section][indexPath.Row].Id].BackgroundColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue).ColorWithAlpha(0.3f);
            }
            else
            {
                // unselect
                selectedItems.Add(SectionsDict[indexPath.Section][indexPath.Row].Id, SectionsDict[indexPath.Section][indexPath.Row]);
                Cells[SectionsDict[indexPath.Section][indexPath.Row].Id].Accessory = UITableViewCellAccessory.Checkmark;
                Cells[SectionsDict[indexPath.Section][indexPath.Row].Id].BackgroundColor = UIColor.Clear;
            }
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (SectionsDict.ContainsKey(indexPath.Section))
            {
                if (SectionsDict[indexPath.Section].Count >= indexPath.Row)
                {
                    if (!Cells.ContainsKey(SectionsDict[indexPath.Section][indexPath.Row].Id))
                    {
                        UITableViewCell cell = new UITableViewCell(UITableViewCellStyle.Default, null);
                        cell.Accessory = UITableViewCellAccessory.Checkmark;
                        cell.BackgroundColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue).ColorWithAlpha(0.3f);
                        cell.TintColor = UIColor.White;
                        cell.TextLabel.Text = SectionsDict[indexPath.Section][indexPath.Row].ItemText;
                        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                        cells.Add(SectionsDict[indexPath.Section][indexPath.Row].Id, cell);
                        selectedItems.Add(SectionsDict[indexPath.Section][indexPath.Row].Id, SectionsDict[indexPath.Section][indexPath.Row]);

                        return cell;
                    }
                    else
                    {
                        return cells[SectionsDict[indexPath.Section][indexPath.Row].Id];
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
            if (SectionsDict.ContainsKey((int)section))
                return SectionsDict[(int)section].Count;
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
                    return "Behaviour";
                case 2:
                    return "Consequence";
            }
            return "";
        }

        public void CleanUp()
        {

        }
    }
}
