using Fabic.Core.Models;
using System.Collections.Generic;
using UIKit;
namespace Fabic.iOS.Interfaces
{
    /// <summary>
    /// I Choose Chart view controller interface.
    /// </summary>
    public interface IIChooseChartViewController
    {
        #region Properties
        UITableView TableView { get; set; }
        List<IChooseChart> DataSource { get; set; }
        UIStoryboard Storyboard { get; }
        #endregion
        #region Methods
        void ShowViewController(UIViewController viewController, IChooseChartPreviewingDelegate previewingDelegate);
        #endregion
    }
}
