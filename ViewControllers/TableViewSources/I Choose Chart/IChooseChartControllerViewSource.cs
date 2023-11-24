using CoreGraphics;
using Fabic.Core.Controllers;
using Fabic.Core.Models;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public delegate void UserDidSelectIChooseChartLevelEventHandler(object sender, UserDidSelectIChooseChartLevelEventArgs args);
    public class UserDidSelectIChooseChartLevelEventArgs : EventArgs
    {
        IChooseChart _chart;
        List<IChooseChartItem> _items;
        Core.Enumerations.IChooseChartItemType _level;
        public IChooseChart Chart { get { return _chart; } }
        public List<IChooseChartItem> ScaleItems { get { return _items; } }
        public Core.Enumerations.IChooseChartItemType BehaviourScaleLevel
        {
            get
            {
                return _level;
            }
        }
        public UserDidSelectIChooseChartLevelEventArgs(IChooseChart chart, List<IChooseChartItem> items, Core.Enumerations.IChooseChartItemType level)
        {
            _chart = chart;
            _items = items;
            _level = level;
        }
    }

    public class IChooseChartViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        string CellIdentifier = "FabicIChooseChartCell";
        double borderIndent = 13;
        IChooseChart Chart = null;
        List<IChooseChartItem> ChartItems = new List<IChooseChartItem>();
        Dictionary<int, FabicIChooseChartCell> cells = new Dictionary<int, FabicIChooseChartCell>();
        Dictionary<IChooseChartItem, nfloat> ChartItemHeights = new Dictionary<IChooseChartItem, nfloat>();
        UIView MainParentView = null; CGRect NavigationBar;
        UITapGestureRecognizer tap = new UITapGestureRecognizer();

        public event UserDidSelectIChooseChartLevelEventHandler UserDidSelectIChooseChartLevelToViewEdit;

        public Dictionary<int, FabicIChooseChartCell> Cells
        {
            get
            {
                return cells;
            }
            set
            {
                cells = new Dictionary<int, FabicIChooseChartCell>();
                ChartItems = FabicDatabaseController.FetchActiveIChooseChartItems(Chart).Result;
            }
        }

        public IChooseChartViewSource(IChooseChart chart, UIView mainView, CGRect navigationBar)
        {
            MainParentView = mainView;
            NavigationBar = navigationBar;
            Chart = chart;
            ChartItems = FabicDatabaseController.FetchActiveIChooseChartItems(Chart).Result;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            // show the popup form
            if (Chart.Archived)
            {
            }
            else if (Chart.FabicExample)
            {
            }
            else
            {
                Core.Enumerations.IChooseChartItemType type = Core.Enumerations.IChooseChartItemType.Behaviour;
                if (indexPath.Row > -1 && indexPath.Row < 2)
                {
                    switch (indexPath.Row)
                    {
                        case 0:
                            type = Core.Enumerations.IChooseChartItemType.Behaviour;
                            break;
                        case 1:
                            type = Core.Enumerations.IChooseChartItemType.Outcome;
                            break;
                    }

                    List<IChooseChartItem> items = new List<IChooseChartItem>();
                    foreach (IChooseChartItem item in ChartItems)
                    {
                        if ((Core.Enumerations.IChooseChartItemType)item.ChartType == type)
                            items.Add(item);
                    }

                    //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PresentModalViewController(vc, true);
                    if (UserDidSelectIChooseChartLevelToViewEdit != null)
                    {
                        UserDidSelectIChooseChartLevelToViewEdit(this, new UserDidSelectIChooseChartLevelEventArgs(Chart, items, type));
                    }
                }
            }
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (!Cells.ContainsKey(indexPath.Row))
            {
                FabicIChooseChartCell cell = (FabicIChooseChartCell)tableView.DequeueReusableCell(CellIdentifier);

                //---- if there are no cells to reuse, create a new one
                if (cell == null)
                {
                    UITableViewCell header = new UITableViewCell();

                    return header;
                }
                else if (indexPath.Row == 2)
                {
                    UITableViewCell header = new UITableViewCell();
                    header.SelectionStyle = UITableViewCellSelectionStyle.None;

                    // here we draw the bottom two boxes of the I Choose Chart
                    // first Code Blue
                    UIView codeBlue = new UIView();
                    codeBlue.Layer.BorderWidth = 2;
                    codeBlue.Layer.BorderColor = UIColor.Blue.CGColor;
                    codeBlue.BackgroundColor = UIColor.Blue.ColorWithAlpha(0.35f);
                    codeBlue.Frame = new CGRect(borderIndent, 0, tableView.Frame.Width / 2 - (borderIndent * 2), 80);
                    header.AddSubview(codeBlue);

                    UILabel codeBlueTitle = new UILabel();
                    codeBlueTitle.Font = UIFont.SystemFontOfSize(9);
                    codeBlueTitle.Text = "Closer to Level 1 – Code Blue";
                    codeBlueTitle.BackgroundColor = UIColor.Clear;
                    codeBlueTitle.TextAlignment = UITextAlignment.Center;
                    codeBlueTitle.Frame = new CGRect(0, 0, codeBlue.Frame.Width, 23);
                    codeBlue.AddSubview(codeBlueTitle);

                    UILabel codeBlueSubTitle = new UILabel();
                    codeBlueSubTitle.Font = UIFont.SystemFontOfSize(8);
                    codeBlueSubTitle.Text = "CALM AND WANTED BODY";
                    codeBlueSubTitle.BackgroundColor = UIColor.White;
                    codeBlueSubTitle.TextAlignment = UITextAlignment.Center;
                    codeBlueSubTitle.Frame = new CGRect((codeBlue.Frame.Width - 125) / 2, 23, 125, 23);
                    codeBlue.AddSubview(codeBlueSubTitle);

                    UILabel codeBlueSummary = new UILabel();
                    codeBlueSummary.Font = UIFont.SystemFontOfSize(8);
                    codeBlueSummary.Text = "Perceived 80-100% equipped to manage life";
                    codeBlueSummary.BackgroundColor = UIColor.Clear;
                    codeBlueSummary.TextAlignment = UITextAlignment.Center;
                    codeBlueSummary.Lines = 2;
                    codeBlueSummary.Frame = new CGRect(10, 46, codeBlue.Frame.Width - 20, 33);
                    codeBlue.AddSubview(codeBlueSummary);

                    UIImageView codeBlueFace = new UIImageView();
                    codeBlueFace.Frame = new CoreGraphics.CGRect(codeBlue.Frame.Width - 33, codeBlue.Frame.Height - 35, 26, 26);
                    codeBlueFace.Image = new UIImage("Faces1.png");
                    codeBlue.AddSubview(codeBlueFace);

                    // then Code Red
                    UIView codeRed = new UIView();
                    codeRed.Layer.BorderWidth = 2;
                    codeRed.Layer.BorderColor = UIColor.Red.CGColor;
                    codeRed.BackgroundColor = UIColor.Red.ColorWithAlpha(0.35f);
                    codeRed.Frame = new CGRect(tableView.Frame.Width / 2 + borderIndent, 0, tableView.Frame.Width / 2 - (borderIndent * 2), 80);
                    header.AddSubview(codeRed);

                    UILabel codeRedTitle = new UILabel();
                    codeRedTitle.Font = UIFont.SystemFontOfSize(9);
                    codeRedTitle.Text = "Closer to Level 5 – Code Red";
                    codeRedTitle.BackgroundColor = UIColor.Clear;
                    codeRedTitle.TextAlignment = UITextAlignment.Center;
                    codeRedTitle.Frame = new CGRect(0, 0, codeRed.Frame.Width, 23);
                    codeRed.AddSubview(codeRedTitle);

                    UILabel codeRedSubTitle = new UILabel();
                    codeRedSubTitle.Font = UIFont.SystemFontOfSize(8);
                    codeRedSubTitle.Text = "MELTDOWN BODY";
                    codeRedSubTitle.BackgroundColor = UIColor.White;
                    codeRedSubTitle.TextAlignment = UITextAlignment.Center;
                    codeRedSubTitle.Frame = new CGRect((codeRed.Frame.Width - 125) / 2, 23, 125, 23);
                    codeRed.AddSubview(codeRedSubTitle);

                    UILabel codeRedSummary = new UILabel();
                    codeRedSummary.Font = UIFont.SystemFontOfSize(8);
                    codeRedSummary.Text = "Perceived 0-20% equipped to manage life";
                    codeRedSummary.BackgroundColor = UIColor.Clear;
                    codeRedSummary.TextAlignment = UITextAlignment.Center;
                    codeRedSummary.Lines = 2;
                    codeRedSummary.Frame = new CGRect(10, 46, codeRed.Frame.Width - 20, 33);
                    codeRed.AddSubview(codeRedSummary);

                    UIImageView codeRedFace = new UIImageView();
                    codeRedFace.Frame = new CoreGraphics.CGRect(codeRed.Frame.Width - 33, codeRed.Frame.Height - 35, 26, 26);
                    codeRedFace.Image = new UIImage("Faces5.png");
                    codeRed.AddSubview(codeRedFace);

                    return header;
                }

                cell.IChooseChart = Chart;
                cell.BackgroundColor = UIColor.Clear;
                cell.IChooseChartItems = ChartItems;

                if (Chart.Archived || Chart.FabicExample)
                    cell.SelectionStyle = UITableViewCellSelectionStyle.None;

                switch (indexPath.Row)
                {
                    case 0:
                        cell.IChooseChartType = Core.Enumerations.IChooseChartItemType.Behaviour;
                        break;
                    case 1:
                        cell.IChooseChartType = Core.Enumerations.IChooseChartItemType.Outcome;
                        break;
                }

                cells.Add(indexPath.Row, cell);

                return cell;
            }
            else
            {
                return cells[indexPath.Row];
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 3;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            // if the row is the first row, return a smaller height;
            if (indexPath.Row == 2)
            {
                return 80;
            }

            // now work out based on the number of items we have and their estimated heights, the best height for the row
            nfloat sumHeightOption1 = 0; nfloat sumHeightOption2 = 0;
            Core.Enumerations.IChooseChartItemType type = Core.Enumerations.IChooseChartItemType.Behaviour;
            double width = ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[0].View.Frame.Width / 2;

            if (ChartItemHeights.Count <= 0)
            {
                foreach (IChooseChartItem item in ChartItems)
                {
                    UILabel label = new UILabel();
                    label.Frame = new CoreGraphics.CGRect(12, 0, width - (borderIndent * 2) - 14, 20);
                    label.Font = UIFont.SystemFontOfSize(9);
                    label.Text = item.ItemText;
                    label.Lines = 100;
                    label.SizeToFit();
                    ChartItemHeights.Add(item, label.Frame.Height + 6);
                }
            }

            if (indexPath.Row == 0)
                type = Core.Enumerations.IChooseChartItemType.Behaviour;
            else if (indexPath.Row == 1)
                type = Core.Enumerations.IChooseChartItemType.Outcome;

            // option 1
            foreach (KeyValuePair<IChooseChartItem, nfloat> kvp in ChartItemHeights)
            {
                if ((Core.Enumerations.IChooseChartOption)kvp.Key.ChartOption == Core.Enumerations.IChooseChartOption.Option1 && (Core.Enumerations.IChooseChartItemType)kvp.Key.ChartType == type)
                {
                    sumHeightOption1 += kvp.Value;
                }
            }

            // option 2
            foreach (KeyValuePair<IChooseChartItem, nfloat> kvp in ChartItemHeights)
            {
                if ((Core.Enumerations.IChooseChartOption)kvp.Key.ChartOption == Core.Enumerations.IChooseChartOption.Option2 && (Core.Enumerations.IChooseChartItemType)kvp.Key.ChartType == type)
                {
                    sumHeightOption2 += kvp.Value;
                }
            }

            // the mimimum height is 105
            if (sumHeightOption1 > 80 && sumHeightOption1 >= sumHeightOption2)
                return sumHeightOption1 + 80;
            else if (sumHeightOption2 > 80 && sumHeightOption2 >= sumHeightOption1)
                return sumHeightOption2 + 80;

            return 160;
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
