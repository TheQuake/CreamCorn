using BeechTree.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

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
        public DbSet<Site> Sites { get; set; }

        public Customer CustomerGet(string customerNumber)
        {
            Customer c = this.Customers.Where(x => x.CustId.Equals(customerNumber)).FirstOrDefault();
            int i = 0;
            int.TryParse(c.TermsCode, out i);
            c.TermsCode = string.Format("Net {0}", i);
            return c;
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
            return (Job) this.Jobs.Where(x => x.JobNo.Equals(jobNumber)).FirstOrDefault();

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