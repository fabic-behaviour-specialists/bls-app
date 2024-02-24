using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public class HomeViewControllerTableViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        public event EventHandler Animate;
        private UITableView TableSource = null;
        string CellIdentifier = "TableCell";
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            TableSource = tableView;

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
                cell.UserInteractionEnabled = true;

                double centerX = tableView.Frame.Width / 2;
                double centerY = 60 / 2;

                switch (indexPath.Row)
                {
                    case 0:
                        FabicButton about = new FabicButton();
                        about.FabicColour = Data.Enums.FabicColour.Gray;
                        about.SetTitle("About This App", UIControlState.Normal);
                        about.Frame = new CGRect(centerX - (about.Frame.Width / 2), centerY - (about.Frame.Height / 2), about.Frame.Width, about.Frame.Height);
                        about.TouchUpInside += About_TouchDown;
                        cell.ContentView.Add(about);
                        break;
                    case 1:
                        FabicButton behaviourScale = new FabicButton();
                        behaviourScale.FabicColour = Data.Enums.FabicColour.Purple;
                        behaviourScale.SetTitle("Behaviour Scale", UIControlState.Normal);
                        behaviourScale.Frame = new CGRect(centerX - (behaviourScale.Frame.Width / 2), centerY - (behaviourScale.Frame.Height / 2), behaviourScale.Frame.Width, behaviourScale.Frame.Height);
                        behaviourScale.TouchDown += BehaviourScale_TouchDown;
                        cell.ContentView.Add(behaviourScale);
                        break;
                    case 2:
                        FabicButton iChooseChart = new FabicButton();
                        iChooseChart.FabicColour = Data.Enums.FabicColour.Blue;
                        iChooseChart.SetTitle("I Choose Chart", UIControlState.Normal);
                        iChooseChart.Frame = new CGRect(centerX - (iChooseChart.Frame.Width / 2), centerY - (iChooseChart.Frame.Height / 2), iChooseChart.Frame.Width, iChooseChart.Frame.Height);
                        iChooseChart.TouchDown += IChooseChart_TouchDown;
                        cell.ContentView.Add(iChooseChart);
                        break;
                    case 3:
                        FabicButton help = new FabicButton();
                        help.FabicColour = Data.Enums.FabicColour.Gray;
                        help.SetTitle("Help and Resources", UIControlState.Normal);
                        help.Frame = new CGRect(centerX - (help.Frame.Width / 2), centerY - (help.Frame.Height / 2), help.Frame.Width, help.Frame.Height);
                        help.TouchDown += Help_TouchDown;
                        cell.ContentView.Add(help);
                        break;
                    //case 4:
                    //    FabicButton aboutApp = new FabicButton();
                    //    aboutApp.FabicColour = Data.Enums.FabicColour.Gray;
                    //    aboutApp.SetTitle("About Fabic", UIControlState.Normal);
                    //    aboutApp.Frame = new CGRect(centerX - (aboutApp.Frame.Width / 2), centerY - (aboutApp.Frame.Height / 2), aboutApp.Frame.Width, aboutApp.Frame.Height);
                    //    aboutApp.TouchDown += AboutApp_TouchDown;
                    //    cell.ContentView.Add(aboutApp);
                    //    break;
                    case 4:
                        FabicButton aboutBLS = new FabicButton();
                        aboutBLS.FabicColour = Data.Enums.FabicColour.Gray;
                        aboutBLS.Font = UIFont.SystemFontOfSize(16);
                        aboutBLS.SetTitle("About Body Life Skills", UIControlState.Normal);
                        aboutBLS.Frame = new CGRect(centerX - (aboutBLS.Frame.Width / 2), centerY - (aboutBLS.Frame.Height / 2), aboutBLS.Frame.Width, aboutBLS.Frame.Height);
                        aboutBLS.TouchDown += AboutBLS_TouchDown; ;
                        cell.ContentView.Add(aboutBLS);
                        break;
                }

                cell.Tag = 200; // mark as loaded;
            }

            return cell;
        }

        private void AboutBLS_TouchDown(object sender, EventArgs e)
        {
            this?.Animate.Invoke(sender, e);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(UIStoryboard.FromName("Main", null).InstantiateViewController("blsAboutVC"), true);
        }

        private void Help_TouchDown(object sender, EventArgs e)
        {
            this?.Animate.Invoke(sender, e);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(UIStoryboard.FromName("Main", null).InstantiateViewController("appHelpVC"), true);
        }

        private void AboutApp_TouchDown(object sender, EventArgs e)
        {
            this?.Animate.Invoke(sender, e);
            //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(UIStoryboard.FromName("Main", null).InstantiateViewController("fabicAboutVC"), true);
            AboutFabicViewController ParallaxViewController = new AboutFabicViewController();
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(ParallaxViewController, true);
        }

        private void About_TouchDown(object sender, EventArgs e)
        {
            this?.Animate.Invoke(sender, e);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(UIStoryboard.FromName("Main", null).InstantiateViewController("appAboutVC"), true);
        }

        private void IChooseChart_TouchDown(object sender, EventArgs e)
        {
            this?.Animate.Invoke(sender, e);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(UIStoryboard.FromName("Main", null).InstantiateViewController("iChooseChartVC"), true);
        }

        private void BehaviourScale_TouchDown(object sender, EventArgs e)
        {
            this?.Animate.Invoke(sender, e);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(UIStoryboard.FromName("Main", null).InstantiateViewController("behaviourScaleVC"), true);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 5;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 65;//(((UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height - 480) * 65));
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
