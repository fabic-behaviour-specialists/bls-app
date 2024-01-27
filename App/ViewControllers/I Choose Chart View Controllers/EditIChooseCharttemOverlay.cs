using CoreGraphics;
using Fabic.Core.Controllers;
using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Fabic.Data.Enums;
using Fabic.Data.Extensions;
using Fabic.iOS.ViewControllers.TableViewSources;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS
{
    public class EditIChooseChartItemOverlay : UIView, IDisposable, ICanCleanUpMyself
    {

        // control declarations
        //UIActivityIndicatorView activitySpinner;
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
        FabicButton navBtnLife; FabicButton navBtnBody;
        UIImageView butterfly;
        UIView superView;

        // behaviour level related
        IChooseChart IChooseChart;
        Core.Enumerations.IChooseChartItemType IChooseChartItemType;
        UIColor ItemLevelColour;
        UIColor ItemLevelShadowColour;
        string ItemLevelOption1Title;
        string ItemLevelOption2Title;
        string ItemLevelOption1SubTitle;
        string ItemLevelOption2SubTitle;
        List<IChooseChartItem> IChooseChartItemsOption1 = new List<IChooseChartItem>();
        List<IChooseChartItem> IChooseChartItemsOption2 = new List<IChooseChartItem>();

        public event EventHandler CloseButtonPressed;
        public event EventHandler ShowEditTutorial;

        public EditIChooseChartItemOverlay(IChooseChart iChooseChart, CGRect frame, Core.Enumerations.IChooseChartItemType iChooseChartItemType, List<IChooseChartItem> IChooseChartItems, UIView SuperView, bool Option2Selected = true) : base(frame)
        {
            this.ApplyLightInterface();
            IChooseChart = iChooseChart;
            IChooseChartItemType = iChooseChartItemType;
            superView = SuperView;

            foreach (IChooseChartItem item in IChooseChartItems)
            {
                if ((Core.Enumerations.IChooseChartOption)item.ChartOption == Core.Enumerations.IChooseChartOption.Option1)
                    IChooseChartItemsOption1.Add(item);
                else
                    IChooseChartItemsOption2.Add(item);
            }

            switch (IChooseChartItemType)
            {
                case Core.Enumerations.IChooseChartItemType.Behaviour: // Behaviour
                    ItemLevelColour = UIColor.Blue.FabicColour(FabicColour.Purple);
                    ItemLevelShadowColour = UIColor.White;
                    ItemLevelOption1Title = "New Skills - Option 1";
                    ItemLevelOption2Title = "Possible Reactions - Option 2";
                    ItemLevelOption1SubTitle = "What are the skills that need to be taught and/or learnt to support with this part of life?";
                    ItemLevelOption2SubTitle = "What are the behaviours, words, thoughts or feelings currently used when this part of life is presented?";
                    break;
                case Core.Enumerations.IChooseChartItemType.Outcome: // Green
                    ItemLevelColour = UIColor.Green.FabicColour(FabicColour.Purple);
                    ItemLevelShadowColour = UIColor.White;
                    ItemLevelOption1Title = "Natural Consequences - Option 1";
                    ItemLevelOption2Title = "Natural Consequences - Option 2";
                    ItemLevelOption1SubTitle = "What are the desired outcomes in this part of life?";
                    ItemLevelOption2SubTitle = "What are the outcomes of using the current behaviours, words, thoughts or feelings in this part of life?";
                    break;
            }

            // configurable bits
            BackgroundColor = UIColor.Clear.ColorWithAlpha(0.88f);
            Layer.CornerRadius = 17;
            Layer.ShadowRadius = 8;
            Layer.ShadowColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple).CGColor;
            Layer.BorderWidth = 2;
            Layer.BorderColor = ItemLevelColour.ColorWithAlpha(0.9f).CGColor;
            ClipsToBounds = true;

            nfloat labelHeight = 50;
            nfloat labelWidth = Frame.Width;

            // derive the center x and y
            nfloat centerX = Frame.Width / 2;

            UIView background = new UIView();
            background.Frame = new CGRect(0, 0, frame.Width, frame.Height);
            background.BackgroundColor = ItemLevelColour.ColorWithAlpha(0.7f);
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
            tableLife.Frame = new CGRect(10, labelHeight * 1.8, frame.Width - 56, frame.Height - 120 - (25 + (labelHeight * 1.5)));
            tableLife.BackgroundColor = ItemLevelColour.ColorWithAlpha(0.1f);
            tableLife.Layer.CornerRadius = 30;
            tableLife.Layer.ShadowOffset = new CGSize(2f, 2f);
            tableLife.Layer.ShadowColor = UIColor.Black.CGColor;
            tableLife.SeparatorColor = ItemLevelColour.ColorWithAlpha(0.8f);
            tableLife.Layer.ShadowRadius = 8;
            tableLife.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            tableLife.Source = new EditIChooseChartViewSource(IChooseChartItemsOption1, this);
            viewLife.AddSubview(tableLife);

            titleLabel = new UILabel(new CGRect(0, 10, labelWidth, labelHeight));
            titleLabel.TextColor = ItemLevelColour;//UIColor.Clear.FabicColour(Data.Enums.FabicColour.LightBlue);
            titleLabel.Text = ItemLevelOption1Title;
            titleLabel.Font = UIFont.FromName("AvenirNext-Medium", 20);
            titleLabel.ShadowColor = ItemLevelShadowColour;//UIColor.White.ColorWithAlpha(1f);
            titleLabel.ShadowOffset = new CGSize(0.6, -1.0);
            titleLabel.TextAlignment = UITextAlignment.Center;
            viewLife.AddSubview(titleLabel);

            subTitleLabel = new UILabel(new CGRect(labelHeight + 4, labelHeight - 30, frame.Width - 110, labelHeight * 2));
            subTitleLabel.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.LightBlue);
            subTitleLabel.Text = ItemLevelOption1SubTitle;
            subTitleLabel.Font = UIFont.FromName("Avenir-LightOblique", 11);
            subTitleLabel.ShadowColor = ItemLevelColour;//UIColor.White.ColorWithAlpha(1f);
            subTitleLabel.ShadowOffset = new CGSize(0.6, -1.0);
            subTitleLabel.TextAlignment = UITextAlignment.Center;
            subTitleLabel.Lines = 2;
            viewLife.AddSubview(subTitleLabel);

            tableBody = new UITableView();
            tableBody.Frame = new CGRect(50, labelHeight * 1.8, frame.Width - 60, frame.Height - 120 - (25 + (labelHeight * 1.5)));
            tableBody.Layer.CornerRadius = 30;
            tableBody.Layer.ShadowOffset = new CGSize(2f, 2f);
            tableBody.Layer.ShadowColor = UIColor.Black.CGColor;
            tableBody.SeparatorColor = ItemLevelColour.ColorWithAlpha(0.8f);
            tableBody.BackgroundColor = ItemLevelColour.ColorWithAlpha(0.1f);
            tableBody.Layer.ShadowRadius = 8;
            tableBody.Source = new EditIChooseChartViewSource(IChooseChartItemsOption2, this);
            viewBody.AddSubview(tableBody);

            titleLabel = new UILabel(new CGRect(centerX - (labelWidth / 2), 10, labelWidth, labelHeight));
            titleLabel.TextColor = UIColor.White.FabicColour(Data.Enums.FabicColour.Blue);
            titleLabel.TextAlignment = UITextAlignment.Center;
            titleLabel.Text = ItemLevelOption2Title;
            titleLabel.TextColor = ItemLevelColour;
            titleLabel.Font = UIFont.FromName("AvenirNext-Medium", 20);
            titleLabel.ShadowColor = ItemLevelShadowColour;//UIColor.White.ColorWithAlpha(1f);
            titleLabel.ShadowOffset = new CGSize(0.6, -1.0);
            viewBody.AddSubview(titleLabel);

            subTitleLabel = new UILabel(new CGRect(30 + 12 + 5, labelHeight - 30, frame.Width - 100, labelHeight * 2));
            subTitleLabel.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.LightBlue);
            subTitleLabel.Text = ItemLevelOption2SubTitle;
            subTitleLabel.Font = UIFont.FromName("Avenir-LightOblique", 11);
            subTitleLabel.ShadowColor = ItemLevelColour;//UIColor.White.ColorWithAlpha(1f);
            subTitleLabel.ShadowOffset = new CGSize(0.6, -1.0);
            subTitleLabel.TextAlignment = UITextAlignment.Center;
            subTitleLabel.Lines = 2;
            viewBody.AddSubview(subTitleLabel);

            addBtnLife = new UIButton(UIButtonType.Custom);
            UIImage addImg = UIImage.FromBundle("Add-Blue");
            addImg = addImg.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            addBtnLife.SetImage(addImg, UIControlState.Normal);
            addBtnLife.TouchDown += AddBtnLife_TouchDown;
            addBtnLife.Frame = new CGRect(frame.Width - 65, labelHeight - 12, 70, labelHeight - 10);
            viewLife.Add(addBtnLife);

            navBtnLife = new FabicButton();
            navBtnLife.FabicColour = FabicColour.Blue;
            navBtnLife.SetTitle(">", UIControlState.Normal);
            navBtnLife.TouchDown += NavBtnLife_TouchDown;
            navBtnLife.TitleLabel.Lines = 3;
            navBtnLife.Frame = new CGRect(frame.Width - 43, (frame.Height / 2) - 40, 40, 40);
            navBtnLife.Layer.CornerRadius = 20f;
            navBtnLife.TitleLabel.Font = UIFont.FromName("AvenirNext-Medium", 20);
            viewLife.Add(navBtnLife);

            lblLife = new UILabel(new CGRect(frame.Width - 43, (frame.Height / 2), 40, 56));
            lblLife.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue);
            lblLife.Text = "Option 2";
            lblLife.Lines = 2;
            lblLife.Font = UIFont.FromName("AvenirNext-Medium", 11);
            //lblLife.ShadowColor = BehaviourLevelShadowColour;//UIColor.White.ColorWithAlpha(1f);
            //lblLife.ShadowOffset = new CGSize(0.6, -1.0);
            lblLife.TextAlignment = UITextAlignment.Center;
            viewLife.AddSubview(lblLife);

            editBtnLife = new UIButton(UIButtonType.Custom);
            UIImage deleteImg = UIImage.FromBundle("Delete-Blue");
            deleteImg = deleteImg.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            editBtnLife.SetImage(deleteImg, UIControlState.Normal);
            editBtnLife.TouchDown += EditBtnLife_TouchDown;
            editBtnLife.Frame = new CGRect(12, labelHeight - 12, 30, labelHeight - 10);
            viewLife.Add(editBtnLife);

            addBtnBody = new UIButton(UIButtonType.Custom);
            addBtnBody.SetImage(addImg, UIControlState.Normal);
            addBtnBody.TouchDown += AddBtnBody_TouchDown;
            addBtnBody.Frame = new CGRect(frame.Width - 65, labelHeight - 12, 70, labelHeight - 10);
            viewBody.Add(addBtnBody);

            navBtnBody = new FabicButton();
            navBtnBody.FabicColour = FabicColour.Blue;
            navBtnBody.SetTitle("<", UIControlState.Normal);
            navBtnBody.TouchDown += NavBtnBody_TouchDown;
            navBtnBody.TitleLabel.Lines = 3;
            navBtnBody.Frame = new CGRect(5, (frame.Height / 2) - 40, 40, 40);
            navBtnBody.Layer.CornerRadius = 20f;
            navBtnBody.TitleLabel.Font = UIFont.FromName("AvenirNext-Medium", 20);
            viewBody.Add(navBtnBody);

            lblBody = new UILabel(new CGRect(5, (frame.Height / 2), 40, 56));
            lblBody.TextColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Blue);
            lblBody.Text = "Option 1";
            lblBody.Lines = 2;
            lblBody.Font = UIFont.FromName("AvenirNext-Medium", 11);
            //lblLife.ShadowColor = BehaviourLevelShadowColour;//UIColor.White.ColorWithAlpha(1f);
            //lblLife.ShadowOffset = new CGSize(0.6, -1.0);
            lblBody.TextAlignment = UITextAlignment.Center;
            viewBody.AddSubview(lblBody);

            editBtnBody = new UIButton(UIButtonType.Custom);
            editBtnBody.SetImage(deleteImg, UIControlState.Normal);
            editBtnBody.TouchDown += EditBtnBody_TouchDown;
            editBtnBody.Frame = new CGRect(12, labelHeight - 12, 30, labelHeight - 10);
            viewBody.Add(editBtnBody);

            if (Option2Selected)
                scrollView.SetContentOffset(new CGPoint(Frame.Width, 0), true);
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
            if (Controllers.SecurityController.FirstTimeUsingIChooseChartItem)
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
                () => { Alpha = 0.95f; },
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
            FabicTextEditOverlay overlay = new FabicTextEditOverlay("", new IChooseChartItem(IChooseChart.Id, "", Core.Enumerations.IChooseChartOption.Option1, IChooseChartItemType));
            overlay.FinishedEditingItem += OverlayAddLife_FinishedEditingItem;
            overlay.Show(superView);
        }

        void OverlayAddLife_FinishedEditingItem(object sender, EventArgs e)
        {
            // refresh the item in the list and save i
            FabicTextEditOverlay overlay = (FabicTextEditOverlay)sender;
            IChooseChartItem item = (IChooseChartItem)overlay.EditingItem;
            item.ItemText = overlay.Text;

            FabicDatabaseController.SaveOrUpdateIChooseChartItem(item);
            IChooseChartItemsOption1.Add(item);

            tableLife.ReloadData();
        }

        void AddBtnBody_TouchDown(object sender, EventArgs e)
        {
            FabicTextEditOverlay overlay = new FabicTextEditOverlay("", new IChooseChartItem(IChooseChart.Id, "", Core.Enumerations.IChooseChartOption.Option2, IChooseChartItemType));
            overlay.FinishedEditingItem += OverlayAddBody_FinishedEditingItem;
            overlay.Show(superView);
        }

        void OverlayAddBody_FinishedEditingItem(object sender, EventArgs e)
        {
            // refresh the item in the list and save i
            FabicTextEditOverlay overlay = (FabicTextEditOverlay)sender;
            IChooseChartItem item = (IChooseChartItem)overlay.EditingItem;
            item.ItemText = overlay.Text;

            FabicDatabaseController.SaveOrUpdateIChooseChartItem(item);
            IChooseChartItemsOption2.Add(item);

            tableBody.ReloadData();
        }

        void EditBtnLife_TouchDown(object sender, EventArgs e)
        {
            if (tableLife.Editing)
            {
                tableLife.Editing = false;
                editBtnLife.SetTitle("Edit", UIControlState.Normal);
                editBtnLife.Font = UIFont.SystemFontOfSize(15);
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
                editBtnBody.SetTitle("Edit", UIControlState.Normal);
                editBtnBody.Font = UIFont.SystemFontOfSize(15);
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
            UIViewController view = UIStoryboard.FromName("Main", null).InstantiateViewController("behaviourScaleEditTutorialView");
            view.ProvidesPresentationContextTransitionStyle = true;
            view.DefinesPresentationContext = true;
            view.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers[((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.ViewControllers.Length - 1].PresentedViewController.PresentViewController(view, true, null);
        }

        public void CleanUp()
        {

        }
    }
}
