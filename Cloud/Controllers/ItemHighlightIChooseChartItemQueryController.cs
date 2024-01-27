using BodyLifeSkillsPlatform.Data.Models;
using Microsoft.Azure.Mobile.Server;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Fabic.Cloud.Controllers
{
    public class ItemHighlightIChooseChartItemQueryController : TableController<ItemHighlight>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<ItemHighlight>(context, Request);
        }

        // GET tables/ItemHighlight/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ItemHighlight> GetItemHighlight(string id)
        {
            return Lookup(id);
        }
    }
}