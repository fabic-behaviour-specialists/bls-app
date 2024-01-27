using CoreGraphics;
using Fabic.Data.Enums;
using Fabic.Data.Extensions;
using System;
using UIKit;

namespace Fabic.iOS
{
    public class ArchivedOverlay : UIView, IDisposable, ICanCleanUpMyself
    {
        // control declarations
        UILabel loadingLabel;
        UIButton undoButton;

        public event EventHandler UndoButtonPressed;

        public override void RemoveFromSuperview()
        {

            if (UndoButtonPressed != null)
                UndoButtonPressed = null;

            base.RemoveFromSuperview();
        }

        public ArchivedOverlay(CGRect frame, FabicColour colour = FabicColour.Purple) : base(frame)
        {
            this.Frame = frame;
            // configurable bits
            BackgroundColor = UIColor.Clear.FabicColour(colour);
            Alpha = 1f;

            nfloat labelHeight = 44;
            nfloat labelWidth = Frame.Width; //- 20;

            // create and configure the "Loading Data" label
            loadingLabel = new UILabel(new CGRect(
                0,
                0,
                labelWidth,
                labelHeight
                ));
            loadingLabel.BackgroundColor = UIColor.Clear;
            loadingLabel.TextColor = UIColor.White;
            loadingLabel.Text = "This Item has been Archived";
            loadingLabel.Lines = 2;
            loadingLabel.TextAlignment = UITextAlignment.Center;
            AddSubview(loadingLabel);

            var img = UIImage.FromFile("undo.png");
            undoButton = new UIButton(UIButtonType.Custom);
            undoButton.SetTitle("Undo", UIControlState.Normal);
            undoButton.Font = UIFont.BoldSystemFontOfSize(16);
            undoButton.Frame = new CGRect(0, loadingLabel.Frame.Y + loadingLabel.Frame.Height + 5, Frame.Width, 20);
            undoButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            undoButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            undoButton.TouchDown += UndoButton_TouchDown;
            AddSubview(undoButton);

            //undoButtonWithPic = new UIButton(UIButtonType.Custom);
            //undoButton.SetTitle("", UIControlState.Normal);
            ////undoButton.Font = UIFont.FromName("
            //undoButton.Frame = new CGRect(0, loadingLabel.Frame.Y + loadingLabel.Frame.Height + 5, Frame.Width, 20);
            //undoButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            //AddSubview(undoButton);
        }

        /// <summary>
        /// Fades out the control and then removes it from the super view
        /// </summary>
        public void Hide()
        {
            UIView.Animate(
                0.5, // duration
                () => { Alpha = 0; },
                () => { RemoveFromSuperview(); }
            );
        }

        /// <summary>
        /// Fades in the control and then removes it from the super view
        /// </summary>
        public void Show(UIView view)
        {
            // set the view defaults
            this.Alpha = 0;
            loadingLabel.Text = "This Item has been Archived";
            undoButton.Hidden = false;

            view.AddSubview(this);
            UIView.Animate(
                0.5, // duration
                () => { Alpha = 1; },
                () => { }
            );
        }

        /// <summary>
        /// Fades in the control and then removes it from the super view
        /// </summary>
        public void Show(UIView view, string text, bool showUndoButton = true)
        {
            this.Alpha = 0;
            loadingLabel.Text = text;
            undoButton.Hidden = !showUndoButton;

            view.AddSubview(this);
            UIView.Animate(
                0.5, // duration
                () => { Alpha = 1; },
                () => { }
            );
        }

        void UndoButton_TouchDown(object sender, EventArgs e)
        {
            if (UndoButtonPressed != null)
                UndoButtonPressed(this, new EventArgs());
        }

        public void CleanUp()
        {
            undoButton.TouchDown -= UndoButton_TouchDown;
            undoButton.Dispose();
        }
    }
}
