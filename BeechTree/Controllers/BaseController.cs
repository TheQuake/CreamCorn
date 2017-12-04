using System.IO;
using System.Text;
using System.Web.Mvc;
using Xceed.Words.NET;

namespace BeechTree.Controllers
{
	public class BaseController : Controller
    {

        // So far, just some DocX & PDF stuff ...

        protected FileResult WordDocument(DocX doc, string fileName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                doc.SaveAs(ms);

                Response.Clear();
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
				//Response.ContentEncoding = Encoding.UTF8;
				Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                //ms.Position = 0;
                //ms.CopyTo(Response.OutputStream);
                ms.WriteTo(Response.OutputStream);
                Response.End();
            }

            return null;
        }

        protected ActionResult Pdf()
        {
            return Pdf(null, null, null);
        }

        protected ActionResult Pdf(string fileDownloadName)
        {
            return Pdf(fileDownloadName, null, null);
        }

        protected ActionResult Pdf(string fileDownloadName, string viewName)
        {
            return Pdf(fileDownloadName, viewName, null);
        }

        protected ActionResult Pdf(object model)
        {
            return Pdf(null, null, model);
        }

        protected ActionResult Pdf(string fileDownloadName, object model)
        {
            return Pdf(fileDownloadName, null, model);
        }

        protected ActionResult Pdf(string fileDownloadName, string viewName, object model, bool landscape = false)
        {
            // Based on View() code in Controller base class from MVC
            if (model != null)
            {
                ViewData.Model = model;
            }
            PdfResult pdf = new PdfResult()
            {
                FileDownloadName = fileDownloadName,
                Landscape = landscape,
                ViewName = viewName,
                ViewData = ViewData,
                TempData = TempData,
                ViewEngineCollection = ViewEngineCollection
            };
            return pdf;
        }

    }
}