using BeechTree.DAL;
using BeechTree.Models;
using Novacode;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;


namespace BeechTree.Controllers
{
	public class UnitController : BaseController
    {
        private DbContext_PmData dbPmData = new DbContext_PmData();

        public ActionResult Index(string filter = null, int page = 1, int pageSize = 5, string sort = "Name", string sortdir = "ASC")
        {
            var records = new PagedList<Unit>();
            ViewBag.filter = filter;
            records.Content = dbPmData.Units
                        .Where(x => filter == null ||
                                (x.Name.Contains(filter))
                                   || x.Name.Contains(filter)
                              )
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = dbPmData.Units
                         .Where(x => filter == null ||
                               (x.Name.Contains(filter)) || x.Name.Contains(filter)).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return View(records);

        }



        protected override void Dispose(bool disposing)
        {
            dbPmData.Dispose();
            base.Dispose(disposing);
        }
    }
}
