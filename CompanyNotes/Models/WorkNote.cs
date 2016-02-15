using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompanyNotes.Models
{
    public class WorkNote
    {
        public int WorkNoteId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string  Caption { get; set; }

        public string Text { get; set; }

        public int CaseId { get; set; } // FK

        public int EmployeeId { get; set; } // FK

        public virtual Case Case { get; set; }
        public virtual Employee Employee { get; set; }
    }
}