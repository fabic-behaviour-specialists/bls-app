﻿// WARNING
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
    [Register ("LoginController")]
    partial class LoginController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgBLSLogin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tblMain { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgBLSLogin != null) {
                imgBLSLogin.Dispose ();
                imgBLSLogin = null;
            }

            if (tblMain != null) {
                tblMain.Dispose ();
                tblMain = null;
            }
        }
    }
}