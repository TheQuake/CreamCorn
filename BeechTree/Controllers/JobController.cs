using BeechTree.DAL;
using BeechTree.Models;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;


namespace BeechTree.Controllers
{
	public class JobController : Controller
    {
        private InvoiceDBContext db = new InvoiceDBContext();

        public ActionResult Index(string filter = null, int page = 1, int pageSize = 5, string sort = "Id", string sortdir = "DESC")
        {
            var records = new PagedList<Job>();
            ViewBag.filter = filter;
            records.Content = db.Jobs
                        .Where(x => filter == null ||
                                (x.JobNo.Contains(filter))
                                   || x.JobNo.Contains(filter)
                              )
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = db.Jobs
                         .Where(x => filter == null ||
                               (x.JobNo.Contains(filter)) || x.JobNo.Contains(filter)).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return View(records);

        }


        public ActionResult Employees(string id, int page = 1, int pageSize = 5, string sort = "ShiftNo", string sortdir = "ASC")
        {
            var records = new PagedList<JobEmployee>
            {
                Content = db.JobEmployees
                        .Where(x => x.JobNo == id)
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList(),

                // Count
                TotalRecords = db.Jobs
                         .Where(x => x.JobNo == id).Count(),

                CurrentPage = page,
                PageSize = pageSize
            };

            return PartialView(records);

        }

        public ActionResult Equipments(string id, int page = 1, int pageSize = 5, string sort = "ShiftNo", string sortdir = "ASC")
        {
            var records = new PagedList<JobEquipment>();
            records.Content = db.JobEquipments
                        .Where(x => x.JobNo == id)
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = db.Jobs
                         .Where(x => x.JobNo == id).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return PartialView(records);

        }

        public ActionResult Shifts(string id, int page = 1, int pageSize = 5, string sort = "ShiftNo", string sortdir = "ASC")
        {
            var records = new PagedList<Job>();
            records.Content = db.Jobs
                        .Where( x => x.JobNo == id)
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = db.Jobs
                         .Where(x => x.JobNo == id).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return PartialView(records);

        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
