using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZaDvermi.Common;
using ZaDvermi.Models;

namespace ZaDvermi.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            var contentItems = new Dictionary<string, List<Article>>();

            // articles
            var articles = Database.Articles
                                   .Where(a => a.ArticleType == ArticleType.PublicArticle && a.Published == true)
                                   .OrderByDescending(a => a.CreateDate)
                                   .Take(3);
            contentItems.Add("Article", articles.ToList());

            // performance
            var performance = Database.Articles
                                      .Where(
                                          a =>
                                          a.ArticleType == ArticleType.Performance && a.Published == true &&
                                          a.ValidFrom >= DateTime.Now)
                                      .OrderBy(a => a.ValidFrom);

            contentItems.Add("Performance", performance.Take(1).ToList());

            // visit book
            var book = Database.Articles
                               .Where(
                                   a =>
                                   a.ArticleType == ArticleType.Book && a.CreatedById == null)
                               .OrderBy(a => Guid.NewGuid())
                               .Take(10);
            var list = book.ToList();
            list.Shuffle();
            contentItems.Add("Book", list);

            return View(contentItems);
        }


        public ActionResult Contact()
        {
            return View();
        }
    }

}
