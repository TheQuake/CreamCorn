using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeechTree.Models
{
    public class Invoice
    {

        public Invoice()
        {
            EagleAddress = new Address();
            BillTo = new Address();
            ShipTo = new Address();
            LineItems = new List<InvoiceLineItem>();
        }


        [Display(Name = "ID")]
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string JobNumber { get; set; }

        public Address EagleAddress { get; set; }

        public Address BillTo { get; set; }

        public Address RemitTo { get; set; }

        public Address ShipTo { get; set; }

        public string Terms { get; set; }

        public List<InvoiceLineItem> LineItems { get; set; }

    }
    public class InvoiceLineItem
    {
        [Display(Name = "ID")]
        [Key]
        public int InvoiceId { get; set; }

        public string Day { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public decimal Amount { get; set; }

        public string ShiftNumber { get; set; }
        public DateTime ShiftDate { get; set; }


    }
}