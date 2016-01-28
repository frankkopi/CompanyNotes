using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompanyNotes.Models
{
    public enum EmployeeType {
        [Display(Name = "Internal Employee")]
        Internal = 1,

        [Display(Name = "External Employee")]
        External = 2
    }

    public abstract class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage="First name cannot be longer than 50 characters ")]
        [Display(Name ="First Name")]
        public string FirstMidName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstMidName + " " + LastName;
            }    
        }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        public string Type { get; set; }

        public virtual ICollection<Case> Cases { get; set; } // many-to-many
        public virtual ICollection<WorkNote> WorkNotes { get; set; } // one-to-many
    }
}