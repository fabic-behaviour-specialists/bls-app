using CoreGraphics;
using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Fabic.iOS.Controllers;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS
{
    public partial class BehaviourScaleEditItemViewController : UIViewController, IDisposable, ICanCleanUpMyself
    {
        public BehaviourScale Scale
        {
            get; set;
        }

        public int BehaviourScaleLevel
        {
            get; set;
        }

        public event EventHandler BehaviourScaleEditItemClose;

        public List<BehaviourScaleItem> BehaviourScaleItems
        {
            get; set;
        }

        public bool BodySelected
        {
            get; set;
        }

        public BehaviourScaleEditItemViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ApplyLightInterface();
            UIWindow keyWindow = UIApplication.SharedApplication.KeyWindow;
            UIGraphics.BeginImageContext(keyWindow.Bounds.Size);
            CGContext context = UIGraphics.GetCurrentContext();
            keyWindow.Layer.RenderInContext(context);
            UIImage capturedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            EditBehaviourScaleItemOverlay loadPop = new EditBehaviourScaleItemOverlay(Scale, this.View.Frame, BehaviourScaleLevel, BehaviourScaleItems, this.View, BodySelected);
            loadPop.CloseButtonPressed += LoadPop_CloseButtonPressed;
            loadPop.ShowEditTutorial += LoadPop_ShowEditTutorial;
            loadPop.Show(this.View);
        }

        private void LoadPop_ShowEditTutorial(object sender, EventArgs e)
        {
            EditBehaviourScaleTutorialViewController view = (EditBehaviourScaleTutorialViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("behaviourScaleEditTutorialView");
            view.ProvidesPresentationContextTransitionStyle = true;
            view.DefinesPresentationContext = true;
            view.BodySelected = BodySelected;
            view.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            SecurityController.FirstTimeUsingBehaviourScaleItem = false;
            this.PresentViewController(view, true, null);
        }

        private void LoadPop_CloseButtonPressed(object sender, EventArgs e)
        {
            BehaviourScaleEditItemClose?.Invoke(sender, e);

            this.DismissModalViewController(true);
        }

        public void CleanUp()
        {

        }
    }
}