using CoreGraphics;
using Fabic.Core.Helpers;
using System;
using UIKit;

namespace Fabic.iOS
{
    public partial class AddBehaviourScaleViewController : UIViewController, IDisposable, ICanCleanUpMyself
    {
        // Please use a scale name that will help you quickly identify a particular scale
        string[] info = { "P", "l", "e", "a", "s", "e", " ", "u", "s", "e", "a", " ", "a", " ", "s", "c", "a", "l", "e", " ", "n", "a", "m", "e", " " };
        int calls = 0;
        System.Threading.Timer timer;

        FabicButton OkButton = new FabicButton();
        FabicTextView TextView = new FabicTextView();
        UILabel InfoLabel = new UILabel();

        public AddBehaviourScaleViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ApplyLightInterface();
            double centerX = View.Frame.Width / 2;
            double bottomY = View.Frame.Height - 200;

            // layout the view
            // start with the text view
            TextView.Frame = new CoreGraphics.CGRect(20, 20, View.Frame.Width - 40, 60);
            TextView.TextAlignment = UITextAlignment.Center;
            this.View.AddSubview(TextView);

            // the label
            InfoLabel.Frame = new CoreGraphics.CGRect(20, 70, View.Frame.Width - 40, View.Frame.Height - bottomY - 70);
            InfoLabel.TextAlignment = UITextAlignment.Center;
            this.View.AddSubview(InfoLabel);

            // ok button
            OkButton.Frame = new CGRect(centerX - (OkButton.Frame.Width / 2), bottomY - (OkButton.Frame.Height / 2), OkButton.Frame.Width, OkButton.Frame.Height);
            OkButton.SetTitle("Save", UIControlState.Normal);
            OkButton.TouchDown += OkButton_Clicked;
            this.View.AddSubview(OkButton);

            barBtnCancel.Clicked += BarBtnCancel_Clicked;

            timer = new System.Threading.Timer(new System.Threading.TimerCallback(timerTick), calls, 1, 2 * info.Length - 1);

        }



        private void timerTick(object timer)
        {
            InvokeOnMainThread(new Action(timerTick));

            //if (calls >= info.Length)
            //    timer
        }

        void timerTick()
        {
            if (calls <= info.Length - 1)
            {
                UIView.BeginAnimations("slideAnimation" + calls);

                UIView.SetAnimationDuration(0.01);
                UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
                UIView.SetAnimationDelegate(this);

                InfoLabel.Text += info[calls];
                UIView.CommitAnimations();

                calls++;
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            TextView.BecomeFirstResponder();
        }

        public override bool BecomeFirstResponder()
        {
            return base.BecomeFirstResponder();
        }

        private void BarBtnCancel_Clicked(object sender, EventArgs e)
        {
            this.DismissModalViewController(true);
        }

        private void OkButton_Clicked(object sender, EventArgs e)
        {
            this.DismissModalViewController(true);
        }

        public void CleanUp()
        {

        }
    }
}