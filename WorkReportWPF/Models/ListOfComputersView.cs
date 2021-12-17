using System;
using WorkReportWPF.Enums;

namespace WorkReportWPF.Models
{
    public class ListOfComputersView
    {
        public int StationID { get; set; }

        public string Line { get; set; }

        public string Name { get; set; }

        public string HostName { get; set; }

        public string Domain { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string PasswordVnc { get; set; }

        public StationEnum Type { get; set; }

        public DateTime? Maintenance { get; set; }

        public string Document { get; set; }

        public string Note { get; set; }

        public bool isVNC { get; set; }

    }
}
