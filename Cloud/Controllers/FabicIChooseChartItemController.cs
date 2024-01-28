using BLS.Cloud.Models;
using Microsoft.Azure.Mobile.Server;
using System.Linq;
using System.Web.Http.Controllers;

namespace Fabic.Cloud.Controllers
{
    public class FabicIChooseChartItemController : TableController<IChooseChartItem>
    {
        MobileServiceContext context;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<IChooseChartItem>(context, Request);
        }

        public IQueryable<IChooseChartItem> GetAllFabicIChooseChartItems()
        {
            if (Query().Where(x => x.UserID == string.Empty).Count() == 0)
                context.SeedFabicIChooseCharts();
            return Query().Where(x => x.UserID == string.Empty);
        }
    }
}