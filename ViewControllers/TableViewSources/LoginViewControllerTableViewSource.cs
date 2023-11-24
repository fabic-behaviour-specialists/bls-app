using CoreAnimation;
using CoreGraphics;
using Curse;
using Fabic.Core.Helpers;
using Fabic.Data.Extensions;
using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;
using Xamarin.LockScreen;
using Xamarin.LockScreen.Security;

namespace Fabic.iOS.ViewControllers.TableViewSources
{
    public class LoginViewControllerTableViewSource : UITableViewSource, IDisposable, ICanCleanUpMyself
    {
        private UIActivityIndicatorView indicatorView;
        private UILabel about;
        private UILabel subTitle;
        private FabicButton login;
        private UIImageView imageAllAmazing;
        private FabicButton register;
        private UITableView TableSource = null;
        string CellIdentifier = "TableCell";

        public event EventHandler ShowAlert;
        public event EventHandler DismissAlert;
        public event EventHandler Animate;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            TableSource = tableView;

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
            }

            bool iPhone5 = tableView.Frame.Width <= 330;

            if (cell.Tag != 200)
            {
                if (iPhone5)
                {
                    tableView.Frame = new CGRect(0, tableView.Frame.Y - 40, tableView.Frame.Width, tableView.Frame.Height + 50);
                }

                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                cell.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0);
                cell.TranslatesAutoresizingMaskIntoConstraints = false;

                double centerX = tableView.Frame.Width / 2;

                // title
                CAGradientLayer gradient = new CAGradientLayer();
                CGColor[] array = new CGColor[2];
                array[0] = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Purple).CGColor;
                array[1] = UIColor.Purple.FabicColour(Data.Enums.FabicColour.LightBlue).CGColor;
                gradient.Colors = array;
                gradient.Frame = new CGRect(0, 0, tableView.Frame.Width, 110);
                UIView view = new UIView();
                view.Frame = new CGRect(20, 0, tableView.Frame.Width, 110);
                view.Layer.AddSublayer(gradient);

                about = new UILabel();
                about.Text = "Welcome to the Body Life Skills Program!";
                about.TextAlignment = UITextAlignment.Center;
                about.Lines = 3;
                if (iPhone5)
                    about.Font = UIFont.SystemFontOfSize(30);
                else
                    about.Font = UIFont.SystemFontOfSize(35);
                about.Frame = new CGRect(5, 0, tableView.Frame.Width - 40, 110);
                //view.Add(about);

                view.MaskView = about;
                cell.ContentView.Add(view);

                // subtitle
                subTitle = new UILabel();
                subTitle.Text = "A simple and powerful program supporting true and lasting behaviour change";
                subTitle.TextAlignment = UITextAlignment.Center;
                subTitle.TextColor = UIColor.Black.FabicColour(Data.Enums.FabicColour.Purple);
                if (iPhone5)
                    subTitle.Font = UIFont.FromName("HelveticaNeue-Light", 15f);
                else
                    subTitle.Font = UIFont.FromName("HelveticaNeue-Light", 20f);

                subTitle.Lines = 3;
                subTitle.Frame = new CGRect(40, 50, tableView.Frame.Width - 80, 200);
                cell.ContentView.Add(subTitle);

                imageAllAmazing = new UIImageView(UIImage.FromBundle("AllAmazing"));
                imageAllAmazing.ContentMode = UIViewContentMode.ScaleAspectFit;
                imageAllAmazing.Frame = new CGRect(30, 200, tableView.Frame.Width - 60, (iPhone5 ? tableView.Frame.Height - 300 : tableView.Frame.Height - 320));
                cell.ContentView.Add(imageAllAmazing);

                // sign up and register
                login = new FabicButton();
                login.FabicColour = Data.Enums.FabicColour.Blue;
                login.SetTitle("Sign In", UIControlState.Normal);
                login.Frame = new CGRect(8, tableView.Frame.Height - 100, tableView.Frame.Width / 2 - 12, 50);
                login.TouchDown += LoginButton_TouchDown;
                login.Enabled = true;
                cell.ContentView.Add(login);

                register = new FabicButton();
                register.FabicColour = Data.Enums.FabicColour.Purple;
                register.SetTitle("Register", UIControlState.Normal);
                register.Frame = new CGRect(centerX + 4, tableView.Frame.Height - 100, tableView.Frame.Width / 2 - 12, 50);
                register.TouchDown += RegisterButton_TouchDown;
                register.Enabled = true;
                cell.ContentView.Add(register);

                indicatorView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
                indicatorView.Frame = new CGRect(0, 250, tableView.Frame.Width, tableView.Frame.Height - 300);
                indicatorView.HidesWhenStopped = true;
                indicatorView.Color = UIColor.Black.FabicColour(Data.Enums.FabicColour.Purple);
                indicatorView.Hidden = true;
                cell.ContentView.Add(indicatorView);

                cell.Tag = 200; // mark as loaded;
            }

            return cell;
        }

        private void RegisterButton_TouchDown(object sender, EventArgs e)
        {
            Curse.CRSRegistrationView alert = new CRSRegistrationView();
            alert.TintColor = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Purple);
            alert.Title = "Body Life Skills Registration";
            alert.Message = "Please enter details";
            alert.Image = new UIImage("butterfly.png");

            CRSRegistrationView.InputTextColor = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Purple);

            var action = new CRSRegistrationAction
            {
                Text = "Cancel",
                Highlighted = false,
                TintColor = UIColor.Black,
                DidSelect = (alert2) =>
                {
                    // Do something here on press
                }
            };

            var input2 = new CRSAlertInput
            {
                Placeholder = "your name...",
                Text = string.Empty,
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Purple),
                OpenAutomatically = true
            };

            var input3 = new CRSAlertInput
            {
                Placeholder = "your email address...",
                Text = string.Empty,
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Purple),
                OpenAutomatically = true
            };

            var input4 = new CRSAlertInput
            {
                Placeholder = "your password...",
                Text = string.Empty,
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Purple),
                OpenAutomatically = true
            };

            var action2 = new CRSRegistrationAction
            {
                Text = "Sign Me Up!",
                Highlighted = true,
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Purple)
            };

            alert.Name = input2;
            alert.Email = input3;
            alert.Password = input4;
            alert.Actions = new CRSRegistrationAction[] { action, action2 };
            alert.RegistrationSuccessful += Alert_LoginSuccessful;
            alert.Show();
        }

        private void LoginButton_TouchDown(object sender, EventArgs e)
        {
            Curse.CRSLoginView alert = new CRSLoginView();
            alert.TintColor = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Purple);
            alert.Title = "Body Life Skills Login";
            alert.Message = "Please enter details";
            alert.Image = new UIImage("butterfly.png");
            CRSLoginView.InputTextColor = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Purple);

            var action = new CRSLoginAction
            {
                Text = "Cancel",
                Highlighted = false,
                TintColor = UIColor.Black,
                DidSelect = (alert2) =>
                {
                    // Do something here on press
                }
            };

            var input2 = new CRSAlertInput
            {
                Placeholder = "your email address...",
                Text = string.Empty,
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Purple),
                OpenAutomatically = true
            };

            var input3 = new CRSAlertInput
            {
                Placeholder = "your password...",
                Text = string.Empty,
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Purple),
                OpenAutomatically = true
            };

            var action2 = new CRSLoginAction
            {
                Text = "Sign Me In!",
                Highlighted = true,
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Purple)
            };

            alert.Input = input2;
            alert.Password = input3;
            alert.Actions = new CRSLoginAction[] { action, action2 };
            alert.LoginSuccessful += Alert_LoginSuccessful;
            alert.Show();
        }

        private void Alert_LoginSuccessful(object sender, EventArgs e)
        {
            if (Controllers.SecurityController.CurrentUser != null)
            {

                CRSAlertView alert = new CRSAlertView();
                alert.TintColor = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Purple);
                alert.Title = "Welcome " + Controllers.SecurityController.CurrentUser.Name + "!";
                alert.Message = "Would you like to set up a personal PIN to keep your information secure?";
                alert.Image = new UIImage("butterfly.png");

                var action = new CRSAlertAction
                {
                    Text = "No",
                    Highlighted = false,
                    TintColor = UIColor.Black,
                    DidSelect = (alert2) =>
                    {
                        navigateToHome();
                    }
                };

                var action2 = new CRSAlertAction
                {
                    Text = "Yes",
                    Highlighted = true,
                    TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Purple),
                    DidSelect = (alert2) =>
                   {
                       LockHandler @delegate = new LockHandler(((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.TopViewController);
                       @delegate.PinSetWasSuccessful += delegate_PinSetWasSuccessful;
                       @delegate.PinSetWasCancelled += delegate_PinSetWasCancelled;
                       var lockScreen = new LockScreenSetupController(@delegate);
                       UIImageView img = new UIImageView(UIImage.FromFile("Waves.png"));
                       img.ContentMode = UIViewContentMode.ScaleAspectFill;
                       lockScreen.LockScreenView.SetupBackgroundView(img);

                       //UnlockApplication();
                       lockScreen.Show(((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.TopViewController);
                       TableSource.ReloadData();
                   }
                };

                alert.Actions = new CRSAlertAction[] { action, action2 };
                alert.Show();
            }
            else
            {
                UIAlertController alert = UIAlertController.Create("Unable to Sign You In", "Whoops! Something went wrong and we could not sign you in!", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (UIAlertAction action) => { alert.DismissViewControllerAsync(true); }));
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.TopViewController.PresentModalViewController(alert, true);
            }
        }

        private void delegate_PinSetWasCancelled(object sender, EventArgs e)
        {
            navigateToHome();
        }

        private void delegate_PinSetWasSuccessful(object sender, EventArgs e)
        {
            navigateToHome();
        }

        private void ShowLoadingView()
        {
            // hide the existing icons
            login.Hidden = true;
            register.Hidden = true;
            imageAllAmazing.Hidden = true;

            // update and display the new icons
            about.Text = "Welcome " + iOS.Controllers.SecurityController.CurrentUser.Name + "!";
            subTitle.Text = "Just a sec whilst we get a few things set up for you...";
            indicatorView.Hidden = false;
            indicatorView.StartAnimating();
            this?.Animate(this, null);
        }

        private async void navigateToHome()
        {
            try
            {
                ShowLoadingView();
                await Task.Run(() => { bool b = Fabic.Core.Controllers.FabicDatabaseController.InitialiseDatabase().Result; });
                // minimise this and push to the root view controller
                UIStoryboard uis = UIStoryboard.FromName("Main", null);
                UIViewController vc = uis.InstantiateViewController("homeVC");
                //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetToolbarHidden(true, false);
                //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(false, false);
                //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BackgroundColor = UIColor.Clear;
                //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.BarTintColor = UIColor.Clear;
                //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.TintColor = UIColor.Clear;
                //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.ShadowImage = new UIImage();
                //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.Translucent = false;

                //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.Toolbar.TintColor = UIColor.White;
                //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.Toolbar.BarTintColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple);

                UIColor white = UIColor.White;

                UIStringAttributes myTextAttrib = new UIStringAttributes() { StrokeColor = white, ForegroundColor = white, Font = UIFont.FromName("Avenir-Heavy", 20) };
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.NavigationBar.TitleTextAttributes = myTextAttrib;
                ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetViewControllers(new UIViewController[] { vc }, false);
                // ((AppDelegate)UIApplication.SharedApplication.Delegate).Window.MakeKeyAndVisible();
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return tableView.Frame.Height;//(((UIApplication.SharedApplication.KeyWindow.Screen.Bounds.Height - 480) * 65));
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 0;
        }

        public override nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            return 0;
        }

        public void CleanUp()
        {

        }
    }
}
