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
                (null);
        }
        
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobEmployee> JobEmployees { get; set; }
        public DbSet<JobEquipment> JobEquipments { get; set; }
        public DbSet<JobShift> JobShifts { get; set; }

    }
}