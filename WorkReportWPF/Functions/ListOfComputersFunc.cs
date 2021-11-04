using System.Collections.Generic;
using System.Linq;
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
                Document = x.Document,
                Note = x.Note
            }).ToList();

            return modificationData;
        }
    }
}
