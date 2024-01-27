using BodyLifeSkillsPlatform.Data;
using BodyLifeSkillsPlatform.Data.Models;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server.Tables.Config;
using Microsoft.Owin;
using Owin;
using Sentry;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Security.Claims;
using System.Web.Http;
using static Fabic.Cloud.Controllers.AccountController;

[assembly: OwinStartup(typeof(Fabic.Cloud.Startup))]
namespace Fabic.Cloud
{
    public class Startup
    {
        public static IDisposable _sentry;
        public static MobileServiceContext Context;
        public static ExternalDatabase InternalDatabase;

        public void Configuration(IAppBuilder app)
        {
            // App code
            ConfigureMobileApp(app);
        }

        public static void ConfigureMobileApp(IAppBuilder app)
        {
            _sentry = SentrySdk.Init(o =>
            {
                o.Dsn = "https://8df39eeccd1e460388a3eccfc8eb339@o156929.ingest.sentry.io/1218148";
                // When configuring for the first time, to see what the SDK is doing:
                o.Debug = true;
                // Set traces_sample_rate to 1.0 to capture 100% of transactions for performance monitoring.
                // We recommend adjusting this value in production.
                o.TracesSampleRate = 1.0; //o.AddEntityFramework();
            });
            SentrySdk.CaptureMessage("Something went wrong");
            HttpConfiguration config = new HttpConfiguration();

            new MobileAppConfiguration()
                .AddMobileAppHomeController()             // from the Home package
                .MapApiControllers()
                .AddTables(                               // from the Tables package
                    new MobileAppTableConfiguration()
                        .MapTableControllers()
                        .AddEntityFramework()             // from the Entity package
                    )
                .MapLegacyCrossDomainController()         // from the CrossDomain package
                .ApplyTo(config);

            // Context: get it from Configuration.cs
            Context = new MobileServiceContext();
            Auth0Controller._SecurityController = new Auth0Controller();
            //ExceptionlessClient.Default.Startup("JuieKcexnsifRjCoIXGKjCAPDhK0y9RFur1mrJKU");

            var migrator = new DbMigrator(new Migrations.Configuration());
            migrator.Configuration.AutomaticMigrationsEnabled = false;//true;
                                                                      //migrator.Update();

            // Map routes by attribute
            config.MapHttpAttributeRoutes();

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MobileServiceInitializer());

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            //if (string.IsNullOrEmpty(settings.HostName))
            //{
            app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions()
            {
                //        // This middleware is intended to be used locally for debugging. By default, HostName will
                //        // only have a value when running in an App Service application.
                //        SigningKey = "",//ConfigurationManager.AppSettings["SigningKey"],
                //        ValidAudiences = new[] { /*ConfigurationManager.AppSettings["ValidAudience"]*/ "" },
                //        ValidIssuers = new[] { /*ConfigurationManager.AppSettings["ValidIssuer"]*/ "" },
                TokenHandler = new AppServiceTokenHandlerWithCustomClaims(config)
            });
            //}

            InternalDatabase = new ExternalDatabase();
            InternalDatabase.DBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MS_TableConnectionString"].ConnectionString;
            InternalDatabase.OpenDatabase(System.Configuration.ConfigurationManager.ConnectionStrings["MS_TableConnectionString"].ConnectionString);

            app.UseWebApi(config);
        }
    }

    public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
    {


    }

    public class AppServiceTokenHandlerWithCustomClaims : AppServiceTokenHandler
    {
        public AppServiceTokenHandlerWithCustomClaims(HttpConfiguration config)
            : base(config)
        {

        }

        public override bool TryValidateLoginToken(
            string token,
            string signingKey,
            IEnumerable<string> validAudiences,
            IEnumerable<string> validIssuers,
            out ClaimsPrincipal claimsPrincipal)
        {
            var validated = base.TryValidateLoginToken(token, signingKey, validAudiences, validIssuers, out claimsPrincipal);

            if (validated)
            {
                //// this is your custom role provider class which would lookup user roles by user id
                //var myRoleProvider = new MyRoleProvider();

                //// get user id (sid)
                //string sid = claimsPrincipal.With(u => u.FindFirst(ClaimTypes.NameIdentifier)).With(u => u.Value);

                //// get user roles (from database, for example)
                //var roles = myRoleProvider.GetUserRolesBySid(sid);
                //foreach (var role in roles)
                //{
                //    ((ClaimsIdentity)claimsPrincipal.Identity).AddClaim(new Claim(ClaimTypes.Role, role));
                //}
            }

            return validated;
        }
    }
}

