using BodyLifeSkillsPlatform.Data.Models;
using Microsoft.Azure.Mobile.Server.Tables;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace BodyLifeSkillsPlatform.Data.Helpers
{
    public static class TableControllerExtensions
    {
        public static void ValidateOwner(this TableController controller, IQueryable<BaseModel> queryable)
        {
            var result = queryable.PerUserFilter(controller.UserId()).FirstOrDefault<BaseModel>();
            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public static string UserId(this TableController controller)
        {
            //var principal = controller.User as ClaimsPrincipal;
            //return principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (controller.Request.Headers.Contains("userId"))
            {
                string userId = controller.Request.Headers.GetValues("userId").ElementAtOrDefault(0);
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    return userId;
                }
            }
            return string.Empty;
        }

        public static bool IsAuthorised(this TableController controller)
        {
            if (controller.Request.Headers.Contains("access_token"))
            {
                string access_token = controller.Request.Headers.GetValues("access_token").ElementAtOrDefault(0);
                var token = (JwtSecurityToken)Cloud.Controllers.AccountController.Auth0Controller._SecurityController._TokenHandler.ReadToken(access_token);
                if (IsValidUser(token))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsValidUser(JwtSecurityToken token)
        {
            if (token == null)
                return false;
            var audience = token.Audiences.FirstOrDefault();
            if (!audience.Equals(Fabic.Cloud.Controllers.AccountController.Auth0Controller._SecurityController._ClientID))
                return false;
            if (!token.Issuer.Equals(Fabic.Cloud.Controllers.AccountController.Auth0Controller._SecurityController._Issuer) && !token.Issuer.Equals($"https://{Fabic.Cloud.Controllers.AccountController.Auth0Controller._SecurityController._Domain}/"))
                return false;
            //if (token.ValidTo.AddMinutes(5) < DateTime.Now)
            //  return false;
            return true;
        }
    }
}
