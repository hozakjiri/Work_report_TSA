using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WorkReportWPF.Models;
using WorkReportWPF.Models.DBModels;

namespace WorkReportWPF.Functions
{
    public class ModificationFunc
    {

        public static IEnumerable LoadProjectList()
        {
            using (DbSettingsContext context = new DbSettingsContext())
            {
                var projectList = context.Projects.Select(x => x.Name).ToList();
                return projectList;
            }
        }

        public static List<TableModificationView> LoadModificationTable()
        {
            using (DbDataContext context = new DbDataContext())
            {
                var projectList = context.Datas.OrderByDescending(o => o.ID).ToList();

                List<TableModificationView> modificationData = projectList.Select(x => new TableModificationView()
                {
                    Username = x.Username,
                    Date = x.Date,
                    Project = x.Project,
                    Comment = x.Comment,
                    Image = x.Image,
                    Time = x.Time,
                }).ToList();

                return modificationData;
            }
        }

        public static void SaveModification(string project, string comment, string date, string minutes, string imagepath, string fulltime)
        {
            var user = LoginFunc.LoadUserData(Environment.UserName);

            try
            {
                using (DbDataContext context = new DbDataContext())
                {

                    Data modificationData = new Data()
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
