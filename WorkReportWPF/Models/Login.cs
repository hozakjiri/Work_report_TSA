using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkReportWPF.Enums;

namespace WorkReportWPF.Models
{
    [Table("Login")]
    public class Login
    {

        [Key]
        public int UserID { get; set; }

        public string Name { get; set; }

        public string UserLogin { get; set; }

        public string Mail { get; set; }

        public LevelEnum Level { get; set; }
    }
}
