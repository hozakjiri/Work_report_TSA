using System.Collections.Generic;
using System.Linq;
using WorkReportWPF.Models;


namespace WorkReportWPF.Functions
{
    public class VncFunc
    {
        public static List<Station> GetAllStations()
        {
            List<Station> AllStations = new List<Station>();
            using (DbSettingsContext data = new DbSettingsContext())
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
    }
}
