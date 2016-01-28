using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyNotes.Models
{
    public class Client
    { 
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Case> Cases { get; set; }  // one-to-many
    }
}