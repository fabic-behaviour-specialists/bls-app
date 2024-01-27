using Foundation;
using System;
using Xamarin.LockScreen.Interfaces;

namespace Xamarin.LockScreen
{
    [Register("LockScreenSetupController")]
    internal class LockScreenSetupController : BaseLockScreenController
    {
        private ILockScreenDelegate setupDelegate { get; set; }
        private string EnteredPin { get; set; }

        #region Constructors

        public LockScreenSetupController(IntPtr handle) : base(handle) { }

        public LockScreenSetupController(ILockScreenDelegate setupDelegate) : base(false, setupDelegate, true)
        {
            this.setupDelegate = setupDelegate;
            this.EnteredPin = string.Empty;
            this.LockScreenView.DetailLabel.Text = "Please enter new pin.";
        }

        #endregion
        #region Pin Proccessing

        protected override void ProcessPin()
        {
            if (String.IsNullOrEmpty(EnteredPin))
            {
                StartPinConfirmation();
            }
            else
            {
                ValidateConfirmedPin();
            }
        }

        private void StartPinConfirmation()
        {
            EnteredPin = currentPin;
            currentPin = string.Empty;
            LockScreenView.UpdateDetailLabelWithString("Re-enter your new pincode".Translate(),
                true, null);
            LockScreenView.ResetAnimated(true);
        }
        private void ValidateConfirmedPin()
        {
            if (EnteredPin.Equals(currentPin))
            {
                setupDelegate.PinSet(currentPin, this);
            }
            else
            {
                LockScreenView.UpdateDetailLabelWithString("Pincode mis-match. Try again.".Translate(), true, null);
                LockScreenView.AnimateFailureNotification();
                LockScreenView.ResetAnimated(true);
                currentPin = string.Empty;
            }
        }

        #endregion
        #region View Controller Stuff

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
        #endregion
    }
}

