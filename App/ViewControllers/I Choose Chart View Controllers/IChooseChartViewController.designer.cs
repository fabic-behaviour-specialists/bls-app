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
    [Register ("IChooseChartViewController")]
    partial class IChooseChartViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView headerView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint layoutTableBottom { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint layoutTxtTitleICC { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView mainTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Fabic.iOS.FabicTextView txtTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (headerView != null) {
                headerView.Dispose ();
                headerView = null;
            }

            if (layoutTableBottom != null) {
                layoutTableBottom.Dispose ();
                layoutTableBottom = null;
            }

            if (layoutTxtTitleICC != null) {
                layoutTxtTitleICC.Dispose ();
                layoutTxtTitleICC = null;
            }

            if (mainTableView != null) {
                mainTableView.Dispose ();
                mainTableView = null;
            }

            if (txtTitle != null) {
                txtTitle.Dispose ();
                txtTitle = null;
            }
        }
    }
}