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
    [Register ("IChooseChartPrintController")]
    partial class IChooseChartPrintController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem barBtnCancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem barBtnDone { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tblMain { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (barBtnCancel != null) {
                barBtnCancel.Dispose ();
                barBtnCancel = null;
            }

            if (barBtnDone != null) {
                barBtnDone.Dispose ();
                barBtnDone = null;
            }

            if (tblMain != null) {
                tblMain.Dispose ();
                tblMain = null;
            }
        }
    }
}