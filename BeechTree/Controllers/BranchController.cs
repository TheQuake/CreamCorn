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
	public class BranchController : BaseController
    {
        private DbContext_Eagle dbEagle = new DbContext_Eagle();

        public ActionResult Index(string filter = null, int page = 1, int pageSize = 10, string sort = "Name", string sortdir = "ASC")
        {
            var records = new PagedList<Branch>();
            ViewBag.filter = filter;
            records.Content = dbEagle.Branches
                        .Where(x => filter == null ||
                                (x.Name.Contains(filter))
                                   || x.Name.Contains(filter)
                              )
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = dbEagle.Branches
						 .Where(x => filter == null ||
                               (x.Name.Contains(filter)) || x.Name.Contains(filter)).Count();

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
