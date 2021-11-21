using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using WorkReportWPF.Enums;
using WorkReportWPF.Models;
using WorkReportWPF.Models.DBModels;

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

        public static void AddTaskNew(string subject, string description, int priority, string sender, string recipient, DateTime term)
        {

            try
            {
                using DbDataContext context = new();

                Task modificationData = new()
                {
                    Date = DateTime.Now.ToString("dd.MM.yyyy"),
                    Subject = subject,
                    Description = description,
                    Note = "",
                    Priority = (priority >= 0 && priority <= 2) ? (TaskEnum)priority : 0,
                    Term = term.ToString("dd.MM.yyyy"),
                    Status = 0,
                    Sender = sender,
                    Recipient = recipient,
                };

                context.Tasks.Add(modificationData);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void EditTask(int ID, string subject, string description, int priority, int status, string sender, string recipient, DateTime term, string note)
        {
            try
            {
                using DbDataContext context = new();

                Task modificationData = new()
                {
                    ID = ID,
                    Subject = subject,
                    Description = description,
                    Note = note,
                    Priority = (priority >= 0 && priority <= 2) ? (TaskEnum)priority : 0,
                    Term = term.ToString("dd.MM.yyyy"),
                    Status = (status >= 0 && status <= 2) ? (StatusEnum)status : 0,
                    Sender = sender,
                    Recipient = recipient,
                };

                using (var context2 = new DbDataContext())
                {
                    context2.Update(modificationData);
                    context2.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


    }
}
