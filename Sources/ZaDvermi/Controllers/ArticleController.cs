using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZaDvermi.Models;

namespace ZaDvermi.Controllers
{
    public class ArticleController : BaseController
    {
        public ActionResult Index()
        {
            var articles = Database.Articles
               .Where(a => a.ArticleType == ArticleType.PublicArticle)
               .OrderByDescending(a=>a.CreateDate);

            if (!ZaDvermi.Security.UserProvider.IsAuthenticated)
                articles = articles.Where(a => a.Published).OrderByDescending(a => a.CreateDate);

            return View(articles.ToList());
            
        }

        [Authorize(Roles = "Administrator,Manager,Editor")]
        public ActionResult Add()
        {
            var article = new Article();
            return View("Edit", article);
        }

        [Authorize(Roles = "Administrator,Manager,Editor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = Database.Articles.SingleOrDefault(a => a.Id == id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View("Edit", article);
        }

        [Authorize(Roles = "Administrator,Manager,Editor")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,Published,Preface")]Article article, int selectedPhotoId)
        {
            if (string.IsNullOrEmpty(article.Title))
            {
                ModelState.AddModelError("Title", "Není vyplněn nadpis článku");
                return View("Edit", article);
            }

            bool isNew = article.Id == 0;
            if (isNew)
            {
                article.CreateDate = DateTime.Now;
                article.CreatedBy = UserProvider.GetCurrentUser(false);
                article.ArticleType = ArticleType.PublicArticle;

                Database.Entry(article).State = EntityState.Added;
            }
            else
            {
                Database.Entry(article).State = EntityState.Modified;
                Database.Entry(article).Property(p => p.CreateDate).IsModified = false;
                Database.Entry(article).Property(p => p.CreatedById).IsModified = false;
                Database.Entry(article).Property(p => p.ArticleType).IsModified = false;
            }
            if (selectedPhotoId != 0)
            {
                var articleMedia = Database.ArticleMedias.FirstOrDefault(a => a.ArticleId == article.Id);
                if (articleMedia == null)
                {
                    articleMedia = new ArticleMedia();
                    Database.ArticleMedias.Add(articleMedia);
                }
                articleMedia.ArticleId = article.Id;
                articleMedia.MediaItemId = selectedPhotoId;
            }
            Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator,Manager,Editor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = Database.Articles.SingleOrDefault(a => a.Id == id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            Database.Articles.Remove(article);
            Database.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = Database.Articles.SingleOrDefault(a => a.Id == id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
    }
}
