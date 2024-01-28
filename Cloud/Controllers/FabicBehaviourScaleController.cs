using BLS.Cloud.Models;
using Microsoft.Azure.Mobile.Server;
using System.Linq;
using System.Web.Http.Controllers;

namespace Fabic.Cloud.Controllers
{
    public class FabicBehaviourScaleController : TableController<BehaviourScale>
    {
        MobileServiceContext context;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<BehaviourScale>(context, Request);
        }

        public IQueryable<BehaviourScale> GetAllFabicBehaviourScales()
        {
            if (Query().Where(x => x.FabicExample).Count() == 0)
                context.SeedFabicBehaviourScaleExamples();
            return Query().Where(x => x.FabicExample);
        }
    }
}