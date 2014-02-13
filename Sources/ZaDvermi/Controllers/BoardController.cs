using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZaDvermi.Models;

namespace ZaDvermi.Controllers
{
    public class BoardController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View(GetPosts(0));
        }

        [Authorize]
        public PartialViewResult List(int indexFrom)
        {
            return PartialView(GetPosts(indexFrom));
        }

        private List<List<Article>> GetPosts(int indexFrom)
        {
            var posts = from p in Database.Articles
                        where p.ArticleType == ArticleType.Board
                        orderby p.CreateDate descending
                        select p;
            var model = new List<List<Article>>(3);

            var result = posts.Skip(indexFrom).Take(3).ToList();
            var helpNumber = new int[] {1, 2, 0};
            foreach (int number in helpNumber)
            {
                var articles = result.Select((value, index) => new {value, index})
                                     .Where(pair => ((pair.index + 1)%3) == number)
                                     .Select(pair => pair.value);
                model.Add(articles.ToList());
            }
            return model;
        }

        [HttpPost]
        [Authorize]
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

       
    }
}
