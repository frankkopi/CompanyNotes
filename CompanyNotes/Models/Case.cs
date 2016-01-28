using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompanyNotes.Models
{
    public class Case
    {
        public int CaseId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public string Manager { get; set; }

        public int ClientId { get; set; }  // FK

        public virtual Client Client { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } // many-to-many
        public virtual ICollection<WorkNote> WorkNotes { get; set; } // one-to-many
        public virtual ICollection<Resident> Residents { get; set; } // one-to-many
        public virtual ICollection<Subcontractor> Subcontractors { get; set; } // many-to-many
    }
}