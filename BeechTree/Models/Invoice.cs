using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeechTree.Models
{
    public class Invoice
    {
        [Display(Name = "ID")]
        [Key]
        public int Id { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string JobNumber { get; set; }

        public Address EagleAddress { get; set; }

        public Address BillTo { get; set; }

        public Address ShipTo { get; set; }

        public string Terms { get; set; }


    }
}