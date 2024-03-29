﻿using System;
using TimeZoneConverter;

namespace BjBygg.Core
{
    public static class DateTimeHelper
    {
        private static readonly TimeZoneInfo localTimeZoneInfo = TZConvert.GetTimeZoneInfo("Central Europe Standard Time");
        private static readonly DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime Now() => DateTime.UtcNow;
        public static DateTime NowLocalTime() => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZoneInfo);
        public static DateTime ConvertEpochToDate(long epochTime) =>
            epochStart.AddSeconds(epochTime);

        public static long ConvertDateToEpoch(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() -
                new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public static DateTime ConvertToLocalTime(DateTime date) =>
            TimeZoneInfo.ConvertTime(date, localTimeZoneInfo);

    }
}
