using BeechTree.DAL;
using BeechTree.Models;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;


namespace BeechTree.Controllers
{
	public class CustomerController : BaseController
    {
        private DbContext_Eagle dbEagle = new DbContext_Eagle();

        public ActionResult Index(string filter = null, int page = 1, int pageSize = 5, string sort = "CustName", string sortdir = "ASC")
        {
            var records = new PagedList<Customer>();
            ViewBag.filter = filter;
            records.Content = dbEagle.Customers
                        .Where(x => filter == null ||
                                (x.CustId.Contains(filter))
                                   || x.CustName.Contains(filter)
                              )
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = dbEagle.Customers
                         .Where(x => filter == null ||
                               (x.CustId.Contains(filter)) || x.CustName.Contains(filter)).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return View(records);

        }

        protected override void Dispose(bool disposing)
        {
            dbEagle.Dispose();
            base.Dispose(disposing);
        }
    }
}
