using CoreGraphics;
using Fabic.Data.Extensions;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public class AboutIChooseChartViewControllerTableViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        private UITableView TableSource = null;
        nint rowShown = -1;
        string CellIdentifier = "AboutIChooseChartTableCell";

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
                    cell.TextToShow = @"<div style=""font-family: arial""><p style=""font-size: 10pt"">The Fabic I Choose Chart is the third step in the Body Life <span style=""color: #0075ad;""><strong>SKILLS</strong></span> program &ndash; teaching and/or learning the skills to self-master life.</p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><span style=""color: #642d88;""><strong style=""font-size: 10pt"">The key purpose of the Fabic <em>I Choose Chart</em> is to offer a means of</strong> <strong>bringing about <span style=""text-decoration: underline;"">lasting behaviour change</span> by supporting a person to develop new wanted or preferred skills to replace their old unwanted or non-preferred behaviours</strong>.</span></p><p style=""font-family:Tahoma, Geneva, sans-serif""><em>The I Choose Chart</em> provides people with a choice of whether to <em>respond</em> (<em>Option One</em>) or <em>react</em> (<em>Option Two</em>) to life.</p><p style=""text-align: center;""><img src=""https://fabicapp.fierydevelopment.com:8080/resources/i-choose-chart.png"" height=""240px""/></p><p style=""font-family:Tahoma, Geneva, sans-serif"">The simplicity of the Fabic <em>I Choose Chart</em> supports people to know that it is never life itself that determines whether someone is at code <strong><span style=""color: #3366ff;"">blue</span></strong>, <strong><span style=""color: #008000;"">green</span></strong>, <strong><span style=""color: #ffcc00;"">yellow</span></strong>, <strong><span style=""color: #ff6600;"">orange</span></strong> or <strong><span style=""color: #ff0000;"">red</span></strong>; rather, it is how we choose to respond to life. Furthermore, there is no &lsquo;other&rsquo; choice until a person has been taught, learnt and developed the required skills (option one from the <em>I Choose Chart</em>) to respond to that particular aspect of life.</p></div>";
                    break;
                case 2:
                    cell.TextToShow = @"<div style=""font-family: arial""><p style=""font-size: 10pt"">As the title suggests, the <em>I Choose Chart</em> offers people a choice of how they respond to life and thus offers them to ultimately choose the natural consequences that occur in their life as a result of their behaviour choices.</p><p style=""font-family:Tahoma, Geneva, sans-serif"">The <em>I Choose Chart</em> offers a means for all of us to learn, develop and replace our old unwanted reactions (option two behaviours) with new wanted or preferred responses (option one behaviours).</p><p style=""font-family:Tahoma, Geneva, sans-serif"">From there the <em>I Choose Chart</em> offers us the choice to embrace that, not only do we choose how we respond to life, but equally we choose the outcomes that naturally occur as a result of our behaviour choices.</p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><strong>Option one behaviours = <span style=""color: #0075ad;"">Wanted</span> natural outcomes</strong></p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><strong>Option two behaviours = <span style=""color: #ff0000;"">Unwanted</span> natural outcomes</strong></p><p style=""font-family:Tahoma, Geneva, sans-serif"">The key purpose of the Fabic <em>I Choose Chart</em> is its role to be:<br/><ol><li><strong>A teaching tool</strong></li><li><strong>A learning tool</strong></li><li><strong>A tool offering self-responsibility</strong></li></ol></p><h3><span style=""color: #0075ad;font-family:Tahoma, Geneva, sans-serif"">The <em>I Choose Chart</em> as a teaching tool:</span></h3><p style=""font-family:Tahoma, Geneva, sans-serif"">The <em>I Choose Chart</em> provides the opportunity for us to teach a person a possible option one response to the part of life they are choosing to self-master, while equally highlighting the outcomes when continuing to choose their current option two reactions.</p><p style=""font-family:Tahoma, Geneva, sans-serif"">We are all forever teachers of life. Embracing this responsibility increases our acceptance and understanding, of ourselves and others. The fact is, we each have skills that we have mastered or are on our way to mastering in life. These skills will be unique to each person. Accepting and appreciating what we have already begun to master, allows us to embrace our role as a potential teacher for that skill. This is a very important role in society that we all have the responsibility for.</p><h3 style=""font-family:Tahoma, Geneva, sans-serif""><span style=""color: #0075ad;""><strong>The <em>I Choose Chart</em> as a learning tool:</strong></span></h3><p style=""font-family:Tahoma, Geneva, sans-serif"">The <em>I Choose Chart</em> as a learning tool provides the opportunity for us to learn new ways of responding to the parts of life that we are yet to self-master.&nbsp;</p><p style=""font-family:Tahoma, Geneva, sans-serif"">We are all forever students of life. Embracing this also increases acceptance and understanding of self and others. There are many parts of life that we may have already mastered, but I am the first to say there are truckloads of life that I am yet to master; for this reason, I am a forever student. This is the case for all and when we are ready to embrace our role as a forever student of life, we will see that life is one massive classroom providing opportunities to learn and embrace new lessons time and time again.</p><h3 style=""font-family:Tahoma, Geneva, sans-serif""><span style=""color: #0075ad;"">The <em>I Choose Chart</em> as a tool offering self-responsibility:</span></h3><p style=""font-family:Tahoma, Geneva, sans-serif"">The <em>I Choose Chart</em>, when properly used, highlights that it is never life that takes us to code blue or code green, yellow, orange or red; rather, it is how we choose to respond or react to life which determines the outcomes that naturally occur as a result of the choices we make.</p><p style=""font-family:Tahoma, Geneva, sans-serif"">Thus, once a person has learnt and developed the option one response to a certain aspect of life, they have a choice of whether they continue to choose their previous option two behaviours or equally have a choice to use the option one response. It is always the person&rsquo;s choice and this must be respected.</p></div>";
                    break;
                case 3:
                    cell.TextToShow = @"<div style=""font-family: arial""><p>Completing the <em>I Choose Chart</em> involves five key steps. Below are the steps in the order in which the author complete them; however, <em>it can be done in whichever order works best for you</em>:<br/><ol style=""font-family:Tahoma, Geneva, sans-serif""><li><strong>I Choose Chart title</strong></li><li><strong>Option Two behaviours</strong></li><li><strong>Option Two outcomes</strong></li><li><strong>Option One outcomes</strong></li><li><strong>Option One behaviours</strong></li></ol></p><h3><span style=""color: #0075ad;font-family:Tahoma, Geneva, sans-serif"">Chart title</span></h3><p>The chart title is directly taken from Step 2: Life. What is the specific part of life that has taken a person higher than code blue on the Fabic Behaviour/Anxiety Scale?</p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><em>Example: Making a mistake or receiving a correction.</em>&nbsp;</p><p style=""text-align: center;""><img src=""https://fabicapp.fierydevelopment.com:8080/resources/I-choose-chart-title.png"" height=""70px""/></p><h3><span style=""color: #0075ad;"">Option Two behaviours &ndash; <span style=""color: #ff0000;"">possible</span> <span style=""color: #ff0000;"">reactions</span></span></h3><p style=""font-family:Tahoma, Geneva, sans-serif"">The Option 2 behaviours list the previous unwanted behaviours the person has been using in reaction to the specific aspect of life highlighted in the chart title. It is important to list the behaviours that are specific to that person. Many people may find the same part of life challenging, but it is more than likely they will have their own unique reactions.</p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><em>Examples: Denying I made a mistake, pretending no mistake was made or blaming someone else for the mistake.</em></p><p syle=""text-align: center;""><img src=""https://fabicapp.fierydevelopment.com:8080/resources/I_Choose_Chart_Option2_Behaviours.png"" height=""260px""/></p><h3 style=""font-family:Tahoma, Geneva, sans-serif""><span style=""color: #0075ad;"">Option Two outcome &ndash; likely natural consequence 2</span></h3><p style=""font-family:Tahoma, Geneva, sans-serif"">The option two outcome draws attention to the natural consequences that are likely to occur as a result of the option two behaviours being used. The key here is not to focus on punishment (an independent unwanted outcome delivered that may not be a natural consequence of the behaviour choice), but rather focus on what will naturally occur as a result of the behaviour choice.</p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><em>Example:</em></p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><em><strong>Punishment:</strong> Sent to room for blaming someone else</em></p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><em><strong>Natural consequence:</strong> I do not learn from this mistake. I keep making the same mistake when life presents this lesson.</em></p><p syle=""text-align: center;""><img src=""https://fabicapp.fierydevelopment.com:8080/resources/I_Choose_Chart_Option2_Outcomes.png"" height=""350px"" /></p><h3 style=""font-family:Tahoma, Geneva, sans-serif""><span style=""color: #0075ad;"">Option One outcome &ndash; likely natural consequence 1</span></h3><p style=""font-family:Tahoma, Geneva, sans-serif"">The option one outcome highlights the natural consequences that are likely to occur as a result of the option one behaviours being used. These are typically the outcomes that a person would like to occur. Here it is important not to focus on artificial reinforcements (an independent wanted outcome that may not be a natural consequence of the behaviour choice), but rather focus on what will naturally occur as a result of the behaviour choice.</p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><em>For example:</em></p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><em><strong>Reinforcement</strong>: I get praise and a high-five for my choice</em></p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><em><strong>Natural consequence</strong>: I do learn from this mistake. I stop making the same mistake when life presents this lesson.</em></p><p syle=""text-align: center;""><img src=""https://fabicapp.fierydevelopment.com:8080/resources/I_Choose_Chart_Option1_Outcomes.png"" height=""350px"" /></p><p style=""text-align: center;""><span style=""color: #642d88;""><em><strong>Note: It is through identifying what the natural consequences of our choices are that lasting behaviour change can occur.</strong></em></span></p><h3 style=""font-family:Tahoma, Geneva, sans-serif""><span style=""color: #0075ad;""><strong>Option one behaviours &ndash; step 3: <span style=""color: #0075ad;"">new skills</span></strong></span></h3><p style=""font-family:Tahoma, Geneva, sans-serif"">The option one behaviours list the new wanted responses that will support a person to respond to the part of life they were previously finding challenging. It is key here that we do not introduce the behaviours we want to see or the behaviours we think are the preferred ones but that we teach what skills are required for the person to self-master the challenging life situation in the chart title. Teaching the behaviours we would prefer another person to use in replacement of skills learnt, developed and practised is a critical, yet very common error guaranteeing lasting behaviour change will not occur.</p><p>When completing the option 1 part we must ask: <strong><em>What is needed for this person to self-master this part of life?</em></strong></p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><span style=""color: #642d88;""><em>Teaching skills (Option one) to self-master life (chart title) =&nbsp;</em><em>lasting behaviour change</em></span>&nbsp;</p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><em>For example: Yes, I made a mistake. Whoops, what can I learn here? Listen with my ears, see with my eyes, connect to myself and learn a new skill. <strong>Then I practise, make more mistakes and keep on practising</strong></em></p><p syle=""text-align: center;""><img src=""https://fabicapp.fierydevelopment.com:8080/resources/I_Choose_Chart_Option1_Behaviours.png"" height=""340px"" /></p><h3 style=""font-family:Tahoma, Geneva, sans-serif""><span style=""color: #0075ad;""><strong>Completed Chart</strong></span></h3><p syle=""text-align: center;""><img src=""https://fabicapp.fierydevelopment.com:8080/resources/I_Choose_Chart_CompletedChart.png"" height=""400px""/></p></div>";
                    break;
                case 4:
                    cell.TextToShow = @"<div style=""font-family: arial""><p>The language of the <em>I Choose Chart</em> is simply based on bringing a level of responsibility to a person, their choices and the outcomes they receive in life. The simplicity of the language is based on:</p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><strong>I choose &hellip; or &hellip; I chose </strong></p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><strong>You choose &hellip; or &hellip; you chose</strong></p><p style=""text-align: center;font-family:Tahoma, Geneva, sans-serif""><strong>We choose &hellip; or &hellip; we chose</strong>&nbsp;</p><p style=""font-family:Tahoma, Geneva, sans-serif""><span style=""text-decoration: underline;"">That is, every single person is responsible for every single choice they have ever made or will make in their future.</span> That is, we are responsible for:<br/><ol style=""font-family:Tahoma, Geneva, sans-serif""><li><strong><span style=""color: #0075ad;"">The behaviours, words, thoughts and feelings we choose</span></strong></li><li><span style=""color: #0075ad;""><strong>The outcomes we receive in life as a result of our choices</strong></span></li><li><strong><span style=""color: #0075ad;"">Who and what we choose to align to and the consequences that come from that choice</span></strong>&nbsp;</li></ol></p><p style=""font-family:Tahoma, Geneva, sans-serif"">Some example phrases we have used:<br/><ol><li><em>Whoops, you chose option two and thus you chose to get expelled. What can you learn from this so you choose differently next time?</em></li><li><em>You chose to eat that food you knew did not agree with you &ndash; whoops. That means you chose to have that belly ache.</em></li><li><em>You chose to use option one behaviour in the classroom. That means you chose to stay in the classroom all day and you chose not to get detention.</em></li><li><em>You chose to go to bed early, meaning you chose to be closer to code blue today.</em></li><li><em>That&rsquo;s your choice if you want to continue that behaviour. Just know you are also choosing that outcome.</em></li></ol></p><p>There is an endless supply of statements that can be provided; the simple message shared is:</p><p style=""text-align: center;""><span style=""color: #642d88;""><strong><em>You choose your behaviours and thus you choose your outcomes.</em></strong></span></p></div>";
                    break;
            }

            return cell;
        }

        IDictionary<nint, nfloat> rowHeightDictionary = new Dictionary<nint, nfloat>();
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (rowShown == indexPath.Section && rowShown > 0)
            {
                int adjustment = 40;
                switch (rowShown)
                {
                    case 0:
                        adjustment = 0;
                        break;
                    case 1:
                        adjustment = 180;
                        break;
                    case 2:
                        adjustment = 200;
                        break;
                    case 3:
                        adjustment = 1550;
                        break;
                    case 4:
                        adjustment = 80;
                        break;
                }

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
            return 5;
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
                    return "The Fabic I Choose Chart";
                case 2:
                    return "The Purpose of the I Choose Chart";
                case 3:
                    return "Completing an I Choose Chart";
                case 4:
                    return "Language of the I Choose Chart";

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
            if (TableSource != null && rowShown > 0) { TableSource.ReloadRows(paths.ToArray(), UITableViewRowAnimation.Automatic); TableSource.ScrollToRow(path, UITableViewScrollPosition.Top, false); }
            if (rowShown == 0)
                UIApplication.SharedApplication.OpenUrl(new NSUrl("https://youtu.be/pN8n2n5eYWE"));
        }

        public void CleanUp()
        {

        }
    }
}
