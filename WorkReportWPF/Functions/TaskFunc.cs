using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using WorkReportWPF.Models;

namespace WorkReportWPF.Functions
{
    public class TaskFunc
    {
        public static List<TableTaskView> LoadTaskTable()
        {
            using DbDataContext context = new();
            var taskList = context.Tasks.ToList();

            List<TableTaskView> taskData = taskList.Select(x => new TableTaskView()
            {
                ID = x.ID,
                Date = (DateTime)ToDate(x.Date, "dd.MM.yyyy"),
                Subject = x.Subject,
                Description = x.Description,
                Note = x.Note,
                Priority = x.Priority,
                Term = (DateTime)ToDate(x.Term, "dd.MM.yyyy"),
                Status = x.Status,
                Sender = x.Sender,
                Recipient = x.Recipient,
            }).ToList();

            return taskData;
        }

        public static List<TableTaskView> LoadMyTaskTable()
        {
            using DbDataContext context = new();
            var taskList = context.Tasks.ToList();

            var loginname = LoginFunc.LoadUserName(Environment.UserName);

            List<TableTaskView> taskData = taskList.Where(d => d.Recipient == loginname || d.Sender == loginname).Select(x => new TableTaskView()
            {
                ID = x.ID,
                Date = (DateTime)ToDate(x.Date, "dd.MM.yyyy"),
                Subject = x.Subject,
                Description = x.Description,
                Note = x.Note,
                Priority = x.Priority,
                Term = (DateTime)ToDate(x.Term, "dd.MM.yyyy"),
                Status = x.Status,
                Sender = x.Sender,
                Recipient = x.Recipient,
            }).ToList();

            return taskData;
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

    }
}
