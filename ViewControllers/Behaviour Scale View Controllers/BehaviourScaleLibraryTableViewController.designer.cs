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
    [Register ("BehaviourScaleLibraryTableViewController")]
    partial class BehaviourScaleLibraryTableViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView mainTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (mainTableView != null) {
                mainTableView.Dispose ();
                mainTableView = null;
            }
        }
    }
}