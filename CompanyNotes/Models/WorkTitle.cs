using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompanyNotes.Models
{
    public class WorkTitle
    {
        public int WorkTitleId { get; set; }

        [Required]
        [Display(Name = "Work Title")]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}