using CoreAnimation;
using CoreGraphics;
using Fabic.Core.Helpers;
using Fabic.Data.Extensions;
using Fabic.iOS.Controllers;
using Foundation;
using System;
using UIKit;

namespace Curse
{
    public class CRSLoginView : UIView
    {
        #region Properties
        const float AnimationDuration = 0.15f;
        const float pad = 20.0f;
        bool _displayOverAlert = true;

        // UI
        UIView _alertContainer;
        UIView _bottomSeparator;
        UIImageView _image;
        UILabel _title;
        UILabel _message;

        // Colors
        public static UIColor Tint = UIColor.FromRGB(3, 127, 241);
        public static UIColor Background = UIColor.FromRGB(0xf3, 0xf3, 0xf3);
        public static UIColor TitleTextColor = UIColor.Black;
        public static UIColor MessageTextColor = UIColor.Black;
        public static UIColor InputTextColor = UIColor.Black;
        public static UIColor ButtonBackground = UIColor.FromRGB(228, 228, 228);
        public static UIColor ButtonHighlighted = UIColor.FromRGB(210, 210, 210);
        public static UIColor SeparatorColor = UIColor.FromRGB(212, 212, 212);

        // Fonts
        public static UIFont TitleFont = UIFont.BoldSystemFontOfSize(18f);
        public static UIFont MessageFont = UIFont.SystemFontOfSize(14f);
        public static UIFont InputFont = UIFont.SystemFontOfSize(14f);
        public static UIFont AlertButtonHighlightedFont = UIFont.BoldSystemFontOfSize(16f);
        public static UIFont AlertButtonNormalFont = UIFont.SystemFontOfSize(16f);

        // Input
        UILabel _inputLabel;
        UITextField _inputTextField;
        UITextField _inputPassword;
        UIImageView _inputImage;
        UIButton _inputButton;
        UIButton _forgotPasswordButton;

        string _t;
        public string Title
        {
            get { return _t; }
            set
            {
                _t = value;
                if (_title != null)
                {
                    _title.Text = value;
                }
            }
        }

        string _m;
        public string Message
        {
            get { return _m; }
            set
            {
                _m = value;
                if (_message != null)
                {
                    _message.Text = value;
                }
            }
        }

        public UIImage Image
        {
            get;
            set;
        }
        public CRSAlertInput Input { get; set; }
        public CRSAlertInput Password { get; set; }
        public CRSLoginAction[] Actions { get; set; }
        public event EventHandler LoginSuccessful;
        public bool IsShowing
        {
            get
            {
                return Superview != null;
            }
        }

        public static UIWindow AlertWindow;
        static UIWindow PreviousKeyWindow;
        static int AlertsDisplayed;
        #endregion

        #region Constructors

        public static CRSLoginView Error(string title, string message, UIImage image = null, string buttonTitle = "Ok")
        {
            var action = new CRSLoginAction
            {
                Text = buttonTitle,
                Highlighted = true,
                TintColor = Tint
            };
            return new CRSLoginView
            {
                Title = title,
                Message = message,
                Image = image != null ? image : UIImage.FromBundle(""),
                Actions = new CRSLoginAction[] { action },
            };
        }
        #endregion

        #region UI
        void CreateAlertWindow()
        {
            if (CRSLoginView.AlertWindow == null)
            {
                if (UIScreen.MainScreen.Bounds.Width > 600)
                {
                    CRSLoginView.AlertWindow = new UIWindow(new CGRect(0, 0, 400, UIScreen.MainScreen.Bounds.Height))
                    {
                        BackgroundColor = UIColor.Clear
                    };
                }
                else
                {
                    CRSLoginView.AlertWindow = new UIWindow(UIScreen.MainScreen.Bounds)
                    {
                        BackgroundColor = UIColor.Clear
                    };
                }
            }
        }

        private void MakeUI()
        {
            CreateAlertWindow();

            // Needs these parameters
            if (CRSLoginView.AlertWindow == null || Title == null || Actions == null || Actions.Length == 0)
            {
                throw new ModelNotImplementedException();
            }

            // Build Main View
            Alpha = 0f;
            BackgroundColor = UIColor.Black.ColorWithAlpha(0.75f);
            Frame = CRSLoginView.AlertWindow.Bounds;

            // Build Container
            nfloat imageWidth = 60f;
            nfloat buttonWidth = (CRSLoginView.AlertWindow.Frame.Width - 2 * pad) / Actions.Length;
            nfloat buttonHeight = 60f;

            _alertContainer = new UIView
            {
                Frame = new CGRect(pad, 0, CRSLoginView.AlertWindow.Frame.Width - 2 * pad, pad + 40),
                BackgroundColor = Background,
                Alpha = 0f
            };
            _alertContainer.Layer.CornerRadius = 12.0f;
            _alertContainer.Layer.MasksToBounds = true;

            _image = new UIImageView
            {
                Frame = new CGRect(_alertContainer.Frame.Width / 2 - imageWidth / 2, pad, Image != null ? imageWidth : 0, Image != null ? imageWidth : 0),
                BackgroundColor = UIColor.Clear,
                TintColor = TitleTextColor,
                Image = Image ?? new UIImage(),
                ContentMode = UIViewContentMode.ScaleAspectFit
            };

            _title = new UILabel
            {
                Frame = new CGRect(pad / 2, _image.Frame.Width > 0 ? _image.Frame.Bottom + 5 : pad, _alertContainer.Frame.Width - pad, 24.0f),
                Text = Title,
                TextColor = TitleTextColor,
                Font = TitleFont,
                Lines = 1,
                AdjustsFontSizeToFitWidth = true,
                MinimumScaleFactor = 0.5f,
                TextAlignment = UITextAlignment.Center
            };

            _message = new UILabel
            {
                Frame = new CGRect(pad / 2, _title.Frame.Bottom + 2, _alertContainer.Frame.Width - pad, 20.0f),
                Text = Message,
                TextColor = MessageTextColor,
                Font = MessageFont,
                Lines = 0,
                TextAlignment = UITextAlignment.Center
            };
            _message.SizeToFit();
            _message.Center = new CGPoint(_title.Center.X, _message.Center.Y);

            if (Input != null)
            {
                _inputImage = new UIImageView
                {
                    Frame = new CGRect(pad / 2, _message.Frame.Bottom + pad / 2, Input.Image != null ? 20 : 0, Input.Image != null ? 20 : 0),
                    Image = Input.Image ?? new UIImage(),
                    TintColor = Input.TintColor != null ? Input.TintColor : Tint
                };

                var startX = Input.Image != null ? pad / 2 + 30 : pad / 2;
                _inputTextField = new UITextField
                {
                    Frame = new CGRect(startX, _message.Frame.Bottom + pad / 2, _alertContainer.Frame.Width - _inputImage.Frame.Right - 20, 30),
                    BackgroundColor = UIColor.White,
                    Placeholder = Input.Placeholder != null ? Input.Placeholder : "",
                    Text = Input.Text != null ? Input.Text : "",
                    TextColor = InputTextColor,
                    Font = InputFont,
                    KeyboardType = UIKeyboardType.EmailAddress,
                    SpellCheckingType = UITextSpellCheckingType.No,
                    BorderStyle = UITextBorderStyle.None,
                    Alpha = 0f,
                    ReturnKeyType = UIReturnKeyType.Next,
                    AutocapitalizationType = UITextAutocapitalizationType.None,
                    KeyboardAppearance = UIKeyboardAppearance.Dark
                };
                _inputTextField.Layer.SublayerTransform = CATransform3D.MakeTranslation(5.0f, 0.0f, 0.0f);

                _inputLabel = new UILabel
                {
                    Frame = new CGRect(startX, _message.Frame.Bottom + pad / 2, _alertContainer.Frame.Width - _inputImage.Frame.Right + 20, 30),
                    TextColor = Input.TintColor != null ? Input.TintColor : Tint,
                    Text = Input.Placeholder != null ? Input.Placeholder : "",
                    Alpha = 0,
                    Font = InputFont
                };

                _inputPassword = new UITextField
                {
                    Frame = new CGRect(startX, _message.Frame.Bottom + pad / 2 + 40, _alertContainer.Frame.Width - _inputImage.Frame.Right - 20, 30),
                    BackgroundColor = UIColor.White,
                    Placeholder = Password.Placeholder != null ? Password.Placeholder : "",
                    SecureTextEntry = true,
                    Text = Password.Text != null ? Password.Text : "",
                    TextColor = InputTextColor,
                    Font = InputFont,
                    KeyboardType = UIKeyboardType.Default,
                    SpellCheckingType = UITextSpellCheckingType.No,
                    BorderStyle = UITextBorderStyle.None,
                    Alpha = 0f,
                    ReturnKeyType = UIReturnKeyType.Done,
                    KeyboardAppearance = UIKeyboardAppearance.Dark
                };

                _forgotPasswordButton = new UIButton();
                _forgotPasswordButton.SetTitleColor(UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple), UIControlState.Normal);
                _forgotPasswordButton.SetTitle("Forgot your password?", UIControlState.Normal);
                _forgotPasswordButton.Frame = new CGRect(startX, _message.Frame.Bottom + pad / 2 + 75, _alertContainer.Frame.Width - _inputImage.Frame.Right - 20, 30);
                _forgotPasswordButton.Font = UIFont.BoldSystemFontOfSize(11);
                _forgotPasswordButton.TouchDown += ForgotPasswordAction;

                _inputButton = new UIButton
                {
                    BackgroundColor = UIColor.Clear
                };
                _inputButton.TouchUpInside += (sender, e) =>
                {
                    ShowInputTextField();
                };

                _inputPassword.Layer.SublayerTransform = CATransform3D.MakeTranslation(5.0f, 0.0f, 0.0f);

                _alertContainer.AddSubviews(new UIView[] { _inputImage, _inputLabel, _inputButton, _inputTextField, _inputPassword, _forgotPasswordButton });

                if (Input.OpenAutomatically)
                {
                    _inputButton.Hidden = true;
                    _inputTextField.Alpha = 1f;
                    _inputPassword.Alpha = 1f;
                    _inputImage.Center = new CGPoint(_inputImage.Center.X, _inputTextField.Center.Y);
                }
                else
                {
                    _inputLabel.SizeToFit();
                    _inputLabel.Alpha = 1f;
                    nfloat width = _inputImage.Frame.Width + 10 + _inputLabel.Frame.Width;
                    _inputImage.Frame = new CGRect(_alertContainer.Frame.Width / 2 - width / 2, _inputImage.Frame.Top, _inputImage.Frame.Width, _inputImage.Frame.Height);
                    _inputLabel.Frame = new CGRect(_inputImage.Frame.Right + 10, _inputLabel.Frame.Top, _inputLabel.Frame.Width, _inputLabel.Frame.Height);
                    _inputImage.Center = new CGPoint(_inputImage.Center.X, _inputLabel.Center.Y);
                    _inputButton.Frame = new CGRect(_inputImage.Frame.Left, _inputLabel.Frame.Top, width, 44);
                    _inputButton.Center = new CGPoint(_inputButton.Center.X, _inputLabel.Center.Y);
                }
            }

            _bottomSeparator = new UIView
            {
                Frame = new CGRect(0, Input == null ? _message.Frame.Bottom + pad : _forgotPasswordButton.Frame.Bottom, _alertContainer.Frame.Width, 1),
                BackgroundColor = SeparatorColor
            };
            _alertContainer.AddSubviews(new UIView[] { _image, _title, _message, _bottomSeparator });

            for (int i = 0; i < Actions.Length; i++)
            {
                CRSLoginAction action = Actions[i];
                var btn = new UIButton
                {
                    Frame = new CGRect(buttonWidth * i, _bottomSeparator.Frame.Bottom, buttonWidth, buttonHeight),
                    BackgroundColor = ButtonBackground,
                    Font = action.Highlighted ? AlertButtonHighlightedFont : AlertButtonNormalFont
                };
                btn.ContentMode = UIViewContentMode.ScaleAspectFit;
                btn.SetTitle(string.IsNullOrEmpty(action.Text) ? "" : action.Text, UIControlState.Normal);
                btn.Tag = i;
                btn.TouchDown += (sender, e) =>
                {
                    btn.BackgroundColor = ButtonHighlighted;
                };
                btn.TouchUpOutside += (sender, e) =>
                {
                    btn.BackgroundColor = ButtonBackground;
                };
                btn.TouchUpInside += (sender, e) =>
                {
                    DidSelectAction((int)btn.Tag);
                };
                btn.SetTitleColor(action.Highlighted ? (action.TintColor ?? Tint) : TitleTextColor, UIControlState.Normal);
                _alertContainer.Add(btn);
                action.DidSelect += (index) =>
                {
                    DidSelectAction((int)btn.Tag);
                };
                if (i < Actions.Length - 1)
                {
                    var s = new UIView
                    {
                        Frame = new CGRect(btn.Frame.Right - 1, btn.Frame.Top, 1, buttonHeight),
                        BackgroundColor = SeparatorColor
                    };
                    _alertContainer.Add(s);
                }
            }

            _inputTextField.Delegate = new InputSource(_inputTextField, _inputPassword, Actions[1], this);
            _inputPassword.Delegate = new InputSource(_inputTextField, _inputPassword, Actions[1], this);

            nfloat alertEnd = _alertContainer.Subviews[_alertContainer.Subviews.Length - 1].Frame.Bottom;
            nfloat d = (UIScreen.MainScreen.Bounds.Height > 650 ? 220 : 165);
            _alertContainer.Frame = new CGRect(_alertContainer.Frame.Left, _alertContainer.Frame.Top - 50, _alertContainer.Frame.Width, alertEnd);
            _alertContainer.Center = new CGPoint(CRSLoginView.AlertWindow.Frame.Width / 2, d);
            Add(_alertContainer);
            this.ApplyLightInterface();
        }
        #endregion

        #region Showing/Hiding
        public void Show(float duration = AnimationDuration * 2)
        {
            if (!_displayOverAlert && AlertsDisplayed > 0)
            {
                return;
            }

            if (_alertContainer == null)
            {
                MakeUI();
            }

            CRSLoginView.PreviousKeyWindow = UIApplication.SharedApplication.KeyWindow;
            CRSLoginView.PreviousKeyWindow.EndEditing(true);

            CRSLoginView.AlertWindow.Alpha = 0f;
            Alpha = 0;
            _alertContainer.Alpha = 0f;
            CRSLoginView.AlertWindow.RootViewController = new LoginPopupViewController(this);
            CRSLoginView.AlertWindow.MakeKeyAndVisible();
            UIView.Animate(duration / 2, () =>
            {
                CRSLoginView.AlertWindow.Alpha = 1f;
                Alpha = 1;
            }, () =>
            {
                if (UIApplication.SharedApplication.KeyWindow != CRSLoginView.AlertWindow)
                {
                    CRSLoginView.AlertWindow.MakeKeyAndVisible();
                }
                UIView.Animate(duration / 2, () =>
                {
                    _alertContainer.Alpha = 1f;
                }, () =>
                {
                    if (Input != null && Input.OpenAutomatically)
                    {
                        _inputTextField.BecomeFirstResponder();
                    }
                });
            });
        }

        public void Hide(Action<CRSLoginView> didHide = null, float duration = AnimationDuration, UIWindow window = null)
        {
            --AlertsDisplayed;

            if (CRSLoginView.AlertWindow == null)
            {
                return;
            }

            if (_inputTextField != null && _inputTextField.IsFirstResponder)
            {
                _alertContainer.EndEditing(true);
            }
            UIView.Animate(AnimationDuration, () =>
            {
                Alpha = 0f;
                CRSLoginView.AlertWindow.Alpha = 0f;
            }, () =>
            {
                CRSLoginView.AlertWindow.RootViewController = new UIViewController();
                if (CRSLoginView.PreviousKeyWindow == null || CRSLoginView.PreviousKeyWindow.Hidden)
                {
                    window = window ?? UIApplication.SharedApplication.KeyWindow;
                    window.MakeKeyAndVisible();
                }
                else
                {
                    CRSLoginView.PreviousKeyWindow.MakeKeyAndVisible();
                }

                if (didHide != null) didHide(this);
            });
        }

        private void ShowInputTextField()
        {
            _inputButton.Hidden = true;
            UIView.Animate(AnimationDuration, () =>
            {
                _inputLabel.Alpha = 0f;
                _inputImage.Frame = new CGRect(pad / 2, _inputImage.Frame.Top, _inputImage.Frame.Width, _inputImage.Frame.Height);
                _inputImage.Center = new CGPoint(_inputImage.Center.X, _inputTextField.Center.Y);
            }, () =>
            {
                _inputTextField.Alpha = 1f;
                _inputTextField.BecomeFirstResponder();
            });
        }
        #endregion

        #region Did Select
        public async void ForgotPasswordAction(object sender, EventArgs eventArgs)
        {
            UIAlertController alert = UIAlertController.Create("Forgot Your Password", "What is your email address?", UIAlertControllerStyle.Alert);
            alert.AddTextField((UITextField f) =>
            {
                f.Placeholder = "Your email to send the password reset instructions to";
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
            AlertWindow.RootViewController.PresentModalViewController(alert, true);
        }

        public async void DidSelectAction(int i)
        {
            if (Input != null)
            {
                Input.Text = _inputTextField.Text;
            }

            // if the sign in button is selected we must validate the entry
            bool close = true;
            if (Actions[i].Text.ToLower() != "cancel")
            {
                // validation...
                if (string.IsNullOrWhiteSpace(_inputTextField.Text.Trim()))
                {
                    // email is not entered
                    UIAlertView alert = new UIAlertView()
                    {
                        Title = "Missing your Email Address",
                        Message = "looks like you have not entered your email address... we need that to know who you are."
                    };
                    alert.TintColor = UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                    alert.AddButton("OK");
                    alert.Show();

                    close = false;
                    _inputTextField.BecomeFirstResponder();
                }
                else if (string.IsNullOrWhiteSpace(_inputPassword.Text.Trim()))
                {
                    // email is not entered
                    UIAlertView alert = new UIAlertView()
                    {
                        Title = "Missing your Password",
                        Message = "looks like you have not entered your password... we need that to know who you are."
                    };
                    alert.TintColor = UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                    alert.AddButton("OK");
                    alert.Show();

                    close = false;
                    _inputPassword.BecomeFirstResponder();
                }
                else if (Fabic.iOS.External.Reachability.RemoteHostStatus() == Fabic.iOS.External.NetworkStatus.NotReachable) // verify the phone is online
                {
                    // offline
                    UIAlertView alert = new UIAlertView()
                    {
                        Title = "You are offline!",
                        Message = "To login you must be connected to the Internet."
                    };
                    alert.TintColor = UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                    alert.AddButton("OK");
                    alert.Show();

                    close = false;
                }
                else
                {
                    BigTed.BTProgressHUD.Show("Signing you in", -1, BigTed.ProgressHUD.MaskType.None);

                    string result = await SecurityController.LoginAsync(_inputTextField.Text, _inputPassword.Text);
                    if (SecurityController.AccessToken.Length <= 0)
                    {
                        BigTed.BTProgressHUD.Dismiss();
                        close = false;
                        UIAlertController alert = UIAlertController.Create("Unable to Sign You In", result, UIAlertControllerStyle.Alert);
                        alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (UIAlertAction action) => { alert.DismissViewControllerAsync(true); }));
                        AlertWindow.RootViewController.PresentModalViewController(alert, true);
                    }
                    else
                        close = true;
                }
            }

            if (close)
            {
                BigTed.BTProgressHUD.Dismiss();
                Hide();
                if (Actions[i].Text.ToLower() != "cancel")
                    LoginSuccessful?.Invoke(this, new EventArgs());
            }
        }
        #endregion

        #region TextFieldSource
        class InputSource : UITextFieldDelegate
        {
            private UITextField _email;
            private UITextField _password;
            private CRSLoginAction _signInBtn;
            private CRSLoginView _loginView;
            public InputSource(UITextField email, UITextField password, CRSLoginAction signInBtn, CRSLoginView loginView) { _email = email; _password = password; _signInBtn = signInBtn; _loginView = loginView; }

            public override bool ShouldReturn(UITextField textField)
            {
                textField.ResignFirstResponder();
                if (textField == _email)
                    _password.BecomeFirstResponder();
                else
                    _signInBtn.DidSelect(null);
                return true;
            }
        }
        #endregion

        #region Keyboard Notifications
        private void OnKeyboardNotification(NSNotification notification)
        {
            //Check if the keyboard is becoming visible
            bool visible = notification.Name == UIKeyboard.WillShowNotification;

            //Start an animation, using values from the keyboard
            UIView.BeginAnimations("AnimateForKeyboard");
            UIView.SetAnimationBeginsFromCurrentState(true);
            UIView.SetAnimationDuration(UIKeyboard.AnimationDurationFromNotification(notification));
            UIView.SetAnimationCurve((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification(notification));

            //Pass the notification, calculating keyboard height, etc.
            if (visible)
            {
                var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
                OnKeyboardChanged(visible, keyboardFrame.Height);
            }
            else
            {
                var keyboardFrame = UIKeyboard.FrameBeginFromNotification(notification);
                OnKeyboardChanged(visible, keyboardFrame.Height);
            }

            //Commit the animation
            UIView.CommitAnimations();
        }

        protected void OnKeyboardChanged(bool visible, nfloat height)
        {
            CreateAlertWindow();

            if (_alertContainer == null) return;

            if (visible)
            {
                _alertContainer.Frame = new CGRect(_alertContainer.Frame.Left, CRSLoginView.AlertWindow.Frame.Height - height - pad - _alertContainer.Frame.Height, _alertContainer.Frame.Width, _alertContainer.Frame.Height);
            }
            else
            {
                _alertContainer.Center = new CGPoint(CRSLoginView.AlertWindow.Frame.Width / 2, CRSLoginView.AlertWindow.Frame.Height / 2);
            }
        }
        #endregion

        #region View Controller
        class LoginPopupViewController : UIViewController
        {
            readonly CRSLoginView _alert;

            public LoginPopupViewController(CRSLoginView alert)
            {
                _alert = alert;
            }

            public override void ViewDidLoad()
            {
                base.ViewDidLoad();
                View.BackgroundColor = UIColor.Clear;
                View.AddSubview(_alert);
            }
        }
        #endregion
    }
}