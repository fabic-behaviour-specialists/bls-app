using CoreGraphics;
using Fabic.Core.Controllers;
using Fabic.Core.Models;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public delegate void UserDidSelectBehaviourLevelEventHandler(object sender, UserDidSelectBehaviourLevelEventArgs args);
    public class UserDidSelectBehaviourLevelEventArgs : EventArgs
    {
        BehaviourScale _scale;
        List<BehaviourScaleItem> _items;
        int _level;
        public BehaviourScale Scale { get { return _scale; } }
        public List<BehaviourScaleItem> ScaleItems { get { return _items; } }
        public int BehaviourScaleLevel
        {
            get
            {
                return _level;
            }
        }
        public UserDidSelectBehaviourLevelEventArgs(BehaviourScale scale, List<BehaviourScaleItem> items, int level)
        {
            _scale = scale;
            _items = items;
            _level = level;
        }
    }

    public class BehaviourScaleViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        string CellIdentifier = "FabicBehaviourScaleCell";
        BehaviourScale Scale = null;
        List<BehaviourScaleItem> ScaleItems = new List<BehaviourScaleItem>();
        Dictionary<int, FabicBehaviourScaleCell> cells = new Dictionary<int, FabicBehaviourScaleCell>();
        Dictionary<BehaviourScaleItem, nfloat> ScaleItemHeights = new Dictionary<BehaviourScaleItem, nfloat>();
        UIView MainParentView = null; CGRect NavigationBar;

        public Dictionary<int, FabicBehaviourScaleCell> Cells
        {
            get
            {
                return cells;
            }
            set
            {
                cells = new Dictionary<int, FabicBehaviourScaleCell>();
                ScaleItems = FabicDatabaseController.FetchActiveBehaviourScalesItems(Scale).Result;
            }
        }

        public event UserDidSelectBehaviourLevelEventHandler UserDidSelectBehaviourLevelToViewEdit;

        public BehaviourScaleViewSource(BehaviourScale scale, UIView mainView, CGRect navigationBar)
        {
            MainParentView = mainView;
            NavigationBar = navigationBar;
            Scale = scale;
            ScaleItems = FabicDatabaseController.FetchActiveBehaviourScalesItems(Scale).Result;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (Scale.Archived)
            {
            }
            else if (Scale.FabicExample)
            {
            }
            else
            {
                int behaviourLevel = 0;
                if (indexPath.Row > -1)
                {
                    switch (indexPath.Row)
                    {
                        case 0:
                            behaviourLevel = 5;
                            break;
                        case 1:
                            behaviourLevel = 4;
                            break;
                        case 2:
                            behaviourLevel = 3;
                            break;
                        case 3:
                            behaviourLevel = 2;
                            break;
                        case 4:
                            behaviourLevel = 1;
                            break;
                    }

                    List<BehaviourScaleItem> behaviourScaleItems = new List<BehaviourScaleItem>();
                    if (ScaleItems != null)
                    {
                        foreach (BehaviourScaleItem item in ScaleItems)
                        {
                            if (item.BehaviourScaleLevel == behaviourLevel)
                                behaviourScaleItems.Add(item);
                        }
                    }
                    //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PresentModalViewController(vc, true);
                    //EditBehaviourScaleItemOverlay loadPop = new EditBehaviourScaleItemOverlay(Scale.ID, new CGRect(0, NavigationBar.Height - 44, MainParentView.Frame.Width, MainParentView.Frame.Height - NavigationBar.Height), behaviourLevel, behaviourScaleItems);
                    //loadPop.Show(MainParentView);
                    if (UserDidSelectBehaviourLevelToViewEdit != null)
                    {
                        UserDidSelectBehaviourLevelToViewEdit(this, new UserDidSelectBehaviourLevelEventArgs(Scale, behaviourScaleItems, behaviourLevel));
                    }
                }
            }
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (!Cells.ContainsKey(indexPath.Row))
            {
                FabicBehaviourScaleCell cell = (FabicBehaviourScaleCell)tableView.DequeueReusableCell(CellIdentifier);

                //---- if there are no cells to reuse, create a new one
                if (cell == null)
                {
                    UITableViewCell header = new UITableViewCell();

                    header.BackgroundColor = UIColor.Clear;
                    header.SelectionStyle = UITableViewCellSelectionStyle.None;
                    return header;
                }

                if (Scale.Archived || Scale.FabicExample)
                    cell.SelectionStyle = UITableViewCellSelectionStyle.None;

                cell.BehaviourScale = Scale;
                cell.BackgroundColor = UIColor.Clear;
                cell.BehaviourScaleItems = ScaleItems;
                switch (indexPath.Row)
                {
                    case 0:
                        cell.BehaviourScaleLevel = 5;
                        break;
                    case 1:
                        cell.BehaviourScaleLevel = 4;
                        break;
                    case 2:
                        cell.BehaviourScaleLevel = 3;
                        break;
                    case 3:
                        cell.BehaviourScaleLevel = 2;
                        break;
                    case 4:
                        cell.BehaviourScaleLevel = 1;
                        break;
                }

                cells.Add(indexPath.Row, cell);

                return cell;
            }
            else
            {
                int level = 0;
                switch (indexPath.Row)
                {
                    case 0:
                        level = 5;
                        break;
                    case 1:
                        level = 4;
                        break;
                    case 2:
                        level = 3;
                        break;
                    case 3:
                        level = 2;
                        break;
                    case 4:
                        level = 1;
                        break;
                }

                foreach (KeyValuePair<int, FabicBehaviourScaleCell> cell in cells)
                {
                    if (cell.Value.BehaviourScaleLevel == level)
                        return cell.Value;
                }
                return null;
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 5;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            // now work out based on the number of items we have and their estimated heights, the best height for the row
            nfloat sumHeightLife = 0; nfloat sumHeightBody = 0;
            double width = ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[0].View.Frame.Width / 3;
            width = width - 10;
            int level = 0;
            switch (indexPath.Row)
            {
                case 0:
                    level = 5;
                    break;
                case 1:
                    level = 4;
                    break;
                case 2:
                    level = 3;
                    break;
                case 3:
                    level = 2;
                    break;
                case 4:
                    level = 1;
                    break;
            }

            if (ScaleItemHeights.Count <= 0)
            {
                if (ScaleItems != null)
                {
                    foreach (BehaviourScaleItem item in ScaleItems)
                    {
                        UILabel label = new UILabel();
                        label.Frame = new CGRect(0, 0, width, 1000);
                        label.Font = UIFont.SystemFontOfSize(9);
                        label.Text = item.Name;
                        label.Lines = 0;
                        label.PreferredMaxLayoutWidth = (nfloat)width;

                        label.SizeToFit();
                        ScaleItemHeights.Add(item, label.Frame.Height + Convert.ToInt32(Math.Truncate(width * 0.115034965034965)));
                    }
                }
            }

            // body
            foreach (KeyValuePair<BehaviourScaleItem, nfloat> kvp in ScaleItemHeights)
            {
                if ((Core.Enumerations.BehaviourScaleItemType)kvp.Key.BehaviourScaleType == Core.Enumerations.BehaviourScaleItemType.Body && kvp.Key.BehaviourScaleLevel == level)
                {
                    sumHeightBody += kvp.Value;
                }
            }

            // life
            foreach (KeyValuePair<BehaviourScaleItem, nfloat> kvp in ScaleItemHeights)
            {
                if ((Core.Enumerations.BehaviourScaleItemType)kvp.Key.BehaviourScaleType == Core.Enumerations.BehaviourScaleItemType.Life && kvp.Key.BehaviourScaleLevel == level)
                {
                    sumHeightLife += kvp.Value;
                }
            }

            // the mimimum height is 105
            if (sumHeightBody < 105)
                return 105;
            else if (sumHeightLife > 105)
                return sumHeightLife;

            return 105;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 0;
        }

        public override nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            return 0;
        }

        public void CleanUp()
        {

        }
    }
}
