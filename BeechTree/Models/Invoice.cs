using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeechTree.Models
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Column("Invoice")]
        public int InvoiceNumber { get; set; }

        public string Po { get; set; }

        public DateTime Invoice_Date { get; set; }
        public string Job_Id { get; set; }
        public string Term_Net { get; set; }

    }

    [NotMapped]
    public class InvoiceViewModel : Invoice
    {

        public InvoiceViewModel()
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
        public decimal Total { get; set; }

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