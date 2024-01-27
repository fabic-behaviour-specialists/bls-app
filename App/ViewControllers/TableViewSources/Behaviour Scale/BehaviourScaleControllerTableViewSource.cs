using CoreGraphics;
using Curse;
using Fabic.Core.Controllers;
using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Fabic.Data.Extensions;
using Foundation;
using System;
using UIKit;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public class BehaviourScaleControllerTableViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        string CellIdentifier = "TableCell";
        FabicButton behaviourScale;
        FabicButton library;
        FabicButton fabic;
        FabicButton archived;
        FabicButton aboutApp;
        FabicButton help3;

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
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                cell.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0);
                cell.TranslatesAutoresizingMaskIntoConstraints = false;

                double centerX = tableView.Frame.Width / 2;
                double centerY = 60 / 2;

                switch (indexPath.Row)
                {
                    case 0:
                        aboutApp = new FabicButton();
                        aboutApp.FabicColour = Data.Enums.FabicColour.Gray;
                        aboutApp.SetTitle("About Behaviour Scale", UIControlState.Normal);
                        aboutApp.Frame = new CGRect(centerX - (aboutApp.Frame.Width / 2), centerY - (aboutApp.Frame.Height / 2), aboutApp.Frame.Width, aboutApp.Frame.Height);
                        aboutApp.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
                        aboutApp.TitleLabel.Lines = 0;
                        aboutApp.TitleLabel.TextAlignment = UITextAlignment.Center;
                        aboutApp.TouchDown += AboutApp_TouchDown; ;
                        cell.ContentView.Add(aboutApp);
                        break;
                    case 1:
                        behaviourScale = new FabicButton();
                        behaviourScale.FabicColour = Data.Enums.FabicColour.Purple;
                        behaviourScale.SetTitle("Add Scale", UIControlState.Normal);
                        behaviourScale.Frame = new CGRect(centerX - (behaviourScale.Frame.Width / 2), centerY - (behaviourScale.Frame.Height / 2), behaviourScale.Frame.Width, behaviourScale.Frame.Height);
                        behaviourScale.TouchDown += BehaviourScale_TouchDown;
                        cell.ContentView.Add(behaviourScale);
                        break;
                    case 2:
                        library = new FabicButton();
                        library.FabicColour = Data.Enums.FabicColour.Purple;
                        library.SetTitle("My Library", UIControlState.Normal);
                        library.Frame = new CGRect(centerX - (library.Frame.Width / 2), centerY - (library.Frame.Height / 2), library.Frame.Width, library.Frame.Height);
                        library.TouchDown += Library_TouchDown;
                        cell.ContentView.Add(library);
                        break;
                    case 3:
                        fabic = new FabicButton();
                        fabic.FabicColour = Data.Enums.FabicColour.Purple;
                        fabic.SetTitle("Fabic Library", UIControlState.Normal);
                        fabic.Frame = new CGRect(centerX - (fabic.Frame.Width / 2), centerY - (fabic.Frame.Height / 2), fabic.Frame.Width, fabic.Frame.Height);
                        fabic.TouchDown += Fabic_TouchDown;
                        cell.ContentView.Add(fabic);
                        break;
                    case 4:
                        archived = new FabicButton();
                        archived.FabicColour = Data.Enums.FabicColour.Purple;
                        archived.SetTitle("Archived Charts", UIControlState.Normal);
                        archived.Frame = new CGRect(centerX - (archived.Frame.Width / 2), centerY - (archived.Frame.Height / 2), archived.Frame.Width, archived.Frame.Height);
                        archived.TouchDown += Archived_TouchDown;
                        cell.ContentView.Add(archived);
                        break;
                    case 5:
                        help3 = new FabicButton();
                        help3.FabicColour = Data.Enums.FabicColour.Gray;
                        help3.SetTitle("Help", UIControlState.Normal);
                        help3.Frame = new CGRect(centerX - (help3.Frame.Width / 2), centerY - (help3.Frame.Height / 2), help3.Frame.Width, help3.Frame.Height);
                        cell.ContentView.Add(help3);
                        break;
                }

                cell.Tag = 200; // mark as loaded;
            }

            return cell;
        }

        void HandleAction(Curse.CRSAlertView obj)
        {
            try
            {
                // Do something here on pre
                string input = obj.Input.Text;

                BehaviourScale bs = new BehaviourScale();
                bs.Archived = false;
                bs.Description = string.Empty;
                bs.FabicExample = false;
                bs.Name = input;

                FabicDatabaseController.SaveOrUpdateBehaviourScale(bs);

                UIViewController controller = UIStoryboard.FromName("Main", null).InstantiateViewController("BehaviourScaleViewIdentifier");
                ((BehaviourScaleViewController)controller).BehaviourScale = bs;
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(controller, true);
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
            }
        }

        void AboutApp_TouchDown(object sender, EventArgs e)
        {
            UIViewController vc = UIStoryboard.FromName("Main", null).InstantiateViewController("behaviourScaleAboutVC");
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(vc, true);
        }

        private void Archived_TouchDown(object sender, EventArgs e)
        {
            UIViewController vc = UIStoryboard.FromName("Main", null).InstantiateViewController("behaviourScaleArchivedTableVC");
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(vc, true);
        }

        private void Fabic_TouchDown(object sender, EventArgs e)
        {
            UIViewController vc = UIStoryboard.FromName("Main", null).InstantiateViewController("behaviourScaleFabicTableVC");
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(vc, true);
        }

        private void Library_TouchDown(object sender, EventArgs e)
        {
            UIViewController vc = UIStoryboard.FromName("Main", null).InstantiateViewController("behaviourScaleLibraryTableVC");
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(vc, true);
        }

        private void BehaviourScale_TouchDown(object sender, EventArgs e)
        {
            CRSAlertView alert = new CRSAlertView();
            alert.TintColor = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Purple);
            alert.Title = "Add a Behaviour Scale";
            alert.Message = "What would you like to call your Behaviour Scale?";
            alert.Image = new UIImage("butterfly.png");

            var action = new CRSAlertAction
            {
                Text = "Cancel",
                Highlighted = false,
                TintColor = UIColor.Black,
                DidSelect = (alert2) =>
                {
                    // Do something here on press
                }
            };

            var input2 = new CRSAlertInput
            {
                Placeholder = "Name of Behaviour Scale",
                Text = string.Empty,
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Purple),
                OpenAutomatically = true
            };

            var action2 = new CRSAlertAction
            {
                Text = "Save",
                Highlighted = true,
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Purple),
                DidSelect = HandleAction
            };

            alert.Input = input2;
            alert.Actions = new CRSAlertAction[] { action, action2 };
            alert.Show();
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 5;
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

        /// <summary>
        /// Unhightlights the items.
        /// </summary>
        public void UnhightlightItems()
        {
            behaviourScale.Unhighlight();
            library.Unhighlight();
            fabic.Unhighlight();
            archived.Unhighlight();
            aboutApp.Unhighlight();
            //help3.Highlighted = false;
        }

        public void CleanUp()
        {

        }
    }
}
