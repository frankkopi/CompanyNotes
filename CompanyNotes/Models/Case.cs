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
        [Display(Name = "Case Number")]
        public int CaseNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expected End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Case Status")]
        public bool IsActive { get; set; }

        [Display(Name = "Site Manager")]
        public string Manager { get; set; }

        public int ClientId { get; set; }  // FK

        public virtual Client Client { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } // many-to-many
        public virtual ICollection<WorkNote> WorkNotes { get; set; } // one-to-many
        public virtual ICollection<Resident> Residents { get; set; } // one-to-many
        public virtual ICollection<Subcontractor> Subcontractors { get; set; } // many-to-many

        public int setCaseNumber(int numberFromDb)
        {
            int currentNumber = -1;

            if (numberFromDb == 0)
            {
                currentNumber = 100100;
            }
            else
            {
                currentNumber = numberFromDb + 100;
            }
            return currentNumber;
        }
    }
}