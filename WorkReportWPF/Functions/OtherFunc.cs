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


        public static string getReplaceString(string datastring, string pomlcka, string ciarka, string opacnapomlcka, string replacechar)
        {
            var data = datastring;

            if (data.Contains(pomlcka))
            {
                data = data.Replace(pomlcka, replacechar);
            }

            else if (data.Contains(ciarka))
            {
                data = data.Replace(ciarka, replacechar);
            }


            if (data.Contains(opacnapomlcka))
            {
                data = data.Replace(opacnapomlcka, replacechar);
            }

            return data;
        }

        //public static string getFolderPath()
        //{
        //    var dlg = new FolderBrowserForWPF.Dialog();
        //    dlg.Title = "Choose Sample Folder";

        //    if (dlg.ShowDialog() == true)
        //    {
        //        return dlg.FileName;
        //    }

        //    return string.Empty;
        //}
    }
}
