using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkReportWPF.Models.DBModels
{
    [Table("Task")]
    public class Task
    {

        [Key]
        public int ID { get; set; }

        public string Date { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public Enums.TaskEnum Priority { get; set; }

        public string Term { get; set; }

        public Enums.StatusEnum Status { get; set; }

        public string Sender { get; set; }

        public string Recipient { get; set; }

    }
}