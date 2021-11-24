using System;

namespace WorkReportWPF.Models
{
    public class TableSampleView
    {
        public int ID { get; set; }

        public string Project { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Placement { get; set; }

        public string Responsible { get; set; }

        public DateTime RevisionValidity { get; set; }

        public DateTime RevisionDate { get; set; }
    }
}

