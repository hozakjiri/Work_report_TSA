using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkReportWPF.Models.DBModels
{
    [Table("Project")]
    public class Project
    {

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

    }
}
