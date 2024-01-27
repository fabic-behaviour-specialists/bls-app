using CoreGraphics;
using Fabic.Data.Extensions;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public class AboutAppViewControllerTableViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        private UITableView TableSource = null;
        nint rowShown = -1;
        string CellIdentifier = "AboutTableCell";

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

            cell.BackgroundColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue).ColorWithAlpha(0.08f);
            cell.Layer.BorderWidth = 1.5f;
            cell.Layer.BorderColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue).ColorWithAlpha(0.15f).CGColor;
            cell.Layer.CornerRadius = 8f;

            if (!fabicCells.ContainsKey(indexPath.Section))
                fabicCells.Add(indexPath.Section, cell);
            else
                fabicCells[indexPath.Section] = cell;

            switch (indexPath.Section)
            {
                case 1:
                    cell.TextToShow = @"<div style=""font-family: arial""><p style=""color: #642d88;""><strong>Fabic’s Body Life Skills program offers a practical application to bring about lasting behaviour change for people of all ages.</strong></p><p><strong>We <em>all</em> use behaviours we would prefer not to use … thus we all use unwanted behaviours; albeit in varying forms and intensity.</strong></p><p style=""text-align:center; color: #0075ad;"">In simplicity this app supports people to</p><p style=""text-align:center; color: #0075ad; font-size:10pt;""><strong><em>""Understand and bring lasting change to behaviours used by self or any other person"".</em></strong></p></div>";
                    break;
                case 2:
                    cell.TextToShow = @"<div style=""font-family: arial""><h2 style=""color: #642d88;"">The <strong><a href=""http://www.bodylifeskills.com"">Body Life Skills</a></strong>&nbsp;program is simply a 3-step process that is based on supporting people to:</h2><ol><li><em>Understand behaviour</em> and then,</li><li><em>Develop</em> the tools to bring about <span style=""text-decoration: underline;""><strong>lasting</strong></span> behaviour change.</li></ol></p><p>This is achieved via:<br/><ul  style=""color: #0075ad;""><li>Step 1 - <strong>BODY</strong></li><li>Step 2 - <strong>LIFE</strong></li><li>Step 3 - <strong>SKILLS</strong></li></ul></p><p style=""color: #642d88;""><strong>In the Body Life Skills program we understand that all unwanted or non-preferred behaviour is a result of anxiety first.</strong></p><p>The definition for anxiety the Body Life Skills Program embraces is:</p><p style=""text-align: center; font-size:9.5pt; color: #642d88;""><em>&ldquo;Anxiety is not feeling equipped to respond to what is in front of you.&rdquo; </em><em></em></p><p>A micro-analysis of this definition understands that:<br/><ul style=""list-style-type: circle;""><li><em>Anxiety</em> ~ is what is <span style=""text-decoration: underline;"">felt in</span> and <span style=""text-decoration: underline;"">expressed from</span> the <strong style=""color: #0075ad;"">body</strong></li><li><em>Not feeling equipped</em> ~ is feeling that you don&rsquo;t have the required <strong style=""color: #0075ad;"">skills</strong></li><li><em>What is in front of you</em> ~ is <strong style=""color: #0075ad;"">life</strong></li></ul></p><h3>With this understanding we embrace that:</h3><ol><li><strong>All</strong> unwanted behaviours, words, thoughts or feelings that come from any person&rsquo;s <em><strong>body</strong></em>, occur as a result of them perceiving that they do not have the required <em><strong>skills</strong></em> to respond to what <strong><em>life</em></strong> has presented them at any given moment.</li><li>When a person perceives they have the required <strong>skills</strong> to respond to what <strong>life</strong> is presenting, their <strong>body</strong> is<em> more likely to</em> express using wanted or preferred behaviours, words, thoughts and feelings.</li></ol><br/><p style=""text-align: center;font-size:10pt;color: #0075ad;""><strong>Thus the Body Life Skills program is a never ending cycle as we will constantly be presented with aspects of life where further skill development would be supportive</strong></p></div>";
                    break;
                case 3:
                    cell.TextToShow = @"<div style=""font-family: arial""><h2 style=""color: #642d88;""><strong>The Body Life Skills program is based on the premise that all behaviour happens for a reason. Behaviour carries two key components: (1) the form and (2) the function</strong></h2><p style=""font-size: 9pt"">The <i><strong>‘form’</strong></i> of behaviour is what is expressed from the body. The form expressed may be actions, reactions, words, thoughts and/or feelings; these are commonly understood under the one umbrella term of <i>‘behaviour’</i>. Sometimes the form (what is expressed from the body) is considered ‘wanted’ or ‘preferred’ while other expressions are judged as ‘unwanted’ or ‘non-preferred’.</p><p style=""font-size: 11pt""><strong>The Body Life Skills program is not interested in the <i>form</i> of behaviour.</strong> Why? Because what a behaviour looks like does not tell us why the behaviour is happening. Rather, the Body Life Skills program places emphasis on the <i>why</i> – the function/reason.</p><p style=""font-size: 9pt"">The function of the behaviour can be understood as the ‘reason why the behaviour happens in the first place’. As much as people may try to debate that a particular behaviour occurred randomly, unprompted and for absolutely no reason, I hold strongly to the premise that every single behaviour happens for a reason.</p><p style=""font-size: 10pt""><strong style=""color: #642d88;"">Yes, it is certainly true that the reason may be difficult to identify. I find it perfectly suitable to say <i>“I don’t yet know what the reason is”</i>; however, it is incorrect to say <i>“there was no reason”</i>.</strong></p><p style=""font-size: 10pt"">The reason for behaviour can be described simply in one word: LIFE. Life is either something a person feels they have the required skills to respond to, or they don’t. When we don’t feel equipped to respond to what life is presenting the body will either respond (using wanted behaviours) or react (using unwanted behaviours).</p><p style=""font-size: 9pt"">Thus the reason for unwanted behaviour is always that a person does not feel completely equipped to respond to an aspect of life. Hence, when unwanted behaviour is being used by anyone, including myself, the question I ask to identify the reason (i.e. the function) is:</p><p style=""font-size: 11pt; text-align:center""><strong>I wonder why?</strong></p><p style=""font-size: 9pt"">I wonder what is the part of life that the person feels they do not have all the required skills to respond to?</p><p style=""font-size: 10pt;color: #0075ad;"">It is important here to distinguish the words <i>“that the person feels they don’t have”</i>. It may be that they do have the skills, or we may think they have the skills, however, if that person does not perceive this for themselves, then that part of life does become the trigger. This point is crucial and needs to be embraced.</p></div>";
                    break;
                case 4:
                    cell.TextToShow = @"<div style=""font-family: arial""><p style=""font-size: 10pt"">Prior to understanding and changing behaviours we must first understand that <strong>behaviour is NOT who a person is but what they do.</strong></p><p style=""color: #0075ad;text-align:center;"">All unwanted behaviours are being used by people who, at the core of their being, are <strong>amazing beings!</strong></p><p style=""color: #642d88; font-size:10pt""><strong>A key principle of the Body Live Skills program is that:</strong></p><p style=""text-align: center; font-size:10pt;""><em>&ldquo;We are human beings and not human doings.&rdquo;</em></p><p><strong style=""font-size:9pt;"">The <em>being</em> is and will always be:</strong><br/>Awesome, amazing, loveable and absolute pure love and joy.</p><p><strong style=""font-size:9pt;"">The <em>doing</em> is:</strong><br/>Simply what a person does. Sometimes what a person does is wanted, preferred or as we say THUMBS UP and other times what is done is unwanted, non-preferred or THUMBS DOWN</p><p style=""text-align: center; font-size:11pt; color: #642d88;"">The Body Life Skills program embraces that at the core of each person is an awesome, amazing, lovable being.</p><p>In fact, the Body Life Skills program embraces that <strong style=""color: #0075ad;"">WE ARE ALL</strong> awesome, amazing and loveable beings.</p><p><strong>We understand that what people &lsquo;do&rsquo; may not always match this amazingness and thus we introduce understanding to identify the reason behind the unwanted, non-preferred and thumbs down &lsquo;doing&rsquo;.</strong></p><p>The Body Life Skills program embraces that all behaviour happens for a reason and thus we understand that there is a part of <strong style=""color: #0075ad;"">life</strong> that is contributing to a person&rsquo;s <strong style=""color: #0075ad;"">body</strong> (including ourselves) &lsquo;doing&rsquo; behaviours that don&rsquo;t match the awesome, amazing, loveable being we innately know them (or ourselves) to be.</p><p style=""font-size:10pt"">However, we all have circumstances presented in our lives, situations that often hurt, that we do not feel equipped to manage. It is in response to these situations that we use behaviours that do not leave ourselves or other people feeling the amazingness that we naturally are.</p><p style=""text-align: center;""><strong style=""color: #0075ad;"">Our behaviours are not who we are! Behaviour is what we do and thus all behaviours can be changed with the support of a willing teacher and a willing student of life.</strong></p></div>";
                    break;
                case 5:
                    cell.TextToShow = @"<div style=""font-family: arial""><p style=""font-size: 11pt"">The quote, <em>""Understanding and Judgement Cannot Exist Together""</em> is self-explanatory; however, it must not be brushed over or ever overlooked as it carries an essential key to bringing lasting behaviour change.</p><p style=""font-size: 10pt;color: #642d88;""><strong>The key message here is:</strong></p><p style=""text-align: center;font-size: 10ptcolor: #642d88;""><em>Judgment closes the door to understanding.</em><br /><em>Lasting behaviour change requires understanding.</em><br /><em>Thus, judgment prevents lasting behaviour change.</em></p><p>By way of explanation: as mentioned previously, there are three key elements to this program: (1) <strong color: #0075ad;>Body</strong>, (2) <strong color: #0075ad;>Life</strong> and (3) <strong color: #0075ad;>Skills</strong> &hellip; and thus:</p><ol><li>Our <strong style=""color: #0075ad;"">body</strong> and what it expresses via its <em>behaviours</em>, <em>words</em>, <em>thoughts</em> and <em>feelings</em> <strong>must never be judged, rather understood.</strong></li><li>Our experience of <strong style=""color: #0075ad;"">LIFE</strong> <strong>must never be judged, rather understood.</strong></li><li>Our existing <strong style=""color: #0075ad;"">skills</strong> and our yet to be developed skills <strong>must never be judged, rather understood.</strong></li></ol><br/><p><strong style=""font-size:10pt"">The simplicity is:</strong><br/>A person perceives they have the required skills to respond to life = the body uses wanted or preferred behaviours</p><p style="" text-align:center;font-size:10pt""><strong><em>OR</em></strong></p><p>A person perceives they do not have the required skills to respond to life = the body is more likely to use unwanted or non-preferred behaviours of varying intensity</p><p style=""text-align: center; color: #0075ad; font-size:10pt""><strong><em>Without judgment, we can bring understanding.<br />With understanding we lay the foundation for <br />people to feel safe to commit to life in full.</em></strong></p></div>";
                    break;
                case 6:
                    cell.TextToShow = @"<div style=""font-family: arial;font-size: 10pt; font-weight: bold;""><ol><li><span style=""font-weight: normal;"">Implement the <strong><a href=""http://www.bodylifeskills.com"" style=""color: #642d88;"">Body Life Skills program</a></strong> in your own life supporting lasting behaviour change for your self or any other person.</span><br/></li><li><span style=""font-weight: normal;"">Learn to increase your <strong style=""color: #0075ad;"">understanding</strong> of your own behaviours, words, thoughts and or feelings.</span><br/></li><li><span style=""font-weight: normal;"">Learn to increase your <strong style=""color: #0075ad;"">understanding</strong> of other people&rsquo;s behaviours, words, thoughts and/or feelings.</span><br/></li><li><span style=""font-weight: normal;"">Learn to develop <strong style=""color: #0075ad;"">skills</strong> to respond to situations in <strong style=""color: #0075ad;"">life</strong> you may not feel completely equipped to respond to. With the new skills learnt you will be left feeling equipped to respond to these daily life challenges.</span><br/></li><li><span style=""font-weight: normal;"">Learn to develop <strong style=""color: #0075ad;"">skills</strong> to respond to life bringing the opportunity for lasting behaviour change.</span><br/></li></ol></div>";
                    break;
                case 7:
                    cell.TextToShow = @"<div style=""font-family: arial""><p style=""font-size: 11pt;""><strong>The Body Life Skills program incorporates both the <strong style=""color: #0075ad;"">Fabic Behaviour/Anxiety Scale&nbsp;</strong>and the <strong  style=""color: #0075ad;"">Fabic I Choose Chart</strong>.</strong></p><p style=""font-size:10pt""><strong style=""color: #642d88;"">The Behaviour Scale identifies the first two steps of the Body Life Skills Program:</strong></p><ol><li><strong style=""color: #0075ad;"">Step 1 &ndash; Body. </strong><br /><em>What behaviours, words, thoughts and feelings a body is using to communicate how it is experiencing life.</em><br/></li><li><strong style=""color: #0075ad;"">Step 2 &ndash; Life </strong><br /><em>What parts of life does a person perceive they do not YET have the required skills to respond to.</em></li></ol><br/><p style=""font-size:10pt""><strong style=""color: #642d88;"">The I Choose Chart supports with:</strong></p><ol start=""3""><li><strong style=""color: #0075ad;"">Step 3 &ndash; Skills</strong><br/><em>Teaching and learning skills to respond to the parts of life identified in Step 2</em></li></ol><br/><p style=""font-size:10pt"">This app provides blank templates of each of these charts allowing you to personalise and build a library of charts of your own completed Behaviour/Anxiety Scales and I Choose Charts, thus supporting self-mastery and lasting behaviour change.</p><p style=""font-size:10pt""><strong style=""color: #642d88;"">In utilising this app, you will have:</strong></p><ol><li>Your own completed library of personalised and completed <strong style=""color: #0075ad;"">Behaviour Scales</strong> for yourself and your family (if in a home setting); your students (if in a school); your colleagues (if in a workplace); your clients (if in a clinical setting) or any other setting.<br/></li><li>A Fabic Library of completed <strong style=""color: #0075ad;"">Behaviour Scale</strong>&nbsp;examples to use as a guide when completing your own or other's Behaviour Scales.<br/></li><li>Your own library of completed <strong style=""color: #0075ad;"">I Choose Charts</strong> that will assist in you to teach and/or learn new skills to respond to the parts of life that you have previously perceived you were not yet equipped to respond to.<br/></li><li>A Fabic Library of completed <strong style=""color: #0075ad;"">I Choose Charts</strong> provides a number of examples of pre-completed I Choose Charts based on commonly experienced aspects of life that many perceive they do not yet have all the required skills to respond to. The completed Fabic library also serves as a teaching tool in that it can&nbsp;assist when completing your own personalised <strong style=""color: #0075ad;"">I Choose Charts</strong>.</li></ol><br/><p style=""text-align: center;font-size:10pt""><strong style=""color: #0075ad;"">For further information on the Body Life Skills program, refer to many free <a href=""https://www.youtube.com/channel/UClxyp32dQcFZiLv8cB3yI9Q"">YouTube clips</a>&nbsp;further explaining this approach.</strong></p></div>";
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
                    adjustment = 50;
                    break;
                case 2:
                    adjustment = 90;
                    break;
                case 3:
                    adjustment = 65;
                    break;
                case 4:
                    adjustment = 65;
                    break;
                case 5:
                    adjustment = 90;
                    break;
                case 6:
                    adjustment = 60;
                    break;
                case 7:
                    adjustment = 80;
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
            return 8;
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
                    return "About This App";
                case 2:
                    return "What is the Body Life Skills Program?";
                case 3:
                    return "Every Behaviour Happens For a Reason";
                case 4:
                    return "Behaviour is Not Who You Are It is What You Do";
                case 5:
                    return "Understanding and Judgement Cannot Exist Together";
                case 6:
                    return "This App is a Tool That Allows You To";
                case 7:
                    return "What Will I Find In This App?";

            }
            return string.Empty;
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            UIView header = new UIView();

            FabicButton cellHeader = new FabicButton();
            cellHeader.FabicColour = Data.Enums.FabicColour.Blue;
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
                UIApplication.SharedApplication.OpenUrl(new NSUrl("https://youtu.be/eqGHDLe_qXM"));
        }

        public void CleanUp()
        {

        }
    }
}
