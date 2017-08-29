using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeechTree.Models
{
    public class Address
    {
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Company Number")]
        public string CompanyNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "First Name")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }


    }
}