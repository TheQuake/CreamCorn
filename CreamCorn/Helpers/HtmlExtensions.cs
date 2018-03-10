using CreamCorn.Controllers;
using CreamCorn.DAL;
using CreamCorn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreamCorn
{
	public static class HtmlExtensions
	{

        public static MvcHtmlString IconFor(this HtmlHelper helper, int companyId, string type)
        {
            DbContext_CreamCorn db = new DbContext_CreamCorn();
            int count = 0;

            string empty = string.Empty;
            string title = string.Empty;
            string glyph = string.Empty;

            switch (type.ToLower())
            {
                case "categories":
                    count = db.CompanyCategories.Where(x => x.CompanyId.Equals(companyId)).Count();
                    glyph = "glyphicon-tags";
                    empty = "There are no categories entered for this company";
                    title = "Categories";
                    break;
                case "contacts":
                    count = db.Contacts.Where(x => x.CompanyId.Equals(companyId)).Count();
                    glyph = "glyphicon-user";
                    empty = "There are no contacts entered for this company";
                    title = "Contacts";
                    break;
            }

            string s = string.Empty;
            if (count > 0)
            {
                s = string.Format("<a data-modal='' href='/company/{0}/{1}' id='{1}' title='{2}'><span class='glyphicon {3}'></span></a>  ", type, companyId, title, glyph);
            }
            else
            {
                s = string.Format("<span class='glyphicon {0}' title='{1}'></span></a>  ", glyph, empty);
            }


            return MvcHtmlString.Create(s);
        }

        public static MvcHtmlString DeleteIconFor(this HtmlHelper helper, int companyId, string type)
        {
			string s = "<a id='btnDelete' class='glyphicon glyphicon-trash' href='javascript:void(0);' style='text-decoration:none;padding-left:12px;' title='Delete' onClick='deleteDocument(companyId);'></a>";
            return MvcHtmlString.Create(s);
        }

		public static MvcHtmlString EditIconFor(this HtmlHelper helper, int companyId, string type)
		{
			string s = string.Format("<span style=padding-left:10px;></span><a data-modal='' href='/company/edit/{0}' id='{0}' title='Edit'><span class='glyphicon glyphicon-edit'></span></a>  ", companyId);
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