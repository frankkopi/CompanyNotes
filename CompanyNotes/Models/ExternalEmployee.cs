using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyNotes.Models
{
    public class ExternalEmployee : Employee
    {
        public int? SubcontractorId { get; set; } // FK

        public virtual Subcontractor Subcontractor { get; set; }
    }
}