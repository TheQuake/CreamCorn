using CreamCorn.DAL;
using CreamCorn.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace CreamCorn.Controllers
{
    public class CompanyController : BaseController
    {
        private DbContext_CreamCorn db = new DbContext_CreamCorn();

        public ActionResult Index(string filter = null, int page = 1, int pageSize = 5, string sort = "Name", string sortdir = "ASC")
        {
            var records = new PagedList<Company>();
            ViewBag.filter = filter;
            records.Content = db.Companies
                        .Where(x => filter == null ||
                                (x.PhoneNumber.Contains(filter))
                                   || x.Name.Contains(filter)
                              )
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = db.Companies
                         .Where(x => filter == null ||
                               (x.PhoneNumber.Contains(filter)) || x.Name.Contains(filter)).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return View(records);

        }

        public ActionResult Add()
        {
            Company c = new Company();
            return PartialView(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Company c)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(c);
            }

            db.Companies.Add(c);
            db.SaveChanges();

            return Json(new { success = true });
        }

		public ActionResult Edit(string Id)
		{
			Company c = new Company();

			var record = db.Companies
						.Where(x => x.Id.ToString() == Id)
						.FirstOrDefault();

			if (record != null)
			{
				c.Id = record.Id;
				c.Name = record.Name;
				c.PhoneNumber = record.PhoneNumber;
				c.CategoryId = record.CategoryId;
			}
			return PartialView(c);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Company c)
		{
			if (!ModelState.IsValid)
			{
				return PartialView(c);
			}

			db.Entry(c).State = EntityState.Modified;
			db.SaveChanges();
			return Json(new { success = true });
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult Delete(string Id, string Name)
		{
			string message = string.Empty;
			var record = db.Companies
						.Where(x => x.Id.ToString() == Id)
						.FirstOrDefault();

			if (record != null)
			{
				db.Companies.Remove(record);
				db.SaveChanges();
				message = string.Format("Company '{0}' removed successfully!", Name);
			}

			return Json(new { id = Id, message = message });
		}


		public ActionResult Categories(string id, int page = 1, int pageSize = 5, string sort = "CategoryId", string sortdir = "ASC")
        {
            var records = new PagedList<CompanyCategory>();
            var query = db.CompanyCategories
                        .Join(db.Categories, cc => cc.CategoryId, c => c.Id, (cc, c) => new { cc, c })
                        .Where(x => x.cc.CompanyId.Equals(id))
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();


            // Count
            records.TotalRecords = db.CompanyCategories
                         .Where(x => x.CompanyId.ToString() == id).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return PartialView(records);

        }

        public ActionResult Contacts(string id, int page = 1, int pageSize = 5, string sort = "Name", string sortdir = "ASC")
        {
            var records = new PagedList<Contact>();
            records.Content = db.Contacts
                        .Where(x => x.CompanyId.ToString() == id)
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = db.Contacts
                         .Where(x => x.CompanyId.ToString() == id).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return PartialView(records);

        }

		public static IEnumerable<SelectListItem> GetCategories()
		{
			var dbc = new CompanyController();
			var records = dbc.db.Categories
						.OrderBy("name asc")
						.ToList();

			IList<SelectListItem> list = new List<SelectListItem>();
			foreach (var i in records)
			{
				SelectListItem item = new SelectListItem();
				item.Text = i.Name;
				item.Value = i.Id.ToString();
				list.Add(item);
			}

			list.Insert(0, new SelectListItem { Text = "Please select a category ...", Value = "" });

			return list;
		}


		protected override void Dispose(bool disposing)
        {
            db.Dispose();
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
