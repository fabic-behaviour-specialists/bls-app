//using Sentry;
using System;

namespace Fabic.Core.Helpers
{
    public static class ExceptionExtensions
    {
        public static void HandleBLSException(this Exception exception)
        {
            //SentrySdk.CaptureException(exception);
        }

        public static void HandleBLSLog(this string exception)
        {
            //SentrySdk.CaptureMessage(exception);
        }
    }
}
