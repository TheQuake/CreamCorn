using BeechTree.Controllers;
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
            string glyph = string.Empty;
            switch (type.ToLower())
            {
                case "employees":
                    glyph = "glyphicon-user";
                    break;
                case "equipments":
                    glyph = "glyphicon-wrench";
                    break;
                case "invoice":
                    glyph = "glyphicon-list-alt";
                    break;
                case "shifts":
                    glyph = "glyphicon-align-justify";
                    break;
            }
            string s = string.Format("<a data-modal='' href='/job/{0}/{1}' id='{1}' title='{0}'><span class='glyphicon {2}'></span></a>  ", type, jobNumber, glyph);


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