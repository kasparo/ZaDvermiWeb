using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZaDvermi.Models;

namespace ZaDvermi.Controllers
{
    [Authorize]
    public class BoardController : BaseController
    {

        public ActionResult Index()
        {
            return View(GetPosts(0));
        }

        public PartialViewResult List(int indexFrom)
        {
            return PartialView(GetPosts(indexFrom));
        }

        private List<List<Article>> GetPosts(int indexFrom)
        {
            var model = new List<List<Article>>(4);

            // board
            var posts = from p in Database.Articles
                        where p.ArticleType == ArticleType.Board
                        orderby p.CreateDate descending
                        select p;
          
            var result = posts.Skip(indexFrom).Take(15).ToList();
            var helpNumber = new int[] { 1, 2, 0 };
            foreach (int number in helpNumber)
            {
                var articles = result.Select((value, index) => new { value, index })
                                     .Where(pair => ((pair.index + 1) % 3) == number)
                                     .Select(pair => pair.value);
                model.Add(articles.ToList());
            }

            // notifications
            var notificaions = from p in Database.Articles
                               where p.ArticleType == ArticleType.PrivateNotification
                               orderby p.CreateDate descending
                               select p;
            var filtered = notificaions.ToList().Where(p => (p.ValidFrom.HasValue && p.ValidFrom.Value.Date <= DateTime.Now.Date) &&
                                   (p.ValidTo.HasValue && p.ValidTo.Value.Date >= DateTime.Now.Date));

            model.Add(filtered.ToList());
            return model;
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(FormCollection form)
        {
            string post = form["Post"];
            if (!String.IsNullOrEmpty(post))
            {
                var article = new Article()
                    {
                        CreateDate = DateTime.Now,
                        CreatedBy = UserProvider.GetCurrentUser(),
                        ArticleType = ArticleType.Board,
                        Content = post,
                        Title = String.Empty
                    };

                Database.Entry(article).State = EntityState.Added;
                Database.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator,Manager")]
        public ActionResult Delete(int id)
        {
            var post = Database.Articles.SingleOrDefault(p => p.Id == id);
            if (post != null)
            {
                Database.Articles.Remove(post);
                Database.SaveChanges();
            }
            return View("Index", GetPosts(0));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNotification(FormCollection form)
        {
            string post = form["Post"];
            if (!String.IsNullOrEmpty(post))
            {

                var article = new Article()
                {
                    CreateDate = DateTime.Now,
                    CreatedBy = UserProvider.GetCurrentUser(),
                    ArticleType = ArticleType.PrivateNotification,
                    Content = post,
                    Title = String.Empty
                };
                var validFrom = DateTime.Now;
                DateTime.TryParse(form["ValidFrom"], out validFrom);
                article.ValidFrom = validFrom;
                var validTo= DateTime.Now.AddDays(7);
                DateTime.TryParse(form["ValidTo"], out validTo);
                article.ValidTo = validTo;

                Database.Entry(article).State = EntityState.Added;
                Database.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
