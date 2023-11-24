using CoreAnimation;
using CoreGraphics;
using Fabic.Core.Helpers;
using Fabic.Data.Extensions;
using Fabic.iOS;
using Fabic.iOS.Controllers;
using Fabic.iOS.ViewControllers;
using Foundation;
using System;
using UIKit;

namespace Curse
{
    public class CRSRegistrationView : UIView
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
        UITextField _inputName;
        UITextField _inputEmail;
        UITextField _inputPassword;
        UIImageView _inputImage;
        UIButton _inputButton;
        UIButton _termsAndConditionsButton;

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
        public CRSAlertInput Name { get; set; }
        public CRSAlertInput Email { get; set; }
        public CRSAlertInput Password { get; set; }
        public CRSRegistrationAction[] Actions { get; set; }
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
        public event EventHandler RegistrationSuccessful;
        #endregion

        #region Constructors

        public static CRSRegistrationView Error(string title, string message, UIImage image = null, string buttonTitle = "Ok")
        {
            var action = new CRSRegistrationAction
            {
                Text = buttonTitle,
                Highlighted = true,
                TintColor = Tint
            };
            return new CRSRegistrationView
            {
                Title = title,
                Message = message,
                Image = image != null ? image : UIImage.FromBundle(string.Empty),
                Actions = new CRSRegistrationAction[] { action },
            };
        }
        #endregion

        #region UI
        void CreateAlertWindow()
        {
            if (CRSRegistrationView.AlertWindow == null)
            {
                CRSRegistrationView.AlertWindow = new UIWindow(UIScreen.MainScreen.Bounds)
                {
                    BackgroundColor = UIColor.Clear
                };
            }
        }

        private void MakeUI()
        {
            CreateAlertWindow();

            // Needs these parameters
            if (CRSRegistrationView.AlertWindow == null || Title == null || Actions == null || Actions.Length == 0)
            {
                throw new ModelNotImplementedException();
            }

            // Build Main View
            Alpha = 0f;
            BackgroundColor = UIColor.Black.ColorWithAlpha(0.75f);
            Frame = CRSRegistrationView.AlertWindow.Bounds;

            // Build Container
            nfloat imageWidth = 60f;
            nfloat buttonWidth = (CRSRegistrationView.AlertWindow.Frame.Width - 2 * pad) / Actions.Length;
            nfloat buttonHeight = 60f;

            _alertContainer = new UIView
            {
                Frame = new CGRect(pad, 0, CRSRegistrationView.AlertWindow.Frame.Width - 2 * pad, pad + 40),
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

            if (Name != null)
            {
                _inputImage = new UIImageView
                {
                    Frame = new CGRect(pad / 2, _message.Frame.Bottom + pad / 2, Name.Image != null ? 20 : 0, Name.Image != null ? 20 : 0),
                    Image = Name.Image ?? new UIImage(),
                    TintColor = Name.TintColor != null ? Name.TintColor : Tint
                };

                var startX = Name.Image != null ? pad / 2 + 30 : pad / 2;

                _inputName = new UITextField
                {
                    Frame = new CGRect(startX, _message.Frame.Bottom + pad / 2, _alertContainer.Frame.Width - _inputImage.Frame.Right - 20, 30),
                    BackgroundColor = UIColor.White,
                    Placeholder = Name.Placeholder != null ? Name.Placeholder : string.Empty,
                    Text = Name.Text != null ? Name.Text : string.Empty,
                    TextColor = InputTextColor,
                    Font = InputFont,
                    KeyboardType = UIKeyboardType.ASCIICapable,
                    SpellCheckingType = UITextSpellCheckingType.No,
                    BorderStyle = UITextBorderStyle.None,
                    Alpha = 0f,
                    AutocapitalizationType = UITextAutocapitalizationType.Words,
                    ReturnKeyType = UIReturnKeyType.Next,
                    KeyboardAppearance = UIKeyboardAppearance.Dark
                };
                _inputName.Layer.SublayerTransform = CATransform3D.MakeTranslation(5.0f, 0.0f, 0.0f);

                _inputLabel = new UILabel
                {
                    Frame = new CGRect(startX, _message.Frame.Bottom + pad / 2, _alertContainer.Frame.Width - _inputImage.Frame.Right + 20, 30),
                    TextColor = Name.TintColor != null ? Name.TintColor : Tint,
                    Text = Name.Placeholder != null ? Name.Placeholder : string.Empty,
                    Alpha = 0,
                    Font = InputFont
                };

                _inputEmail = new UITextField
                {
                    Frame = new CGRect(startX, _message.Frame.Bottom + pad / 2 + 40, _alertContainer.Frame.Width - _inputImage.Frame.Right - 20, 30),
                    BackgroundColor = UIColor.White,
                    Placeholder = Email.Placeholder != null ? Email.Placeholder : string.Empty,
                    Text = Email.Text != null ? Email.Text : string.Empty,
                    TextColor = InputTextColor,
                    Font = InputFont,
                    KeyboardType = UIKeyboardType.EmailAddress,
                    SpellCheckingType = UITextSpellCheckingType.No,
                    BorderStyle = UITextBorderStyle.None,
                    AutocapitalizationType = UITextAutocapitalizationType.None,
                    Alpha = 1f,
                    ReturnKeyType = UIReturnKeyType.Done,
                    KeyboardAppearance = UIKeyboardAppearance.Dark
                };

                _inputPassword = new UITextField
                {
                    Frame = new CGRect(startX, _message.Frame.Bottom + pad / 2 + 80, _alertContainer.Frame.Width - _inputImage.Frame.Right - 20, 30),
                    BackgroundColor = UIColor.White,
                    Placeholder = Password.Placeholder != null ? Password.Placeholder : string.Empty,
                    SecureTextEntry = true,
                    Text = Password.Text != null ? Password.Text : string.Empty,
                    TextColor = InputTextColor,
                    Font = InputFont,
                    KeyboardType = UIKeyboardType.Default,
                    SpellCheckingType = UITextSpellCheckingType.No,
                    BorderStyle = UITextBorderStyle.None,
                    Alpha = 1f,
                    ReturnKeyType = UIReturnKeyType.Done,
                    KeyboardAppearance = UIKeyboardAppearance.Dark
                };
                _inputPassword.Layer.SublayerTransform = CATransform3D.MakeTranslation(5.0f, 0.0f, 0.0f);

                _inputPassword.Delegate = new InputSource(_inputName, _inputEmail, _inputPassword, Actions[1], this);
                _inputName.Delegate = new InputSource(_inputName, _inputEmail, _inputPassword, Actions[1], this);
                _inputEmail.Delegate = new InputSource(_inputName, _inputEmail, _inputPassword, Actions[1], this);
                _inputEmail.Layer.SublayerTransform = CATransform3D.MakeTranslation(5.0f, 0.0f, 0.0f);

                _inputButton = new UIButton
                {
                    BackgroundColor = UIColor.Clear
                };
                _inputButton.TouchUpInside += (sender, e) =>
                {
                    ShowInputTextField();
                };

                _termsAndConditionsButton = new UIButton();
                _termsAndConditionsButton.SetTitleColor(UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple), UIControlState.Normal);
                _termsAndConditionsButton.SetTitle("By signing up you are agreeing to our ts and cs", UIControlState.Normal);
                _termsAndConditionsButton.Frame = new CGRect(startX, _message.Frame.Bottom + pad / 2 + 40 + 75, _alertContainer.Frame.Width - _inputImage.Frame.Right - 20, 30);
                _termsAndConditionsButton.Font = UIFont.BoldSystemFontOfSize(10);
                _termsAndConditionsButton.TouchDown += TsAndCsAction;

                _alertContainer.AddSubviews(new UIView[] { _inputImage, _inputLabel, _inputButton, _inputName, _inputEmail, _inputPassword, _termsAndConditionsButton });

                if (Name.OpenAutomatically)
                {
                    _inputButton.Hidden = true;
                    _inputName.Alpha = 1f;
                    _inputEmail.Alpha = 1f;
                    _inputImage.Center = new CGPoint(_inputImage.Center.X, _inputName.Center.Y);
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
                Frame = new CGRect(0, Name == null ? _message.Frame.Bottom + pad : _termsAndConditionsButton.Frame.Bottom, _alertContainer.Frame.Width, 1),
                BackgroundColor = SeparatorColor
            };
            _alertContainer.AddSubviews(new UIView[] { _image, _title, _message, _bottomSeparator });

            for (int i = 0; i < Actions.Length; i++)
            {
                CRSRegistrationAction action = Actions[i];
                var btn = new UIButton
                {
                    Frame = new CGRect(buttonWidth * i, _bottomSeparator.Frame.Bottom, buttonWidth, buttonHeight),
                    BackgroundColor = ButtonBackground,
                    Font = action.Highlighted ? AlertButtonHighlightedFont : AlertButtonNormalFont
                };
                btn.ContentMode = UIViewContentMode.ScaleAspectFit;
                btn.SetTitle(string.IsNullOrEmpty(action.Text) ? string.Empty : action.Text, UIControlState.Normal);
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
                action.DidSelect += (index) =>
                {
                    DidSelectAction((int)btn.Tag);
                };
                btn.SetTitleColor(action.Highlighted ? (action.TintColor ?? Tint) : TitleTextColor, UIControlState.Normal);
                _alertContainer.Add(btn);
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
            nfloat alertEnd = _alertContainer.Subviews[_alertContainer.Subviews.Length - 1].Frame.Bottom;
            nfloat d = (UIScreen.MainScreen.Bounds.Height > 650 ? 220 : 165);
            _alertContainer.Frame = new CGRect(_alertContainer.Frame.Left, 0, _alertContainer.Frame.Width, alertEnd);
            _alertContainer.Center = new CGPoint(CRSRegistrationView.AlertWindow.Frame.Width / 2, d);

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

            CRSRegistrationView.PreviousKeyWindow = UIApplication.SharedApplication.KeyWindow;
            CRSRegistrationView.PreviousKeyWindow.EndEditing(true);

            CRSRegistrationView.AlertWindow.Alpha = 0f;
            Alpha = 0;
            _alertContainer.Alpha = 0f;
            CRSRegistrationView.AlertWindow.RootViewController = new LoginPopupViewController(this);
            CRSRegistrationView.AlertWindow.MakeKeyAndVisible();
            UIView.Animate(duration / 2, () =>
            {
                CRSRegistrationView.AlertWindow.Alpha = 1f;
                Alpha = 1;
            }, () =>
            {
                if (UIApplication.SharedApplication.KeyWindow != CRSRegistrationView.AlertWindow)
                {
                    CRSRegistrationView.AlertWindow.MakeKeyAndVisible();
                }
                UIView.Animate(duration / 2, () =>
                {
                    _alertContainer.Alpha = 1f;
                }, () =>
                {
                    if (Name != null && Name.OpenAutomatically)
                    {
                        _inputName.BecomeFirstResponder();
                    }
                });
            });
        }

        public void Hide(Action<CRSRegistrationView> didHide = null, float duration = AnimationDuration, UIWindow window = null)
        {
            --AlertsDisplayed;

            if (CRSRegistrationView.AlertWindow == null)
            {
                return;
            }

            if (_inputName != null && _inputName.IsFirstResponder)
            {
                _alertContainer.EndEditing(true);
            }
            UIView.Animate(AnimationDuration, () =>
            {
                Alpha = 0f;
                CRSRegistrationView.AlertWindow.Alpha = 0f;
            }, () =>
            {
                CRSRegistrationView.AlertWindow.RootViewController = new UIViewController();
                if (CRSRegistrationView.PreviousKeyWindow == null || CRSRegistrationView.PreviousKeyWindow.Hidden)
                {
                    window = window ?? UIApplication.SharedApplication.KeyWindow;
                    window.MakeKeyAndVisible();
                }
                else
                {
                    CRSRegistrationView.PreviousKeyWindow.MakeKeyAndVisible();
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
                _inputImage.Center = new CGPoint(_inputImage.Center.X, _inputName.Center.Y);
            }, () =>
            {
                _inputName.Alpha = 1f;
                _inputName.BecomeFirstResponder();
            });
        }
        #endregion


        #region Did Select
        public async void TsAndCsAction(object sender, EventArgs eventArgs)
        {
            TermsAndConditionsViewController ParallaxViewController = (TermsAndConditionsViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("legalVC");
            ParallaxViewController.Closed += ParallaxViewController_Closed;
            UINavigationController nav = new UINavigationController(ParallaxViewController);
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PresentViewController(nav, true, () => { });
            Hide();
        }

        private void ParallaxViewController_Closed(object sender, EventArgs e)
        {
            Show();
        }

        public async void DidSelectAction(int i)
        {
            if (Name != null)
            {
                Name.Text = _inputName.Text;
            }

            // if the sign in button is selected we must validate the entry
            bool close = true;
            if (Actions[i].Text.ToLower() != "cancel")
            {
                // validation...
                if (string.IsNullOrWhiteSpace(_inputName.Text.Trim()))
                {
                    // email is not entered
                    UIAlertView alert = new UIAlertView()
                    {
                        Title = "Missing your Name",
                        Message = "looks like you have not entered your amazing name... we need that to know who you are."
                    };
                    alert.TintColor = UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                    alert.AddButton("OK");
                    alert.Show();

                    close = false;
                    _inputName.BecomeFirstResponder();
                }
                else if (string.IsNullOrWhiteSpace(_inputEmail.Text.Trim()))
                {
                    // email is not entered
                    UIAlertView alert = new UIAlertView()
                    {
                        Title = "Missing your Email Address",
                        Message = "looks like you have not entered your amazing email address... we need that to know who you are."
                    };
                    alert.TintColor = UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                    alert.AddButton("OK");
                    alert.Show();

                    close = false;
                    _inputEmail.BecomeFirstResponder();
                }
                else if (string.IsNullOrWhiteSpace(_inputPassword.Text.Trim()))
                {
                    // password is not entered
                    UIAlertView alert = new UIAlertView()
                    {
                        Title = "Missing your Password",
                        Message = "looks like you have not entered your amazing password... we need that to know who you are."
                    };
                    alert.TintColor = UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                    alert.AddButton("OK");
                    alert.Show();

                    close = false;
                    _inputPassword.BecomeFirstResponder();
                }
                //else if (string.IsNullOrWhiteSpace(_inputPassword.Text.Trim()))
                //{
                //    // email is not entered
                //    UIAlertView alert = new UIAlertView()
                //    {
                //        Title = "Confirm your Password",
                //        Message = "just to double triple check your amazing password is correct... we need you to enter it again."
                //    };
                //    alert.TintColor = UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                //    alert.AddButton("OK");
                //    alert.Show();

                //    close = false;
                //    _inputPassword.BecomeFirstResponder();
                //}
                //else if (_inputPassword.Text.Trim() != _inputEmail.Text.Trim())
                //{
                //    // email is not entered
                //    UIAlertView alert = new UIAlertView()
                //    {
                //        Title = "Your Passwords are not the same!",
                //        Message = "you have made a mistake entering your amazing password as they are not the same... can you check this out and try entering it again."
                //    };
                //    alert.TintColor = UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                //    alert.AddButton("OK");
                //    alert.Show();

                //    close = false;
                //    _inputEmail.BecomeFirstResponder();
                //}
                //else if (_inputPassword.Text.Trim() != _inputEmail.Text.Trim()) // check for the complexity of the password
                //{
                //    // email is not entered
                //    UIAlertView alert = new UIAlertView()
                //    {
                //        Title = "Your Passwords are not the same!",
                //        Message = "you have made a mistake entering your amazing password as they are not the same... can you check this out and try entering it again."
                //    };
                //    alert.TintColor = UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                //    alert.AddButton("OK");
                //    alert.Show();

                //    close = false;
                //    _inputEmail.BecomeFirstResponder();
                //}
                else if (Fabic.iOS.External.Reachability.InternetConnectionStatus() == Fabic.iOS.External.NetworkStatus.NotReachable) // verify the phone is online
                {
                    // offline
                    UIAlertView alert = new UIAlertView()
                    {
                        Title = "You are offline!",
                        Message = "To register you must be connected to the Internet."
                    };
                    alert.TintColor = UIColor.Black.FabicColour(Fabic.Data.Enums.FabicColour.Purple);
                    alert.AddButton("OK");
                    alert.Show();

                    close = false;
                }
                else
                {
                    BigTed.BTProgressHUD.Show("Signing you up for Body Life Skills", -1, BigTed.ProgressHUD.MaskType.None);

                    string result = await SecurityController.RegisterAsync(_inputName.Text, _inputEmail.Text, _inputPassword.Text);
                    if (SecurityController.AccessToken.Length <= 0)
                    {
                        BigTed.BTProgressHUD.Dismiss();
                        close = false;
                        if (result.Contains("user already exists"))
                            result = "Looks like a user already exists with this email address. If you have forgotten your password, click on the 'Sign In' button and then click 'Forgot my Amazing Password" +
                                "' and follow the prompts to reset your password.";
                        UIAlertController alert = UIAlertController.Create("Unable to Sign You Up", result, UIAlertControllerStyle.Alert);
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
                    RegistrationSuccessful?.Invoke(this, new EventArgs());
            }
        }
        #endregion


        #region TextFieldSource
        class InputSource : UITextFieldDelegate
        {
            private UITextField _email;
            private UITextField _password;
            private UITextField _confirmPassword;
            private CRSRegistrationAction _signInBtn;
            private CRSRegistrationView _loginView;
            public InputSource(UITextField email, UITextField password, UITextField confirmPassword, CRSRegistrationAction signInBtn, CRSRegistrationView loginView) { _email = email; _password = password; _confirmPassword = confirmPassword; _signInBtn = signInBtn; _loginView = loginView; }

            public override bool ShouldReturn(UITextField textField)
            {
                textField.ResignFirstResponder();
                if (textField == _email)
                    _password.BecomeFirstResponder();
                else if (textField == _password)
                    _confirmPassword.BecomeFirstResponder();
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
                _alertContainer.Frame = new CGRect(_alertContainer.Frame.Left, CRSRegistrationView.AlertWindow.Frame.Height - height - pad - _alertContainer.Frame.Height, _alertContainer.Frame.Width, _alertContainer.Frame.Height);
            }
            else
            {
                _alertContainer.Center = new CGPoint(CRSRegistrationView.AlertWindow.Frame.Width / 2, CRSRegistrationView.AlertWindow.Frame.Height / 2);
            }
        }
        #endregion


        #region View Controller
        class LoginPopupViewController : UIViewController
        {
            readonly CRSRegistrationView _alert;

            public LoginPopupViewController(CRSRegistrationView alert)
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