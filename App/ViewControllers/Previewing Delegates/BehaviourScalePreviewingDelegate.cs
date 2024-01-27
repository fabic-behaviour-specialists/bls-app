using CoreGraphics;
using Fabic.iOS.Interfaces;
using Foundation;
using System;
using UIKit;

namespace Fabic.iOS
{
    public class BehaviourScalePreviewingDelegate : UIViewControllerPreviewingDelegate
    {
        #region Computed Properties
        public IBehaviourScaleViewController MasterController { get; set; }
        #endregion

        #region Constructors
        public BehaviourScalePreviewingDelegate(IBehaviourScaleViewController masterController)
        {
            // Initialize
            this.MasterController = masterController;
        }

        public BehaviourScalePreviewingDelegate(NSObjectFlag t) : base(t)
        {
        }

        public BehaviourScalePreviewingDelegate(IntPtr handle) : base(handle)
        {
        }
        #endregion

        #region Override Methods
        /// Present the view controller for the "Pop" action.
        public override void CommitViewController(IUIViewControllerPreviewing previewingContext, UIViewController viewControllerToCommit)
        {
            // Reuse Peek view controller for details presentation
            MasterController.ShowViewController(viewControllerToCommit, this);
        }

        /// Create a previewing view controller to be shown at "Peek".
        public override UIViewController GetViewControllerForPreview(IUIViewControllerPreviewing previewingContext, CGPoint location)
        {
            // Grab the item to preview
            var indexPath = MasterController.TableView.IndexPathForRowAtPoint(location);
            var cell = MasterController.TableView.CellAt(indexPath);
            var item = MasterController.DataSource[indexPath.Row];

            UIViewController controller = UIStoryboard.FromName("Main", null).InstantiateViewController("BehaviourScaleViewIdentifier");
            ((BehaviourScaleViewController)controller).BehaviourScale = item;

            // Set the source rect to the cell frame, so everything else is blurred.
            previewingContext.SourceRect = cell.Frame;

            return controller;
        }
        #endregion
    }
}
