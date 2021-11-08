using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkReportWPF.Models.DBModels
{
    [Table("Members")]
    public class Member
    {

        [Key]
        public int ID { get; set; }

        public string Mail { get; set; }

    }
}