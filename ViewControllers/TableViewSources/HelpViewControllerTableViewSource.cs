using CoreGraphics;
using Fabic.Data.Extensions;
using Foundation;
using System;
using UIKit;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public class HelpControllerTableViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        private UITableView TableSource = null;
        string CellIdentifier = "helpCell";
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Section == 1)
            {
                UITableViewCell imageCell = new UITableViewCell();

                UIImage image = new UIImage("BLSLogo-App.png");
                UIImageView imageView = new UIImageView(image);
                imageView.BackgroundColor = UIColor.Clear;
                imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                imageView.Frame = new CGRect(20, 10, tableView.Frame.Width - 40, 60);

                imageCell.AddSubview(imageView);

                return imageCell;
            }
            else
            {
                FabicHelpCell cell = (FabicHelpCell)tableView.DequeueReusableCell(CellIdentifier);
                cell.BackgroundColor = UIColor.Clear;

                TableSource = tableView;

                //---- if there are no cells to reuse, create a new one
                if (cell == null)
                {
                    //cell = new FabicHelpCell() UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
                }

                switch (indexPath.Section)
                {
                    case 2:
                        switch (indexPath.Row)
                        {
                            case 0:
                                cell.lblTitle.Text = "Fabic Publishing";
                                cell.lblSubTitle.Text = "This App is brought to you by Fabic Publishing";
                                break;
                            case 1:
                                cell.lblTitle.Text = "The Body Life Skills Program";
                                cell.lblSubTitle.Text = "Find out more about the Body Life Skills Program on this purpose-built website";
                                break;
                            case 2:
                                cell.lblTitle.Text = "Tanya Curtis";
                                cell.lblSubTitle.Text = "The Body Life Skills program is brought to you by Tanya Curtis";
                                break;
                            case 3:
                                cell.lblTitle.Text = "Fabic Behaviour Specialist Centre";
                                cell.lblSubTitle.Text = "More about Fabic, a clinic offering lasting behaviour change via the Body Life Skills program";
                                break;
                            case 4:
                                cell.lblTitle.Text = "Fabic Shop";
                                cell.lblSubTitle.Text = "Fabic posters and other products including all those used through this app and more";
                                break;
                        }
                        break;
                    case 3:
                        switch (indexPath.Row)
                        {
                            case 0:
                                cell.lblTitle.Text = "Fabic YouTube";
                                cell.lblSubTitle.Text = "Videos presented by Tanya Curtis, a practical means of living the BLS program every day";
                                break;
                            case 1:
                                cell.lblTitle.Text = "Fabic SoundCloud";
                                cell.lblSubTitle.Text = "Audio that support with lasting behaviour change";
                                break;
                            case 2:
                                cell.lblTitle.Text = "Fabic Facebook";
                                cell.lblSubTitle.Text = "For up-to-date Fabic posts, we invite you to like the Fabic Facebook page";
                                break;
                            case 3:
                                cell.lblTitle.Text = "Fabic LinkedIn";
                                cell.lblSubTitle.Text = "For up-to-date Fabic posts, we invite you to like Fabic LinkedIn";
                                break;
                            case 4:
                                cell.lblTitle.Text = "Fabic Newsletter";
                                cell.lblSubTitle.Text = "Fabic information and so much more; sign up here to the newsletter";
                                break;
                        }
                        break;
                    case 4:
                        switch (indexPath.Row)
                        {
                            case 0:
                                cell.lblTitle.Text = "Books for Our Being Series";
                                cell.lblSubTitle.Text = "Reminding you forever of the innate beauty and natural innocence of our being";
                                break;
                            case 1:
                                cell.lblTitle.Text = "Body Life Skills Series";
                                cell.lblSubTitle.Text = "Books to support with understanding the Body Life Skills and bringing it into your life";
                                break;
                        }
                        break;
                    case 5:
                        cell.lblTitle.Text = "Legal Agreement and Terms & Conditions";
                        cell.lblSubTitle.Text = "Legal conditions with using this app";
                        break;
                }

                return cell;
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            switch (section)
            {
                case 0:
                    return 0;
                case 1:
                    return 1;
                case 2:
                    return 5;
                case 3:
                    return 5;
                case 4:
                    return 2;
                case 5:
                    return 1;
                default:
                    return 0;
            }
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Section == 1)
                return 100;
            else if (indexPath.Section == 5 && indexPath.Row == 1)
                return 85;
            return 65;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            if (section == 0)
                return 110;
            return 40;
        }

        public override nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            return 0;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 6;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            switch (section)
            {
                case 1:
                    return "How to use this app demonstration";
                case 2:
                    return "About Fabic";
                case 3:
                    return "Social Media";
                case 4:
                    return "Fabic Publishing";
                case 5:
                    return "Other links";
            }
            return "";
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            if (section == 0)
            {
                UIView view = new UIView();

                string html = @"<div style=""font-family: arial; text-align:center;""><h2 style=""color: #642d88;"">The Body Life Skills program is quite simple in its theory, however often requires support in application. </h2><h3 style=""color: #0075ad; text-align:left;"">Below is list of links that will lead you to further information as a support:</h3><div>";

                UITextView txtMain = new UITextView();
                var error = new NSError();
                var docAttributes = new NSAttributedStringDocumentAttributes()
                {
                    StringEncoding = NSStringEncoding.UTF8,
                    DocumentType = NSDocumentType.HTML
                };
                txtMain.Editable = false;
                txtMain.ScrollEnabled = false;
                txtMain.BackgroundColor = UIColor.Clear;
                txtMain.TintColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple);
                txtMain.Selectable = true;
                txtMain.AttributedText = new NSAttributedString(html, docAttributes, ref error);
                txtMain.Frame = new CGRect(10, 10, tableView.Frame.Width - 20, 370);
                view.AddSubview(txtMain);

                return view;
            }
            return null;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            switch (indexPath.Section)
            {
                case 1:
                    UIApplication.SharedApplication.OpenUrl(new NSUrl("https://youtu.be/eqGHDLe_qXM"));
                    break;
                case 2:
                    switch (indexPath.Row)
                    {
                        case 0:
                            UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.fabicpublishing.com.au"));
                            break;
                        case 1:
                            UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.bodylifeskills.com"));
                            break;
                        case 2:
                            UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.tanyacurtis.com.au"));
                            break;
                        case 3:
                            UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.fabic.com.au/"));
                            break;
                        case 4:
                            UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.fabic.com.au/shop"));
                            break;
                    }
                    break;
                case 3:
                    switch (indexPath.Row)
                    {
                        case 0:
                            UIApplication.SharedApplication.OpenUrl(new NSUrl("https://www.youtube.com/channel/UClxyp32dQcFZiLv8cB3yI9Q"));
                            break;
                        case 1:
                            UIApplication.SharedApplication.OpenUrl(new NSUrl("https://soundcloud.com/fabic-pty-ltd"));
                            break;
                        case 2:
                            UIApplication.SharedApplication.OpenUrl(new NSUrl("https://www.facebook.com/fabic.com.au"));
                            break;
                        case 3:
                            UIApplication.SharedApplication.OpenUrl(new NSUrl("https://www.linkedin.com/company/fabic-functional-assessment-&-behaviour-interventions-clinic"));
                            break;
                        case 4:
                            UIApplication.SharedApplication.OpenUrl(new NSUrl("https://confirmsubscription.com/h/i/F6F3E0503132E463"));
                            break;
                    }
                    break;
                case 4:
                    switch (indexPath.Row)
                    {
                        case 0:
                            UIApplication.SharedApplication.OpenUrl(new NSUrl("https://www.fabic.com.au/product-category/books/"));
                            break;
                        case 1:
                            UIApplication.SharedApplication.OpenUrl(new NSUrl("https://www.fabic.com.au/product-category/books/"));
                            break;
                    }
                    break;
                case 5:
                    TermsAndConditionsViewController ParallaxViewController = (TermsAndConditionsViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("legalVC");
                    UINavigationController nav = new UINavigationController(ParallaxViewController);
                    ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PresentViewController(nav, true, () => { });
                    break;
            }
        }

        public void CleanUp()
        {

        }
    }
}
