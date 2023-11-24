using BigTed;
using CoreGraphics;
using Fabic.Core.Controllers;
using Fabic.Core.Models;
using Fabic.Data.Extensions;
using Foundation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public class AboutBLSViewControllerTableViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        private UITableView TableSource = null;
        List<FabicVideo> Videos;
        string CellIdentifier = "AboutBLSTableCell";

        Dictionary<nint, UITableViewCell> fabicCells = new Dictionary<nint, UITableViewCell>();

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Row > 0)
            {
                FabicImageCell cell = (FabicImageCell)tableView.DequeueReusableCell(CellIdentifier);
                TableSource = tableView;

                //---- if there are no cells to reuse, create a new one
                if (cell == null)
                {
                    cell = (FabicImageCell)new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
                }
                if (fabicCells.ContainsKey(indexPath.Row))
                    return fabicCells[indexPath.Row];

                //if (cell.Tag != 200)
                //{
                if (Videos == null)
                {
                    BTProgressHUD.Show();
                    Task.Run(() => { Videos = FabicDatabaseController.FetchAboutBLSVideos().Result; }).Wait();
                    BTProgressHUD.Dismiss();
                }

                if (Videos.Count > indexPath.Row - 1)
                {
                    cell.lblTitle.Text = Videos[indexPath.Row - 1].Name;
                    cell.lblTitle.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue);
                    cell.lblDescription.Text = Videos[indexPath.Row - 1].Description;
                    cell.lblDescription.Font = UIFont.SystemFontOfSize(12, UIFontWeight.Light);
                    cell.BackgroundColor = UIColor.Clear;
                    cell.SelectionStyle = UITableViewCellSelectionStyle.Default;
                    if (Videos[indexPath.Row - 1].ImageData != null)
                        cell.imgClip.Image = UIImage.LoadFromData(NSData.FromArray(Videos[indexPath.Row - 1].ImageData));
                    fabicCells.Add(indexPath.Row, cell);
                    cell.Tag = 200; // mark as loaded;
                    return cell;
                }
                else
                {
                    UITableViewCell moreCell = new UITableViewCell();

                    moreCell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                    UILabel label = new UILabel();
                    label.Frame = new CGRect(0, 34, tableView.Frame.Width, 34);
                    label.TextAlignment = UITextAlignment.Center;
                    label.Lines = 0;
                    label.Font = UIFont.BoldSystemFontOfSize(17);
                    label.TextColor = UIColor.White;
                    label.BackgroundColor = UIColor.Clear;
                    moreCell.BackgroundColor = UIColor.Black.FabicColour(Data.Enums.FabicColour.Purple);
                    label.Text = "View more at www.bodylifeskills.com";
                    moreCell.AddSubview(label);
                    fabicCells.Add(indexPath.Row, moreCell);
                    moreCell.Tag = 200; // mark as loaded;
                    return moreCell;
                }
            }
            else
            {
                UITableViewCell cell = new UITableViewCell(new CGRect(0, 0, tableView.Frame.Width, 270 + 170));
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                cell.BackgroundColor = UIColor.Clear;

                UIImage image = new UIImage("BLSLogo-App.png");
                UIImageView imageView = new UIImageView(image);
                imageView.BackgroundColor = UIColor.Clear;
                imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                imageView.Frame = new CGRect(10, 10, tableView.Frame.Width - 20, 169);

                string html = @"<div style=""font-family: arial""><p style=""text-align:center; color: #642d88;""><strong>A simple and powerful program supporting true and lasting behaviour change.</strong></p><p>The Body Life Skills series offers a simple, practical, user friendly and life changing 3-step process bringing about lasting behaviour change</p><p>To create lasting behaviour change, we must be willing to identify the reason why the behaviour is occuring.</p><p>The Body Life Skills program understands that all unwanted behaviour is a result of anxiety first.</p><p style=""text-align:center; color: #0075ad;""><em>""Anxiety is not feeling equipped to respond to<br/>what is in front of you""</em></p><p style=""text-align:center; font-size:10pt;""><strong>Anxiety</strong> ~ is what is felt in and expressed from the BODY</p><p style=""text-align:center; font-size:10pt;""><strong>Not feeling equipped</strong> ~ is not feeling like you have the required <strong>SKILLS</strong></p><p style=""text-align:center; font-size:10pt;""><strong>What is in front of you ~ LIFE</strong></p></div>";

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
                txtMain.Frame = new CGRect(10, 160, tableView.Frame.Width - 20, 270);
                cell.AddSubview(imageView);
                cell.AddSubview(txtMain);
                return cell;
            }
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Row <= Videos.Count)
            {
                //  XCDYouTubeVideoPlayerViewController videoViewController = new XCDYouTubeVideoPlayerViewController().IniWithVideoIdentifier(new NSString(Videos[indexPath.Row - 1].URL));
                ////  videoViewController.PreferredVideoQualities = new [];
                //  ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PresentModalViewController(videoViewController, true);
                UIApplication.SharedApplication.OpenUrl(new NSUrl(Videos[indexPath.Row - 1].URL));
            }
            else
            {
                // view more online
                UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.bodylifeskills.com"));
            }
        }

        IDictionary<nint, nfloat> rowHeightDictionary = new Dictionary<nint, nfloat>();
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Row == 0)
                return 270 + 170;
            return 108;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 0;
        }

        public override nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            return 0;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (Videos == null)
            {
                BTProgressHUD.Show();
                Task.Run(() => { Videos = FabicDatabaseController.FetchAboutBLSVideos().Result; }).Wait();
                BTProgressHUD.Dismiss();
            }

            return Videos.Count + 2;
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            UIView header = new UIView();
            return header;
        }

        public void CleanUp()
        {

        }
    }
}
