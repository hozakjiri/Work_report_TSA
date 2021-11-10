using System;
using System.ComponentModel.DataAnnotations;

namespace WorkReportWPF.Models
{
    public class TableModificationView
    {
        public int ID { get; set; }

        public string Username { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        public string Project { get; set; }

        public string Comment { get; set; }

        public string Time { get; set; }

        public bool Image { get; set; }

        public string Note { get; set; }

        public string ImagePath { get; set; }

    }
}
