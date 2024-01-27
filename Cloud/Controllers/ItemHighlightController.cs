using BodyLifeSkillsPlatform.Data.Models;
using Microsoft.Azure.Mobile.Server;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;

namespace Fabic.Cloud.Controllers
{
    public class ItemHighlightController : TableController<ItemHighlight>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<ItemHighlight>(context, Request);
        }

        // GET tables/ItemHighlight
        public IQueryable<ItemHighlight> GetAllItemHighlights()
        {
            return Query();
        }

        // GET tables/ItemHighlight/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ItemHighlight> GetItemHighlight(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/ItemHighlight/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ItemHighlight> PatchItemHighlight(string id, Delta<ItemHighlight> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/ItemHighlight
        public async Task<IHttpActionResult> PostIChooseChartItem(ItemHighlight item)
        {
            ItemHighlight current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ItemHighlight/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteItemHighlight(string id)
        {
            return DeleteAsync(id);
        }
    }
}