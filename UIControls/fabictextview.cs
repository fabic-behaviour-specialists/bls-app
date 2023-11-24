using CoreAnimation;
using CoreGraphics;
using Fabic.Data.Extensions;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS
{
    public partial class FabicTextView : UITextView, IDisposable, ICanCleanUpMyself
    {
        float fontSize = 23.4f;
        string placeHolder = string.Empty;
        bool withLines = true;
        CALayer placeholderLayer = null;

        public event EventHandler TextChanged;
        public event EventHandler DoneClicked;

        public string PlaceHolder
        {
            get { return placeHolder; }
            set { placeHolder = value; }
        }

        public bool WithLines
        {
            get { return withLines; }
            set { withLines = value; }
        }

        public Data.Enums.FabicColour BarColour
        {
            get;
            set;
        }

        public FabicTextView(bool _withLines = true) : base()
        {
            Initialize();

            this.ContentMode = UIViewContentMode.Redraw;
            BarColour = Data.Enums.FabicColour.Purple;
            withLines = _withLines;
        }

        public FabicTextView(IntPtr handle) : base(handle)
        {
            Initialize();

            BarColour = Data.Enums.FabicColour.Purple;
            this.ContentMode = UIViewContentMode.Redraw;
        }

        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);

            if (withLines)
            {
                //Get the current drawing context
                CGContext context = UIGraphics.GetCurrentContext();
                //Set the line color and width
                context.SetStrokeColor(UIColor.Blue.FabicColour(Data.Enums.FabicColour.Gray).CGColor);
                context.SetLineWidth(0.7f);

                //Start a new Path
                context.BeginPath();

                //Find the number of lines in our textView + add a bit more height to draw lines in the empty part of the view
                nfloat numberOfLines = (this.Frame.Height + this.Bounds.Size.Height) / fontSize;

                //Set the line offset from the baseline. (I'm sure there's a concrete way to calculate this.)
                nfloat baselineOffset = 6.0f;

                //iterate over numberOfLines and draw each line
                for (int x = 0; x < numberOfLines; x++)
                {
                    //0.5f offset lines up line with pixel boundary
                    context.MoveTo(this.Bounds.X, fontSize * x + 0.9f + baselineOffset);

                    context.AddLineToPoint(this.Bounds.Size.Width, fontSize * x + 0.9f + baselineOffset);
                }

                //Close our Path and Stroke (draw) it
                context.ClosePath();

                context.StrokePath();
            }
        }

        void Initialize()
        {
            UIToolbar toolBar = new UIToolbar(new CGRect(0, 0, this.Frame.Width, 40));
            PlaceHolder = "Please enter text";
            KeyboardType = UIKeyboardType.ASCIICapable;
            KeyboardAppearance = UIKeyboardAppearance.Light;
            KeyboardDismissMode = UIScrollViewKeyboardDismissMode.Interactive;
            ReturnKeyType = UIReturnKeyType.Default;
            InputAccessoryView = toolBar;

            toolBar.BarStyle = UIBarStyle.Default;
            toolBar.BarTintColor = UIColor.Blue.FabicColour(BarColour);
            toolBar.TintColor = UIColor.Blue.FabicColour(BarColour);
            UIBarButtonItem done = new UIBarButtonItem(UIBarButtonSystemItem.Done, HandleEventHandler);
            done.TintColor = UIColor.White;//Blue.FabicColour(Data.Enums.FabicColour.Blue);

            UIBarButtonItem space = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);

            List<UIBarButtonItem> items = new List<UIBarButtonItem>();
            items.Add(space);
            items.Add(done);

            toolBar.Items = items.ToArray();

            ShouldBeginEditing = t =>
            {
                if (Text == PlaceHolder)
                    Text = string.Empty;

                return true;
            };
            ShouldEndEditing = t =>
            {
                if (string.IsNullOrEmpty(Text))
                    Text = PlaceHolder;

                if (this.TextChanged != null)
                    this.TextChanged(this, new EventArgs());

                return true;
            };

        }

        void HandleEventHandler(object sender, EventArgs e)
        {
            ResignFirstResponder();
            DoneClicked?.Invoke(sender, e);
        }

        public override bool BecomeFirstResponder()
        {
            // this is where we draw the line under the text view to show focus
            //if (border != null)
            //{
            //	border.Hidden = false;
            //	border.Opacity = 0;
            //	UIView.Animate(
            //		2, // duration
            //		() => { border.Opacity = 0.9f; },
            //		() => { }
            //	);
            //}

            return base.BecomeFirstResponder();
        }

        public override bool ResignFirstResponder()
        {
            //if (border != null)
            //{
            //	UIView.Animate(
            //		2, // duratio
            //		() => { border.Opacity = 1f; },
            //		() => { border.Hidden = true; border.Opacity = 0f; }
            //	);
            //}
            return base.ResignFirstResponder();
        }

        void Handle_Changed(object sender, EventArgs e)
        {
            // text has changed, resize the text view
            // estimate new height..


            //border.Frame = new CoreGraphics.CGRect(10, this.Frame.Size.Height - 1, this.Frame.Width - 20, 1);
        }

        public override void DidChangeValue(string forKey)
        {
            base.DidChangeValue(forKey);
            //border.Frame = new CoreGraphics.CGRect(10, this.Frame.Size.Height - 1, this.Frame.Width - 20, 1);
        }

        public void CleanUp()
        {

        }
    }
}