using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZaDvermi.Models;

namespace ZaDvermi.Controllers
{
    public class BookController : BaseController
    {
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult List(int indexFrom)
        {
            var items =
                Database.Articles
                        .Where(a => a.ArticleType == ArticleType.Book)
                        .OrderByDescending(a => a.CreateDate)
                        .Skip(indexFrom)
                        .Take(5);

            return PartialView(items.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            if (!String.IsNullOrEmpty(form["Name"]) && !String.IsNullOrEmpty(form["Post"]))
            {
                var article = new Article()
                    {
                        Title = form["Name"],
                        Preface = form["Post"],
                        CreateDate = DateTime.Now,
                        ArticleType = ArticleType.Book 
                    };

                if (ZaDvermi.Security.UserProvider.IsAuthenticated)
                    article.CreatedById = UserProvider.GetCurrentUser(false).Id;

                Database.Articles.Add(article);
                Database.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator,Manager,Editor")]
        public ActionResult Delete(int id)
        {
            var post = Database.Articles.SingleOrDefault(p => p.Id == id);
            if (post != null)
            {
                Database.Articles.Remove(post);
                Database.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
