using CoreGraphics;
using Foundation;
using LocalAuthentication;
using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.LockScreen.Interfaces;
using Xamarin.LockScreen.Views;

namespace Xamarin.LockScreen
{
    public class BaseLockScreenController : UIViewController
    {
        public LockScreenView LockScreenView { get { return (LockScreenView)View; } }
        protected string currentPin = "";
        private bool isComplexPin = false;
        private bool forNewPIN = false;
        private ILockScreenDelegate lockDelegate;

        public BaseLockScreenController(IntPtr handle) : base(handle)
        {

        }

        public BaseLockScreenController(bool complexPin, ILockScreenDelegate lockDelegate, bool forNewPIN = false) : base()
        {
            this.isComplexPin = complexPin;
            this.lockDelegate = lockDelegate;
            this.forNewPIN = forNewPIN;
        }
        #region View Controller Lifecycle Methods

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View = new LockScreenView((System.Drawing.RectangleF)View.Bounds, isComplexPin);
            SetupButtonMapping();
            UIImageView imageView = new UIImageView(UIImage.FromFile("Waves"));
            UIImageView footerView = new UIImageView(UIImage.FromFile("fabic-footer"));
            imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
            footerView.ContentMode = UIViewContentMode.ScaleAspectFill;

            SetBackgroundView(imageView);

            footerView.Frame = new CGRect(0, this.View.Frame.Height - 50, this.View.Frame.Width, 50);
            this.View.AddSubview(footerView);

            LockScreenView.CancelButton.TouchUpInside += CancelButtonSelected;
            LockScreenView.DeleteButton.TouchUpInside += DeleteButtonSelected;
            LockScreenView.OkButton.TouchUpInside += OkButtonSelected;
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            var current = UIDevice.CurrentDevice.UserInterfaceIdiom;
            if (current == UIUserInterfaceIdiom.Pad)
                return UIInterfaceOrientationMask.All;
            if (current == UIUserInterfaceIdiom.Phone)
                return UIInterfaceOrientationMask.Portrait | UIInterfaceOrientationMask.PortraitUpsideDown;

            return UIInterfaceOrientationMask.All;
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            if (LockScreenView.BackgroundView != null)
                return UIStatusBarStyle.LightContent;
            var color = LockScreenView.BackgroundColor;
            if (color == null)
                color = LockScreenView.BackgroundColor = UIColor.Black;
            List<float> componentColors = new List<float>();
            foreach (var item in color.CGColor.Components)
            {
                componentColors.Add((float)item);
            }
            float colorBrightness = (componentColors.Count == 2 ? componentColors[0] :
                ((componentColors[0] * 299) + (componentColors[1] * 587) + (componentColors[2] * 114)) / 1000);

            return colorBrightness < 0.5 ? UIStatusBarStyle.LightContent : UIStatusBarStyle.Default;
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            // if touchID is supported use that to authenticate

            var context = new LAContext();
            var error = new NSError();

            if (!forNewPIN)
            {
                if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out error))
                {
                    var authenticated = await context.EvaluatePolicyAsync(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, "Login to Fabic App");
                    if (authenticated.Item1)
                    {
                        lockDelegate.UnlockWasSuccessfulForPadLockScreenViewController(this);
                    }
                }
            }
        }

        #endregion

        #region Localization

        internal void SetLockScreenTitle(string title)
        {
            Title = title;
            LockScreenView.EnterPasscodeLabel.Text = title;
        }
        internal void SetSubtitleText(string text)
        {
            LockScreenView.DetailLabel.Text = text;
        }
        internal void SetCancelButtonText(string text)
        {
            LockScreenView.CancelButton.SetTitle(text, UIControlState.Normal);
            LockScreenView.CancelButton.SizeToFit();
        }

        internal void SetDeleteButtonText(string text)
        {
            LockScreenView.DeleteButton.SetTitle(text, UIControlState.Normal);
            LockScreenView.DeleteButton.SizeToFit();
        }

        internal void SetBackgroundView(UIView backgroundView)
        {
            LockScreenView.SetupBackgroundView(backgroundView);
            if (UIDevice.CurrentDevice.CheckSystemVersion(6, 1))
                SetNeedsStatusBarAppearanceUpdate();
        }

        #endregion

        #region Helper Methods


        private void SetupButtonMapping()
        {
            foreach (var button in LockScreenView.ButtonArray)
            {
                button.TouchUpInside += ButtonSelected;
            }
        }
        private void ButtonSelected(object sender, EventArgs ea)
        {
            UIButton button = (UIButton)sender;
            int pin = (int)button.Tag;
            NewPinSelected(pin);
        }
        private void CancelButtonSelected(object sender, EventArgs ea)
        {
            if (lockDelegate != null)
            {
                lockDelegate.UnlockWasCancelledForPadLockScreen(this);
            }
        }
        private void DeleteButtonSelected(object sender, EventArgs ea)
        {
            DeleteFromPin();
        }
        private void OkButtonSelected(object sender, EventArgs ea)
        {
            ProcessPin();
        }
        internal void CancelButtonDisabled(bool disabled)
        {
            LockScreenView.CancelButtonDisabled = disabled;
        }
        protected virtual void ProcessPin()
        {
            throw new NotImplementedException(
                "You must sublass this controller and customize the process to your needs.");
        }


        #endregion

        #region Button Methods

        internal void NewPinSelected(int pinNumber)
        {
            if (isComplexPin && currentPin.Length >= 4)
                return;

            currentPin = string.Format("{0}{1}", currentPin, pinNumber);
            if (isComplexPin)
                LockScreenView.UpdatePinTextFieldWithLength(currentPin.Length);
            else
            {
                int currentlySelected = currentPin.Length - 1;
                LockScreenView.DigitArray[currentlySelected].SetSelected(true, true, null);
            }
            if (currentPin.Length == 1)
            {
                LockScreenView.ShowDeleteButtonAnimated(true);
                if (isComplexPin)
                    LockScreenView.ShowOKButtonAnimated(true, true);
            }
            else if (!isComplexPin && currentPin.Length == 4)
            {
                LockScreenView.DigitArray[LockScreenView.DigitArray.Length - 1].SetSelected(true, true, null);
                ProcessPin();
            }
        }

        internal void DeleteFromPin()
        {
            if (currentPin.Length == 0)
                return;
            currentPin = currentPin.Remove(currentPin.Length - 1, 1);
            if (isComplexPin)
                LockScreenView.UpdatePinTextFieldWithLength(currentPin.Length);
            else
            {
                int pinToDelselect = currentPin.Length;
                LockScreenView.DigitArray[pinToDelselect].SetSelected(false, true, null);
            }
            if (currentPin.Length == 0)
            {
                LockScreenView.ShowCancelButtonAnimated(true);
                LockScreenView.ShowOKButtonAnimated(false, true);
            }
        }

        public async void Show(UIViewController presentOver)
        {
            this.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            this.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
            await presentOver.PresentViewControllerAsync(this, true);
        }

        #endregion
    }
}
