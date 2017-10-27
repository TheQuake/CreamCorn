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
	public class ServiceCodeController : BaseController
    {
        private DbContext_Eagle dbEagle = new DbContext_Eagle();

        public ActionResult Index(string filter = null, int page = 1, int pageSize = 5, string sort = "Value", string sortdir = "ASC")
        {
            var records = new PagedList<ServiceCode>();
            ViewBag.filter = filter;
            records.Content = dbEagle.ServiceCodes
                        .Where(x => filter == null ||
                                (x.Value.Contains(filter))
                                   || x.Value.Contains(filter)
                              )
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = dbEagle.ServiceCodes
                         .Where(x => filter == null ||
                               (x.Value.Contains(filter)) || x.Value.Contains(filter)).Count();

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
