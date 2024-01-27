using BodyLifeSkillsPlatform.Data.Helpers;
using BodyLifeSkillsPlatform.Data.Models;
using Microsoft.Azure.Mobile.Server;
using System;
using System.Data;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using static Fabic.Cloud.Controllers.AccountController;

namespace Fabic.Cloud.Controllers
{
    public class BehaviourScaleItemController : TableController<BehaviourScaleItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<BehaviourScaleItem>(context, Request);
        }

        // GET tables/BehaviourScaleItem
        public IQueryable<BehaviourScaleItem> GetAllBehaviourScaleItems()
        {
            return Query().PerUserFilter<BehaviourScaleItem>(this.UserId());
        }

        // GET tables/BehaviourScaleItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<BehaviourScaleItem> GetBehaviourScaleItem(string id)
        {
            return new SingleResult<BehaviourScaleItem>(Lookup(id).Queryable.PerUserFilter(this.UserId()));
        }

        // PATCH tables/BehaviourScaleItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<BehaviourScaleItem> PatchBehaviourScaleItem(string id, Delta<BehaviourScaleItem> patch)
        {
            try
            {
                if (this.IsAuthorised())
                {
                    BehaviourScaleItem check = GetBehaviourScaleItem(id).Queryable.FirstOrDefault();
                    if (check is null)
                    {
                        patch.GetEntity().CreatedAt = DateTimeOffset.Now;
                        patch.GetEntity().UpdatedAt = DateTimeOffset.Now;
                        patch.GetEntity().UserID = this.UserId();
                        Startup.InternalDatabase.ExecuteReader("INSERT INTO BehaviourScaleItems (Id, BehaviourScale, Name, BehaviourScaleLevel, BehaviourScaleType, Archived, Migrated, UserID, CreatedAt, UpdatedAt, Deleted)" +
                           " VALUES (" +
                           "'" + id + "', " +
                           "'" + patch.GetEntity().BehaviourScale + "', " +
                            "'" + patch.GetEntity().Name + "', " +
                              patch.GetEntity().BehaviourScaleLevel + ", " +
                               patch.GetEntity().BehaviourScaleType + ", 0, 0, " +
                               "'" + patch.GetEntity().UserID + "', " +
                              patch.GetEntity().CreatedAt.Value.ToSQLFormat() + ", " +
                                patch.GetEntity().UpdatedAt.Value.ToSQLFormat() + ", " +
                               "0)");
                    }
                    else
                        patch.Patch(GetBehaviourScaleItem(id).Queryable.FirstOrDefault());
                    // return UpdateAsync(id, patch);
                    return new Task<BehaviourScaleItem>(new Func<BehaviourScaleItem>(() => { return GetBehaviourScaleItem(id).Queryable.FirstOrDefault(); }));
                }
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
            }
            return null;
        }

        // POST tables/BehaviourScaleItem
        public async Task<IHttpActionResult> PostBehaviourScaleItem(BehaviourScaleItem item)
        {
            try
            {
                if (ActionContext.Request.Headers.Contains("access_token"))
                {
                    string access_token = ActionContext.Request.Headers.GetValues("access_token").ElementAtOrDefault(0);
                    var token = (JwtSecurityToken)Auth0Controller._SecurityController._TokenHandler.ReadToken(access_token);
                    if (BehaviourScaleReportController.IsValidUser(token))
                    {
                        item.UserID = this.UserId();
                        item.CreatedAt = DateTimeOffset.Now;
                        item.UpdatedAt = DateTimeOffset.Now;
                        // item.Id = new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd").ToString();
                        BehaviourScaleItem check = GetBehaviourScaleItem(item.Id).Queryable.FirstOrDefault();
                        if (check is null)
                        {
                            Startup.InternalDatabase.ExecuteReader("INSERT INTO BehaviourScaleItems (Id, BehaviourScale, Name, BehaviourScaleLevel, BehaviourScaleType, Archived, Migrated, UserID, CreatedAt, UpdatedAt, Deleted)" +
                               " VALUES (" +
                               "'" + item.Id + "', " +
                               "'" + item.BehaviourScale + "', " +
                                "'" + item.Name + "', " +
                                  item.BehaviourScaleLevel + ", " +
                                   item.BehaviourScaleType + ", 0, 0, " +
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

        // DELETE tables/BehaviourScaleItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteBehaviourScaleItem(string id)
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