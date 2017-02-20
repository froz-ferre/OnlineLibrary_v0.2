using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace OnlineLibrary.Helpers
{
    public static class Paging
    {
        public static MvcHtmlString pagingNavigator(this HtmlHelper helper, int pageNum, int itemCount, int pageSize )
        {
            StringBuilder sb = new StringBuilder();

            int pagesCount = (int)Math.Ceiling((double)itemCount / pageSize);

            if (pageNum > 0)
            {
                sb.Append(helper.ActionLink("< Prev page |", "Index", new { pageNum = pageNum - 1 }));
            }
            else
            {
                sb.Append(HttpUtility.HtmlEncode("< Prev page |"));
            }
            if (pageNum < pagesCount - 1)
            {
                sb.Append(helper.ActionLink("| Next page >", "Index", new { pageNum = pageNum + 1 }));
            }
            else
            {
                sb.Append(HttpUtility.HtmlEncode("| Next page >"));
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}