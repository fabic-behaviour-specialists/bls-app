using Sentry;
using System;

namespace BLS.Cloud.Helpers
{
    public static class ExceptionExtensions
    {
        public static void HandleBLSException(this Exception exception)
        {
            SentrySdk.CaptureException(exception);
        }
    }
}
