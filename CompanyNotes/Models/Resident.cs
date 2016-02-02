using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompanyNotes.Models
{
    public class Resident
    {
        public int ResidentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CaseId { get; set; } // FK

        public virtual Case Case { get; set; }
    }
}