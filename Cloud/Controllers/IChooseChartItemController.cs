using BLS.Cloud.Helpers;
using BLS.Cloud.Models;
using Microsoft.Azure.Mobile.Server;
using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;

namespace Fabic.Cloud.Controllers
{
    public class IChooseChartItemController : TableController<IChooseChartItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<IChooseChartItem>(context, Request);
        }

        // GET tables/IChooseChartItem
        public IQueryable<IChooseChartItem> GetAllIChooseChartItems()
        {
            return Query().PerUserFilter<IChooseChartItem>(this.UserId());
        }

        // GET tables/IChooseChartItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<IChooseChartItem> GetIChooseChartItem(string id)
        {
            return new SingleResult<IChooseChartItem>(Lookup(id).Queryable.PerUserFilter(this.UserId()));
        }

        // PATCH tables/IChooseChartItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<IChooseChartItem> PatchIChooseChartItem(string id, Delta<IChooseChartItem> patch)
        {
            if (this.IsAuthorised())
            {
                IChooseChartItem check = GetIChooseChartItem(patch.GetEntity().Id).Queryable.FirstOrDefault();
                if (check is null)
                {
                    patch.GetEntity().UserID = this.UserId();
                    patch.GetEntity().CreatedAt = DateTimeOffset.Now;
                    patch.GetEntity().UpdatedAt = DateTimeOffset.Now;
                    Startup.InternalDatabase.ExecuteReader("INSERT INTO IChooseChartItems (Id, IChooseChart, ItemText, ChartOption, ChartType, Archived, Migrated, UserID, CreatedAt, UpdatedAt, Deleted)" +
                       " VALUES (" +
                       "'" + patch.GetEntity().Id + "', " +
                       "'" + patch.GetEntity().IChooseChart + "', " +
                        "'" + patch.GetEntity().ItemText + "', " +
                          patch.GetEntity().ChartOption + ", " +
                           patch.GetEntity().ChartType + ", 0, 0, " +
                           "'" + patch.GetEntity().UserID + "', " +
                           patch.GetEntity().CreatedAt.Value.ToSQLFormat() + ", " +
                                patch.GetEntity().UpdatedAt.Value.ToSQLFormat() + ", " +
                           "0)");
                }
                patch.Patch(GetIChooseChartItem(id).Queryable.FirstOrDefault());
                // Task<BehaviourScale> bs = UpdateAsync(id, patch);
                return new Task<IChooseChartItem>(new Func<IChooseChartItem>(() => { return GetIChooseChartItem(id).Queryable.FirstOrDefault(); }));
            }
            return null;
        }

        // POST tables/IChooseChartItem
        public async Task<IHttpActionResult> PostIChooseChartItem(IChooseChartItem item)
        {
            try
            {
                if (ActionContext.Request.Headers.Contains("access_token"))
                {
                    string access_token = ActionContext.Request.Headers.GetValues("access_token").ElementAtOrDefault(0);
                    var token = (JwtSecurityToken)AccountController.Auth0Controller._SecurityController._TokenHandler.ReadToken(access_token);
                    if (BehaviourScaleReportController.IsValidUser(token))
                    {
                        item.UserID = this.UserId();
                        item.CreatedAt = DateTimeOffset.Now;
                        item.UpdatedAt = DateTimeOffset.Now;
                        // item.Id = new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd").ToString();
                        IChooseChartItem check = GetIChooseChartItem(item.Id).Queryable.FirstOrDefault();
                        if (check is null)
                        {
                            Startup.InternalDatabase.ExecuteReader("INSERT INTO IChooseChartItems (Id, IChooseChart, ItemText, ChartOption, ChartType, Archived, Migrated, UserID, CreatedAt, UpdatedAt, Deleted)" +
                               " VALUES (" +
                               "'" + item.Id + "', " +
                               "'" + item.IChooseChart + "', " +
                                "'" + item.ItemText + "', " +
                                  item.ChartOption + ", " +
                                   item.ChartType + ", 0, 0, " +
                                   "'" + item.UserID + "', " +
                                     item.CreatedAt.Value.ToSQLFormat() + ", " +
                                item.UpdatedAt.Value.ToSQLFormat() + ", " +
                                   "0)");
                        }
                        return CreatedAtRoute("Tables", new { id = item.Id }, item);
                    }
                }
            }
            catch (Exception ex)
            {
                //var err = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                //{
                //    Content = new StringContent(ex.Message + Environment.NewLine + ex.StackTrace)
                //};
                ex.HandleBLSException();
            }
            return null;
        }

        // DELETE tables/IChooseChartItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteIChooseChartItem(string id)
        {
            if (this.IsAuthorised())
            {
                this.ValidateOwner(Query().Where(x => x.Id == id));
                return DeleteAsync(id);
            }
            return null;
        }
    }
}