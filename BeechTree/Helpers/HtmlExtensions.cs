using BeechTree.Controllers;
using BeechTree.DAL;
using BeechTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeechTree
{
	public static class HtmlExtensions
	{

        public static MvcHtmlString IconFor(this HtmlHelper helper, string jobNumber, string type)
        {
            DbContext_PmData dbPmData = new DbContext_PmData();
            int count = 0;
            bool isInvoice = false;

            string empty = string.Empty;
            string title = string.Empty;
            string glyph = string.Empty;

            switch (type.ToLower())
            {
                case "employees":
                    count = dbPmData.JobEmployees.Where(x => x.JobNo.Equals(jobNumber)).Count();
                    glyph = "glyphicon-user";
                    empty = "There are no employees entered for this job";
                    title = "Employees";
                    break;
                case "equipments":
                    count = dbPmData.JobEquipments.Where(x => x.JobNo.Equals(jobNumber)).Count();
                    glyph = "glyphicon-wrench";
                    empty = "There is no equipment entered for this job";
                    title = "Equipment";
                    break;
                case "invoice":
                    count = 1;
                    isInvoice = true;
                    glyph = "glyphicon-list-alt";
                    title = "Create invoice";
                    break;
                case "shifts":
                    count = dbPmData.JobShifts.Where(x => x.JobNo.Equals(jobNumber)).Count();
                    empty = "There are no shifts entered for this job";
                    glyph = "glyphicon-align-justify";
                    title = "Shifts";
                    break;
            }
            string s = string.Empty;
            if (count > 0)
            {
                if (isInvoice)
                {
                    s = string.Format("<a href='/job/{0}/{1}' id='{1}' title='{2}'><span class='glyphicon {3}'></span></a>  ", type, jobNumber, title, glyph);
                }
                else
                {
                    s = string.Format("<a data-modal='' href='/job/{0}/{1}' id='{1}' title='{2}'><span class='glyphicon {3}'></span></a>  ", type, jobNumber, title, glyph);
                }
            }
            else
            {
                s = string.Format("<span class='glyphicon {0}' title='{1}'></span></a>  ", glyph, empty);
            }


            return MvcHtmlString.Create(s);
        }

        public static MvcHtmlString ImageFor(this HtmlHelper helper, string contentType)
		{
			string s = "";
			switch (contentType)
			{
				case "application/pdf":
					s = string.Format("<img src=\"{0}\" width=\"24\" height=\"24\" />", "/Content/pdf.48.png");
					break;
				case "image/jpeg":
					s = string.Format("<img src=\"{0}\" width=\"24\" height=\"24\" />", "/Content/image.48.png");
					break;
                case "text/plain":
                    s = string.Format("<img src=\"{0}\" width=\"24\" height=\"24\" />", "/Content/notepad.48.png");
                    break;
            }
            return MvcHtmlString.Create(s);

		}

		public static MvcHtmlString DocumentActive(this HtmlHelper helper, bool isActive)
		{
			string s = string.Empty;

			if (isActive)
			{
				s = "<span class=\"glyphicon glyphicon-ok\" />";
			}

			return MvcHtmlString.Create(s);
		}

		public static MvcHtmlString PhotoFor(this HtmlHelper helper, byte[] photo, string size)
		{
			string s = "";
			if (photo == null || photo.Length == 0)
			{
				s = string.Format("<img src=\"/Content/no-photo.png\" width=\"{0}\" height=\"{0}\" />", size);
			}
			else
			{
				string imageBase64 = Convert.ToBase64String(photo);
				string imageSrc = string.Format("data:image/jpg;base64,{0}", imageBase64);
				s = string.Format("<img src=\"{0}\" width=\"{1}\" height=\"{1}\" />", imageSrc, size);
			}
			return MvcHtmlString.Create(s);

		}


	}
}