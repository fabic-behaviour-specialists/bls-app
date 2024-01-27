using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server.Login;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Fabic.Cloud.Controllers
{
    [MobileAppController]
    public class AccountController : ApiController
    {
        [Route(".auth/login/auth0")]
        public class Auth0Controller : ApiController
        {
            public static Auth0Controller _SecurityController;
            public JwtSecurityTokenHandler _TokenHandler;
            public string _ClientID, _Domain;
            public string _SigningKey, _Audience, _Issuer;

            public Auth0Controller()
            {
                // Information for the incoming Auth0 Token
                _Domain = "fabic.au.auth0.com"; //Environment.GetEnvironmentVariable("AUTH0_DOMAIN");
                _ClientID = "https://api.fabic.com.au/";//Environment.GetEnvironmentVariable("AUTH0_CLIENTID");

                // Information for the outgoing ZUMO Token
                _SigningKey = "S2MqfF7SXrv1wDhUlmCwOY0VChrhA3c8";
                var website = Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME");
                _Audience = $"https://api.fabic.com.au/";
                _Issuer = $"https://api.fabic.com.au/";

                // Token Handler
                _TokenHandler = new JwtSecurityTokenHandler();
            }

            [HttpPost]
            public IHttpActionResult Post([FromBody] Auth0User body)
            {
                if (body == null || body.access_token == null || body.access_token.Length == 0)
                {
                    return BadRequest();
                }

                try
                {
                    var token = (JwtSecurityToken)_TokenHandler.ReadToken(body.access_token);
                    if (!IsValidUser(token))
                    {
                        return Unauthorized();
                    }

                    var subject = token.Claims.FirstOrDefault(c => c.Type.Equals("sub"))?.Value;
                    var email = token.Claims.FirstOrDefault(c => c.Type.Equals("azp"))?.Value;
                    if (subject == null || email == null)
                    {
                        return BadRequest();
                    }

                    var claims = new Claim[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, subject),
                    new Claim(JwtRegisteredClaimNames.Azp, email)
                    };

                    JwtSecurityToken zumoToken = AppServiceLoginHandler.CreateToken(
                        claims, _SigningKey, _Audience, _Issuer, TimeSpan.FromDays(30));
                    return Ok(new LoginResult()
                    {
                        AuthenticationToken = zumoToken.RawData,
                        User = new LoginResultUser { UserId = email }
                    });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Auth0 JWT Exception = {ex.Message}");
                    throw ex;
                }
            }

            private bool IsValidUser(JwtSecurityToken token)
            {
                if (token == null)
                    return false;
                var audience = token.Audiences.FirstOrDefault();
                if (!audience.Equals(_ClientID))
                    return false;
                if (!token.Issuer.Equals($"https://{_Domain}/"))
                    return false;
                if (token.ValidTo.AddMinutes(5) < DateTime.Now)
                    return false;
                return true;
            }
        }

        public class Auth0User
        {
            public string access_token { get; set; }
        }

        public class LoginResult
        {
            [JsonProperty(PropertyName = "authenticationToken")]
            public string AuthenticationToken { get; set; }

            [JsonProperty(PropertyName = "user")]
            public LoginResultUser User { get; set; }
        }

        public class LoginResultUser
        {
            [JsonProperty(PropertyName = "userId")]
            public string UserId { get; set; }
        }
    }
}
