using BigTed;
using Fabic.Core.Controllers;
using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Fabic.Data.Extensions;
using Fabic.iOS.Interfaces;
using Fabic.iOS.ViewControllers.TableViewSources;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIKit;

namespace Fabic.iOS
{
    public partial class BehaviourScaleArchivedTableViewController : UITableViewController, IBehaviourScaleViewController, IDisposable, ICanCleanUpMyself
    {
        ViewControllers.Search.BehaviourScaleSearchResultViewController resultsTableController;
        UISearchController searchController;
        bool searchControllerWasActive;
        bool searchControllerSearchFieldWasFirstResponder;
        bool useRefreshControl = false;
        UIRefreshControl RefreshControl;

        public BehaviourScaleArchivedTableViewController(IntPtr handle) : base(handle)
        {
        }

        public List<BehaviourScale> DataSource
        {
            get;
            set;
        }

        public void CleanUp()
        {

        }

        public void ShowViewController(UIViewController viewController, BehaviourScalePreviewingDelegate previewingDelegate)
        {
            // navigate to the behaviour scal
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(viewController, true);
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ApplyLightInterface();
            // Perform any additional setup after loading the view, typically from a nib.
            UIImageView backgroundImage = new UIImageView(this.View.Bounds);
            backgroundImage.Image = new UIImage("Waves.png");
            backgroundImage.ContentMode = UIViewContentMode.ScaleAspectFill;

            mainTableView.BackgroundView = backgroundImage;
            mainTableView.BackgroundColor = UIColor.Clear;

            //if (TraitCollection.ForceTouchCapability == UIForceTouchCapability.Available)
            //{
            //	// Regiser for Peek and Pop
            //	RegisterForPreviewingWithDelegate(new BehaviourScalePreviewingDelegate(this), View);
            //}
            #region Search
            resultsTableController = new ViewControllers.Search.BehaviourScaleSearchResultViewController
            {
                FilteredBehaviourScales = new List<BehaviourScale>()
            };

            searchController = new UISearchController(resultsTableController)
            {
                WeakDelegate = this,
                //DimsBackgroundDuringPresentation = false,
                WeakSearchResultsUpdater = this
            };

            searchController.SearchBar.SizeToFit();
            searchController.SearchBar.TintColor = UIColor.Black.FabicColour(Data.Enums.FabicColour.Purple);
            //searchController.SearchBar.TintColor = UIColor.White;
            TableView.TableHeaderView = searchController.SearchBar;

            //resultsTableController.TableView.WeakDelegate = this;
            searchController.SearchBar.WeakDelegate = this;

            DefinesPresentationContext = true;

            if (searchControllerWasActive)
            {
                searchController.Active = searchControllerWasActive;
                searchControllerWasActive = false;

                if (searchControllerSearchFieldWasFirstResponder)
                {
                    searchController.SearchBar.BecomeFirstResponder();
                    searchControllerSearchFieldWasFirstResponder = false;
                }
            }

            this.ExtendedLayoutIncludesOpaqueBars = true;
            #endregion
            #region Refresh
            AddRefreshControl();
            TableView.Add(RefreshControl);
            #endregion
        }

        public async override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(false, true);

            await RefreshAsync();

            this.Title = "Archived Charts";
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            // ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(true, false);
            this.Title = "Back";
        }

        #region Search
        [Export("updateSearchResultsForSearchController:")]
        public virtual void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            var tableController = (ViewControllers.Search.BehaviourScaleSearchResultViewController)searchController.SearchResultsController;
            tableController.FilteredBehaviourScales = PerformSearch(searchController.SearchBar.Text);
            tableController.TableView.ReloadData();

            searchController.SearchBar.SizeToFit();
        }

        List<BehaviourScale> PerformSearch(string searchString)
        {
            searchString = searchString.Trim();
            string[] searchItems = string.IsNullOrEmpty(searchString)
                ? new string[0]
                : searchString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var filteredProducts = new List<BehaviourScale>();

            foreach (var item in searchItems)
            {
                IEnumerable<BehaviourScale> query =
                    from p in DataSource
                    where p.Name.IndexOf(item, StringComparison.OrdinalIgnoreCase) >= 0
                    orderby p.Name
                    select p;

                filteredProducts.AddRange(query);
            }

            return filteredProducts.Distinct().ToList();
        }
        #endregion
        #region Refresh
        async Task RefreshAsync()
        {
            // only activate the refresh control if the feature is available
            //if (useRefreshControl)
            //    RefreshControl.BeginRefreshing();
            if (!RefreshControl.Refreshing)
                BTProgressHUD.Show();

            await Task.Run(() => { DataSource = FabicDatabaseController.FetchArchivedBehaviourScales(); });
            BehaviourScaleArchivedTableViewSource source = new BehaviourScaleArchivedTableViewSource(DataSource);
            mainTableView.Source = source;

            if (RefreshControl.Refreshing)
                RefreshControl.EndRefreshing();

            TableView.ReloadData();

            BTProgressHUD.Dismiss();
        }

        // This method will add the UIRefreshControl to the table view if
        // it is available, ie, we are running on iOS 6+
        void AddRefreshControl()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
            {
                // the refresh control is available, let's add it
                RefreshControl = new UIRefreshControl();
                RefreshControl.TintColor = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Blue);
                RefreshControl.ValueChanged += async (sender, e) =>
                {
                    //tableItems.Add(new TableItem("Bulbs") { ImageName = "Bulbs.jpg" });
                    await RefreshAsync();
                };
                useRefreshControl = true;
            }
        }
        #endregion
    }
}