using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WorkReportWPF.Enums;
using WorkReportWPF.Models;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace WorkReportWPF.Functions
{
    public static class ListOfComputersFunc
    {
        public static List<ListOfComputersView> LoadListOfComputersTable()
        {
            using DbSettingsContext context = new();
            var stationList = context.Stations.OrderByDescending(o => o.StationID).ToList();

            List<ListOfComputersView> modificationData = stationList.Select(x => new ListOfComputersView()
            {
                StationID = x.StationID,
                Line = x.Line,
                Name = x.Name,
                HostName = x.HostName,
                Domain = x.Domain,
                User = x.User,
                Password = x.Password,
                PasswordVnc = x.PasswordVnc,
                Type = x.Type,
                RevisionDate = OtherFunc.ToDate(x.RevisionDate, "dd.MM.yyyy") != null ? OtherFunc.ToDate(x.RevisionDate, "dd.MM.yyyy") : DateTime.MinValue,
                RevisionValidity = OtherFunc.ToDate(x.RevisionValidity, "dd.MM.yyyy") != null ? OtherFunc.ToDate(x.RevisionValidity, "dd.MM.yyyy") : DateTime.MinValue,
                Note = x.Note,
                isVNC = x.isVNC
            }).ToList();

            return modificationData;
        }

        public static void AddComputers(string line, string name, string hostname, string domain, string user, string pass, string passvnc, int type, string note, bool isvnc, string revisiondate, string revisionvalidity)
        {

            try
            {
                using DbSettingsContext context = new();

                Station stationData = new()
                {
                    Line = line,
                    Name = name,
                    HostName = hostname,
                    Domain = domain,
                    User = user,
                    Password = pass,
                    PasswordVnc = passvnc,
                    Type = (type >= 0 && type <= 1) ? (StationEnum)type : 0,
                    RevisionDate = revisiondate,
                    RevisionValidity = revisionvalidity,
                    Note = note,
                    isVNC = isvnc
                };

                context.Stations.Add(stationData);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void EditComputers(int ID, string line, string name, string hostname, string domain, string user, string pass, string passvnc, int type, string note, bool isvnc, string revisiondate, string revisionvalidity)
        {

            try
            {
                Station stationData = new()
                {
                    StationID = ID,
                    Line = line,
                    Name = name,
                    HostName = hostname,
                    Domain = domain,
                    User = user,
                    Password = pass,
                    PasswordVnc = passvnc,
                    Type = (type >= 0 && type <= 1) ? (StationEnum)type : 0,
                    RevisionDate = revisiondate,
                    RevisionValidity = revisionvalidity,
                    Note = note,
                    isVNC = isvnc
                };


                using (var context2 = new DbSettingsContext())
                {
                    context2.Stations.Update(stationData);
                    context2.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteComputers(int? id)
        {
            try
            {
                using DbSettingsContext context = new();

                if (id != null)
                {
                    var customer = context.Stations.Find(id);
                    context.Stations.Remove(customer);
                    context.SaveChanges();
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
                Microsoft.Office.Interop.Outlook.Application oApp = new();
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
                OTask.Importance = Outlook.OlImportance.olImportanceHigh;
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

    }
}
