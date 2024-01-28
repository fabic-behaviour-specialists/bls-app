using System;

namespace BLS.Cloud.Helpers
{
    public static class DateTimeExtensions
    {
        public static string ToSQLFormat(this DateTimeOffset date)
        {
            return "CAST('" + date.Month.ToString() + "/" + date.Day.ToString() + "/" + date.Year.ToString() + " " + date.Hour.ToString() + ":" + date.Minute.ToString() + ":" + date.Second.ToString() + " +02:00' AS datetimeoffset(7))";
        }
    }
}
