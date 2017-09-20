using BeechTree.DAL;
using BeechTree.Models;
using Novacode;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;


namespace BeechTree.Controllers
{
	public class JobController : BaseController
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

        public ActionResult Invoice(string id)
        {
            Invoice i = db.InvoiceGet(id);

            string fileName = string.Format("Invoice_{0}.docx", i.JobNumber);
            string template = Server.MapPath("~/Templates/Invoice.docx");
            using (DocX doc = DocX.Load(template))
            {

                string value = string.Empty;
                string token = string.Empty;

                PropertyInfo[] pi = i.GetType().GetProperties();
                foreach (PropertyInfo p in pi)
                {
                    switch (p.Name.ToLower())
                    {
                        case "billto":
                            PropertyInfo[] pb = i.BillTo.GetType().GetProperties();
                            foreach (PropertyInfo pib in pb)
                            {
                                token = string.Format("{{{0}}}", pib.Name);
                                value = pib.GetValue(i.BillTo).ToString();
                                doc.ReplaceText(token, value, false, RegexOptions.IgnoreCase);
                            }
                            break;
                        case "eagle":
                            PropertyInfo[] pe = i.EagleAddress.GetType().GetProperties();
                            foreach (PropertyInfo pie in pe)
                            {
                                token = string.Format("{{{0}}}", pie.Name);
                                value = pie.GetValue(i.EagleAddress).ToString();
                                doc.ReplaceText(token, value, false, RegexOptions.IgnoreCase);
                            }
                            break;
                        case "shipto":
                            break;
                        default:
                            token = string.Format("{{{0}}}", p.Name);
                            value = p.GetValue(i).ToString();
                            doc.ReplaceText(token, value, false, RegexOptions.IgnoreCase);
                            break;
                    }
                }

                //doc.SaveAs(path_documents);

                return WordDocument(doc, template, fileName);

            }

            // only return pdf if DocX fails.
            return Pdf("invoice.pdf", "Invoice", i, true);
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
