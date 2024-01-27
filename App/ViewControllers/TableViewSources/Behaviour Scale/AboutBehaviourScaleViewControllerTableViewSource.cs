using CoreGraphics;
using Fabic.Data.Extensions;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public class AboutBehaviourScaleViewControllerTableViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        private UITableView TableSource = null;
        nint rowShown = -1;
        string CellIdentifier = "AboutBehaviourTableCell";

        Dictionary<nint, FabicTextCell> fabicCells = new Dictionary<nint, FabicTextCell>();

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            FabicTextCell cell = (FabicTextCell)tableView.DequeueReusableCell(CellIdentifier);
            TableSource = tableView;

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            {
                //cell = new FabicTextCell();
            }

            cell.BackgroundColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple).ColorWithAlpha(0.08f);
            cell.Layer.BorderWidth = 1.5f;
            cell.Layer.BorderColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple).ColorWithAlpha(0.15f).CGColor;
            cell.Layer.CornerRadius = 8f;

            if (!fabicCells.ContainsKey(indexPath.Section))
                fabicCells.Add(indexPath.Section, cell);
            else
                fabicCells[indexPath.Section] = cell;

            switch (indexPath.Section)
            {
                case 0:
                    cell.TextToShow = @"<div style=""font-family: arial"">The Behaviour/Anxiety Scale:</h1><div><ol style=""font-size: 10pt""><li>Acts as a communication tool that assists us to learn to identify and understand how our <span style=""color: #0075ad;""><strong>body</strong></span> is communicating its experience of <span style=""color: #0075ad;""><strong>life</strong></span>.</li><li>Offers a form of communication that is interpreted the same way by <span style=""text-decoration: underline;"">ALL</span> people, thus increasing successful communication and reducing unnecessary conflict and/or unnecessary escalation of unwanted behaviours.</li><li>Increases our understanding of how a person is experiencing their day-to-day life.</li><li>Visually depicts the different levels from where each person either responds or reacts on a scale with five separate levels.</li></ol><p>&nbsp;</p><p style=""text-align: center;""><img src=""https://fabicapp.fierydevelopment.com:8080/resources/bls-chart-colours.png"" height=""300px""/></p><p><em>Everyone fluctuates between&nbsp;Level 1 (calm and preferred behaviours)&nbsp;and&nbsp;Level 5 (meltdowns)&nbsp;on a regular basis.</em></p><p>The Behaviour Scale makes this previously abstract and mysterious fluctuation visual, concrete and personalised for everyone and thus ensures that many people have&nbsp;<strong>exactly </strong>the&nbsp;<strong>same message</strong>&nbsp;about a person who has a completed Behaviour/Anxiety Scale.&nbsp;</p><p><span style=""color: #0075ad;""><strong>At some stage everyone has had a meltdown and reached Level 5. But meltdown behaviours look different for each person.&nbsp;</strong></span></p><p>For example: some people may cry, withdraw, overeat, refuse to eat, use aggression, self-harm, harm others, use drugs or alcohol, verbally attack others, destruct property, go very quiet or refuse to participate in anything. In fact, a meltdown is any excess emotional response. All meltdowns are a result of escalated anxiety; escalated anxiety because the person perceives they are not equipped to manage what is currently presented to them in life ... their challenging situations.&nbsp;</p><p>The key purpose of the Fabic Behaviour/Anxiety Scale is to bring <em><strong>understanding</strong></em> to a person&rsquo;s unwanted or non-preferred behaviours.</p><p>This scale offers a visual means of communication that is accessible to everyone across all settings. This cohesive approach brings a simple and concrete terminology to describe the first two steps of the Body Life Skills program:</p><ol><li><strong>Step 1: Body</strong></li><li><strong>Step 2: Life</strong></li></ol></div></div>";
                    break;
                case 1:
                    cell.TextToShow = @"<div style=""font-family: arial""><p>The key purpose of the Fabic Behaviour/Anxiety Scale is to bring understanding to any body using any unwanted behaviour. <em><span>With understanding we free the person from judgment and thus can use the behaviour scale for three key roles in understanding and changing behaviour</span></em>. Using the Fabic Behaviour Scale effectively provides:</p><ol><li><strong>A form of communication</strong></li><li><strong>An assessment tool</strong></li><li><strong>An indicator of when to implement behaviour change interventions</strong></li></ol><br/><h3><strong><span style=""color: #0075ad;"">A form of communication:</span></strong></h3><p>As a form of communication, the behaviour scale offers:</p><ul><li>Multiple people to bring the same understanding to one person&rsquo;s body and their experience of life</li><li>One person to brings understanding to other people&rsquo;s body their experience of life</li><li>One person to bring self-awareness and have a self-monitoring tool of their own body and how it is experiencing life</li></ul><br/><h3><span style=""color: #0075ad;"">An assessment tool:</span></h3><p>As an assessment tool the behaviour scale is used to identify the parts of life each person is either responding or reacting to. &nbsp;When using the Behaviour Scale as an assessment tool, we simply ask:</p><p style=""text-align: center;""><em><strong>&ldquo;What colour did you go to when &hellip;?&rdquo;</strong></em></p><p>&nbsp;When asking <em>&ldquo;what colour did you go to when?&rdquo;</em> there are only five possible answers and thus the question is not as overwhelming as could be if an open-ended question was asked. &nbsp;With the above closed question there are only five possible responses:</p><ol><li><span style=""color: #0075ad;""><strong>Code blue</strong></span></li><li><strong><span style=""color: #00a453;"">Code green</span></strong></li><li><strong><span style=""color: #f1c92d;"">Code yellow</span></strong></li><li><strong><span style=""color: #f36a30;"">Code orange</span></strong></li><li><strong><span style=""color: #cd212b;"">Code red</span></strong></li></ol><br/><p>This leads to a clear concrete list of the events of life that have each individual person at code blue, green, yellow, orange and/or red &hellip; thus a clear list of the parts of life a person is yet to develop skills to self-master.</p><br/><h3><span style=""color: #0075ad;""><strong>An indicator of when to implement behaviour change:</strong></span></h3><p>A completed Behaviour/Anxiety Scale provides a concrete indicator of the exact behaviours the body is expressing at code blue, code green, code yellow and code red. When individualised in this way, it provides a means of identifying when to implement healing and skills building strategies.&nbsp;</p><p><strong>A common error with behaviour change techniques is that they are offered once a person has already escalated to code orange or code red, but at this stage it is too late to bring about lasting behaviour change</strong>; <em>at code orange and code red we have tools to support with <span style=""text-decoration: underline;"">relief</span> of the current symptoms but none that will bring about <span style=""text-decoration: underline;"">lasting behaviour change</span></em>.</p><p>Lasting behaviour change will come about when a person is provided with the opportunity to learn, practise and implement new skills to respond to the aspect of life they currently do not feel equipped to respond to. As is the case for us all, our ability to learn new skills and implement existing skills is at its peak when we are at code blue, yet this ease disappears as a body progresses up the behaviour scale. Below is a scale that highlights our ability to learn, practise and implement new skills at each colour:</p><p style=""padding-left: 30px;""><span style=""color: #0075ad;""><strong>Code blue</strong></span> &ndash; <span style=""color: #0075ad;"">best opportunity to learn, practise and implement new and existing skills</span></p><p style=""padding-left: 30px;""><strong><span style=""color: #00a453;"">Code green</span></strong> &ndash; <span style=""color: #00a453;"">skills learning, practising and implementing is &lsquo;a little bit&rsquo; diminished</span></p><p style=""padding-left: 30px;""><span style=""color: #f1c92d;""><strong>Code yellow</strong></span> &ndash; <span style=""color: #f1c92d;"">skills learning, practising and implementing is &lsquo;more&rsquo; diminished</span></p><p style=""padding-left: 30px;""><strong><span style=""color: #f36a30;"">Code orange</span></strong> &ndash; <span style=""color: #f36a30;"">skills learning, practising and implementing is &lsquo;a lot&rsquo; diminished</span></p><p style=""padding-left: 30px;""><strong><span style=""color: #cd212b;"">Code red</span></strong> &ndash;<span style=""color: #cd212b;""> skills learning, practising and implementing is basically non-existent</span></p><p style=""color: #642d88; text-align: center;""><strong>Thus the behaviour scale offers a very simple, practical and concrete measure to support people to know when to implement behaviour change techniques.</strong></p></div>";
                    break;
                case 2:
                    cell.TextToShow = @"<div style=""font-family: arial""><p style=""color: #642d88; ""><span style=""color: #333333;"">On the right-hand side of the scale we list the individual&rsquo;s unique behaviours, words, thoughts and feelings used when escalating from code blue up to code red. This list provides a concrete visual that means all people have the exact same understanding of what a body is expressing at any particular colour:</span></p><p style=""text-align: center;""><img src=""https://fabicapp.fierydevelopment.com:8080/resources/bls-chart-body-explanation.png"" height=""310px""/></p><p>For example, Step 1 on a completed Behaviour/Anxiety Scale might look like:</p><p style=""text-align: center;""><img src=""https://fabicapp.fierydevelopment.com:8080/resources/bls-chart-body-examples.PNG"" height=""590px"" /></p></div>";
                    break;
                case 3:
                    cell.TextToShow = @"<div style=""font-family: arial""><p style=""color: #642d88;""><span style=""color: #333333;"">On the left-hand side of the scale we list the aspects of life that we or they either have or have not developed the skills to respond to which are unique to each one of us. These are the specific aspects of life that will have someone at:</span></p><p style=""text-align: center;""><img src=""https://fabicapp.fierydevelopment.com:8080/resources/bls-chart-life-explanation.png"" height=""310px""/></p><p>For example, Step 2 on a completed Behaviour/Anxiety Scale might look like:</p><p style=""text-align: center;""><img src=""https://fabicapp.fierydevelopment.com:8080/resources/bls-chart-life-examples.PNG"" height=""590px""/></p></div>";
                    break;
            }

            return cell;
        }

        IDictionary<nint, nfloat> rowHeightDictionary = new Dictionary<nint, nfloat>();
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            int adjustment = 40;
            switch (rowShown)
            {
                case 0:
                    adjustment = 0;
                    break;
                case 1:
                    adjustment = 580;
                    break;
                case 2:
                    adjustment = 800;
                    break;
                case 3:
                    adjustment = 800;
                    break;
                case 4:
                    adjustment = 500;
                    break;
            }
            if (rowShown == indexPath.Section)
            {
                nfloat d = 0;
                if (!rowHeightDictionary.ContainsKey(rowShown))
                {
                    d = fabicCells[indexPath.Section].AttributedTextToShow.GetBoundingRect(new CGSize(fabicCells[indexPath.Section].TextBoxWidth, 1000), NSStringDrawingOptions.UsesLineFragmentOrigin, null).Height + adjustment;
                    rowHeightDictionary.Add(rowShown, d);
                }
                else
                    d = rowHeightDictionary[rowShown];

                return d;
            }

            return 0;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 60;
        }

        public override nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            return 0;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 4;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 1;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            switch (section)
            {
                case 0:
                    return "Video Demo";
                case 1:
                    return "The Behaviour Scale";
                case 2:
                    return "Step 1 — Body";
                case 3:
                    return "Step 2 — Life";
                case 4:
                    return "Using The Behaviour Scale";

            }
            return string.Empty;
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            UIView header = new UIView();

            FabicButton cellHeader = new FabicButton();
            cellHeader.FabicColour = Data.Enums.FabicColour.Purple;
            cellHeader.Frame = new CGRect(5, 0, tableView.Frame.Width - 10, 60);
            cellHeader.SetTitle(TitleForHeader(tableView, section), UIControlState.Normal);
            cellHeader.TitleLabel.Lines = 3;
            cellHeader.TitleLabel.TextAlignment = UITextAlignment.Center;
            cellHeader.TouchDown += CellHeader_TouchDown;
            cellHeader.Tag = section;
            header.AddSubview(cellHeader);

            return header;
        }

        void CellHeader_TouchDown(object sender, EventArgs e)
        {
            bool minimiseRow = false;

            if (rowShown == -1 || (rowShown != ((FabicButton)sender).Tag))
                rowShown = ((FabicButton)sender).Tag;
            else
                minimiseRow = true;

            NSIndexPath path = NSIndexPath.FromRowSection(0, rowShown);

            if (minimiseRow)
                rowShown = -1;

            List<NSIndexPath> paths = new List<NSIndexPath>(); paths.Add(path);
            if (TableSource != null && rowShown > 0) { TableSource.ReloadRows(paths.ToArray(), UITableViewRowAnimation.Fade); TableSource.ScrollToRow(path, UITableViewScrollPosition.Top, true); }
            if (rowShown == 0)
                UIApplication.SharedApplication.OpenUrl(new NSUrl("https://youtu.be/VGRswwR7Kog"));
        }

        public void CleanUp()
        {

        }
    }
}
