using CompanyNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyNotes.ViewModels
{
    public class CreateResidentViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CaseId { get; set; } // FK
        public int CaseNumber { get; set; }
    }
}