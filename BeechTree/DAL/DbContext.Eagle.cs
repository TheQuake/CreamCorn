using BeechTree.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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

		#region DbSets
		public DbSet<Branch> Branches { get; set; }
		public DbSet<Customer> Customers { get; set; }
        public DbSet<Job> Jobs { get; set; }
		public DbSet<JobSearch> Searches { get; set; }
		public DbSet<ServiceCode> ServiceCodes { get; set; }
        public DbSet<Site> Sites { get; set; }
        #endregion

        #region CRUDs
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

		public JobAdd JobSave(JobAdd j)
		{
			try
			{
				using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Eagle"].ToString()))
				{
					cnn.Open();
					using (SqlCommand cmd = new SqlCommand("sp_InsertJobNo", cnn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@BranchCode", j.BranchCode);
						cmd.Parameters.AddWithValue("@BranchId", j.BranchId);
						cmd.Parameters.AddWithValue("@ConcurrencyId", j.ConcurrencysId);
						cmd.Parameters.AddWithValue("@CurrencyId", j.CurrencysCode);
						cmd.Parameters.AddWithValue("@CustId", j.CustomerCode);
						cmd.Parameters.AddWithValue("@DepartmentId", j.DepartmentId);
						cmd.Parameters.AddWithValue("@DistCode", j.DistrictCode);
						cmd.Parameters.AddWithValue("@JobDate", j.JobDate);
						cmd.Parameters.AddWithValue("@JobName", j.JobName);
						cmd.Parameters.AddWithValue("@JobNo", j.JobNumber);
						cmd.Parameters.AddWithValue("@PriorityId", j.PriorityId);
						cmd.Parameters.AddWithValue("@SalesRepCode", j.SalesRepCode);
						cmd.Parameters.AddWithValue("@ServiceCode", j.ServiceCode);
						cmd.Parameters.AddWithValue("@JobSiteId", j.SiteId);
						cmd.Parameters.AddWithValue("@JobSiteName", j.SiteName);
						cmd.Parameters.AddWithValue("@TaxGrpId", j.TaxGroupCode);
						cmd.Parameters.AddWithValue("@TermsCode", j.TermsCode);
						cmd.Parameters.AddWithValue("@ZoneId", j.ZoneId);

						SqlParameter p = new SqlParameter("@InsertCount", SqlDbType.Int);
						p.Direction = ParameterDirection.Output;
						cmd.Parameters.Add(p);


						j.Id = Convert.ToInt32(cmd.ExecuteScalar());
						return j;

					}

				}

			}
			catch (Exception ex)
			{
				j.message = ex.Message;
				return j;
			}

		}


        #endregion

    }
}