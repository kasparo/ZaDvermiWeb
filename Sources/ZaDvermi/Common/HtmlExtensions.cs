using System;
using System.Collections.Generic;
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

        public static bool IsAuthor(this HtmlHelper html, ZaDvermi.Models.User creator)
        {
            return creator.Email.Equals(html.ViewContext.RequestContext.HttpContext.User.Identity.Name, StringComparison.CurrentCultureIgnoreCase);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            var rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}