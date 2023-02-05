using System;
using System.Collections.Generic;
using System.Text;
using TimeZoneConverter;

namespace SyncingSyllabi.Common.Tools.Helpers
{
    public class TimeZoneHelper
    {
        public static DateTime ConvertToUserTimeZone(DateTime dateTime, string timeZone)
        {
            string tz = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(timeZone))
                {
                    tz = "America/Los_Angeles";
                }
                else
                {
                    tz = timeZone;
                }

                TimeZoneInfo timeZoneInfo = TZConvert.GetTimeZoneInfo(tz);
                DateTime userDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZoneInfo);

                return userDateTime;
            }
            catch
            {

                TimeZoneInfo timeZoneInfo = TZConvert.GetTimeZoneInfo("America/Los_Angeles");
                DateTime userDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZoneInfo);

                return userDateTime;
            }
        }
    }
}
