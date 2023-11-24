using Fabic.Core.Helpers;
using Fabic.Data.Extensions;
using Fabic.iOS.Controllers;
using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;
using Xamarin.LockScreen;

namespace Fabic.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        //private PushHandler pushHandler;

        public UIApplicationShortcutItem LaunchedShortcutItem { get; set; }
        // Azure app-specific connection string and hub path
        public const string ConnectionString = "Endpoint=sb://fabicapp.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=GgGHbt9VbfLuC7hYbheY/TSMM7eXZLEktWeXk4i1flc=";
        public const string NotificationHubPath = "Fabic";


        public override UIWindow Window
        {
            get;
            set;
        }

        public UINavigationController RootNavController
        {
            get;
            set;
        }

        public bool HandleShortcutItem(UIApplicationShortcutItem shortcutItem)
        {
            var handled = false;

            // Anything to process?
            if (shortcutItem == null) return false;
            try
            {
                UIStoryboard uis = UIStoryboard.FromName("Main", null);
                UIViewController vc;
                // Take action based on the shortcut type
                switch (shortcutItem.Type)
                {
                    case "com.fabicbehaviourcenter.Fabic.iOS.000":
                        Console.WriteLine("Behaviour Scales shortcut selected");
                        vc = uis.InstantiateViewController("behaviourScaleLibraryTableVC");
                        RootNavController.PushViewController(vc, true);
                        handled = true;
                        break;
                    case "com.fabicbehaviourcenter.Fabic.iOS.001":
                        Console.WriteLine("I Choose Charts shortcut selected");
                        vc = uis.InstantiateViewController("iChooseChartLibraryTableVC");
                        RootNavController.PushViewController(vc, true);
                        handled = true;
                        break;
                }
            }

            catch (Exception ex)
            {
                ex.HandleBLSException();
            }
            // Return results
            return handled;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            try
            {
                // Register for notifications
                //if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                //{
                //    var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                //           UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                //           new NSSet());

                //    UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                //    UIApplication.SharedApplication.RegisterForRemoteNotifications();
                //}
                //else
                //{
                //    UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                //    UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
                //}

                //UAirship.TakeOff();
                //string channelId = UAirship.Push().ChannelID;
                //UAirship.Push().UserPushNotificationsEnabled = true;

                //// Initialise Exception handler
                //ExceptionExtensions.InitialiseExceptionHandler();
                //UAirship.Push().ResetBadge();
                //pushHandler = new PushHandler();
                //UAirship.Push().PushNotificationDelegate = pushHandler;
                NSString messageListUpdated = new NSString("com.urbanairship.notification.message_list_updated");

                NSNotificationCenter.DefaultCenter.AddObserver(messageListUpdated, (notification) =>
                {
                    //refreshMessageCenterBadge();
                });

                // Initialise and load the main views
                Window = new UIWindow(UIScreen.MainScreen.Bounds);

                UIStoryboard uis = UIStoryboard.FromName("Main", null);
                UIViewController vc = uis.InstantiateViewController("homeVC");

                try
                {
                    if (SecurityController.CurrentUser != null)
                    {
                        // Initialise the Fabic Database
                        bool result = Fabic.Core.Controllers.FabicDatabaseController.InitialiseDatabase().Result;
                        ((MainLockScreenController)vc).IsLocked = false;
                        RootNavController = new UINavigationController(vc);
                    }
                    else
                    {
                        vc = uis.InstantiateViewController("loginVC");
                        RootNavController = new UINavigationController(vc);
                    }
                }
                catch (Exception ex)
                {
                    ex.HandleBLSException();
                    vc = uis.InstantiateViewController("loginVC");
                    RootNavController = new UINavigationController(vc);
                }

                RootNavController.SetToolbarHidden(true, false);
                RootNavController.SetNavigationBarHidden(false, false);
                RootNavController.NavigationBar.BackgroundColor = UIColor.Clear;
                RootNavController.NavigationBar.BarTintColor = UIColor.Clear;
                RootNavController.NavigationBar.TintColor = UIColor.Clear;
                RootNavController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                RootNavController.NavigationBar.ShadowImage = new UIImage();
                RootNavController.NavigationBar.Translucent = false;

                RootNavController.Toolbar.TintColor = UIColor.White;
                RootNavController.Toolbar.BarTintColor = UIColor.Clear.FabicColour(Data.Enums.FabicColour.Purple);

                UIColor white = UIColor.White;

                UIStringAttributes myTextAttrib = new UIStringAttributes() { StrokeColor = white, ForegroundColor = white, Font = UIFont.FromName("Avenir-Heavy", 20) };
                RootNavController.NavigationBar.TitleTextAttributes = myTextAttrib;

                Window.RootViewController = RootNavController;
                Window.MakeKeyAndVisible();
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
            }

            return true;
        }

        public override void PerformActionForShortcutItem(UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
        {
            completionHandler(HandleShortcutItem(shortcutItem));
        }
        //}

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message)
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background execution this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public async override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive.
            // If the application was previously in the background, optionally refresh the user interface.
            // refresh the access token if needed
            if (Fabic.iOS.External.Reachability.InternetConnectionStatus() != Fabic.iOS.External.NetworkStatus.NotReachable && Fabic.iOS.Controllers.SecurityController.CurrentUser != null)
            {
                try
                {
                    if (Fabic.iOS.Controllers.SecurityController.AccessTokenExpiry < DateTime.Now)
                    {
                        await Fabic.iOS.Controllers.SecurityController.RefreshAccessTokenAsync();
                        Task.Run(() => Core.Controllers.FabicDatabaseController._Client.RefreshUserAsync().Result).Wait();
                    }

                    if (Core.Controllers.FabicDatabaseController._Client.SyncContext.PendingOperations > 0)
                        Task.Run(() => Core.Controllers.FabicDatabaseController._Client.SyncContext.PushAsync(new System.Threading.CancellationToken()));
                }
                catch (Exception ex)
                {
                    ex.HandleBLSException();
                }
            }
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

        //public async override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        //{
        //    //NotificationHubClient client = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://fabicapp.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=GgGHbt9VbfLuC7hYbheY/TSMM7eXZLEktWeXk4i1flc=", "Fabic", true);
        //    //await client.CreateAppleNativeRegistrationAsync(deviceToken.ToString());
        //    Hub = new SBNotificationHub(ConnectionString, NotificationHubPath);

        //    Hub.UnregisterAllAsync(deviceToken, (error) => {
        //        if (error != null)
        //        {
        //            Console.WriteLine("Error calling Unregister: {0}", error.ToString());
        //            return;
        //        }

        //        NSSet tags = null; // create tags if you want
        //        Hub.RegisterNativeAsync(deviceToken, tags, (errorCallback) => {
        //            if (errorCallback != null)
        //                Console.WriteLine("RegisterNativeAsync error: " + errorCallback.ToString());
        //        });
        //    });
        //}

        //public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        //{
        //    ProcessNotification(userInfo, false);
        //}

        //void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
        //{
        //    // Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
        //    if (null != options && options.ContainsKey(new NSString("aps")))
        //    {
        //        //Get the aps dictionary
        //        NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

        //        string alert = string.Empty;

        //        //Extract the alert text
        //        // NOTE: If you're using the simple alert by just specifying
        //        // "  aps:{alert:"alert msg here"}  ", this will work fine.
        //        // But if you're using a complex alert with Localization keys, etc.,
        //        // your "alert" object from the aps dictionary will be another NSDictionary.
        //        // Basically the JSON gets dumped right into a NSDictionary,
        //        // so keep that in mind.
        //        if (aps.ContainsKey(new NSString("alert")))
        //            alert = (aps[new NSString("alert")] as NSString).ToString();

        //        //If this came from the ReceivedRemoteNotification while the app was running,
        //        // we of course need to manually process things like the sound, badge, and alert.
        //        if (!fromFinishedLaunching)
        //        {
        //            //Manually show an alert
        //            if (!string.IsNullOrEmpty(alert))
        //            {
        //                UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
        //                avAlert.Show();
        //            }
        //        }
        //    }
        //  }
    }

    //public class PushHandler : UAPushNotificationDelegate
    //{
    //    public override void ReceivedBackgroundNotification(UANotificationContent notificationContent, Action<UIBackgroundFetchResult> completionHandler)
    //    {
    //        Console.WriteLine("The application received a background notification");

    //        completionHandler(UIBackgroundFetchResult.NoData);
    //    }

    //    public override void ReceivedForegroundNotification(UANotificationContent notificationContent, Action completionHandler)
    //    {

    //        // Application received a foreground notification
    //        Console.WriteLine("The application received a foreground notification");

    //        // iOS 10 - let foreground presentations options handle it
    //        if (NSProcessInfo.ProcessInfo.IsOperatingSystemAtLeastVersion(new NSOperatingSystemVersion(10, 0, 0)))
    //        {
    //            completionHandler();
    //            return;
    //        }

    //        UIAlertController alertController = UIAlertController.Create(title: notificationContent.AlertTitle,
    //                                                                     message: notificationContent.AlertBody,
    //                                                                     preferredStyle: UIAlertControllerStyle.Alert);

    //        UIAlertAction okAction = UIAlertAction.Create(title: "OK", style: UIAlertActionStyle.Default, handler: (UIAlertAction action) =>
    //        {
    //            string messageID = UAInboxUtils.InboxMessageID(notificationContent.NotificationInfo);


    //            if (messageID != null)
    //            {
    //                UAActionRunner.RunAction("open_mc_action", NSObject.FromObject(messageID), UASituation.ManualInvocation);
    //            }
    //        });

    //        alertController.AddAction(okAction);

    //        UIViewController topController = UIApplication.SharedApplication.KeyWindow.RootViewController;

    //        alertController.PopoverPresentationController.SourceView = topController.View;

    //        topController.PresentViewController(alertController, true, null);

    //        completionHandler();
    //    }

    //    public override void ReceivedNotificationResponse(UANotificationResponse notificationResponse, Action completionHandler)
    //    {
    //        Console.WriteLine("The user selected the following action identifier::{0}", notificationResponse.ActionIdentifier);

    //        UANotificationContent notificationContent = notificationResponse.NotificationContent;

    //        String message = String.Format("Action Identifier:{0}", notificationResponse.ActionIdentifier);
    //        String alertBody = notificationContent.AlertBody;

    //        if (alertBody.Length > 0)
    //        {
    //            message += String.Format("\nAlert Body:\n{0}", alertBody);
    //        }

    //        String responseText = notificationResponse.ResponseText;

    //        if (responseText != null)
    //        {
    //            message += String.Format("\nResponse:\n{0}", responseText);
    //        }

    //        UIAlertController alertController = UIAlertController.Create(title: notificationContent.AlertTitle,
    //                                                                     message: alertBody,
    //                                                                     preferredStyle: UIAlertControllerStyle.Alert);

    //        UIAlertAction okAction = UIAlertAction.Create(title: "OK", style: UIAlertActionStyle.Default, handler: null);
    //        alertController.AddAction(okAction);

    //        UIViewController topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
    //        if (alertController.PopoverPresentationController != null)
    //        {
    //            alertController.PopoverPresentationController.SourceView = topController.View;
    //        }

    //        topController.PresentViewController(alertController, true, null);

    //        completionHandler();
    //    }

    //    public override UNNotificationPresentationOptions PresentationOptions(UNNotification notification)
    //    {
    //        return UNNotificationPresentationOptions.Alert | UNNotificationPresentationOptions.Sound;
    //    }
    //}
}