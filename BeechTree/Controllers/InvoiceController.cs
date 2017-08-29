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
            Address shipTo = new Address()
            {
                FirstName = "Joe",
                LastName = "Blow",
                CompanyName = "US Steel"
            };

            Address billTo = new Address()
            {
                FirstName = "Joe",
                LastName = "Blow",
                CompanyName = "US Steel"
            };

            Address eagle = new Address()
            {
                FirstName = "Eddie",
                LastName = "Eagleman",
                CompanyName = "Eagle Services"
            };

            Invoice i = new Invoice()
            {
                JobNumber = jobNumber,
                EagleAddress = eagle,
                ShipTo=shipTo,
                BillTo=billTo
            };


            return View(i);

        }




    }
}
