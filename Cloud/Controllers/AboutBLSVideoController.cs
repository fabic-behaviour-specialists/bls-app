using BodyLifeSkillsPlatform.Data.Models;
using Microsoft.Azure.Mobile.Server;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Fabic.Cloud.Controllers
{
    public class AboutBLSVideoController : TableController<FabicVideo>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<FabicVideo>(context, Request);
        }

        // GET tables/ItemHighlight
        public IQueryable<FabicVideo> GetAllAboutBLSVideos()
        {
            IQueryable<FabicVideo> videos = Query().Where(x => !x.AboutFabic);
            foreach (FabicVideo video in videos)
                video.LoadImageData();
            return videos;
        }

        // GET tables/ItemHighlight/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<FabicVideo> GetFabicVideo(string id)
        {
            SingleResult<FabicVideo> video = Lookup(id);
            if (video != null)
                video.Queryable.ElementAtOrDefault(0).LoadImageData();
            return Lookup(id);
        }

        //// PATCH tables/ItemHighlight/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public Task<FabicVideo> PatchFabicVideo(string id, Delta<FabicVideo> patch)
        //{
        //    return UpdateAsync(id, patch);
        //}

        //// POST tables/ItemHighlight
        //public async Task<IHttpActionResult> PostIChooseChartItem(ItemHighlight item)
        //{
        //    ItemHighlight current = await InsertAsync(item);
        //    return CreatedAtRoute("Tables", new { id = current.Id }, current);
        //}

        //// DELETE tables/ItemHighlight/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public Task DeleteItemHighlight(string id)
        //{
        //    return DeleteAsync(id);
        //}
    }
}