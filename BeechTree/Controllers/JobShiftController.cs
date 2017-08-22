using BeechTree.DAL;
using BeechTree.Models;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;


namespace BeechTree.Controllers
{
	public class JobShiftController : Controller
    {
        private InvoiceDBContext db = new InvoiceDBContext();
        //http://www.advancesharp.com/blog/1125/search-sort-paging-insert-update-and-delete-with-asp-net-mvc-and-bootstrap-modal-popup-part-1

        // GET: /Phone/
        public ActionResult Index(string filter = null, int page = 1, int pageSize = 5, string sort = "Id", string sortdir = "DESC")
        {
            var records = new PagedList<JobShift>();
            ViewBag.filter = filter;
            records.Content = db.JobShifts 
                        .Where(x => filter == null ||
                                (x.JobNo.Contains(filter))
                                   || x.JobNo.Contains(filter)
                              )
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = db.JobShifts
                         .Where(x => filter == null ||
                               (x.JobNo.Contains(filter)) || x.JobNo.Contains(filter)).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return View(records);

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
