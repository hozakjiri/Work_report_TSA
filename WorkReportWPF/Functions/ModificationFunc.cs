using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using WorkReportWPF.Models;
using WorkReportWPF.Models.DBModels;

namespace WorkReportWPF.Functions
{
    public class ModificationFunc
    {

        public static IEnumerable LoadProjectList()
        {
            using DbSettingsContext context = new();
            var projectList = context.Projects.Select(x => x.Name).ToList();
            return projectList;
        }

        public static List<TableModificationView> LoadModificationTable()
        {
            using DbDataContext context = new();
            var projectList = context.Datas.OrderByDescending(o => o.ID).ToList();

            List<TableModificationView> modificationData = projectList.Select(x => new TableModificationView()
            {
                ID = x.ID,
                Username = x.Username,
                Date = (DateTime)ToDate(x.Date, "dd.MM.yyyy"),
                //Date = (DateTime)ToDate(x.Date, "M/d/yyyy h:mm:ss"),
                Project = x.Project,
                Comment = x.Comment,
                Image = x.Image == "yes" ? true : false,
                Time = x.Time,
            }).ToList();

            return modificationData;
        }

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

        public static void SaveModification(string project, string comment, string date, string minutes, string imagepath, string fulltime)
        {
            var user = LoginFunc.LoadUserData(Environment.UserName);

            try
            {
                using DbDataContext context = new();

                Data modificationData = new()
                {
                    Username = user.Name,
                    Date = date,
                    Project = project,
                    Comment = comment,
                    ImageStr = imagepath,
                    Time = minutes,
                    DateTimeStr = fulltime,
                };

                context.Datas.Add(modificationData);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
