using Foundation;
using Security;

namespace Xamarin.LockScreen.Security
{
    public static class Keychain
    {
        private static string Key { get { return NSBundle.MainBundle.BundleIdentifier + " - Locker"; } }

        private static SecRecord GenerateRecord()
        {
            return new SecRecord(SecKind.GenericPassword)
            {
                Generic = NSData.FromString(Key),
                Service = Key,
                Label = Key,
                Description = Key,
                Synchronizable = true
            };
        }
        private static SecRecord QueryRecord(SecRecord toSearch)
        {
            SecStatusCode res;
            var match = SecKeyChain.QueryAsRecord(toSearch, out res);
            return match;
        }

        public static bool IsPasswordSet()
        {
            var settings = NSUserDefaults.StandardUserDefaults;
            if (settings.StringForKey("PIN") != null)
            {
                if (settings.StringForKey("PIN").Length > 0)
                    return true;
            }
            return false;
        }

        public static string GetPassword()
        {
            var settings = NSUserDefaults.StandardUserDefaults;
            return settings.StringForKey("PIN");
        }

        public static void SavePassword(string value)
        {
            var settings = NSUserDefaults.StandardUserDefaults;
            if (value == null)
            {
                settings.SetString("", "PIN");
            }
            else
            {
                settings.SetString(value, "PIN");
            }
            settings.Synchronize();
        }

        public static bool IsTouchIDEnabled()
        {
            var settings = NSUserDefaults.StandardUserDefaults;
            return settings.BoolForKey("TouchID");
        }

        public static void SaveTouchID(bool enabled = true)
        {
            var settings = NSUserDefaults.StandardUserDefaults;
            settings.SetBool(enabled, "TouchID");
            settings.Synchronize();
        }
    }
}