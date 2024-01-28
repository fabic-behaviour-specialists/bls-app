namespace BLS.Cloud.Helpers
{
    public static class BoolExtensions
    {
        /// <summary>
        /// Returns 1 for true and 0 for false
        /// </summary>
        /// <returns></returns>
        public static int ToSQLFormat(this bool boolean)
        {
            if (boolean)
            {
                return 1;
            }

            return 0;
        }
    }
}
