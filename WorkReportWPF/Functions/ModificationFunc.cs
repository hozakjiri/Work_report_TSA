using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows;
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
                Date = (DateTime)OtherFunc.ToDate(x.Date, "dd.MM.yyyy"),
                //Date = (DateTime)ToDate(x.Date, "M/d/yyyy h:mm:ss"),
                Project = x.Project,
                Comment = x.Comment,
                Image = x.Image == "yes" ? true : false,
                Time = x.Time,
                Note = x.Note,
                ImagePath = x.ImageStr
            }).ToList();

            return modificationData;
        }

        public static List<TableModificationView> LoadModificationTableTOP()
        {
            using DbDataContext context = new();
            var projectList = context.Datas.OrderByDescending(o => o.ID).ToList();

            List<TableModificationView> modificationData = projectList.Select(x => new TableModificationView()
            {
                ID = x.ID,
                Username = x.Username,
                Date = (DateTime)OtherFunc.ToDate(x.Date, "dd.MM.yyyy"),
                //Date = (DateTime)ToDate(x.Date, "M/d/yyyy h:mm:ss"),
                Project = x.Project,
                Comment = x.Comment,
                Image = x.Image == "yes" ? true : false,
                Time = x.Time,
                Note = x.Note,
                ImagePath = x.ImageStr
            }).OrderByDescending(o => o.ID).Take(5).ToList();

            return modificationData;
        }

        public static List<TableModificationView> LoadUserModificationTable()
        {
            using DbDataContext context = new();

            var name = LoginFunc.LoadUserName(Environment.UserName);
            var date = DateTime.Now.ToString("dd.MM.yyyy");

            var projectList = context.Datas.Where(x => x.Username == name && x.Date == date).ToList();

            List<TableModificationView> modificationData = projectList.Select(x => new TableModificationView()
            {
                ID = x.ID,
                Username = x.Username,
                Date = (DateTime)OtherFunc.ToDate(x.Date, "dd.MM.yyyy"),
                Project = x.Project,
                Comment = x.Comment,
                Image = x.Image == "yes" ? true : false,
                Time = x.Time,
                Note = x.Note,
                ImagePath = x.ImageStr
            }).ToList();

            return modificationData;
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
                    Note = ""
                };

                context.Datas.Add(modificationData);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void EditModification(string project, string comment, string date, string minutes, string imagepath, string fulltime, string note)
        {
            try
            {
                Data modificationData = new()
                {
                    Username = OtherFunc.user,
                    Date = date,
                    Project = project,
                    Comment = comment,
                    ImageStr = imagepath,
                    Time = minutes,
                    DateTimeStr = fulltime,
                    Note = note
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

        public static void SendMail(List<TableModificationView> data, string Shift, string mailTo)
        {
            try
            {
                MailMessage oMail = new()
                {
                    From = new MailAddress("WorkReport@hella.com")
                };
                //oMail.To.Add(mailTo);
                oMail.To.Add("jizi.hozak@hella.com");

                oMail.IsBodyHtml = true;

                oMail.Subject = "WorkReport " + DateTime.Now.ToString("dd.MM.yyyy") + " - " + Shift;

                //var Data = new Attachment(attachment, MediaTypeNames.Application.Octet);
                //if (!string.IsNullOrEmpty(attachment))
                //{
                //    // '// Add time stamp information for the file.
                //    ContentDisposition disposition1 = Data.ContentDisposition;
                //    disposition1.CreationDate = File.GetCreationTime(attachment);
                //    disposition1.ModificationDate = File.GetLastWriteTime(attachment);
                //    disposition1.ReadDate = File.GetLastAccessTime(attachment);
                //    // '// Add the file attachment to this e-mail message.
                //    oMail.Attachments.Add(Data);
                //}

                StringBuilder sb = new();
                sb.AppendLine("<p>Ahojte, </p>");
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
                MailBody.AppendLine("<th>Date</th>");
                MailBody.AppendLine("<th>Username</th>");
                MailBody.AppendLine("<th>Project</th>");
                MailBody.AppendLine("<th>Comment</th>");
                MailBody.AppendLine("<th>Time</th>");
                MailBody.AppendLine("<th>Note</th>");
                MailBody.AppendLine("</tr>");
                MailBody.AppendLine("</thead>");
                MailBody.AppendLine("<tbody>");


                foreach (var line in data)
                {
                    MailBody.AppendLine("<tr>");
                    MailBody.AppendLine("<td>" + line.Date + "</td>");
                    MailBody.AppendLine("<td>" + line.Username + "</td>");
                    MailBody.AppendLine("<td>" + line.Project + "</td>");
                    MailBody.AppendLine("<td>" + line.Comment + "</td>");
                    MailBody.AppendLine("<td>" + line.Time + "</td>");
                    MailBody.AppendLine("<td>" + line.Note + "</td>");
                    MailBody.AppendLine("</tr>");
                }

                MailBody.AppendLine("</tbody>");
                MailBody.AppendLine("</table>");

                oMail.Body = sf.ToString() + sb.ToString() + MailBody.ToString();


                SmtpClient smtp = new("smtphub.dc.hella.com");
                smtp.Port = 25;
                MessageBox.Show("Email send", "Mail");
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
