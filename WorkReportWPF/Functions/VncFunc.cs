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
                AllStations = data.Stations.ToList();
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
    }
}
