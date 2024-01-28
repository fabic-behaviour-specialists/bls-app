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
    public class BehaviourScaleController : TableController<BehaviourScale>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<BehaviourScale>(context, Request);
        }

        // GET tables/BehaviourScale
        public IQueryable<BehaviourScale> GetAllBehaviourScales()
        {
            if (this.IsAuthorised())
                return Query().PerUserFilter<BehaviourScale>(this.UserId());
            else
                return null;
        }

        // GET tables/BehaviourScale/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<BehaviourScale> GetBehaviourScale(string id)
        {
            if (this.IsAuthorised())
                return new SingleResult<BehaviourScale>(Lookup(id).Queryable.PerUserFilter(this.UserId()));
            return null;
        }

        // PATCH tables/BehaviourScale/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<BehaviourScale> PatchBehaviourScale(string id, Delta<BehaviourScale> patch)
        {
            try
            {
                if (this.IsAuthorised())
                {
                    BehaviourScale check = GetBehaviourScale(id).Queryable.FirstOrDefault();
                    if (check is null)
                    {
                        patch.GetEntity().CreatedAt = DateTimeOffset.Now;
                        patch.GetEntity().UpdatedAt = DateTimeOffset.Now;
                        patch.GetEntity().UserID = this.UserId();
                        Startup.InternalDatabase.ExecuteReader("INSERT INTO BehaviourScales (Id, Description, Name, Archived, Migrated, FabicExample, UserID, CreatedAt, UpdatedAt, Deleted)" +
                       " VALUES (" +
                       "'" + id + "', " +
                       "'" + patch.GetEntity().Description + "', " +
                        "'" + patch.GetEntity().Name + "', " +
                          (patch.GetEntity().Archived ? "1" : "0") + ", " +
                           "0, 0, " +
                           "'" + patch.GetEntity().UserID + "', " +
                             patch.GetEntity().CreatedAt.Value.ToSQLFormat() + ", " +
                                patch.GetEntity().UpdatedAt.Value.ToSQLFormat() + ", " +
                           "0)");
                    }
                    else
                        patch.Patch(GetBehaviourScale(id).Queryable.FirstOrDefault());
                    // Task<BehaviourScale> bs = UpdateAsync(id, patch);
                    return new Task<BehaviourScale>(new Func<BehaviourScale>(() => { return GetBehaviourScale(id).Queryable.FirstOrDefault(); }));
                }
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
            }
            return null;
        }

        // POST tables/BehaviourScale
        public async Task<IHttpActionResult> PostBehaviourScale(BehaviourScale item)
        {
            try
            {
                if (ActionContext.Request.Headers.Contains("access_token"))
                {
                    string access_token = ActionContext.Request.Headers.GetValues("access_token").ElementAtOrDefault(0);
                    var token = (JwtSecurityToken)AccountController.Auth0Controller._SecurityController._TokenHandler.ReadToken(access_token);
                    if (BehaviourScaleReportController.IsValidUser(token))
                    {
                        item.CreatedAt = DateTimeOffset.Now;
                        item.UpdatedAt = DateTimeOffset.Now;
                        item.UserID = this.UserId();
                        // item.Id = new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd").ToString();
                        BehaviourScale check = GetBehaviourScale(item.Id).Queryable.FirstOrDefault();
                        if (check is null)
                        {
                            Startup.InternalDatabase.ExecuteReader("INSERT INTO BehaviourScales (Id, Description, Name, Archived, Migrated, FabicExample, UserID, CreatedAt, UpdatedAt, Deleted)" +
                           " VALUES (" +
                           item.Id.ToSQLFormat() + ", " +
                           item.Description.ToSQLFormat() + ", " +
                           item.Name.ToSQLFormat() + ", " +
                              (item.Archived ? "1" : "0") + ", " +
                               "0, 0, " +
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

        // DELETE tables/BehaviourScale/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteBehaviourScale(string id)
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