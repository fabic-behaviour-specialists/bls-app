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
    public class IChooseChartController : TableController<IChooseChart>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<IChooseChart>(context, Request);
        }

        // GET tables/IChooseChart
        public IQueryable<IChooseChart> GetAllIChooseCharts()
        {
            if (this.IsAuthorised())
                return Query().PerUserFilter<IChooseChart>(this.UserId());
            return null;
        }

        // GET tables/IChooseChart/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<IChooseChart> GetIChooseChart(string id)
        {
            if (this.IsAuthorised())
                return new SingleResult<IChooseChart>(Lookup(id).Queryable.PerUserFilter(this.UserId()));
            return null;
        }

        // PATCH tables/IChooseChart/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<IChooseChart> PatchIChooseChart(string id, Delta<IChooseChart> patch)
        {
            if (this.IsAuthorised())
            {
                IChooseChart check = GetIChooseChart(id).Queryable.FirstOrDefault();
                if (check is null)
                {
                    patch.GetEntity().CreatedAt = DateTimeOffset.Now;
                    patch.GetEntity().UpdatedAt = DateTimeOffset.Now;
                    patch.GetEntity().UserID = this.UserId();
                    Startup.InternalDatabase.ExecuteReader("INSERT INTO IChooseCharts (Id, Name, Archived, Migrated, FabicExample, UserID, CreatedAt, UpdatedAt, Deleted)" +
                   " VALUES (" +
                   "'" + id + "', " +
                    "'" + patch.GetEntity().Name + "', " +
                      (patch.GetEntity().Archived ? "1" : "0") + ", " +
                       "0, 0, " +
                       "'" + patch.GetEntity().UserID + "', " +
                       patch.GetEntity().CreatedAt.Value.ToSQLFormat() + ", " +
                                patch.GetEntity().UpdatedAt.Value.ToSQLFormat() + ", " +
                       "0)");
                }
                patch.Patch(GetIChooseChart(id).Queryable.FirstOrDefault());
                // Task<BehaviourScale> bs = UpdateAsync(id, patch);
                return new Task<IChooseChart>(new Func<IChooseChart>(() => { return GetIChooseChart(id).Queryable.FirstOrDefault(); }));
            }
            return null;
        }

        // POST tables/IChooseChart
        public async Task<IHttpActionResult> PostIChooseChart(IChooseChart item)
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
                        IChooseChart check = GetIChooseChart(item.Id).Queryable.FirstOrDefault();
                        if (check is null)
                        {
                            item.CreatedAt = DateTimeOffset.Now;
                            item.UpdatedAt = DateTimeOffset.Now;
                            Startup.InternalDatabase.ExecuteReader("INSERT INTO IChooseCharts (Id, Name, Archived, Migrated, FabicExample, UserID, CreatedAt, UpdatedAt, Deleted)" +
                           " VALUES (" +
                           "'" + item.Id + "', " +
                            "'" + item.Name + "', " +
                              (item.Archived ? "1" : "0") + ", " +
                               "0, 0, " +
                               "'" + item.UserID + "', " +
                                item.CreatedAt.Value.ToSQLFormat() + ", " +
                                item.UpdatedAt.Value.ToSQLFormat() + ", " +
                               "0)");
                        }
                    }
                    return CreatedAtRoute("Tables", new { id = item.Id }, item);
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

        // DELETE tables/IChooseChart/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteIChooseChart(string id)
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