using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkReportWPF.Models.DBModels
{
    [Table("Sample")]
    public class Sample
    {

        [Key]
        public int ID { get; set; }

        public string Project { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Placement { get; set; }

        public string Responsible { get; set; }

        public string RevisionValidity { get; set; }

        public string RevisionDate { get; set; }

    }
}
