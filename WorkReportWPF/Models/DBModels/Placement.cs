using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkReportWPF.Models.DBModels
{
    [Table("Sample")]
    public class Placement
    {

        [Key]
        public int ID { get; set; }

        public string Place { get; set; }

        public string Sector { get; set; }

    }
}
