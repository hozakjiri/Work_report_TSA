using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkReportWPF.Models.DBModels
{
    [Table("Data")]
    public class Data
    {

        [Key]
        public int ID { get; set; }

        public string Username { get; set; }

        public string Date { get; set; }

        public string Project { get; set; }

        public string Comment { get; set; }

        public string ImageStr { get; set; }

        public string Time { get; set; }

        public string DateTimeStr { get; set; }

        [NotMapped]
        public string Image
        {
            get
            {
                return (!string.IsNullOrEmpty(ImageStr)) ? "yes" : "no";
            }
        }

    }
}
