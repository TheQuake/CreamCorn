using BeechTree.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace BeechTree.DAL
{
    public class DbContext_Eagle : DbContext
    {        
        public DbContext_Eagle()
            : base("Eagle")
        {
            Database.SetInitializer<InvoiceDBContext>
                (null);
        }
        
        public DbSet<Customer> Customers { get; set; }

        public List<Customer> CustomersGet(string companyNumber)
        {
            var records = this.Customers
                        .Where(x => x.CompanyNumber.Equals(companyNumber))
                        .OrderBy(x => x.CompanyName)
                        .ToList();

            return records;
        }


    }
}