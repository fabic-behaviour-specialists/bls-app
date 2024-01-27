using Fabic.Core.Helpers;
using System;
using UIKit;

namespace Fabic.iOS
{
    public partial class fabicSettingsNav : UINavigationController
    {
        public fabicSettingsNav(IntPtr handle) : base(handle)
        {
            this.ApplyLightInterface();
        }
    }
}