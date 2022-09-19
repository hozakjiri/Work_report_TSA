using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml.Linq;
using Windows.System;
using WorkReportWPF.Models;
using WorkReportWPF.Models.DBModels;
using Outlook = Microsoft.Office.Interop.Outlook;
using DDay.iCal.Serialization.iCalendar;
using DDay.iCal;
using System.IO;

namespace WorkReportWPF.Functions
{
    public static class SamplesFunc
    {
        public static List<TableSampleView> LoadSamplesTable()
        {
            using DbDataContext context = new();
            var sampleList = context.Samples.OrderByDescending(o => o.ID).ToList();

            List<TableSampleView> sampleData = sampleList.Select(x => new TableSampleView()
            {
                ID = x.ID,
                Project = x.Project,
                Name = x.Name,
                Description = x.Description,
                Placement = x.Placement,
                Responsible = x.Responsible,
                RevisionDate = (DateTime)OtherFunc.ToDate(x.RevisionDate, "dd.MM.yyyy"),
                RevisionValidity = (DateTime)OtherFunc.ToDate(x.RevisionValidity, "dd.MM.yyyy"),
                Folder = x.Folder,
            }).ToList();

            return sampleData;
        }

        public static void SendMail(string project, string name, string description, string placement, string responsible, DateTime revisionDate, DateTime revisionValidity, string label, string folder)
        {
            Sample sampleData = new()
            {
                Project = project,
                Name = name,
                Description = description,
                Placement = placement,
                Responsible = responsible,
                RevisionDate = revisionDate.ToString("dd.MM.yyyy"),
                RevisionValidity = revisionValidity.ToString("dd.MM.yyyy"),
                Label = label,
                Folder = folder
            };

            try
            {
                MailMessage oMail = new()
                {
                    From = new MailAddress("WorkReport@hella.com")
                };

                string mailstring = "";

                try
                {
                    mailstring = LoginFunc.LoadUserMail(sampleData.Responsible);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                oMail.To.Add(mailstring);

                oMail.IsBodyHtml = true;

                oMail.Subject = "WorkReport_TaskManager " + DateTime.Now.ToString("dd.MM.yyyy");

                oMail.Priority = MailPriority.High;

                StringBuilder sb = new();
                sb.AppendLine("<p>You get new sample, </p>");
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


                var datatable = sampleData;
                StringBuilder MailBody = new();

                MailBody.AppendLine("<table>");
                MailBody.AppendLine("<thead>");
                MailBody.AppendLine("<tr>");
                MailBody.AppendLine("<th>Project</th>");
                MailBody.AppendLine("<th>Name</th>");
                MailBody.AppendLine("<th>Description</th>");
                MailBody.AppendLine("<th>RevisionDate</th>");
                MailBody.AppendLine("<th>RevisionValidity</th>");
                MailBody.AppendLine("</tr>");
                MailBody.AppendLine("</thead>");
                MailBody.AppendLine("<tbody>");

                MailBody.AppendLine("<tr>");
                MailBody.AppendLine("<td>" + sampleData.Project + "</td>");
                MailBody.AppendLine("<td>" + sampleData.Name + "</td>");
                MailBody.AppendLine("<td>" + sampleData.Description + "</td>");
                MailBody.AppendLine("<td>" + sampleData.Placement + "</td>");
                MailBody.AppendLine("<td>" + sampleData.RevisionDate + "</td>");
                MailBody.AppendLine("<td>" + sampleData.RevisionValidity + "</td>");
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

        private static byte[] ReadBinary(string fileName)
        {
            byte[] binaryData = null;
            using (FileStream reader = new FileStream(fileName,
              FileMode.Open, FileAccess.Read))
            {
                binaryData = new byte[reader.Length];
                reader.Read(binaryData, 0, (int)reader.Length);
            }
            return binaryData;
        }
        public static void Test()
        {
            const string filepath = @"C:\temp\ical.test.ics";
            // use PUBLISH for appointments
            // use REQUEST for meeting requests
            const string METHOD = "REQUEST";

            // Properties of the meeting request
            // keep guid in sending program to modify or cancel the request later
            Guid uid = Guid.Parse("2B127C67-73B3-43C5-A804-5666C2CA23C9");
            string VisBetreff = "This is the subject of the meeting request";
            string TerminVerantwortlicherEmail = "peter.hajduch@hella.com";
            string bodyPlainText = "This is the simple iCal plain text msg";
            string bodyHtml = "This is the simple <b>iCal HTML message</b>";
            string location = "Meeting room 101";
            // 1: High
            // 5: Normal
            // 9: low
            int priority = 1;
            //=====================================
            MailMessage message = new MailMessage();

            message.From = new MailAddress("sender@myorg.com");
            message.To.Add(new MailAddress(TerminVerantwortlicherEmail));
            message.Subject = "[VIS-Termin] " + VisBetreff;

            // Plain Text Version
            message.Body = bodyPlainText;

            // HTML Version
            string htmlBody = bodyHtml;
            AlternateView HTMLV = AlternateView.CreateAlternateViewFromString(htmlBody,
              new System.Net.Mime.ContentType("text/html"));

            // iCal
            IICalendar iCal = new iCalendar();
            iCal.Method = METHOD;
            iCal.ProductID = "My Metting Product";

            // Create an event and attach it to the iCalendar.
            Event evt = iCal.Create<Event>();
            evt.UID = uid.ToString();

            evt.Class = "PUBLIC";
            // Needed by Outlook
            evt.Created = new iCalDateTime(DateTime.Now);

            evt.DTStamp = new iCalDateTime(DateTime.Now);
            evt.Transparency = TransparencyType.Transparent;

            // Set the event start / end times
            evt.Start = new iCalDateTime(2014, 10, 3, 8, 0, 0);
            evt.End = new iCalDateTime(2014, 10, 3, 8, 15, 0);
            evt.Location = location;

            //var organizer = new Organizer("the.organizer@myCompany.com");
            //evt.Organizer = organizer;

            // Set the longer description of the event, plain text
            evt.Description = bodyPlainText;

            // Event description HTML text
            // X-ALT-DESC;FMTTYPE=text/html
            var prop = new CalendarProperty("X-ALT-DESC");
            prop.AddParameter("FMTTYPE", "text/html");
            prop.AddValue(bodyHtml);
            evt.AddProperty(prop);

            // Set the one-line summary of the event
            evt.Summary = VisBetreff;
            evt.Priority = priority;

            //--- attendes are optional
            IAttendee at = new Attendee("mailto:jiri.hozak@hella.com");
            at.ParticipationStatus = "NEEDS-ACTION";
            at.RSVP = true;
            at.Role = "REQ-PARTICIPANT";
            evt.Attendees.Add(at);

            // Let’s also add an alarm on this event so we can be reminded of it later.
            Alarm alarm = new Alarm();

            // Display the alarm somewhere on the screen.
            alarm.Action = AlarmAction.Display;

            // This is the text that will be displayed for the alarm.
            alarm.Summary = "Upcoming meeting: " + VisBetreff;

            // The alarm is set to occur 30 minutes before the event
            alarm.Trigger = new Trigger(TimeSpan.FromMinutes(-30));

            //--- Attachments
            //string filename = "Test.docx";

            // Add an attachment to this event
            //IAttachment attachment = new DDay.iCal.Attachment();
            //attachment.Data = ReadBinary(@"C:\temp\Test.docx");
            //attachment.Parameters.Add("X-FILENAME", filename);
            //evt.Attachments.Add(attachment);

            iCalendarSerializer serializer = new iCalendarSerializer();
            serializer.Serialize(iCal, filepath);

            // the .ics File as a string
            string iCalStr = serializer.SerializeToString(iCal);

            // .ics as AlternateView (used by Outlook)
            // text/calendar part: method=REQUEST
            System.Net.Mime.ContentType calendarType =
              new System.Net.Mime.ContentType("text/calendar");
            calendarType.Parameters.Add("method", METHOD);
            AlternateView ICSview =
              AlternateView.CreateAlternateViewFromString(iCalStr, calendarType);

            // Compose
            message.AlternateViews.Add(HTMLV);
            message.AlternateViews.Add(ICSview); // must be the last part

            // .ics as Attachment (used by mail clients other than Outlook)
            Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(iCalStr);
            var ms = new System.IO.MemoryStream(bytes);
            var a = new System.Net.Mail.Attachment(ms,
              "VIS-Termin.ics", "text/calendar");
            message.Attachments.Add(a);

            // Send Mail
            SmtpClient client = new SmtpClient();
            client.Send(message);
        }

        public static List<TableUpcomming> LoadSamplesTableWithComputers()
        {
            using DbDataContext contextData = new();
            using DbSettingsContext contextSettings = new();

            var sampleList = contextData.Samples.ToList();


            List<TableUpcomming> sampleList2 = sampleList.Select(x => new TableUpcomming()
            {
                ID = x.ID,
                Project = x.Project,
                Name = x.Name,
                Responsible = x.Responsible,
                RevisionDate = x.RevisionValidity != null ? (DateTime)OtherFunc.ToDate(x.RevisionValidity, "dd.MM.yyyy") : DateTime.MinValue,
                Type = "Headlamp",
            }).ToList();

            var ComputersList = contextSettings.Stations.ToList();

            List <TableUpcomming> ComputersList2 = ComputersList.Select(x => new TableUpcomming()
            {
                ID = x.StationID,
                Project = x.Line,
                Name = x.Name,
                Responsible = "Team",
                RevisionDate = x.RevisionValidity != null ? (DateTime)OtherFunc.ToDate(x.RevisionValidity, "dd.MM.yyyy") : DateTime.MinValue,
                Type = "Machine",
            }).ToList();

            List<TableUpcomming> unionlist = sampleList2.Union(ComputersList2).ToList();

            List<TableUpcomming> result = unionlist.Where(x => x.RevisionDate >= DateTime.Now.AddMonths(-1)).Select(x => new TableUpcomming()
            {
                ID = x.ID,
                Project = x.Project,
                Name = x.Name,
                Responsible = x.Responsible,
                RevisionDate = x.RevisionDate,
                Type = x.Type,
            }).OrderByDescending(o => o.RevisionDate).ThenBy(z => z.Name).ToList();

            return result;
        }

        public static void AddSample(string project, string name, string description, string placement, string responsible, DateTime revisionDate, DateTime revisionValidity, string label, string folder)
        {

            try
            {
                using DbDataContext context = new();

                Sample sampleData = new()
                {
                    Project = project,
                    Name = name,
                    Description = description,
                    Placement = placement,
                    Responsible = responsible,
                    RevisionDate = revisionDate.ToString("dd.MM.yyyy"),
                    RevisionValidity = revisionValidity.ToString("dd.MM.yyyy"),
                    Label = label,
                    Folder = folder
                };

                context.Samples.Add(sampleData);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void EditSample(int ID, string project, string name, string description, string placement, string responsible, DateTime revisionDate, DateTime revisionValidity, string label, string folder)
        {

            try
            {
                Sample sampleData = new()
                {
                    ID = ID,
                    Project = project,
                    Name = name,
                    Description = description,
                    Placement = placement,
                    Responsible = responsible,
                    RevisionDate = revisionDate.ToString("dd.MM.yyyy"),
                    RevisionValidity = revisionValidity.ToString("dd.MM.yyyy"),
                    Label = label,
                    Folder = folder
                };


                using (var context2 = new DbDataContext())
                {
                    context2.Samples.Update(sampleData);
                    context2.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CreateTask(string Mail, DateTime FromDate, DateTime ToDate, string Subject, string Body)
        {
            try
            {
                Outlook.Application oApp = new();
                Outlook.NameSpace oNS = oApp.GetNamespace("mapi");
                oNS.Logon("Outlook", Missing.Value, false, true);
                Outlook.TaskItem OTask = oApp.CreateItem(Outlook.OlItemType.olTaskItem);

                OTask.Assign();
                // Add recipients to the task
                OTask.StatusReport();
                OTask.Recipients.Add(Mail);
                // Add the subject to the task
                OTask.Subject = Subject;
                // Add the body to the task
                OTask.Body = Body;
                // Add the Task duedate
                OTask.DueDate = ToDate;
                // Set the reminder to the task
                OTask.ReminderSet = true;
                // Set the reminder time
                OTask.DateCompleted = ToDate;
                OTask.ReminderTime = OTask.DueDate;
                // If you want to display the task uncomment the next line
                OTask.PercentComplete = 0;
                OTask.StartDate = FromDate;
                // Save the task to outlook
                OTask.Importance = Outlook.OlImportance.olImportanceHigh;
                OTask.ReminderTime.AddDays(-3);

                // Send the task
                // OTask.Display(True)
                OTask.Save();

                oNS.Logoff();
                oApp = default;
                oNS = default;
            }
            // OTask.Send()

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void DeleteSample(int? id)
        {
            try
            {
                using DbDataContext context = new();

                if (id != null)
                {
                    var sample = context.Samples.Find(id);
                    context.Samples.Remove(sample);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }


}
