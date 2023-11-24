using Sentry;
using UIKit;

namespace Fabic.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            using (SentrySdk.Init(o =>
            {
                o.Dsn = "https://7cf001303afb4d4f9835a2c5789fb473@o459916.ingest.sentry.io/5459571";
                // When configuring for the first time, to see what the SDK is doing:
                o.Debug = true;
                // Set traces_sample_rate to 1.0 to capture 100% of transactions for performance monitoring.
                // We recommend adjusting this value in production.
                o.TracesSampleRate = 1.0;
            }))
            {
                // App code
                UIApplication.Main(args, null, "AppDelegate");
            }
        }
    }
}