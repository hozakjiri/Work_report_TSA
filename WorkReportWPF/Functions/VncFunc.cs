using System;
using System.Collections.Generic;
using System.Linq;
using WorkReportWPF.Models;

namespace WorkReportWPF.Functions
{
    public class VncFunc
    {
        public static List<Station> GetAllStations()
        {
            List<Station> AllStations = new();
            using (DbSettingsContext data = new())
            {
                AllStations = data.Stations.Where(x => x.isVNC == true && x.HostName != null).ToList();
            }

            if (AllStations.Count > 0)
            {
                return AllStations.OrderBy(o => o.Name).ThenBy(d => d.HostName).ToList();
            }
            else
            {
                return new List<Station>();
            }

        }

        public static string GetPassword(string hostname)
        {
            string Password = "";
            using (DbSettingsContext data = new())
            {
                Password = data.Stations.Where(a => a.HostName.Contains(hostname)).Select(a => a.PasswordVnc).FirstOrDefault();
            }

            if (!string.IsNullOrEmpty(Password))
            {
                return Password;
            }
            else
            {
                return "";
            }

        }

        public static void SaveVNC(string line, string name, string hostname, string domain, string user, string pass, string passvnc)
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
                    Type = Enums.StationEnum.BlackBox,
                    Maintence = "",
                    Note = "",
                    isVNC = true
                };

                context.Stations.Add(stationData);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
