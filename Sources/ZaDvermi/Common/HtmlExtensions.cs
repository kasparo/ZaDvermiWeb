using System;
using System.Web.Mvc;
using System.Linq;
namespace ZaDvermi.Common
{
    public static class HtmlExtensions
    {
        public static bool IsAccessible(this HtmlHelper html, params string[] roles)
        {
            return roles.Any(r => html.ViewContext.RequestContext.HttpContext.User.IsInRole(r));
        }
    }
}