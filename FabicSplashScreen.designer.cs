// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Fabic.iOS
{
    [Register ("FabicSplashScreen")]
    partial class FabicSplashScreen
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint heightConstraint { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCopyright { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (heightConstraint != null) {
                heightConstraint.Dispose ();
                heightConstraint = null;
            }

            if (lblCopyright != null) {
                lblCopyright.Dispose ();
                lblCopyright = null;
            }
        }
    }
}