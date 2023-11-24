using CoreGraphics;
using Fabic.Core.Controllers;
using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Fabic.Data.Enums;
using Fabic.Data.Extensions;
using Fabic.iOS.Controllers;
using Fabic.iOS.UIControls;
using Fabic.iOS.ViewControllers.TableViewSources;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS
{
    public class EditBehaviourScaleItemOverlay : UIView, IDisposable, ICanCleanUpMyself
    {

        // control declarations
        EditBehaviourScaleTutorialViewController tutorialViewController;
        FabicButterflyAnimationLayer _AnimationLayer;
        UILabel titleLabel;
        UILabel subTitleLabel;
        UILabel lblLife, lblBody;
        FabicButton confirmButton;
        UIPageControl pageControl;
        UIScrollView scrollView;
        UIView viewLife; UIView viewBody;
        UITableView tableLife; UITableView tableBody;
        UIButton addBtnLife; UIButton addBtnBody; UIButton helpButton;
        UIButton editBtnLife; UIButton editBtnBody;
        UIButton navBtnLife; UIButton navBtnBody;
        UIImageView butterfly;
        UIView superView;

        // behaviour level related
        bool BodySelected;
        BehaviourScale BehaviourScale;
        int BehaviourScaleLevel;
        UIColor BehaviourLevelColour;
        UIColor BehaviourLevelShadowColour;
        string BehaviourLevelLifeTitle;
        string BehaviourLevelBodyTitle;
        List<BehaviourScaleItem> BehaviourScaleItemsBody = new List<BehaviourScaleItem>();
        List<BehaviourScaleItem> BehaviourScaleItemsLife = new List<BehaviourScaleItem>();

        public event EventHandler CloseButtonPressed;
        public event EventHandler ShowEditTutorial;

        public EditBehaviourScaleItemOverlay(BehaviourScale behaviourScale, CGRect frame, int behaviourScaleLevel, List<BehaviourScaleItem> BehaviourScaleItems, UIView SuperView, bool bodySelected = true) : base(frame)
        {
            this.ApplyLightInterface();
            BehaviourScale = behaviourScale;
            BehaviourScaleLevel = behaviourScaleLevel;
            superView = SuperView;
            BodySelected = bodySelected;

            foreach (BehaviourScaleItem item in BehaviourScaleItems)
            {
                if ((Core.Enumerations.BehaviourScaleItemType)item.BehaviourScaleType == Core.Enumerations.BehaviourScaleItemType.Body)
                    BehaviourScaleItemsBody.Add(item);
                else
                    BehaviourScaleItemsLife.Add(item);
            }

            switch (BehaviourScaleLevel)
            {
                case 1: // Blue
                    BehaviourLevelColour = UIColor.Blue.FabicColour(Data.Enums.FabicColour.DarkBlue);
                    BehaviourLevelShadowColour = UIColor.White;
                    BehaviourLevelLifeTitle = "Code Blue - Life";
                    BehaviourLevelBodyTitle = "Code Blue - Body";
                    break;
                case 2: // Green
                    BehaviourLevelColour = UIColor.Green.FabicColour(Data.Enums.FabicColour.Green);
                    BehaviourLevelShadowColour = UIColor.White;
                    BehaviourLevelLifeTitle = "Code Green - Life";
                    BehaviourLevelBodyTitle = "Code Green - Body";
                    break;
                case 3: // Yellow
                    BehaviourLevelColour = UIColor.Yellow.FabicColour(Data.Enums.FabicColour.Yellow);
                    BehaviourLevelShadowColour = UIColor.LightGray;
                    BehaviourLevelLifeTitle = "Code Yellow - Life";
                    BehaviourLevelBodyTitle = "Code Yellow - Body";
                    break;
                case 4: // Orange
                    BehaviourLevelColour = UIColor.Orange.FabicColour(Data.Enums.FabicColour.Orange);
                    BehaviourLevelShadowColour = UIColor.LightGray;
                    BehaviourLevelLifeTitle = "Code Orange - Life";
                    BehaviourLevelBodyTitle = "Code Orange - Body";
                    break;
                case 5: // Red
                    BehaviourLevelColour = UIColor.Gray.FabicColour(Data.Enums.FabicColour.Red);
                    BehaviourLevelShadowColour = UIColor.White;
                    BehaviourLevelLifeTitle = "Code Red - Life";
                    BehaviourLevelBodyTitle = "Code Red - Body";
                    break;
            }

            // configurable bits
            BackgroundColor = UIColor.Clear.ColorWithAlpha(0.88f);
            Layer.CornerRadius = 17;
            Layer.ShadowRadius = 8;
            Layer.ShadowColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple).CGColor;
            Layer.BorderWidth = 2;
            Layer.BorderColor = BehaviourLevelColour.ColorWithAlpha(0.9f).CGColor;
            ClipsToBounds = true;

            nfloat labelHeight = 50;
            nfloat labelWidth = Frame.Width;

            // derive the center x and y
            nfloat centerX = Frame.Width / 2;

            UIView background = new UIView();
            background.Frame = new CGRect(0, 0, frame.Width, frame.Height);
            background.BackgroundColor = BehaviourLevelColour.ColorWithAlpha(0.7f);
            AddSubview(background);

            UIVisualEffect blurEffect = UIBlurEffect.FromStyle(UIBlurEffectStyle.ExtraLight);
            UIVisualEffectView visualEffectView = new UIVisualEffectView(blurEffect);
            visualEffectView.Frame = new CGRect(0, 0, frame.Width, frame.Height); ;
            visualEffectView.AutoresizingMask = UIViewAutoresizing.All;
            AddSubview(visualEffectView);

            UIImageView backgroundButterflies = new UIImageView(UIImage.FromFile("Waves.png"));
            backgroundButterflies.Frame = this.Frame;
            backgroundButterflies.ContentMode = UIViewContentMode.ScaleAspectFill;
            backgroundButterflies.Alpha = 1f;
            AddSubview(backgroundButterflies);

            // create the new helper button
            helpButton = new UIButton(UIButtonType.Custom);
            helpButton.Frame = new CGRect(centerX - (labelWidth / 2), Frame.Height - 40, labelWidth, 40);
            helpButton.TitleLabel.TextAlignment = UITextAlignment.Center;
            helpButton.SetTitleColor(UIColor.Blue.FabicColour(FabicColour.Purple), UIControlState.Normal);
            helpButton.Font = UIFont.SystemFontOfSize(15);
            helpButton.SetTitle("Help", UIControlState.Normal);
            helpButton.TouchDown += HelpButton_TouchDown; ;
            //AddSubview(helpButton);

            // create the new confirm button
            confirmButton = new FabicButton();
            confirmButton.Frame = new CGRect(20, Frame.Height - 80, labelWidth - 40, 40);
            confirmButton.SetTitle("Close", UIControlState.Normal);
            confirmButton.TouchDown += ConfirmButton_TouchDown;
            AddSubview(confirmButton);

            // Add the additional butterfly
            butterfly = new UIImageView(new UIImage("butterfly.png"));
            butterfly.ContentMode = UIViewContentMode.ScaleAspectFill;
            butterfly.Frame = new CGRect(Frame.Width - 44, Frame.Height - 105, 35, 35);
            butterfly.Transform = CGAffineTransform.MakeRotation((nfloat)Math.PI / 12);
            butterfly.Layer.ShadowOffset = new CGSize(6f, 6f);
            butterfly.Layer.ShadowColor = UIColor.White.CGColor;
            AddSubview(butterfly);

            // Add the page control
            pageControl = new UIPageControl();
            pageControl.Frame = new CGRect(centerX - (labelWidth / 2), Frame.Height - 110, labelWidth, 20);
            pageControl.PageIndicatorTintColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple);
            pageControl.CurrentPageIndicatorTintColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.LightBlue);
            pageControl.Pages = 2;
            pageControl.CurrentPage = 0;
            pageControl.TouchDown += HandleEventHandler;
            AddSubview(pageControl);

            // Add the scroll view
            scrollView = new UIScrollView();
            scrollView.PagingEnabled = true;
            scrollView.ScrollEnabled = true;
            scrollView.ContentSize = new CGSize(frame.Width * 2, frame.Height - 120);
            scrollView.Scrolled += ScrollView_Scrolled;
            scrollView.Frame = new CGRect(0, 0, frame.Width, frame.Height - 120);
            scrollView.BackgroundColor = UIColor.Clear;
            scrollView.ShowsHorizontalScrollIndicator = false;
            scrollView.ShowsVerticalScrollIndicator = false;
            AddSubview(scrollView);

            viewLife = new UIView();
            viewLife.Frame = new CGRect(0, 17, frame.Width, frame.Height - 120);
            scrollView.AddSubview(viewLife);

            viewBody = new UIView();
            viewBody.Frame = new CGRect(frame.Width, 17, frame.Width, frame.Height - 120);
            scrollView.AddSubview(viewBody);

            tableLife = new UITableView();
            tableLife.Frame = new CGRect(10, labelHeight * 1.5, frame.Width - 56, frame.Height - 120 - (25 + (labelHeight * 1.5)));
            tableLife.BackgroundColor = BehaviourLevelColour.ColorWithAlpha(0.1f);
            tableLife.Layer.CornerRadius = 30;
            tableLife.Layer.ShadowOffset = new CGSize(2f, 2f);
            tableLife.Layer.ShadowColor = UIColor.Black.CGColor;
            tableLife.SeparatorColor = BehaviourLevelColour.ColorWithAlpha(0.8f);
            tableLife.Layer.ShadowRadius = 8;
            tableLife.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            tableLife.Source = new EditBehaviourScaleViewSource(BehaviourScaleItemsLife, this);
            viewLife.AddSubview(tableLife);

            titleLabel = new UILabel(new CGRect(0, 10, labelWidth, labelHeight));
            titleLabel.TextColor = BehaviourLevelColour;//UIColor.Clear.FabicColour(Data.Enums.FabicColour.LightBlue);
            titleLabel.Text = BehaviourLevelLifeTitle;
            titleLabel.Font = UIFont.FromName("AvenirNext-Medium", 20);
            titleLabel.ShadowColor = BehaviourLevelShadowColour;//UIColor.White.ColorWithAlpha(1f);
            titleLabel.ShadowOffset = new CGSize(0.6, -1.0);
            titleLabel.TextAlignment = UITextAlignment.Center;
            viewLife.AddSubview(titleLabel);

            //subTitleLabel = new UILabel(new CGRect(0, labelHeight - 15, labelWidth, labelHeight));
            //subTitleLabel.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.LightBlue);
            //subTitleLabel.Text = "A test";
            //subTitleLabel.Font = UIFont.FromName("Avenir-LightOblique", 17);
            //subTitleLabel.ShadowColor = BehaviourLevelShadowColour;//UIColor.White.ColorWithAlpha(1f);
            //subTitleLabel.ShadowOffset = new CGSize(0.6, -1.0);
            //subTitleLabel.TextAlignment = UITextAlignment.Center;
            //viewLife.AddSubview(subTitleLabel);

            tableBody = new UITableView();
            tableBody.Frame = new CGRect(50, labelHeight * 1.5, frame.Width - 60, frame.Height - 120 - (25 + (labelHeight * 1.5)));
            tableBody.Layer.CornerRadius = 30;
            tableBody.Layer.ShadowOffset = new CGSize(2f, 2f);
            tableBody.Layer.ShadowColor = UIColor.Black.CGColor;
            tableBody.SeparatorColor = BehaviourLevelColour.ColorWithAlpha(0.8f);
            tableBody.BackgroundColor = BehaviourLevelColour.ColorWithAlpha(0.1f);
            tableBody.Layer.ShadowRadius = 8;
            tableBody.Source = new EditBehaviourScaleViewSource(BehaviourScaleItemsBody, this);
            viewBody.AddSubview(tableBody);

            titleLabel = new UILabel(new CGRect(centerX - (labelWidth / 2), 10, labelWidth, labelHeight));
            titleLabel.TextColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Blue);
            titleLabel.TextAlignment = UITextAlignment.Center;
            titleLabel.Text = BehaviourLevelBodyTitle;
            titleLabel.TextColor = BehaviourLevelColour;
            titleLabel.Font = UIFont.FromName("AvenirNext-Medium", 20);
            titleLabel.ShadowColor = BehaviourLevelShadowColour;//UIColor.White.ColorWithAlpha(1f);
            titleLabel.ShadowOffset = new CGSize(0.6, -1.0);
            viewBody.AddSubview(titleLabel);

            //subTitleLabel = new UILabel(new CGRect(0, labelHeight - 15, labelWidth, labelHeight));
            //subTitleLabel.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.LightBlue);
            //subTitleLabel.Text = "A test";
            //subTitleLabel.Font = UIFont.FromName("Avenir-LightOblique", 17);
            //subTitleLabel.ShadowColor = BehaviourLevelShadowColour;//UIColor.White.ColorWithAlpha(1f);
            //subTitleLabel.ShadowOffset = new CGSize(0.6, -1.0);
            //subTitleLabel.TextAlignment = UITextAlignment.Center;
            //viewBody.AddSubview(subTitleLabel);

            addBtnLife = new UIButton(UIButtonType.Custom);
            UIImage addImg = UIImage.FromBundle("Add");
            addImg = addImg.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            addBtnLife.SetImage(addImg, UIControlState.Normal);
            addBtnLife.TouchDown += AddBtnLife_TouchDown;
            addBtnLife.Frame = new CGRect(frame.Width - 65, labelHeight - 12, 70, labelHeight - 10);
            viewLife.Add(addBtnLife);

            navBtnLife = new FabicButtonPurple();
            navBtnLife.SetTitle(">", UIControlState.Normal);
            navBtnLife.TouchDown += NavBtnLife_TouchDown;
            navBtnLife.TitleLabel.Lines = 3;
            navBtnLife.Frame = new CGRect(frame.Width - 43, (frame.Height / 2) - 40, 40, 40);
            navBtnLife.Layer.CornerRadius = 20f;
            navBtnLife.TitleLabel.Font = UIFont.FromName("AvenirNext-Medium", 20);
            viewLife.Add(navBtnLife);

            lblLife = new UILabel(new CGRect(frame.Width - 43, (frame.Height / 2), 40, 40));
            lblLife.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple);
            lblLife.Text = "Body";
            lblLife.Font = UIFont.FromName("AvenirNext-Medium", 13);
            //lblLife.ShadowColor = BehaviourLevelShadowColour;//UIColor.White.ColorWithAlpha(1f);
            //lblLife.ShadowOffset = new CGSize(0.6, -1.0);
            lblLife.TextAlignment = UITextAlignment.Center;
            viewLife.AddSubview(lblLife);

            editBtnLife = new UIButton(UIButtonType.Custom);
            UIImage deleteImg = UIImage.FromBundle("Delete");
            deleteImg = deleteImg.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            editBtnLife.SetImage(deleteImg, UIControlState.Normal);
            editBtnLife.TouchDown += EditBtnLife_TouchDown;
            editBtnLife.Frame = new CGRect(12, labelHeight - 12, 30, labelHeight - 10);
            viewLife.Add(editBtnLife);

            addBtnBody = new UIButton();
            addBtnBody.SetImage(addImg, UIControlState.Normal);
            addBtnBody.TouchDown += AddBtnBody_TouchDown;
            addBtnBody.Frame = new CGRect(frame.Width - 65, labelHeight - 12, 70, labelHeight - 10);
            viewBody.Add(addBtnBody);

            navBtnBody = new FabicButtonPurple();
            navBtnBody.SetTitle("<", UIControlState.Normal);
            navBtnBody.TouchDown += NavBtnBody_TouchDown;
            navBtnBody.TitleLabel.Lines = 3;
            navBtnBody.Frame = new CGRect(5, (frame.Height / 2) - 40, 40, 40);
            navBtnBody.Layer.CornerRadius = 20f;
            navBtnBody.TitleLabel.Font = UIFont.FromName("AvenirNext-Medium", 20);
            viewBody.Add(navBtnBody);

            lblBody = new UILabel(new CGRect(5, (frame.Height / 2), 40, 40));
            lblBody.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple);
            lblBody.Text = "Life";
            lblBody.Font = UIFont.FromName("AvenirNext-Medium", 13);
            //lblLife.ShadowColor = BehaviourLevelShadowColour;//UIColor.White.ColorWithAlpha(1f);
            //lblLife.ShadowOffset = new CGSize(0.6, -1.0);
            lblBody.TextAlignment = UITextAlignment.Center;
            viewBody.AddSubview(lblBody);

            editBtnBody = new UIButton();
            editBtnBody.SetImage(deleteImg, UIControlState.Normal);
            editBtnBody.TouchDown += EditBtnBody_TouchDown;
            editBtnBody.Frame = new CGRect(12, labelHeight - 12, 30, labelHeight - 10);
            viewBody.Add(editBtnBody);

            if (bodySelected)
                scrollView.SetContentOffset(new CGPoint(Frame.Width, 0), true);

            _AnimationLayer = new FabicButterflyAnimationLayer();
            this.Add(_AnimationLayer);
        }

        private void NavBtnBody_TouchDown(object sender, EventArgs e)
        {
            scrollView.SetContentOffset(new CGPoint(0, 0), true);
        }

        private void NavBtnLife_TouchDown(object sender, EventArgs e)
        {
            scrollView.SetContentOffset(new CGPoint(Frame.Width, 0), true);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (SecurityController.FirstTimeUsingBehaviourScaleItem)
                ShowEditTutorial?.Invoke(this, null);
        }

        /// <summary>
        /// Fades out the control and then removes it from the super view
        /// </summary>
        public void Hide()
        {
            UIView.Animate(
                0.5, // duration
                () => { Alpha = 0; },
                () => { RemoveFromSuperview(); if (this.CloseButtonPressed != null) { CloseButtonPressed(this, new EventArgs()); } }
            );
        }

        /// <summary>
        ///  Adds it to the super view and then  Fades in the control
        /// </summary>
        public void Show(UIView superview)
        {
            superview.AddSubview(this);
            Alpha = 0f;
            UIView.Animate(
                0.5, // duration
                () =>
                {
                    Alpha = 0.95f;
                },
                () => { }
            );
        }

        void ConfirmButton_TouchDown(object sender, EventArgs e)
        {
            Hide();
        }

        void HandleEventHandler(object sender, EventArgs e)
        {

        }

        void ScrollView_Scrolled(object sender, EventArgs e)
        {
            pageControl.CurrentPage = (int)Math.Floor(scrollView.ContentOffset.X / scrollView.Frame.Size.Width);
        }

        void AddBtnLife_TouchDown(object sender, EventArgs e)
        {
            FabicTextEditOverlay overlay = new FabicTextEditOverlay("", new BehaviourScaleItem(BehaviourScale.Id, "", BehaviourScaleLevel, Core.Enumerations.BehaviourScaleItemType.Life));
            overlay.FinishedEditingItem += OverlayAddLife_FinishedEditingItem;
            overlay.Show(superView);//((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers.Length - 1].View);
        }

        void OverlayAddLife_FinishedEditingItem(object sender, EventArgs e)
        {
            // refresh the item in the list and save i
            FabicTextEditOverlay overlay = (FabicTextEditOverlay)sender;
            BehaviourScaleItem item = (BehaviourScaleItem)overlay.EditingItem;
            item.Name = overlay.Text;

            FabicDatabaseController.SaveOrUpdateBehaviourScaleItem(item);
            BehaviourScaleItemsLife.Add(item);

            tableLife.ReloadData();
            _AnimationLayer.Animate(2);
        }

        void AddBtnBody_TouchDown(object sender, EventArgs e)
        {
            FabicTextEditOverlay overlay = new FabicTextEditOverlay("", new BehaviourScaleItem(BehaviourScale.Id, "", BehaviourScaleLevel, Core.Enumerations.BehaviourScaleItemType.Body));
            overlay.FinishedEditingItem += OverlayAddBody_FinishedEditingItem;
            overlay.Show(superView);//((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers.Length - 1].View);
        }

        void OverlayAddBody_FinishedEditingItem(object sender, EventArgs e)
        {
            // refresh the item in the list and save i
            FabicTextEditOverlay overlay = (FabicTextEditOverlay)sender;
            BehaviourScaleItem item = (BehaviourScaleItem)overlay.EditingItem;
            item.Name = overlay.Text;

            FabicDatabaseController.SaveOrUpdateBehaviourScaleItem(item);
            BehaviourScaleItemsBody.Add(item);

            tableBody.ReloadData();
            _AnimationLayer.Animate(2);
        }

        void EditBtnLife_TouchDown(object sender, EventArgs e)
        {
            if (tableLife.Editing)
            {
                tableLife.Editing = false;
                editBtnLife.SetTitle("Remove", UIControlState.Normal);
                editBtnLife.Font = UIFont.BoldSystemFontOfSize(15);
            }
            else
            {
                tableLife.Editing = true;
                editBtnLife.SetTitle("Done", UIControlState.Normal);
                editBtnLife.Font = UIFont.SystemFontOfSize(15, UIFontWeight.Heavy);
            }
        }

        void EditBtnBody_TouchDown(object sender, EventArgs e)
        {
            if (tableBody.Editing)
            {
                tableBody.Editing = false;
                editBtnBody.SetTitle("Remove", UIControlState.Normal);
                editBtnBody.Font = UIFont.BoldSystemFontOfSize(15);
            }
            else
            {
                tableBody.Editing = true;
                editBtnBody.SetTitle("Done", UIControlState.Normal);
                editBtnBody.Font = UIFont.SystemFontOfSize(15, UIFontWeight.Heavy);
            }
        }

        void HelpButton_TouchDown(object sender, EventArgs e)
        {
            // open the tutorial view
            EditBehaviourScaleTutorialViewController view = (EditBehaviourScaleTutorialViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("behaviourScaleEditTutorialView");
            view.ProvidesPresentationContextTransitionStyle = true;
            view.BodySelected = BodySelected;
            view.DefinesPresentationContext = true;
            view.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers.Length - 1].PresentedViewController.PresentViewController(view, true, null);
        }

        public void CleanUp()
        {

        }
    }
}
