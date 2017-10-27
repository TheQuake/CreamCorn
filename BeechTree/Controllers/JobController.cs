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
	public class JobController : BaseController
    {
        private DbContext_Eagle dbEagle = new DbContext_Eagle();
        private DbContext_PmData dbPmData = new DbContext_PmData();

        public ActionResult Index(string filter = null, int page = 1, int pageSize = 5, string sort = "Id", string sortdir = "DESC")
        {
            var records = new PagedList<Job>();
            ViewBag.filter = filter;
            records.Content = dbEagle.Jobs
                        .Where(x => filter == null ||
                                (x.JobNo.Contains(filter))
                                   || x.JobNo.Contains(filter)
                              )
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = dbEagle.Jobs
                         .Where(x => filter == null ||
                               (x.JobNo.Contains(filter)) || x.JobNo.Contains(filter)).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return View(records);

        }

        public ActionResult Add()
        {
            JobAdd j = new JobAdd();
            j.StartDate = DateTime.Today;
            return PartialView(j);
        }

        public ActionResult Employees(string id, int page = 1, int pageSize = 5, string sort = "ShiftNo", string sortdir = "ASC")
        {
            var records = new PagedList<JobEmployee>
            {
                Content = dbPmData.JobEmployees
                        .Where(x => x.JobNo == id)
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList(),

                // Count
                TotalRecords = dbPmData.JobEmployees
                         .Where(x => x.JobNo == id).Count(),

                CurrentPage = page,
                PageSize = pageSize
            };

            return PartialView(records);

        }

        public ActionResult Equipments(string id, int page = 1, int pageSize = 5, string sort = "ShiftNo", string sortdir = "ASC")
        {
            var records = new PagedList<JobEquipment>();
            records.Content = dbPmData.JobEquipments
                        .Where(x => x.JobNo == id)
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = dbPmData.JobEquipments
                         .Where(x => x.JobNo == id).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return PartialView(records);

        }

		public ActionResult Invoice(string id)
		{
			// id = jobNumber

			// get invoice/job info
			InvoiceViewModel i = dbPmData.InvoiceCreate(id);
			Job j = dbEagle.JobGet(id);

			// get customer
			Customer c = dbEagle.CustomerGet(j.CustNo);


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
								token = string.Format("{{{0}.{1}}}", p.Name, pib.Name);
								value = string.Format("{0}", pib.GetValue(i.BillTo));
								doc.ReplaceText(token, value, false, RegexOptions.IgnoreCase);
							}
							break;
						case "eagleaddress":
							PropertyInfo[] pe = i.EagleAddress.GetType().GetProperties();
							foreach (PropertyInfo pie in pe)
							{
								token = string.Format("{{{0}.{1}}}", p.Name, pie.Name);
								value = string.Format("{0}", pie.GetValue(i.EagleAddress));
								doc.ReplaceText(token, value, false, RegexOptions.IgnoreCase);
							}
							break;
						case "lineitems":
							Table t = LineItemsTable(doc, i.LineItems);
							Table placeholderTable = doc.Tables[0];
							placeholderTable.InsertTableAfterSelf(t);
							placeholderTable.Remove();
							break;
						case "shipto":
							PropertyInfo[] ps = i.ShipTo.GetType().GetProperties();
							foreach (PropertyInfo pis in ps)
							{
								token = string.Format("{{{0}.{1}}}", p.Name, pis.Name);
								value = string.Format("{0}", pis.GetValue(i.ShipTo));
								doc.ReplaceText(token, value, false, RegexOptions.IgnoreCase);
							}
							break;
						case "remitto":
							PropertyInfo[] pr = i.RemitTo.GetType().GetProperties();
							foreach (PropertyInfo pir in pr)
							{
								token = string.Format("{{{0}.{1}}}", p.Name, pir.Name);
								value = string.Format("{0}", pir.GetValue(i.RemitTo));
								doc.ReplaceText(token, value, false, RegexOptions.IgnoreCase);
							}
							break;
						case "date":
							token = string.Format("{{{0}}}", p.Name);
							value = string.Format("{0}", p.GetValue(i));
							DateTime dt;
							DateTime.TryParse(value, out dt);
							value = dt.ToShortDateString();
							doc.ReplaceText(token, value, false, RegexOptions.IgnoreCase);
							break;
						case "terms":
							token = string.Format("{{{0}}}", p.Name);
							value = string.Format("{0}", c.TermsCode);
							doc.ReplaceText(token, value, false, RegexOptions.IgnoreCase);
							break;
						case "total":
							token = string.Format("{{{0}}}", p.Name);
							value = string.Format("{0:C}", i.Total);
							doc.ReplaceText(token, value, false, RegexOptions.IgnoreCase);
							break;
						default:
							token = string.Format("{{{0}}}", p.Name);
							value = string.Format("{0}", p.GetValue(i));
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

		public ActionResult Shifts(string id, int page = 1, int pageSize = 5, string sort = "ShiftNo", string sortdir = "ASC")
        {
            var records = new PagedList<JobShift>();
            records.Content = dbPmData.JobShifts
                        .Where( x => x.JobNo == id)
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = dbPmData.JobShifts
                         .Where(x => x.JobNo == id).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return PartialView(records);

        }


		public static IEnumerable<SelectListItem> ServiceCodesGet()
        {
            var db = new DbContext_Eagle();
            var records = db.ServiceCodes.ToList();

            IList<SelectListItem> list = new List<SelectListItem>();
            foreach (ServiceCode i in records)
            {
                SelectListItem item = new SelectListItem();
                item.Text = i.Value;
                item.Value = i.Id.ToString();
                list.Add(item);
            }
            List<SelectListItem> sortedItems = new List<SelectListItem>(list.OrderBy(i => i.Text));
            sortedItems.Insert(0, new SelectListItem { Text = "Please select a service code ...", Value = "" });

            return sortedItems;
        }

		public static IEnumerable<SelectListItem> SitesGet()
		{
			var db = new DbContext_Eagle();
			var records = db.Sites.ToList();

			IList<SelectListItem> list = new List<SelectListItem>();
			foreach (Site i in records)
			{
				SelectListItem item = new SelectListItem();
				item.Text = i.Name;
				item.Value = i.Id.ToString();
				list.Add(item);
			}
			List<SelectListItem> sortedItems = new List<SelectListItem>(list.OrderBy(i => i.Text));
			sortedItems.Insert(0, new SelectListItem { Text = "Please select a site ...", Value = "" });

			return sortedItems;
		}


		private Table LineItemsTable(DocX doc, List<InvoiceLineItem> items)
        {
            // rows + heaader row
            Table t = doc.AddTable(items.Count + 1, 4);
            t.Alignment = Alignment.center;
            t.Design = TableDesign.LightGrid;

            // table header row
            t.Rows[0].Cells[0].Paragraphs.First().Append("Day");
            t.Rows[0].Cells[1].Paragraphs.First().Append("Date");
            t.Rows[0].Cells[2].Paragraphs.First().Append("Shift");
            t.Rows[0].Cells[3].Paragraphs.First().Append("Amount");

            for (int i = 0; i < items.Count; i++)
            {
                t.Rows[i + 1].Cells[0].Paragraphs.First().Append(items[i].Day);
                t.Rows[i + 1].Cells[1].Paragraphs.First().Append(items[i].ShiftDate.ToShortDateString());
                t.Rows[i + 1].Cells[2].Paragraphs.First().Append(items[i].Shift);
                t.Rows[i + 1].Cells[3].Paragraphs.First().Append(items[i].Amount.ToString("N2"));

                t.Rows[i + 1].Cells[3].SetDirection(Direction.RightToLeft);

            }

            t.AutoFit = AutoFit.ColumnWidth;
            return t;

        }

        protected override void Dispose(bool disposing)
        {
            dbEagle.Dispose();
            dbPmData.Dispose();
            base.Dispose(disposing);
        }
    }
}
