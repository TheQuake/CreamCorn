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
    public class DbContext_PmData : DbContext
    {        
        public DbContext_PmData()
            : base("PmData")
        {
            Database.SetInitializer<DbContext_PmData>
                (null);
        }
        
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<JobEmployee> JobEmployees { get; set; }
        public DbSet<JobEquipment> JobEquipments { get; set; }
        public DbSet<JobShift> JobShifts { get; set; }
        public DbSet<Unit> Units { get; set; }

        public InvoiceViewModel InvoiceCreate(string jobNumber)
        {

            InvoiceViewModel i = new InvoiceViewModel();

            // get shifts (job in lieu of job master)
            List<JobShift> shifts = this.JobShiftsGet(jobNumber);

            // calc employee price
            List<JobEmployee> employees = this.JobEmployeesGet(jobNumber);
            decimal totalLabor = TotalLabor(employees);

            // calc equipment price
            List<JobEquipment> equipments = this.JobEquipmentsGet(jobNumber);
            decimal totalEquipment = TotalEquipment(equipments);

            i.Total = totalEquipment + totalLabor;

            int lineItemCount = 0;

            foreach (JobShift s in shifts)
            {
                InvoiceLineItem li = new InvoiceLineItem()
                {
                    Amount = totalEquipment, 
                    Day = s.ShiftDate.DayOfWeek.ToString(),
                    Shift = string.Format("{0} - {1}", s.ShiftStart, s.ShiftStop),
                    ShiftDate = s.ShiftDate,
                    ShiftNumber = s.ShiftNo.ToString()
                };
                i.LineItems.Add(li);
                lineItemCount++;
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

            i.InvoiceNumber = InvoiceGetNextNumber();
            return i;
        }

        private int InvoiceGetNextNumber()
        {
            int invoiceNumber = this.Invoices.Max(x => x.InvoiceNumber);
            invoiceNumber++;

            return invoiceNumber;
        }

        private List<JobEmployee> JobEmployeesGet(string jobNumber)
        {
            var records = this.JobEmployees
                        .Where(x => x.JobNo.Equals(jobNumber))
                        .OrderBy(x => x.Id)
                        .ToList();

            return records;
        }

        private List<JobEquipment> JobEquipmentsGet(string jobNumber)
        {
            var records = this.JobEquipments
                        .Where(x => x.JobNo.Equals(jobNumber))
                        .OrderBy(x => x.Id)
                        .ToList();

            return records;
        }

        private List<JobShift> JobShiftsGet(string jobNumber)
        {
            var records = this.JobShifts
                        .Where(x => x.JobNo.Equals(jobNumber))
                        .OrderBy(x => x.Id)
                        .ToList();

            return records;
        }

        private decimal TotalEquipment(List<JobEquipment> items)
        {
            decimal d = 0;
            foreach (JobEquipment item in items)
            {
                d += item.price_actual * (decimal) item.UnitQty_Actual;
            }
            return d;
        }

        private decimal TotalLabor(List<JobEmployee> items)
        {
            decimal d = 0;
            foreach (JobEmployee item in items)
            {
                // ***TODO
                //d += item..price_actual * item.Qty;
            }
            return d;
        }


    }
}