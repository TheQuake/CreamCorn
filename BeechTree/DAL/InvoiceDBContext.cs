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

            // get shifts (job in lieu of job master)
            List<Job> shifts = this.InvoiceShiftsGet(jobNumber);

            // get "day" for shift start & stop


            // calc employee price
            List<JobEmployee> employees = this.InvoiceEmployeesGet(jobNumber);

            // calc equipment price
            List<JobEquipment> equipments = this.InvoiceEquipmentsGet(jobNumber);
            
            foreach (Job j in shifts)
            {
                InvoiceLineItem li = new InvoiceLineItem()
                {
                    Amount = equipments[2].price_actual, 
                    Day = j.ShiftDate.DayOfWeek.ToString(),
                    Shift = string.Format("{0} - {1}", j.ShiftStart, j.ShiftStop),
                    ShiftDate = j.ShiftDate,
                    ShiftNumber = j.ShiftNo.ToString()
                };
                i.LineItems.Add(li);
            }


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
            i.RemitTo = eagle;

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
                        i.Date = DateTime.Now;
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
        public List<JobEmployee> InvoiceEmployeesGet(string jobNumber)
        {
            var records = this.JobEmployees
                        .Where(x => x.JobNo.Equals(jobNumber))
                        .OrderBy(x => x.Id)
                        .ToList();

            return records;
        }

        public List<JobEquipment> InvoiceEquipmentsGet(string jobNumber)
        {
            var records = this.JobEquipments
                        .Where(x => x.JobNo.Equals(jobNumber))
                        .OrderBy(x => x.Id)
                        .ToList();

            return records;
        }

        public List<Job> InvoiceShiftsGet(string jobNumber)
        {
            // when job master is located, this can be pmshiftdata
            var records = this.Jobs
                        .Where(x => x.JobNo.Equals(jobNumber))
                        .OrderBy(x => x.Id)
                        .ToList();

            return records;
        }


    }
}