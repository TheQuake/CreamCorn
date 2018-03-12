using CreamCorn.DAL;
using CreamCorn.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace CreamCorn.Controllers
{
    public class ContactController : BaseController
    {
        private DbContext_CreamCorn db = new DbContext_CreamCorn();

        public ActionResult Index(string filter = null, int page = 1, int pageSize = 5, string sort = "Name", string sortdir = "ASC")
        {
            var records = new PagedList<Contact>();
            ViewBag.filter = filter;
            records.Content = db.Contacts
                        .Where(x => filter == null ||
                                (x.Name.Contains(filter))
                                   || x.Name.Contains(filter)
                              )
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = db.Companies
                         .Where(x => filter == null ||
                               (x.Name.Contains(filter)) || x.Name.Contains(filter)).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return View(records);

        }

        public ActionResult Add()
        {
			Contact c = new Contact();
            return PartialView(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Contact c)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(c);
            }

            db.Contacts.Add(c);
            db.SaveChanges();

            return Json(new { success = true });
        }

		public ActionResult Edit(string Id)
		{
			Contact c = new Contact();

			var record = db.Contacts
						.Where(x => x.Id.ToString() == Id)
						.FirstOrDefault();

			if (record != null)
			{
				c.Id = record.Id;
				c.Name = record.Name;
				c.CompanyId = record.CompanyId;
			}
			return PartialView(c);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Contact c)
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
			var record = db.Contacts
						.Where(x => x.Id.ToString() == Id)
						.FirstOrDefault();

			if (record != null)
			{
				db.Contacts.Remove(record);
				db.SaveChanges();
				message = string.Format("Contact '{0}' removed successfully!", Name);
			}

			return Json(new { id = Id, message = message });
		}


		public static IEnumerable<SelectListItem> GetCompanies()
		{
			var dbc = new ContactController();
			var records = dbc.db.Companies
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

			list.Insert(0, new SelectListItem { Text = "Please select a company ...", Value = "" });

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
