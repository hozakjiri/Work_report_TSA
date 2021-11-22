using System;
using System.Collections.Generic;
using System.Linq;
using WorkReportWPF.Enums;
using WorkReportWPF.Models;

namespace WorkReportWPF.Functions
{
    public class ListOfComputersFunc
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
                Maintence = x.Maintence,
                Note = x.Note
            }).ToList();

            return modificationData;
        }

        public static void AddComputers(string line, string name, string hostname, string domain, string user, string pass, string passvnc, int type, string maintence, string note)
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
                    Maintence = maintence,
                    Note = note,
                };

                context.Stations.Add(stationData);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void EditComputers(int ID, string line, string name, string hostname, string domain, string user, string pass, string passvnc, int type, string maintence, string note)
        {


        }

    }
}
