﻿using BeechTree.DAL;
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
        //http://www.advancesharp.com/blog/1125/search-sort-paging-insert-update-and-delete-with-asp-net-mvc-and-bootstrap-modal-popup-part-1

        // GET: /Phone/
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


        public ActionResult Equipments(int id = 0)
        {
            Job record = db.Jobs.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return PartialView(record);
        }

        public ActionResult Shifts(int id = 0)
        {
            Job record = db.Jobs.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return PartialView(record);
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
