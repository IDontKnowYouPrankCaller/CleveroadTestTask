namespace CleveroadTestProject.Infrastructure.Extensions
{
    #region namespaces
    using System;
    #endregion

    public static class DateTimeHelper
    {
        public static long ToUnix(this DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);
        }
    }
}
