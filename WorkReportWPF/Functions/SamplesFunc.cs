using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WorkReportWPF.Models;
using WorkReportWPF.Models.DBModels;
using Outlook = Microsoft.Office.Interop.Outlook;

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
                Folder = x.Folder
            }).ToList();

            return sampleData;
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
