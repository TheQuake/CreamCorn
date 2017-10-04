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
            Address = new Address();
        }

        [Display(Name = "Customer Id")]
        [Key]
        public string CustId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustName { get; set; }

        public Address Address { get; set; }

        [Display(Name = "Terms")]
        public string TermsCode { get; set; }


    }
}