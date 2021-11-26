using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
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

            List<TableTaskView> taskData = new();
            if (taskList != null)
            {
                taskData = taskList.Select(x => new TableTaskView()
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
            }

            return taskData;
        }

        public static List<TableTaskView> LoadTaskTableTOP()
        {
            using DbDataContext context = new();
            var taskList = context.Tasks.ToList();

            List<TableTaskView> taskData = new();
            if (taskList != null)
            {
                taskData = taskList.Select(x => new TableTaskView()
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
                }).OrderByDescending(o => o.Date).Take(5).ToList();
            }

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

        public static int LoadMyTask()
        {
            using DbDataContext context = new();
            var taskList = context.Tasks.ToList();
            var loginname = LoginFunc.LoadUserName(Environment.UserName);

            var countTask = 0;
            countTask = taskList.Where(x => x.Recipient == loginname && x.Status != (StatusEnum)StatusEnum.Started).Count();

            return countTask;
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
            Task modificationData = new();

            try
            {
                using DbDataContext context = new();

                modificationData = new()
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

            try
            {
                SendMail(modificationData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void EditTask(int ID, DateTime date, string subject, string description, int priority, int status, string sender, string recipient, DateTime term, string note)
        {
            try
            {
                using DbDataContext context = new();

                Task taskData = new()
                {
                    ID = ID,
                    Date = date.ToString("dd.MM.yyyy"),
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
                    context2.Tasks.Update(taskData);
                    context2.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void SendMail(Task data)
        {
            try
            {
                MailMessage oMail = new()
                {
                    From = new MailAddress("WorkReport@hella.com")
                };

                string mailstring = "";

                try
                {
                    mailstring = LoginFunc.LoadUserMail(data.Sender);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                oMail.To.Add(mailstring);

                oMail.IsBodyHtml = true;

                oMail.Subject = "WorkReport_TaskManager " + DateTime.Now.ToString("dd.MM.yyyy");

                StringBuilder sb = new();
                sb.AppendLine("<p>You get new job, </p>");
                sb.AppendLine("<br/>");

                StringBuilder sf = new();
                sf.AppendLine("<style>");
                sf.AppendLine("table, th, td {");
                sf.AppendLine("border: 1px solid black;");
                sf.AppendLine("border-collapse: collapse;");
                sf.AppendLine("}");
                sf.AppendLine("th, td {");
                sf.AppendLine("padding: 5px;");
                sf.AppendLine("Text-align: Left;");
                sf.AppendLine("}");
                sf.AppendLine("p {");
                sf.AppendLine("padding: 0px;");
                sf.AppendLine("Text-align: Left;");
                sf.AppendLine("margin: 0px;");
                sf.AppendLine("}");
                sf.AppendLine("</style>");


                var datatable = data;
                StringBuilder MailBody = new();

                MailBody.AppendLine("<table>");
                MailBody.AppendLine("<thead>");
                MailBody.AppendLine("<tr>");
                MailBody.AppendLine("<th>Sender</th>");
                MailBody.AppendLine("<th>Subject</th>");
                MailBody.AppendLine("<th>Description</th>");
                MailBody.AppendLine("<th>Priority</th>");
                MailBody.AppendLine("</tr>");
                MailBody.AppendLine("</thead>");
                MailBody.AppendLine("<tbody>");

                MailBody.AppendLine("<tr>");
                MailBody.AppendLine("<td>" + data.Sender + "</td>");
                MailBody.AppendLine("<td>" + data.Subject + "</td>");
                MailBody.AppendLine("<td>" + data.Description + "</td>");
                MailBody.AppendLine("<td>" + data.Priority + "</td>");
                MailBody.AppendLine("<td>" + data.Term + "</td>");
                MailBody.AppendLine("</tr>");

                MailBody.AppendLine("</tbody>");
                MailBody.AppendLine("</table>");

                oMail.Body = sf.ToString() + sb.ToString() + MailBody.ToString();


                SmtpClient smtp = new("smtphub.dc.hella.com");
                smtp.Port = 25;
                smtp.Send(oMail);
                //Data.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
