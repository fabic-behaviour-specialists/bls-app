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
    public class AboutFabicViewControllerTableViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        private UITableView TableSource = null;
        List<AboutFabicVideo> Videos;

        Dictionary<nint, UITableViewCell> fabicCells = new Dictionary<nint, UITableViewCell>();

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Row > 0)
            {
                UITableViewCell cell = new UITableViewCell();
                TableSource = tableView;

                //- if there are no cells to reuse, create a new one
                //if (cell == null)
                //{
                //    cell = (FabicImageCell)new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
                //}
                if (fabicCells.ContainsKey(indexPath.Row))
                    return fabicCells[indexPath.Row];

                //if (cell.Tag != 200)
                //{
                if (Videos == null)
                {
                    BTProgressHUD.Show();
                    Task.Run(() => { Videos = FabicDatabaseController.FetchAboutFabicVideos().Result; }).Wait();
                    BTProgressHUD.Dismiss();
                }

                if (Videos.Count > indexPath.Row - 1)
                {
                    UILabel title = new UILabel();
                    title.Frame = new CGRect(15, 5, tableView.Frame.Width, 26);
                    title.Lines = 2;
                    title.TextColor = UIColor.Black.FabicColour(Data.Enums.FabicColour.Purple);
                    title.Font = UIFont.BoldSystemFontOfSize(15);
                    title.Text = Videos[indexPath.Row - 1].Name;
                    cell.AddSubview(title);

                    UIImage imageSource = (Videos[indexPath.Row - 1].ImageData != null ? UIImage.LoadFromData(NSData.FromArray(Videos[indexPath.Row - 1].ImageData)) : new UIImage());
                    UIImageView image = new UIImageView(imageSource);
                    image.ContentMode = UIViewContentMode.ScaleAspectFit;
                    image.ClipsToBounds = image.Layer.MasksToBounds = true;
                    image.Frame = new CGRect(tableView.Frame.Width - 145, 28, 105, 70);
                    cell.AddSubview(image);

                    UILabel description = new UILabel();
                    description.Frame = new CGRect(15, 28, tableView.Frame.Width - image.Frame.Width - 65, 75);
                    description.Lines = 100;
                    description.Font = UIFont.GetPreferredFontForTextStyle(UIFontTextStyle.Subheadline).WithSize(11);
                    description.Text = Videos[indexPath.Row - 1].Description;
                    description.TextAlignment = UITextAlignment.Justified;
                    cell.AddSubview(description);

                    fabicCells.Add(indexPath.Row, cell);
                    cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                    cell.Tag = 200; // mark as loaded;
                    cell.TintColor = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Blue);
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
                    label.Text = "View more at www.fabic.com.au";
                    moreCell.AddSubview(label);
                    fabicCells.Add(indexPath.Row, moreCell);
                    moreCell.Tag = 200; // mark as loaded;
                    return moreCell;
                }
            }
            else
            {
                UITableViewCell cell = new UITableViewCell(new CGRect(0, 0, tableView.Frame.Width, 270 + 190));
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                cell.BackgroundColor = UIColor.Clear;

                UIImage image = new UIImage("fabic-logo.png");
                UIImageView imageView = new UIImageView(image);
                imageView.BackgroundColor = UIColor.Clear;
                imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                imageView.Frame = new CGRect(10, 10, tableView.Frame.Width - 20, 155);

                string html = @"<div style=""font-family: arial""><p style=""text-align:center; color: #642d88;""><strong>The heart of Fabic’s philosophy is to always meet each person for who they are, and not what they do.</strong></p><p>Tanya Curtis founded Fabic in 2006 with the vision of supporting people to understand and change unwanted behaviours while at the same time valuing each person's unique qualities.</p><p style=""text-align:center; color: #642d88;""><strong>Brisbane - Gold Coast - Lismore - Online - On-site</strong></p><p>Fabic (which stands for Functional Assessment & Behaviour Interventions Centre) is a Multi-Disciplinary Behaviour Specialist clinic that offers a complete range of services to support children, teenagers and adults to develop the understanding and life skills required to live their full potential. In addition to face-to-face workshops and sessions, Fabic's services are also available online and thus are available to all no matter where you are.</p><p style=""text-align:center; font-size:10pt;""><strong>We support people to understand and change unwanted behaviours.</strong></strong></p><p style=""text-align:center; font-size:10pt;""><strong style=""color: #0075ad;"">Behaviour is not who we are it is what we DO. You can learn to change what behaviour you choose ... but no matter what, you cannot change that you WILL ALWAYS be the awesome, amazing and loveable you.</strong></p></div>";

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
                txtMain.Frame = new CGRect(10, 159, tableView.Frame.Width - 20, 390);
                cell.AddSubview(imageView);
                cell.AddSubview(txtMain);
                return cell;
            }
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Row <= Videos.Count)
            {
                //    XCDYouTubeVideoPlayerViewController videoViewController = new XCDYouTubeVideoPlayerViewController().IniWithVideoIdentifier(new NSString(Videos[indexPath.Row - 1].URL));
                //    //videoViewController.PreferredVideoQualities = new [];
                //    ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PresentModalViewController(videoViewController, true);
                UIApplication.SharedApplication.OpenUrl(new NSUrl(Videos[indexPath.Row - 1].URL));
            }
            else
            {
                // view more online
                UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.fabic.com.au"));
            }
        }

        IDictionary<nint, nfloat> rowHeightDictionary = new Dictionary<nint, nfloat>();
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Row == 0)
                return 360 + 210;
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
                Task.Run(() => { Videos = FabicDatabaseController.FetchAboutFabicVideos().Result; }).Wait();
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
