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
        //http://www.advancesharp.com/blog/1125/search-sort-paging-insert-update-and-delete-with-asp-net-mvc-and-bootstrap-modal-popup-part-1

        // GET: /Phone/
        public ActionResult Index(string filter = null, int page = 1, int pageSize = 5, string sort = "PhoneId", string sortdir = "DESC")
        {
            var records = new PagedList<Invoice>();
            ViewBag.filter = filter;
            records.Content = db.Invoices
                        .Where(x => filter == null ||
                                (x.Model.Contains(filter))
                                   || x.Company.Contains(filter)
                              )
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = db.Invoices
                         .Where(x => filter == null ||
                               (x.Model.Contains(filter)) || x.Company.Contains(filter)).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return View(records);

        }


        // GET: /Phone/Details/5
        public ActionResult Details(int id = 0)
        {
            Invoice record =  db.Invoices.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return PartialView("Details", record);
        }


        // GET: /Phone/Create
        [HttpGet]
        public ActionResult Create()
        {
            var record = new Invoice();
            return PartialView("Create", record);
        }


        // POST: /Phone/Create
        [HttpPost]
        public ActionResult Create(Invoice record)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(record);
                db.SaveChanges();
                return Json(new { success = true });
            }
			return PartialView("Create", record);
			//return Json(phone, JsonRequestBehavior.AllowGet);
        }


        // GET: /Phone/Edit/5
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var record = db.Invoices.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }

            return PartialView("Edit", record);
        }


        // POST: /Phone/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Invoice record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(record).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Edit", record);
        }

        //
        // GET: /Phone/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Invoice record = db.Invoices.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", record);
        }


        //
        // POST: /Phone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var record = db.Invoices.Find(id);
            db.Invoices.Remove(record);
            db.SaveChanges();
            return Json(new { success = true });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
