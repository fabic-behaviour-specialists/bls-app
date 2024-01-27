using CoreGraphics;
using Fabic.Data.Extensions;
using Fabic.iOS.Controllers;
using Foundation;
using LocalAuthentication;
using System;
using UIKit;
using Xamarin.LockScreen;
using Xamarin.LockScreen.Security;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public class SettingsControllerTableViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        private UITableView TableSource = null;
        UILabel enabledLabel;
        UILabel disabledLabel;
        UILabel signOutLabel;
        UIImageView enabledImage;
        private UIViewController MainController;
        string CellIdentifier = "settingsCell";

        public SettingsControllerTableViewSource(UIViewController mainController)
        {
            MainController = mainController;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var context = new LAContext();
            var error = new NSError();
            UITableViewCell cell = null;//tableView.DequeueReusableCell(CellIdentifier);
            TableSource = tableView;

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
            }

            if (indexPath.Section == 0)
            {
                cell.TextLabel.Text = "Enable PIN Code Security";

                //if (disabledLabel != null)
                //	disabledLabel.RemoveFromSuperview();
                //if (enabledLabel != null)
                //	enabledLabel.RemoveFromSuperview();
                //if (enabledImage != null)
                //	enabledImage.RemoveFromSuperview();

                //cell.DetailTextLabel.Text =
                if (Keychain.IsPasswordSet())
                {
                    enabledImage = new UIImageView(UIImage.FromBundle("Tick"));
                    enabledImage.Frame = new CGRect(MainController.View.Frame.Width - 45, 5, 35, 35);
                    cell.AddSubview(enabledImage);

                    enabledLabel = new UILabel();
                    enabledLabel.Text = "Enabled";
                    enabledLabel.Font = UIFont.SystemFontOfSize(UIFont.SmallSystemFontSize, UIFontWeight.Regular);
                    enabledLabel.TextColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Gray);
                    enabledLabel.Frame = new CGRect(MainController.View.Frame.Width - 100, 15, 55, 15);
                    cell.AddSubview(enabledLabel);
                }
                else
                {
                    disabledLabel = new UILabel();
                    disabledLabel.Text = "Disabled";
                    disabledLabel.Font = UIFont.SystemFontOfSize(UIFont.SmallSystemFontSize, UIFontWeight.Regular);
                    disabledLabel.TextColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Gray);
                    disabledLabel.Frame = new CGRect(MainController.View.Frame.Width - 60, 15, 55, 15);
                    cell.AddSubview(disabledLabel);
                }
            }
            else if (indexPath.Section == 1)
            {
                cell.TextLabel.Text = "Change your password";
            }
            else if (false)//indexPath.Section == 2 && Keychain.IsPasswordSet() && context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out error))
            {
                // touch ID/face ID
                if (SecurityController.CheckBiometricAuthenticationType() == LABiometryType.FaceId)
                    cell.TextLabel.Text = "Enable Face ID";
                else
                    cell.TextLabel.Text = "Enable Touch ID";

                //if (disabledLabel != null)
                //	disabledLabel.RemoveFromSuperview();
                //if (enabledLabel != null)
                //	enabledLabel.RemoveFromSuperview();
                //if (enabledImage != null)
                //	enabledImage.RemoveFromSuperview();

                //cell.DetailTextLabel.Text =
                if (Keychain.IsTouchIDEnabled())
                {
                    enabledImage = new UIImageView(UIImage.FromBundle("Tick"));
                    enabledImage.Frame = new CGRect(MainController.View.Frame.Width - 45, 5, 35, 35);
                    cell.AddSubview(enabledImage);

                    enabledLabel = new UILabel();
                    enabledLabel.Text = "Enabled";
                    enabledLabel.Font = UIFont.SystemFontOfSize(UIFont.SmallSystemFontSize, UIFontWeight.Regular);
                    enabledLabel.TextColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Gray);
                    enabledLabel.Frame = new CGRect(MainController.View.Frame.Width - 100, 15, 55, 15);
                    cell.AddSubview(enabledLabel);
                }
                else
                {
                    disabledLabel = new UILabel();
                    disabledLabel.Text = "Disabled";
                    disabledLabel.Font = UIFont.SystemFontOfSize(UIFont.SmallSystemFontSize, UIFontWeight.Regular);
                    disabledLabel.TextColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Gray);
                    disabledLabel.Frame = new CGRect(MainController.View.Frame.Width - 60, 15, 55, 15);
                    cell.AddSubview(disabledLabel);
                }
            }
            else
            {
                signOutLabel = new UILabel();
                signOutLabel.Text = "Sign Out ";// + Controllers.SecurityController.CurrentUser.Name;
                signOutLabel.Font = UIFont.SystemFontOfSize(22);
                signOutLabel.TextColor = UIColor.Blue.FabicColour(Data.Enums.FabicColour.Red);
                signOutLabel.TextAlignment = UITextAlignment.Center;
                signOutLabel.Frame = new CGRect(0, 0, tableView.Frame.Width, cell.Frame.Height);
                cell.AddSubview(signOutLabel);
            }
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.CellAt(indexPath);
            var context = new LAContext();
            var error = new NSError();
            if (indexPath.Section == 0)
            {
                if (cell != null)
                {
                    if (!Keychain.IsPasswordSet())
                    {
                        setUpPIN();
                    }
                    else
                    {
                        unSetPIN();
                        Keychain.SaveTouchID(false);
                    }
                }
            }
            else if (indexPath.Section == 1)
            {
                // change password
                UIAlertController alert = UIAlertController.Create("Forgot Your Password", "What is your email address?", UIAlertControllerStyle.Alert);
                alert.AddTextField((UITextField f) =>
                {
                    f.Placeholder = "Your email address to send the instructions to";
                    f.TextColor = UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                });
                alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, (UIAlertAction action) => { }));
                alert.AddAction(UIAlertAction.Create("Continue", UIAlertActionStyle.Default, (UIAlertAction action) =>
                {
                    UITextField emailTextField = alert.TextFields[0];
                    bool result = SecurityController.ResetPassword(emailTextField.Text, "");
                    if (result)
                    {
                        UIAlertView subalert = new UIAlertView()
                        {
                            Title = "Forgot You Password",
                            Message = "If the email address you entered is one we recognise we have sent the instructions to it. Please check your inbox now."
                        };
                        subalert.TintColor = UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                        subalert.AddButton("OK");
                        subalert.Show();
                        alert.DismissViewControllerAsync(true);
                    }
                    else
                    {
                        UIAlertView subalert = new UIAlertView()
                        {
                            Title = "Unable to reset password",
                            Message = "An error occurred while resetting your password. Please try again later."
                        };
                        subalert.TintColor = UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                        subalert.AddButton("OK");
                        subalert.Show();
                        alert.DismissViewControllerAsync(true);
                    }
                }));
                MainController.PresentModalViewController(alert, true);
                cell.SetSelected(false, false);
            }
            else if (false)//(indexPath.Section == 2 && context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out error)) || (indexPath.Section == 1 && Keychain.IsPasswordSet() && context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out error)))
            {
                // touch ID
                if (Keychain.IsTouchIDEnabled())
                {
                    Keychain.SaveTouchID(false);
                }
                else
                {
                    Keychain.SaveTouchID(true);
                }
            }
            else
            {
                // sign out
                Controllers.SecurityController.SignOut();

                UIStoryboard uis = UIStoryboard.FromName("Main", null);
                UIViewController vc = uis.InstantiateViewController("loginVC");
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController = new UINavigationController(vc);
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetToolbarHidden(true, false);
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(false, false);
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BackgroundColor = UIColor.Clear;
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BarTintColor = UIColor.Clear;
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.TintColor = UIColor.Clear;
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.ShadowImage = new UIImage();
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.Translucent = false;

                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.Toolbar.TintColor = UIColor.White;
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.Toolbar.BarTintColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple);

                UIColor white = UIColor.White;

                UIStringAttributes myTextAttrib = new UIStringAttributes() { StrokeColor = white, ForegroundColor = white, Font = UIFont.FromName("Avenir-Heavy", 20) };
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.TitleTextAttributes = myTextAttrib;

                ((AppDelegate)UIApplication.SharedApplication.Delegate).Window.RootViewController = ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController;
            }
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            // then check if touch ID is supported
            var context = new LAContext();
            var error = new NSError();

            if (false)//context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out error))
            {
                // check if the PIN is enabled
                if (Keychain.IsPasswordSet())
                    return 4;
            }

            return 3;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 1;
        }

        public override string TitleForFooter(UITableView tableView, nint section)
        {
            var context = new LAContext();
            var error = new NSError();

            if (section == 0)
                return "Add more security to the app by securing your information with a PIN";
            else if (section == 1)
                return "Update your password in case you have forgotten it";
            else if (false)//section == 2 && Keychain.IsPasswordSet() && context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out error))
            {
                if (SecurityController.CheckBiometricAuthenticationType() == LABiometryType.FaceId)
                    return "Enabled Face ID Authentication for faster and more secure security of your information";
                else
                    return "Enabled Touch ID Authentication for faster and more secure security of your information";
            }
            else
                return "";
        }

        public void CleanUp()
        {

        }

        private void setUpPIN()
        {
            var lockScreen = new LockScreenSetupController(new LockHandler(MainController));
            UIImageView img = new UIImageView(UIImage.FromFile("Waves.png"));
            img.ContentMode = UIViewContentMode.ScaleAspectFill;
            lockScreen.LockScreenView.SetupBackgroundView(img);

            UnlockApplication();
            lockScreen.Show(MainController);
            TableSource.ReloadData();
        }

        private void unSetPIN()
        {
            Keychain.SavePassword(null);
            TableSource.ReloadData();
        }

        public static void UnlockApplication()
        {
            NSUserDefaults settings = NSUserDefaults.StandardUserDefaults;
            settings.SetBool(false, "IsLocked");
            settings.Synchronize();
        }
    }
}
