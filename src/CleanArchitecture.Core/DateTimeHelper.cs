using System;
using TimeZoneConverter;

namespace CleanArchitecture.Core
{
    public static class DateTimeHelper
    {
        private static readonly TimeZoneInfo timeZoneInfo = TZConvert.GetTimeZoneInfo("Central Europe Standard Time");
        private static readonly DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime Now() => 
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);

        public static DateTime ConvertEpochToDate(long epochTime) =>
            TimeZoneInfo.ConvertTimeFromUtc(epochStart.AddSeconds(epochTime), timeZoneInfo);

        public static long ConvertDateToEpoch(DateTime date) => 
            (long)Math.Round((date.ToUniversalTime() -
                new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
