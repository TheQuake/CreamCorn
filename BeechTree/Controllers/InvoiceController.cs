using BeechTree.DAL;
using BeechTree.Models;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;


namespace BeechTree.Controllers
{
	public class InvoiceController : Controller
    {
        private InvoiceDBContext db = new InvoiceDBContext();

        public ActionResult Index(string jobNumber)
        {

            // get invoice and line items
            // sproc?

            Invoice i = loadSampleData(jobNumber);

            Invoice i2 = db.Get(jobNumber);

            // **TODO get from db or settings, not from sample data
            i2.EagleAddress = i.EagleAddress;

            return View(i2);

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
