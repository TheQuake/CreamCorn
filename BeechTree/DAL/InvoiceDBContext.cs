using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BeechTree.Models;

namespace BeechTree.DAL
{
    public class InvoiceDBContext : DbContext
    {        
        public InvoiceDBContext()
            : base("PmData")
        {
            Database.SetInitializer<InvoiceDBContext>
                (new DropCreateDatabaseIfModelChanges<InvoiceDBContext>());
        }
        
        public DbSet<Invoice> Invoices { get; set; }

    }
}