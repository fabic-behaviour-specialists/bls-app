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
    [Register ("HomeController")]
    partial class HomeController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView fabicImageBackground { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint homeTop { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint imgBLSLeft { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint imgBLSRight { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint imgHomeBLSHeight { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint tableTopConstraint { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tblMain { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (fabicImageBackground != null) {
                fabicImageBackground.Dispose ();
                fabicImageBackground = null;
            }

            if (homeTop != null) {
                homeTop.Dispose ();
                homeTop = null;
            }

            if (imgBLSLeft != null) {
                imgBLSLeft.Dispose ();
                imgBLSLeft = null;
            }

            if (imgBLSRight != null) {
                imgBLSRight.Dispose ();
                imgBLSRight = null;
            }

            if (imgHomeBLSHeight != null) {
                imgHomeBLSHeight.Dispose ();
                imgHomeBLSHeight = null;
            }

            if (tableTopConstraint != null) {
                tableTopConstraint.Dispose ();
                tableTopConstraint = null;
            }

            if (tblMain != null) {
                tblMain.Dispose ();
                tblMain = null;
            }
        }
    }
}