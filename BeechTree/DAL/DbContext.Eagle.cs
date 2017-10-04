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
            Database.SetInitializer<DbContext_Eagle>
                (null);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Job> Jobs { get; set; }

        public Customer CustomerGet(string customerNumber)
        {
            return (Customer)this.Customers.Where(x => x.CustId.Equals(customerNumber));

        }

        public List<Customer> CustomersGet(string customerId)
        {
            var records = this.Customers
                        .Where(x => x.CustId.Equals(customerId))
                        .OrderBy(x => x.CustName)
                        .ToList();

            return records;
        }

        public Job JobGet(string jobNumber)
        {
            return (Job) this.Jobs.Where(x => x.JobNo.Equals(jobNumber));

        }

        public List<Job> JobsGet(string jobNumber)
        {
            var records = this.Jobs
                        .Where(x => x.JobNo.Equals(jobNumber))
                        .ToList();

            return records;
        }

    }
}