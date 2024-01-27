using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Foundation;
using LocalAuthentication;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Fabic.iOS.Controllers
{
    public static class SecurityController
    {
        private static FabicUser _User;
        private static string Password
        {
            get { string value = NSUserDefaults.StandardUserDefaults.StringForKey("UserPassword"); return value; }
            set { NSUserDefaults.StandardUserDefaults.SetString(value, "UserPassword"); NSUserDefaults.StandardUserDefaults.Synchronize(); }
        }
        private static string Username
        {
            get { string value = NSUserDefaults.StandardUserDefaults.StringForKey("UserEmail"); return value; }
            set { NSUserDefaults.StandardUserDefaults.SetString(value, "UserEmail"); NSUserDefaults.StandardUserDefaults.Synchronize(); }
        }
        private static DateTime IdentityAccessTokenExpiry
        {
            get { DateTime value = DateTime.FromFileTime(NSUserDefaults.StandardUserDefaults.IntForKey("IdentityAccessTokenExpiry")); return value; }
            set { NSUserDefaults.StandardUserDefaults.SetInt((nint)value.ToFileTime(), "IdentityAccessTokenExpiry"); NSUserDefaults.StandardUserDefaults.Synchronize(); }
        }
        public static DateTime AccessTokenExpiry
        {
            get { DateTime value = DateTime.FromFileTime(NSUserDefaults.StandardUserDefaults.IntForKey("AccessTokenExpiry")); return value; }
            set { NSUserDefaults.StandardUserDefaults.SetInt((nint)value.ToFileTime(), "AccessTokenExpiry"); NSUserDefaults.StandardUserDefaults.Synchronize(); }
        }
        public static bool FirstTimeUsingBehaviourScale
        {
            get { bool value = NSUserDefaults.StandardUserDefaults.BoolForKey("FirstTimeUsingBehaviourScale"); return value; }
            set { NSUserDefaults.StandardUserDefaults.SetBool(value, "FirstTimeUsingBehaviourScale"); NSUserDefaults.StandardUserDefaults.Synchronize(); }
        }
        public static bool FirstTimeUsingBehaviourScaleItem
        {
            get { bool value = NSUserDefaults.StandardUserDefaults.BoolForKey("FirstTimeUsingBehaviourScaleItem"); return value; }
            set { NSUserDefaults.StandardUserDefaults.SetBool(value, "FirstTimeUsingBehaviourScaleItem"); NSUserDefaults.StandardUserDefaults.Synchronize(); }
        }
        public static bool FirstTimeUsingIChooseChart
        {
            get { bool value = NSUserDefaults.StandardUserDefaults.BoolForKey("FirstTimeUsingIChooseChart"); return value; }
            set { NSUserDefaults.StandardUserDefaults.SetBool(value, "FirstTimeUsingIChooseChart"); NSUserDefaults.StandardUserDefaults.Synchronize(); }
        }
        public static bool FirstTimeUsingIChooseChartItem
        {
            get { bool value = NSUserDefaults.StandardUserDefaults.BoolForKey("FirstTimeUsingIChooseChartItem"); return value; }
            set { NSUserDefaults.StandardUserDefaults.SetBool(value, "FirstTimeUsingIChooseChartItem"); NSUserDefaults.StandardUserDefaults.Synchronize(); }
        }
        public static bool FirstTimeUsingApp
        {
            get
            {
                bool value = NSUserDefaults.StandardUserDefaults.BoolForKey("FirstTimeUsingApp");
                return value;
            }
            set { NSUserDefaults.StandardUserDefaults.SetBool(value, "FirstTimeUsingApp"); NSUserDefaults.StandardUserDefaults.Synchronize(); }
        }
        private static string IdentityAccessToken
        {
            get
            {
                string value = NSUserDefaults.StandardUserDefaults.StringForKey("IdentityAccessToken");
                if (value == null) { value = ""; }
                return value;
            }
            set { NSUserDefaults.StandardUserDefaults.SetString(value, "IdentityAccessToken"); NSUserDefaults.StandardUserDefaults.Synchronize(); }
        }
        public static string AccessToken
        {
            get { string value = NSUserDefaults.StandardUserDefaults.StringForKey("AccessToken"); if (value == null) { value = ""; } return value; }
            set { NSUserDefaults.StandardUserDefaults.SetString(value, "AccessToken"); NSUserDefaults.StandardUserDefaults.Synchronize(); }
        }
        public static FabicUser CurrentUser
        {
            get
            {
                if (_User == null)
                {
                    var value = NSUserDefaults.StandardUserDefaults.ValueForKey(new NSString("CurrentUser"));
                    if (value != null)
                    {
                        if (value.GetType() == typeof(NSDictionary))
                        {
                            NSDictionary nSMutableDictionary = (NSDictionary)NSUserDefaults.StandardUserDefaults.ValueForKey(new NSString("CurrentUser"));
                            if (nSMutableDictionary != null)
                            {
                                _User = new FabicUser();
                                _User.Email = (NSString)nSMutableDictionary.ValueForKey(new NSString("Email"));
                                _User.Name = (NSString)nSMutableDictionary.ValueForKey(new NSString("Name"));
                                _User.GravatarURL = (NSString)nSMutableDictionary.ValueForKey(new NSString("Picture"));
                                _User.UserID = (NSString)nSMutableDictionary.ValueForKey(new NSString("ID"));
                            }
                        }
                        else if (value.GetType() == typeof(NSMutableDictionary))
                        {
                            NSMutableDictionary nSMutableDictionary = (NSMutableDictionary)NSUserDefaults.StandardUserDefaults.ValueForKey(new NSString("CurrentUser"));
                            if (nSMutableDictionary != null)
                            {
                                _User = new FabicUser();
                                _User.Email = (NSString)nSMutableDictionary.ValueForKey(new NSString("Email"));
                                _User.Name = (NSString)nSMutableDictionary.ValueForKey(new NSString("Name"));
                                _User.GravatarURL = (NSString)nSMutableDictionary.ValueForKey(new NSString("Picture"));
                                _User.UserID = (NSString)nSMutableDictionary.ValueForKey(new NSString("ID"));
                            }
                        }
                    }
                }
                return _User;
            }
            set
            {
                _User = value;
                if (value != null)
                {
                    NSMutableDictionary nSMutableDictionary = new NSMutableDictionary();
                    nSMutableDictionary.SetValueForKey(new NSString(_User.Email), new NSString("Email"));
                    nSMutableDictionary.SetValueForKey(new NSString(_User.Name), new NSString("Name"));
                    nSMutableDictionary.SetValueForKey(new NSString(_User.UserID), new NSString("ID"));
                    nSMutableDictionary.SetValueForKey(new NSString(_User.GravatarURL), new NSString("Picture"));
                    NSUserDefaults.StandardUserDefaults.SetValueForKey(nSMutableDictionary, new NSString("CurrentUser"));
                }
                else
                {
                    NSUserDefaults.StandardUserDefaults.SetValueForKey(new NSString(""), new NSString("CurrentUser"));
                }
                NSUserDefaults.StandardUserDefaults.Synchronize();
            }
        }

        public static async Task<string> GetAccessTokenAsync()
        {
            string result = AccessToken;
            if (AccessTokenExpiry < DateTime.Now)
            {
                await Task.Run(() =>
                {
                    result = RefreshAccessToken();
                });
            }
            return result;
        }

        public static async Task<FabicUser> GetUserInfoAsync()
        {
            FabicUser result = null;
            await Task.Run(() =>
            {
                result = GetUserInfo();
            });

            if (result != null)
                await Fabic.Core.Controllers.FabicDatabaseController.InitialiseDatabase();

            return result;
        }

        /// <summary>
        /// Retrieves the current user info from the access token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static FabicUser GetUserInfo()
        {
            CurrentUser = null;
            try
            {
                var client = new RestClient("https://fabic.au.auth0.com/userinfo");
                var request = new RestRequest();
                request.AddHeader("Authorization", "Bearer " + IdentityAccessToken);
                RestResponse response = client.Get(request);

                if (response.IsSuccessful)
                {
                    dynamic stuff = JObject.Parse(response.Content);
                    CurrentUser = FabicUser.LoadFromData(stuff.sub.ToString(), ((JObject)stuff).Property("http://fiz/full_name").Value.ToString(), stuff.email.ToString(), stuff.picture.ToString());

                }
                else
                {
                    dynamic stuff = JObject.Parse(response.Content);
                    return stuff.description;
                }
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return null;
            }
            return CurrentUser;
        }

        public static async Task<bool> ResetPasswordAsync(string emailAddress, string password)
        {
            bool result = true;
            await Task.Run(() =>
            {
                result = ResetPassword(emailAddress, password);
            });

            return result;
        }

        /// <summary>
        /// Resets the users password when they have forgotton it
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool ResetPassword(string emailAddress, string password)
        {
            CurrentUser = null;
            try
            {
                var client = new RestClient("https://fabic.au.auth0.com/dbconnections/change_password");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{  \"email\": \"" + emailAddress + "\",\"password\": \"" + password + "\",\"client_id\": \"r4U8jEuhvyVdabjLhrNYci0FcaCHGUzm\",\"connection\": \"Username-Password-Authentication\"}", ParameterType.RequestBody);
                RestResponse response = client.Post(request);

                if (response.IsSuccessful)
                {

                }
                else
                {
                    dynamic stuff = JObject.Parse(response.Content);
                    return stuff.description;
                }
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return false;
            }
            return true;
        }

        public static async Task<string> LoginAsync(string email, string password)
        {
            string result = "";
            await Task.Run(() =>
            {
                result = Login(email, password);
            });

            if (AccessToken.Length > 0)
                await Task.Run(() =>
                {
                    GetUserInfo();
                });

            return result;
        }

        /// <summary>
        /// Signs the user in and retrieves a valid access token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Login(string email, string password)
        {
            AccessToken = "";
            IdentityAccessToken = "";
            CurrentUser = null;
            try
            {
                // first identity
                Password = password;
                Username = email;
                var client = new RestClient("https://fabic.au.auth0.com/oauth/token");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{  \"scope\": \"ReadWrite openid\",\"grant_type\": \"password\",\"client_id\": \"r4U8jEuhvyVdabjLhrNYci0FcaCHGUzm\",\"username\": \"" + email +
                    "\",\"password\": \"" + password + "\",\"audience\": \"https://fabic.au.auth0.com/userinfo\",\"client_secret\": \"YvKZpCLzhLN34EhEpGkSRpZ3e_sHaR3RKrig1xkQTMx4DCiZ60XomkdOZBFLA6HA\"}", ParameterType.RequestBody);
                RestResponse response = client.Post(request);

                if (response.IsSuccessful)
                {
                    dynamic stuff = JObject.Parse(response.Content);
                    IdentityAccessToken = stuff.access_token;
                    IdentityAccessTokenExpiry = DateTime.Now.AddSeconds(Convert.ToDouble((int)stuff.expires_in));
                    FirstTimeUsingBehaviourScale = true;
                    FirstTimeUsingBehaviourScaleItem = true;
                    FirstTimeUsingIChooseChart = true;
                    FirstTimeUsingIChooseChartItem = true;
                    FirstTimeUsingApp = true;
                }
                else
                {
                    dynamic stuff = JObject.Parse(response.Content);
                    return stuff.error_description;
                }

                // then the main access token
                request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{  \"scope\": \"ReadWrite openid offline_access profile\",\"grant_type\": \"password\",\"client_id\": \"r4U8jEuhvyVdabjLhrNYci0FcaCHGUzm\",\"username\": \"" + email +
                    "\",\"password\": \"" + password + "\",\"audience\": \"https://api.fabic.com.au/\",\"client_secret\": \"YvKZpCLzhLN34EhEpGkSRpZ3e_sHaR3RKrig1xkQTMx4DCiZ60XomkdOZBFLA6HA\"}", ParameterType.RequestBody);
                response = client.Post(request);

                if (response.IsSuccessful)
                {
                    dynamic stuff = JObject.Parse(response.Content);
                    AccessToken = stuff.access_token;
                    AccessTokenExpiry = DateTime.Now.AddSeconds(Convert.ToDouble((int)stuff.expires_in));
                }
                else
                {
                    dynamic stuff = JObject.Parse(response.Content);
                    return stuff.error_description;
                }
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return ex.Message;
            }
            return AccessToken;
        }

        public static async Task<string> RefreshAccessTokenAsync()
        {
            string result = "";
            await Task.Run(() =>
            {
                result = RefreshAccessToken();
            });

            return result;
        }

        /// <summary>
        /// Refreshes the access token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string RefreshAccessToken()
        {
            try
            {
                var client = new RestClient("https://fabic.au.auth0.com/oauth/token");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{  \"scope\": \"ReadWrite openid offline_access profile\",\"grant_type\": \"password\",\"client_id\": \"r4U8jEuhvyVdabjLhrNYci0FcaCHGUzm\",\"username\": \"" + Username +
                    "\",\"password\": \"" + Password + "\",\"audience\": \"https://api.fabic.com.au/\",\"client_secret\": \"YvKZpCLzhLN34EhEpGkSRpZ3e_sHaR3RKrig1xkQTMx4DCiZ60XomkdOZBFLA6HA\"}", ParameterType.RequestBody);
                RestResponse response = client.Post(request);

                if (response.IsSuccessful)
                {
                    dynamic stuff = JObject.Parse(response.Content);
                    AccessToken = stuff.access_token;
                    AccessTokenExpiry = DateTime.Now.AddSeconds(Convert.ToDouble((int)stuff.expires_in));
                }
                else
                {
                    dynamic stuff = JObject.Parse(response.Content);
                    return stuff.description;
                }
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return ex.Message;
            }
            return AccessToken;
        }

        public static async Task<string> RegisterAsync(string name, string email, string password)
        {
            string result = "";
            await Task.Run(() =>
            {
                string s = Register(name, email, password);
                if (!string.IsNullOrWhiteSpace(s))
                    result = s;
            });
            if (string.IsNullOrWhiteSpace(result))
                result = await LoginAsync(email, password);

            return result;
        }

        /// <summary>
        /// Signs the user up and retrieves a valid access token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Register(string name, string email, string password)
        {
            AccessToken = "";
            CurrentUser = null;
            try
            {
                var client = new RestClient("https://fabic.au.auth0.com/dbconnections/signup");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"client_id\": \"S2MqfF7SXrv1wDhUlmCwOY0VChrhA3c8\",\"email\": \"" + email +
                             "\",\"password\": \"" + password + "\",\"connection\": \"Username-Password-Authentication\",\"user_metadata\": {\"fullname\": \"" + name + "\"}}", ParameterType.RequestBody);
                RestResponse response = client.Post(request);

                if (response.IsSuccessful)
                {
                    //dynamic stuff = JObject.Parse(response.Content);
                    //AccessToken = stuff.access_token;
                    FirstTimeUsingBehaviourScale = true;
                    FirstTimeUsingBehaviourScaleItem = true;
                    FirstTimeUsingIChooseChart = true;
                    FirstTimeUsingIChooseChartItem = true;
                }
                else
                {
                    dynamic stuff = JObject.Parse(response.Content);
                    return stuff.description;
                }
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return ex.Message;
            }
            return "";
        }

        /// <summary>
        /// Signs the current user out
        /// </summary>
        public static void SignOut()
        {
            CurrentUser = null;
            IdentityAccessToken = "";
            IdentityAccessTokenExpiry = DateTime.Now;
            AccessToken = "";
            AccessTokenExpiry = DateTime.Now;
            Password = "";
            Username = "";
        }

        /// <summary>
        /// Returns the type of biometric authentication that is supported by the device
        /// </summary>
        /// <returns></returns>
        public static LABiometryType CheckBiometricAuthenticationType()
        {
            LAContext context = new LAContext();
            NSError err = new NSError();
            context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out err);
            return context.BiometryType;
        }
    }

    public class NSObjectWrapper : NSObject
    {
        public object Context;

        public NSObjectWrapper(object obj) : base()
        {
            this.Context = obj;
        }

        public static NSObjectWrapper Wrap(object obj)
        {
            return new NSObjectWrapper(obj);
        }
    }
}