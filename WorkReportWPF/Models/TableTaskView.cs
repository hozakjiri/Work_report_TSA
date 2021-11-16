using System;

namespace WorkReportWPF.Models
{
    public class TableTaskView
    {

        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public Enums.TaskEnum Priority { get; set; }

        public DateTime Term { get; set; }

        public Enums.StatusEnum Status { get; set; }

        public string Sender { get; set; }

        public string Recipient { get; set; }

    }
}
