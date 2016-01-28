using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompanyNotes.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyNotes.ViewModels
{
    public class CreateInternalEmployeeViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters ")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

    }
}