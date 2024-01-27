using CoreGraphics;
using Fabic.Data.Enums;
using Fabic.Data.Extensions;
using Fabic.iOS.UIControls;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS
{
    public class TutorialOverlay : UIView, IDisposable, ICanCleanUpMyself
    {
        // control declarations
        FabicButton button;
        UIButton closeButton;
        UIButton rightButton;
        UIButton leftButton;
        UIPageControl pageControl;
        UIImage snapshot;
        UIImageView backgroundImage;
        TutorialOverlayItemGestureHandler tapGesture;
        NSData ButterFlyData;
        UIImageView imageButterFly;

        // variables
        List<TutorialOverlayItem> TutorialElements = new List<TutorialOverlayItem>();
        FabicColour MainColour;
        FabicColour SubColour;
        bool CloseButtonHidden = false;
        bool BackgroundImageHidden = true;
        string CloseButtonText = "Close";
        nfloat ButtonY = 0;


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Fabic.iOS.TutorialOverlay"/> shows a close button.
        /// </summary>
        /// <value><c>true</c> if show close button; otherwise, <c>false</c>.</value>
        public bool ShowCloseButton
        {
            get { return !CloseButtonHidden; }
            set { CloseButtonHidden = !value; if (closeButton != null) { closeButton.Hidden = CloseButtonHidden; } }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Fabic.iOS.TutorialOverlay"/> show background image.
        /// </summary>
        /// <value><c>true</c> if show background image; otherwise, <c>false</c>.</value>
        public bool ShowBackgroundImage
        {
            get { return !BackgroundImageHidden; }
            set { BackgroundImageHidden = !value; if (backgroundImage != null) { backgroundImage.Hidden = BackgroundImageHidden; } }
        }

        public event EventHandler CloseButtonPressed;

        public TutorialOverlay(CGRect frame, UIImage baseView, List<TutorialOverlayItem> items, FabicColour colour = FabicColour.Purple, FabicColour subColour = FabicColour.LightBlue, int buttonY = 0, string closeButtonText = "Close") : base(frame)
        {
            NSUrl path = NSBundle.MainBundle.GetUrlForResource("butterfly", "gif");
            ButterFlyData = NSData.FromUrl(path);
            //  imageButterFly = AnimatedImageView.GetAnimatedImageView(ButterFlyData, "fabic");

            this.Frame = frame;
            this.UserInteractionEnabled = true;
            snapshot = baseView;
            TutorialElements = items;
            ButtonY = buttonY;
            CloseButtonText = closeButtonText;

            MainColour = colour;
            SubColour = subColour;

            backgroundImage = new UIImageView(frame);
            backgroundImage.Hidden = BackgroundImageHidden;
            backgroundImage.UserInteractionEnabled = false;
            backgroundImage.Image = baseView;
            AddSubview(backgroundImage);

            tapGesture = new TutorialOverlayItemGestureHandler();
            tapGesture.TutorialOverlay = this;

            this.AddGestureRecognizer(tapGesture);

            // configurable bits
            BackgroundColor = UIColor.White.FabicColour(FabicColour.Gray).ColorWithAlpha(0.88f);
        }

        bool loaded = false;
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            nfloat centerX = Frame.Width / 2;
            nfloat centerY = Frame.Height / 2;

            if (!loaded)
            {
                // tutorial items
                for (int i = 0; i < TutorialElements.Count; i++)
                {
                    this.AddSubview(TutorialElements[i]);

                    if (i > 0)
                    {
                        TutorialElements[i].Alpha = 0;
                    }
                }

                // Add the page control
                pageControl = new UIPageControl();
                pageControl.Frame = new CGRect(centerX - (Frame.Width / 2), Frame.Height - 100, Frame.Width, 20);
                pageControl.PageIndicatorTintColor = UIColor.Blue.FabicColour(MainColour);
                pageControl.CurrentPageIndicatorTintColor = UIColor.Clear.FabicColour(SubColour);
                pageControl.Pages = TutorialElements.Count;
                pageControl.CurrentPage = 0;
                AddSubview(pageControl);

                closeButton = new UIButton(UIButtonType.Custom);
                closeButton.SetTitle("X", UIControlState.Normal);
                closeButton.Font = UIFont.BoldSystemFontOfSize(24);
                if (ButtonY == 0)
                    closeButton.Frame = new CGRect(20, 20, 40, 40);
                else
                    closeButton.Frame = new CGRect(20, ButtonY, 40, 40);
                closeButton.SetTitleColor(UIColor.White.FabicColour(FabicColour.Gray), UIControlState.Normal);
                closeButton.Layer.CornerRadius = 20;
                closeButton.Layer.BorderWidth = 3;
                closeButton.Layer.BorderColor = UIColor.White.FabicColour(FabicColour.Gray).CGColor;
                closeButton.BackgroundColor = UIColor.White;
                closeButton.TouchDown += CloseButton_TouchDown;
                closeButton.Hidden = CloseButtonHidden;
                AddSubview(closeButton);

                leftButton = new UIButton(UIButtonType.Custom);
                leftButton.SetTitle("<", UIControlState.Normal);
                leftButton.Font = UIFont.FromName("EuphemiaUCAS", 34);
                leftButton.Frame = new CGRect(0, centerY - 30, 40, 60);
                leftButton.SetTitleColor(UIColor.White.FabicColour(MainColour), UIControlState.Normal);
                leftButton.Layer.CornerRadius = 20;
                leftButton.Layer.BorderWidth = 0;
                leftButton.Layer.BorderColor = UIColor.White.FabicColour(FabicColour.Gray).CGColor;
                leftButton.BackgroundColor = UIColor.Clear;
                leftButton.Alpha = 0;
                leftButton.TouchDown += LeftButton_TouchDown;
                AddSubview(leftButton);

                rightButton = new UIButton(UIButtonType.Custom);
                rightButton.SetTitle(">", UIControlState.Normal);
                rightButton.Font = UIFont.FromName("EuphemiaUCAS", 34);
                rightButton.Frame = new CGRect(Frame.Width - 40, centerY - 30, 40, 60);
                rightButton.SetTitleColor(UIColor.White.FabicColour(MainColour), UIControlState.Normal);
                rightButton.Layer.CornerRadius = 20;
                rightButton.Layer.BorderWidth = 0;
                rightButton.Layer.BorderColor = UIColor.White.FabicColour(FabicColour.Gray).CGColor;
                rightButton.BackgroundColor = UIColor.Clear;
                rightButton.TouchDown += RightButton_TouchDown; ;
                AddSubview(rightButton);

                button = new FabicButton();
                button.FabicColour = MainColour;
                button.SetTitle("Next", UIControlState.Normal);
                button.Frame = new CGRect(centerX - 100, Frame.Height - 60, 200, 50);
                button.TouchDown += Button_TouchDown;
                AddSubview(button);

                if (TutorialElements.Count > 0)
                {
                    //imageButterFly = new UIImageView();
                    //imageButterFly.ContentMode = UIViewContentMode.ScaleAspectFit;
                    //imageButterFly = AnimatedImageView.GetAnimatedImageView(ButterFlyData, "fabic", imageButterFly);
                    //imageButterFly.Frame = new CGRect(-100, -50, 80, 40);
                    //imageButterFly.ClipsToBounds = true;
                    //imageButterFly.Layer.ShadowColor = UIColor.Black.CGColor;
                    //imageButterFly.Layer.ShadowOpacity = 0.4f;
                    //imageButterFly.Layer.ShadowOffset = new CGSize(2, 4);
                    //imageButterFly.Layer.ShadowRadius = 3;
                    //AddSubview(imageButterFly);

                    UIViewPropertyAnimator propertyAnimator = new UIViewPropertyAnimator(3, UIViewAnimationCurve.EaseOut, () => { });
                    propertyAnimator.AddAnimations(() =>
                    {
                        //imageButterFly.Frame = new CGRect(new CGPoint((TutorialElements[0]).TutorialLabelFrame.Location.X - 40, (TutorialElements[0]).TutorialLabelFrame.Location.Y - 20), new CGSize(80, 40));
                    }, 1);
                    propertyAnimator.StartAnimation();
                }
            }
            loaded = true;
        }



        void startTutorial()
        {

        }

        async void Button_TouchDown(object sender, EventArgs e)
        {
            // scrolls the tutorial...
            nint currentPage = pageControl.CurrentPage;
            nint nextPage = currentPage + 1;
            if (nextPage < TutorialElements.Count)
            {
                UIView controlShown = null;
                UIView controlToBeShown = null;

                // first identify the control that is to be shown and the one that is shown
                // control
                controlShown = TutorialElements[(int)currentPage];

                if (nextPage < TutorialElements.Count)
                    controlToBeShown = TutorialElements[(int)nextPage];

                // next fade out the control shown and fade in the control to be shown
                if (controlToBeShown != null)
                {
                    controlToBeShown.Alpha = 0;
                    //controlToBeShown.Hidden = false;
                }
                UIView.Animate(
                    1, // duration
                    () => { controlShown.Alpha = 0; if (controlToBeShown != null) { controlToBeShown.Alpha = 1; } leftButton.Alpha = 1; if (nextPage == TutorialElements.Count - 1) { rightButton.Alpha = 0; if (!ShowCloseButton) { button.SetTitle("Back", UIControlState.Normal); } else { button.SetTitle(CloseButtonText, UIControlState.Normal); } } pageControl.CurrentPage++; },
                    () =>
                    {
                        // controlShown.Hidden = true;
                        if (nextPage == TutorialElements.Count - 1)
                        {
                            rightButton.Enabled = false;
                        }
                    }
                );
                //if (controlToBeShown != null)
                //{
                //    UIViewPropertyAnimator propertyAnimator = new UIViewPropertyAnimator(3, UIViewAnimationCurve.EaseOut, () => { });
                //    propertyAnimator.AddAnimations(() =>
                //    {
                //        //imageButterFly = AnimatedImageView.GetAnimatedImageView(ButterFlyData, "fabic", imageButterFly);
                //        imageButterFly.Frame = new CGRect(new CGPoint(((TutorialOverlayItem)controlToBeShown).TutorialLabelFrame.Location.X - 40, ((TutorialOverlayItem)controlToBeShown).TutorialLabelFrame.Location.Y - 20), new CGSize(80, 40));
                //    }, 1);
                //    propertyAnimator.StartAnimation();
                //}
            }
            else if (ShowCloseButton)
            {
                // close the tutorial..

                UIView.Animate(
                    0.5, // duration
                    () => { Alpha = 0; },
                    () => { RemoveFromSuperview(); CloseButtonPressed?.Invoke(this, e); }
                );
            }
            else
            {
                // go back
                UIView controlToBeShown = null;
                UIView controlShown = TutorialElements[TutorialElements.Count - 1];
                if (currentPage > 0)
                    controlToBeShown = TutorialElements[0];


                // next fade out the control shown and fade in the control to be shown
                UIView.Animate(
                    1, // duration
                    () => { controlShown.Alpha = 0; if (controlToBeShown != null) { controlToBeShown.Alpha = 1; } rightButton.Alpha = 1; rightButton.Enabled = true; leftButton.Alpha = 0; pageControl.CurrentPage = 0; },
                    () =>
                    {
                    });
                button.SetTitle("Next", UIControlState.Normal);

                if (controlToBeShown != null)
                {
                    UIViewPropertyAnimator propertyAnimator = new UIViewPropertyAnimator(3, UIViewAnimationCurve.EaseOut, () => { });
                    propertyAnimator.AddAnimations(() =>
                    {
                        imageButterFly.Frame = new CGRect(new CGPoint(((TutorialOverlayItem)controlToBeShown).TutorialLabelFrame.Location.X - 40, ((TutorialOverlayItem)controlToBeShown).TutorialLabelFrame.Location.Y - 20), new CGSize(80, 40));
                    }, 1);
                    propertyAnimator.StartAnimation();
                }
            }
        }

        void ScrollView_Scrolled(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Fades out the control and then removes it from the super view
        /// </summary>
        public void Hide()
        {

        }

        /// <summary>
        /// Fades in the control and then removes it from the super view
        /// </summary>
        public void Show(UIView view)
        {
            // set the view defaults
            this.Alpha = 0;
            //loadingLabel.Text = "This Item has been Archived";
            //undoButton.Hidden = false;

            view.AddSubview(this);
            UIView.Animate(
                1, // duration
                () => { Alpha = 1; },
                () => { startTutorial(); }
            );
        }

        /// <summary>
        /// Fades in the control and then removes it from the super view
        /// </summary>
        public void Show(UIView view, string text, bool showUndoButton = true)
        {
            this.Alpha = 0;
            //loadingLabel.Text = text;
            //undoButton.Hidden = !showUndoButton;

            view.AddSubview(this);
            UIView.Animate(
                0.5, // duration
                () => { Alpha = 1; },
                () => { }
            );
        }

        void CloseButton_TouchDown(object sender, EventArgs e)
        {
            UIView.Animate(
                0.5, // duration
                () => { Alpha = 0; },
                () => { RemoveFromSuperview(); if (CloseButtonPressed != null) { CloseButtonPressed(this, e); } }
            );
        }

        void LeftButton_TouchDown(object sender, EventArgs e)
        {
            // scrolls the tutorial...
            nint currentPage = pageControl.CurrentPage;
            nint previousPage = currentPage - 1;
            if (currentPage > 0)
            {
                UIView controlShown = null;
                UIView controlToBeShown = null;

                // first identify the control that is to be shown and the one that is shown

                controlShown = TutorialElements[(int)previousPage + 1];
                if (currentPage > 0)
                    controlToBeShown = TutorialElements[(int)previousPage];


                // next fade out the control shown and fade in the control to be shown
                if (controlToBeShown != null)
                {
                    controlToBeShown.Alpha = 0;
                    //controlToBeShown.Hidden = false;
                }
                UIView.Animate(
                    1, // duration
                    () => { controlShown.Alpha = 0; if (controlToBeShown != null) { controlToBeShown.Alpha = 1; } rightButton.Alpha = 1; rightButton.Enabled = true; if (previousPage == 0) { leftButton.Alpha = 0; } if (previousPage < TutorialElements.Count - 1) { button.SetTitle("Next", UIControlState.Normal); } pageControl.CurrentPage--; },
                    () =>
                    {
                        //controlShown.Hidden = true;
                    }
                );

                if (controlToBeShown != null)
                {
                    UIViewPropertyAnimator propertyAnimator = new UIViewPropertyAnimator(3, UIViewAnimationCurve.EaseOut, () => { });
                    propertyAnimator.AddAnimations(() =>
                    {
                        imageButterFly.Frame = new CGRect(new CGPoint(((TutorialOverlayItem)controlToBeShown).TutorialLabelFrame.Location.X - 40, ((TutorialOverlayItem)controlToBeShown).TutorialLabelFrame.Location.Y - 20), new CGSize(80, 40));
                    }, 1);
                    propertyAnimator.StartAnimation();
                }
            }
        }

        void RightButton_TouchDown(object sender, EventArgs e)
        {
            Button_TouchDown(sender, e);
        }

        public void CleanUp()
        {

        }

        void HandleAction()
        {

        }
    }

    public class TutorialOverlayItemGestureHandler : UITapGestureRecognizer
    {
        public TutorialOverlay TutorialOverlay
        {
            get;
            set;
        }

        public override CGPoint LocationOfTouch(nint touchIndex, UIView inView)
        {
            return base.LocationOfTouch(touchIndex, inView);
        }

        public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            if (TutorialOverlay != null)
            {
                foreach (UIView view in TutorialOverlay.Subviews)
                {
                    if (view.GetType() == typeof(TutorialOverlayItem))
                    {
                        if (view.Alpha == 1)
                        {
                            TutorialOverlayItem item = (TutorialOverlayItem)view;
                            CGPoint location = LocationInView(TutorialOverlay);
                            if (location.X >= item.TutorialItemFrame.X && location.Y >= item.TutorialItemFrame.Y && location.X <= item.TutorialItemFrame.Width + item.TutorialItemFrame.X && location.Y <= item.TutorialItemFrame.Height + item.TutorialItemFrame.Y)
                            {
                                item.DidTouchItem(touches, evt, location);
                            }
                        }
                    }
                }
            }
        }

        public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (TutorialOverlay != null)
            {
                foreach (UIView view in TutorialOverlay.Subviews)
                {
                    if (view.GetType() == typeof(TutorialOverlayItem))
                    {
                        if (view.Alpha == 1)
                        {
                            TutorialOverlayItem item = (TutorialOverlayItem)view;
                            CGPoint location = LocationInView(TutorialOverlay);
                            if (location.X >= item.TutorialItemFrame.X && location.Y >= item.TutorialItemFrame.Y && location.X <= item.TutorialItemFrame.Width + item.TutorialItemFrame.X && location.Y <= item.TutorialItemFrame.Height + item.TutorialItemFrame.Y)
                            {
                                item.DidStopTouchingItem(touches, evt, location);
                            }
                        }
                    }
                }
            }
        }
    }
}
