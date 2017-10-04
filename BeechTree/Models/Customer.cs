using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeechTree.Models
{
    [Table("tblArCust")]
    public class Customer
    {
        public Customer()
        {
        }

        [Display(Name = "Customer Id")]
        [Key]
        public string CustId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustName { get; set; }

        [Display(Name = "Address")]
        public string Addr1 { get; set; }

        public string Addr2 { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }


        [Display(Name = "Terms")]
        public string TermsCode { get; set; }


    }
}