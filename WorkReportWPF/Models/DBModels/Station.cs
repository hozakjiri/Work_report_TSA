using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkReportWPF.Enums;

namespace WorkReportWPF.Models
{
    [Table("Station")]
    public class Station
    {
        [Key]
        public int StationID { get; set; }

        public string Line { get; set; }

        public string Name { get; set; }

        public string HostName { get; set; }

        public string Domain { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string PasswordVnc { get; set; }

        public StationEnum Type { get; set; }

        public string Maintence { get; set; }

        public string Note { get; set; }

        public bool isVNC { get; set; }
    }
}
