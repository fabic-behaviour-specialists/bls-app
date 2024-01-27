using CoreGraphics;
using Fabic.Core.Helpers;
using Fabic.Core.Models;
using System;
using System.Collections.Generic;
using UIKit;

namespace Fabic.iOS
{
    public delegate void UserDidEditIChooseChartEventHandler(object sender, EventArgs args);
    public partial class IChooseChartEditItemViewController : UIViewController, IDisposable, ICanCleanUpMyself
    {
        public event UserDidEditIChooseChartEventHandler UserDidEditIChooseChart;

        public IChooseChart Chart
        {
            get; set;
        }

        public Core.Enumerations.IChooseChartItemType ChartType
        {
            get; set;
        }

        public List<IChooseChartItem> ChartItems
        {
            get; set;
        }

        public bool Option2Selected
        {
            get; set;
        }

        public IChooseChartEditItemViewController(IntPtr handle) : base(handle)
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

            EditIChooseChartItemOverlay loadPop = new EditIChooseChartItemOverlay(Chart, this.View.Frame, ChartType, ChartItems, this.View, Option2Selected);
            loadPop.CloseButtonPressed += LoadPop_CloseButtonPressed;
            loadPop.ShowEditTutorial += LoadPop_ShowEditTutorial;
            loadPop.Show(this.View);
        }

        private void LoadPop_ShowEditTutorial(object sender, EventArgs e)
        {
            EditBehaviourScaleTutorialViewController view = (EditBehaviourScaleTutorialViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("behaviourScaleEditTutorialView");
            view.ProvidesPresentationContextTransitionStyle = true;
            view.DefinesPresentationContext = true;
            view.BodySelected = Option2Selected;
            view.ForIChooseChart = true;
            view.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            Controllers.SecurityController.FirstTimeUsingIChooseChartItem = false;
            //view.NavigationBarFrame = this.NavigationController.NavigationBar.Frame;
            //view.ToolBarFrame = this.NavigationController.Toolbar.Frame;
            //view.Closed += View_Closed;
            this.PresentViewController(view, true, null);
        }

        private void LoadPop_CloseButtonPressed(object sender, EventArgs e)
        {
            this.UserDidEditIChooseChart?.Invoke(this, new EventArgs());

            this.DismissModalViewController(true);
        }

        public void CleanUp()
        {

        }
    }
}