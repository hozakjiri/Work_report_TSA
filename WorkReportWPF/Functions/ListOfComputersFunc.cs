using System;
using System.Collections.Generic;
using System.Linq;
using WorkReportWPF.Enums;
using WorkReportWPF.Models;

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
                Maintenance = OtherFunc.ToDate(x.Maintence, "dd.MM.yyyy") != null ? OtherFunc.ToDate(x.Maintence, "dd.MM.yyyy") : DateTime.MinValue,
                Note = x.Note,
                isVNC = x.isVNC
            }).ToList();

            return modificationData;
        }

        public static void AddComputers(string line, string name, string hostname, string domain, string user, string pass, string passvnc, int type, string maintenance, string note, bool isvnc)
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
                    Maintence = maintenance,
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

        public static void EditComputers(int ID, string line, string name, string hostname, string domain, string user, string pass, string passvnc, int type, string maintenance, string note, bool isvnc)
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
                    Maintence = maintenance,
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

    }
}
