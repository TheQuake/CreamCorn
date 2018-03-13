using CreamCorn.DAL;
using CreamCorn.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace CreamCorn.Controllers
{
    public class CategoryController : BaseController
    {
        private DbContext_CreamCorn db = new DbContext_CreamCorn();

        public ActionResult Index(string filter = null, int page = 1, int pageSize = 5, string sort = "Name", string sortdir = "ASC")
        {
            var records = new PagedList<Category>();
            ViewBag.filter = filter;
            records.Content = db.Categories
						.Where(x => filter == null ||
                                (x.Name.Contains(filter))
                                   || x.Name.Contains(filter)
                              )
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = db.Categories
						 .Where(x => filter == null ||
                               (x.Name.Contains(filter)) || x.Name.Contains(filter)).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return View(records);

        }

        public ActionResult Add()
        {
			Category c = new Category();
            return PartialView(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category c)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(c);
            }

            db.Categories.Add(c);
            db.SaveChanges();

            return Json(new { success = true });
        }

		public ActionResult Edit(string Id)
		{
			Category c = new Category();

			var record = db.Categories
						.Where(x => x.Id.ToString() == Id)
						.FirstOrDefault();

			if (record != null)
			{
				c.Id = record.Id;
				c.Name = record.Name;
			}
			return PartialView(c);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Category c)
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
			var record = db.Categories
						.Where(x => x.Id.ToString() == Id)
						.FirstOrDefault();

			if (record != null)
			{
				db.Categories.Remove(record);
				db.SaveChanges();
				message = string.Format("Category '{0}' removed successfully!", Name);
			}

			return Json(new { id = Id, message = message });
		}

		public ActionResult Companies(string id, int page = 1, int pageSize = 5, string sort = "Name", string sortdir = "ASC")
		{
			var records = new PagedList<Company>();
			records.Content = db.Companies
						.Where(x => x.CategoryId.ToString() == id)
						.OrderBy(sort + " " + sortdir)
						.Skip((page - 1) * pageSize)
						.Take(pageSize)
						.ToList();

			// Count
			records.TotalRecords = db.Companies
						 .Where(x => x.CategoryId.ToString() == id).Count();

			records.CurrentPage = page;
			records.PageSize = pageSize;

			return PartialView(records);

		}


		protected override void Dispose(bool disposing)
        {
            db.Dispose();
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
