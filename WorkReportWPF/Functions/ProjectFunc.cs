using System;
using System.Collections.Generic;
using System.Linq;
using WorkReportWPF.Models.DBModels;

namespace WorkReportWPF.Functions
{
    public class ProjectFunc
    {

        public static List<Project> LoadProjectsData()
        {
            using DbSettingsContext context = new();
            List<Project> ProjectName = new();
            if (context != null)
            {
                ProjectName = context.Projects.ToList();
                return ProjectName;
            }
            else
            {
                return ProjectName;
            }
        }

        public static void SaveProject(string name)
        {

            try
            {
                using DbSettingsContext context = new();
                if (!string.IsNullOrEmpty(name))
                {
                    Project ProjectData = new()
                    {
                        Name = name,
                    };
                    context.Projects.Add(ProjectData);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteProject(int? id)
        {

            try
            {
                using DbSettingsContext context = new();

                if (id != null)
                {
                    var project = context.Projects.Find(id);
                    context.Projects.Remove(project);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void EditProject(Project data)
        {
            try
            {
                using DbSettingsContext context = new();
                var project = context.Projects.Find(data.ID);

                if (project != null)
                {
                    Project ProjectData = new()
                    {
                        Name = data.Name,
                    };
                    context.Projects.Update(ProjectData);
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
