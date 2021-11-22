using System;
using System.Globalization;
using System.Threading;

namespace WorkReportWPF.Functions
{
    public class OtherFunc
    {
        public static string user = LoginFunc.LoadUserName(Environment.UserName);

        public static DateTime? ToDate(string dateTimeStr, params string[] dateFmt)
        {
            const DateTimeStyles style = DateTimeStyles.AllowWhiteSpaces;

            if (dateFmt == null)
            {
                var dateInfo = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
                dateFmt = dateInfo.GetAllDateTimePatterns();
            }
            DateTime dt = default;
            var result = DateTime.TryParseExact(dateTimeStr, dateFmt, CultureInfo.InvariantCulture, style, out dt) ? dt : (DateTime?)default;
            return result;
        }

    }
}
