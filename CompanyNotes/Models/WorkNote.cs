using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyNotes.Models
{
    public class WorkNote
    {
        public int WorkNoteId { get; set; }
        public DateTime Date { get; set; }
        public string  Caption { get; set; }
        public string Text { get; set; }
        public int CaseId { get; set; } // FK
        public int EmployeeId { get; set; } // FK

        public virtual Case Case { get; set; }
        public virtual Employee Employee { get; set; }
    }
}