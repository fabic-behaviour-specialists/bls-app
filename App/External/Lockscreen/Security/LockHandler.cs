using Curse;
using Fabic.Data.Extensions;
using Fabic.iOS.Controllers;
using Foundation;
using LocalAuthentication;
using System;
using UIKit;
using Xamarin.LockScreen.Interfaces;

namespace Xamarin.LockScreen.Security
{
    public class LockHandler : ILockScreenDelegate
    {
        private UIViewController parent;
        public event EventHandler PinSetWasSuccessful;
        public event EventHandler PinSetWasCancelled;

        public LockHandler(UIViewController parent)
        {
            this.parent = parent;
        }
        public virtual async void UnlockWasCancelledForPadLockScreen(BaseLockScreenController padLockScreenController)
        {
            await parent.DismissViewControllerAsync(true);
        }
        public virtual async void UnlockWasSuccessfulForPadLockScreenViewController(BaseLockScreenController padLockScreenController)
        {
            MainLockScreenController.UnlockApplication();
            await parent.DismissViewControllerAsync(true);
            PinSetWasCancelled?.Invoke(this, new EventArgs())
;
        }
        public virtual void UnlockWasUnsuccessful(string badPin, int afterAttempt, BaseLockScreenController padLockScreenController)
        {
            Console.WriteLine("Failed attempt number {0} with pin: {1}", afterAttempt, badPin);
        }

        public virtual void AttemptsExpiredForPadLockScreenViewController(BaseLockScreenController padLockScreenController)
        {
            Console.WriteLine("User has been locked out...");
        }
        public virtual bool ValidatePin(BaseLockScreenController padLockScreenController, string pin)
        {
            var savedPin = Keychain.GetPassword();
            return savedPin.Equals(pin);
        }

        /// <summary>
        /// <para>
        /// Called right after the pin has been verified and set. Then saves the pin to the user's keychain.
        ///  Override this to perform custom saves of pin.</para>
        /// <para></para>
        /// <para>
        /// NOTE: At a minimum when overriding, call
        ///  <code>
        /// ((ILockableScreen)padLockScreenSetupViewController).IsLocked = false;
        /// </code>
        /// to notify the controller to not re-present the lock screen.
        /// </para>
        /// </summary>
        /// <param name="pin">The pin the user entered.</param>
        /// <param name="padLockScreenSetupViewController">The controller who is in charge of setting the new pin.</param>
        /// <remarks>>At a minimum when overriding, call ((ILockableScreen)parent).IsLocked = false; to
        /// notify the controller to not re-present the lock screen.
        /// </remarks>
        public virtual async void PinSet(string pin, BaseLockScreenController padLockScreenSetupViewController)
        {
            // first save the PIN
            Keychain.SavePassword(pin);

            // then check if touch ID is supported
            var context = new LAContext();
            var error = new NSError();

            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out error))
            {
                if (SecurityController.CheckBiometricAuthenticationType() == LABiometryType.TouchId)
                {
                    // ask if they would like to support touch ID
                    CRSAlertView alert = new CRSAlertView();
                    alert.TintColor = UIColor.Purple.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                    if (SecurityController.CheckBiometricAuthenticationType() == LABiometryType.FaceId)
                    {
                        alert.Title = "Login with FaceID";
                        alert.Message = "Your device supports FaceID. Would you like to sign into Fabic App using FaceID?";
                    }
                    else
                    {
                        alert.Title = "Login with TouchID";
                        alert.Message = "Your device supports TouchID. Would you like to sign into Fabic App using TouchID?";
                    }
                    alert.Image = new UIImage("butterfly.png");

                    var action = new CRSAlertAction
                    {
                        Text = "No",
                        Highlighted = false,
                        TintColor = UIColor.Black,
                        DidSelect = (alert2) =>
                        {
                            // Do something here on press
                            Keychain.SaveTouchID(false);
                        }
                    };

                    var action2 = new CRSAlertAction
                    {
                        Text = "Yes",
                        Highlighted = true,
                        TintColor = UIColor.Cyan.FabicColour(Fabic.Data.Enums.FabicColour.Purple),
                        DidSelect = (alert2) =>
                        {
                            // Do something here on press
                            Keychain.SaveTouchID(true);
                        }
                    };

                    alert.Actions = new CRSAlertAction[] { action, action2 };
                    alert.Show();
                }
            }

            MainLockScreenController.UnlockApplication();
            await parent.DismissViewControllerAsync(true);
            PinSetWasSuccessful?.Invoke(this, new EventArgs())
;
        }

    }
}

