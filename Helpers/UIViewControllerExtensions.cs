using System;
using UIKit;

namespace Fabic.Core.Helpers
{
    public static class UIViewControllerExtensions
    {
        public static void ApplyLightInterface(this UIViewController view)
        {
            string version = UIDevice.CurrentDevice.SystemVersion;
            if (version.Contains("."))
                version = version.Substring(0, version.IndexOf("."));
            int v = Convert.ToInt32(version);
            if (v >= 13)
            {
                // code here
                view.OverrideUserInterfaceStyle = UIUserInterfaceStyle.Light;
            }
        }

        public static void ApplyLightInterface(this UIView view)
        {
            string version = UIDevice.CurrentDevice.SystemVersion;
            if (version.Contains("."))
                version = version.Substring(0, version.IndexOf("."));
            int v = Convert.ToInt32(version);
            if (v >= 13)
            {
                // code here
                view.OverrideUserInterfaceStyle = UIUserInterfaceStyle.Light;
            }
        }
    }
}