using BigTed;
using Curse;
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
    public partial class BehaviourScaleLibraryTableViewController : UITableViewController, IBehaviourScaleViewController, IDisposable, ICanCleanUpMyself
    {
        ViewControllers.Search.BehaviourScaleSearchResultViewController resultsTableController;
        UISearchController searchController;
        bool searchControllerWasActive;
        bool searchControllerSearchFieldWasFirstResponder;
        bool useRefreshControl = false;
        UIRefreshControl RefreshControl;

        public List<BehaviourScale> DataSource
        {
            get;
            set;
        }

        public BehaviourScaleLibraryTableViewController(IntPtr handle) : base(handle)
        {
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

            UIImage addImg = UIImage.FromBundle("Add");
            addImg = addImg.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            UIBarButtonItem addBtn = new UIBarButtonItem(addImg, UIBarButtonItemStyle.Bordered, AddBtn_Clicked);
            this.NavigationItem.RightBarButtonItem = addBtn;

            // Check to see if 3D Touch is available
            // 		if (TraitCollection.ForceTouchCapability == UIForceTouchCapability.Available)
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

            this.Title = "My Behaviour Scales";
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            //((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.SetNavigationBarHidden(true, false);
            //this.Title = "Back";
        }

        void HandleAction(Curse.CRSAlertView obj)
        {
            // Do something here on pre
            string input = obj.Input.Text;

            BehaviourScale bs = new BehaviourScale();
            bs.Archived = false;
            bs.Description = "";
            bs.FabicExample = false;
            bs.Name = input;

            FabicDatabaseController.SaveOrUpdateBehaviourScale(bs);

            UIViewController controller = UIStoryboard.FromName("Main", null).InstantiateViewController("BehaviourScaleViewIdentifier");
            ((BehaviourScaleViewController)controller).BehaviourScale = bs;
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(controller, true);
        }

        void AddBtn_Clicked(object sender, EventArgs e)
        {
            CRSAlertView alert = new CRSAlertView();
            alert.TintColor = UIColor.Purple.FabicColour(Data.Enums.FabicColour.Purple);
            alert.Title = "Add a Behaviour Scale";
            alert.Message = "What would you like to call your Behaviour Scale?";
            alert.Image = new UIImage("fabic-logo.png");

            var action = new CRSAlertAction
            {
                Text = "Cancel",
                Highlighted = false,
                TintColor = UIColor.Black,
                DidSelect = (alert2) =>
                {
                    // Do something here on press
                }
            };

            var input2 = new CRSAlertInput
            {
                Placeholder = "Name of Behaviour Scale",
                Text = "",
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Purple),
                OpenAutomatically = true
            };

            var action2 = new CRSAlertAction
            {
                Text = "Save",
                Highlighted = true,
                TintColor = UIColor.Cyan.FabicColour(Data.Enums.FabicColour.Purple),
                DidSelect = HandleAction
            };

            alert.Input = input2;
            alert.Actions = new CRSAlertAction[] { action, action2 };
            alert.Show();
        }

        public void ShowViewController(UIViewController viewController, BehaviourScalePreviewingDelegate previewingDelegate)
        {
            ((AppDelegate)UIApplication.SharedApplication.Delegate).RootNavController.PushViewController(viewController, true);
        }

        public void CleanUp()
        {

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

            await Task.Run(() => { DataSource = FabicDatabaseController.FetchActiveBehaviourScales().Result; });
            BehaviourScaleLibraryTableViewSource source = new BehaviourScaleLibraryTableViewSource();
            source.DataSource = DataSource;
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