using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyNotes.Models
{
    public class Subcontractor
    {
        public int SubcontractorId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<ExternalEmployee> ExternalEmployees { get; set; } // one-to-many
        public virtual ICollection<Case> Cases { get; set; }  // many-to-many
    }
}