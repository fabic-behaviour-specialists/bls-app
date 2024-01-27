using BodyLifeSkillsPlatform.Data.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;

namespace Fabic.Cloud.Controllers
{
    public class ManagementController : ApiController
    {
        // GET tables/ItemHighlight/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [HttpPost]
        [Route("management/syncuserdata")]
        public void SyncUserData(string json)
        {
            SyncData data = (SyncData)JsonConvert.DeserializeObject(json);
            MobileServiceContext context = new MobileServiceContext();
            foreach (var e in context.BehaviourScales)
            {
                if (e.UserID == data.UserID)
                {
                    foreach (var b in data.Scales)
                    {
                        if (e.Id == b.Id)
                        {
                            context.BehaviourScales.Remove(e);
                            context.BehaviourScales.Add(b);
                        }
                    }
                }
            }
            context.SaveChanges();
            //DomainManager = new EntityDomainManager<FabicVideo>(context, Request);
        }
    }

    public class SyncData
    {
        public string UserID { get; set; }
        public List<IChooseChart> Charts { get; set; }
        public List<IChooseChartItem> ChartItems { get; set; }
        public List<BehaviourScale> Scales { get; set; }
        public List<BehaviourScaleItem> ScaleItems { get; set; }
    }
}