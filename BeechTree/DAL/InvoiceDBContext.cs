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

        public Invoice InvoiceGet(string jobNumber)
        {

            Invoice i = new Invoice();

            // during testing
            //i = loadSampleData(jobNumber);
            //return i;
            // during testing

            // ****TODO - get from db or config
            Address eagle = new Address()
            {
                FirstName = "Eddie",
                LastName = "Eagleman",
                CompanyName = "Eagle Services Corporation",
                Address1 = "2702 Beech Street",
                Address2 = "",
                City = "Valparaiso",
                State = "IN",
                Zip = "46383",
                Phone = "219-464-8888",
                Web = "www.eagleservices.com"
            };
            i.EagleAddress = eagle;

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

        private Invoice loadSampleData(string jobNumber)
        {
            Address shipTo = new Address()
            {
                FirstName = "Joe",
                LastName = "Blow",
                CompanyName = "US Steel"
            };

            Address billTo = new Address()
            {
                CompanyName = "US Steel",
                CompanyNumber = "MIT120",
                Address1 = "250 US Hwy 12",
                Address2 = "",
                City = "Burns Harbor",
                State = "IN",
                Zip = "46304",
            };

            Address eagle = new Address()
            {
                FirstName = "Eddie",
                LastName = "Eagleman",
                CompanyName = "Eagle Services Corporation",
                Address1 = "2702 Beech Street",
                Address2 = "",
                City = "Valparaiso",
                State = "IN",
                Zip = "46383",
                Phone = "219-464-8888",
                Web = "www.eagleservices.com"
            };

            Invoice i = new Invoice()
            {
                Id = 12345,
                JobNumber = jobNumber,
                EagleAddress = eagle,
                ShipTo = shipTo,
                BillTo = billTo,
                PurchaseOrderNumber = "PO12345",
                Terms = "Net 30"
            };

            return i;

        }


    }
}