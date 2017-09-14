using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BeechTree.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

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

        public Invoice Get(string jobNumber)
        {
            Invoice i = new Invoice();

            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["PmData"].ToString()))
            {
                cnn.Open();

                using (SqlCommand cmd = new SqlCommand("InvoiceHeaderGet", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@JobNumber", jobNumber);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        i.JobNumber = jobNumber;
                        i.PurchaseOrderNumber = dr["PurchaseOrderNumber"].ToString();
                        i.Terms = dr["Terms"].ToString();
                        i.BillTo.CompanyNumber = dr["CompanyNumber"].ToString();
                        i.BillTo.CompanyName = dr["CompanyName"].ToString();
                        i.BillTo.Address1 = dr["Address1"].ToString();
                        i.BillTo.Address2 = dr["Address2"].ToString();
                        i.BillTo.City = dr["City"].ToString();
                        i.BillTo.State = dr["State"].ToString();
                        i.BillTo.Zip = dr["Zip"].ToString();

                        i.ShipTo.CompanyName = dr["ShipToCompanyName"].ToString();
                        i.ShipTo.FirstName = dr["ShipToFirstName"].ToString();
                        i.ShipTo.LastName = dr["ShipToLastName"].ToString();

                    }
                    dr.Close();
                }

            }

            return i;
        }

    }
}