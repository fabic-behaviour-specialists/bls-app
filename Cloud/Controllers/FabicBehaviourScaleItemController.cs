using BLS.Cloud.Models;
using Microsoft.Azure.Mobile.Server;
using System.Linq;
using System.Web.Http.Controllers;

namespace Fabic.Cloud.Controllers
{
    public class FabicBehaviourScaleItemController : TableController<BehaviourScaleItem>
    {
        MobileServiceContext context;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<BehaviourScaleItem>(context, Request);
        }

        public IQueryable<BehaviourScaleItem> GetAllFabicBehaviourScaleItems()
        {
            if (Query().Where(x => x.UserID == string.Empty).Count() == 0)
                context.SeedFabicBehaviourScaleExamples();
            return Query().Where(x => x.UserID == string.Empty);
        }
    }
}