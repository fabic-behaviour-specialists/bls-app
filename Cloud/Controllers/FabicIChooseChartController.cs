using BLS.Cloud.Models;
using Microsoft.Azure.Mobile.Server;
using System.Linq;
using System.Web.Http.Controllers;

namespace Fabic.Cloud.Controllers
{
    public class FabicIChooseChartController : TableController<IChooseChart>
    {
        MobileServiceContext context;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<IChooseChart>(context, Request);
        }

        public IQueryable<IChooseChart> GetAllFabicIChooseCharts()
        {
            if (Query().Where(x => x.FabicExample).Count() == 0)
                context.SeedFabicIChooseCharts();
            return Query().Where(x => x.FabicExample);
        }
    }
}