using Fabic.Core.Models;
using System.Collections.Generic;
using UIKit;
namespace Fabic.iOS.Interfaces
{
    /// <summary>
    /// Behaviour scale view controller interface.
    /// </summary>
    public interface IBehaviourScaleViewController
    {
        #region Properties
        UITableView TableView { get; set; }
        List<BehaviourScale> DataSource { get; set; }
        UIStoryboard Storyboard { get; }
        #endregion
        #region Methods
        void ShowViewController(UIViewController viewController, BehaviourScalePreviewingDelegate previewingDelegate);
        #endregion
    }
}
