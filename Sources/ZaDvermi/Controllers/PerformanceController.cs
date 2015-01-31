using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZaDvermi.Models;
namespace ZaDvermi.Controllers
{
    public class PerformanceController : BaseController
    {
        public ActionResult Index()
        {
            var performance = Database.Articles
                .Where(a => a.ArticleType == ArticleType.Performance);

            if (!ZaDvermi.Security.UserProvider.IsAuthenticated)
                performance = performance.Where(a => a.Published);

            ViewBag.Articles = new SelectList(Database.Articles.Where(a => a.ArticleType == ArticleType.PublicArticle), "Id", "Title"); 
            ViewBag.PhotoAlbums = new SelectList(Database.Articles.Where(a => a.ArticleType == ArticleType.PhotoAlbum), "Id", "Title");

            return View(performance.ToList());
        }

        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Add()
        {
            TempData["SelectedPhotoId"] = 0;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Administrator,Manager")]
        public ActionResult Add([Bind(Include = "Title,ValidFrom,Content,Published")]Article performance, int selectedPhotoId)
        {
            if (string.IsNullOrEmpty(performance.Title) || !performance.ValidFrom.HasValue)
            {
                ModelState.AddModelError("", "Místo a datum jsou povinné položky");
                TempData["SelectedPhotoId"] = selectedPhotoId;

                return View(performance);
            }

            Database.Entry(performance).State = System.Data.Entity.EntityState.Added;

            performance.CreateDate = DateTime.Now;
            performance.CreatedBy = UserProvider.GetCurrentUser(false);
            performance.ArticleType = ArticleType.Performance;
            performance.Published = true;
            
            if (selectedPhotoId != 0)
            {
                var articleMedia = new ArticleMedia()
                {
                    ArticleId = performance.Id,
                    MediaItemId = selectedPhotoId
                };
                Database.ArticleMedias.Add(articleMedia);

            }
            Database.SaveChanges();


            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator,Manager")]
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

        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Administrator,Manager")]
        public ActionResult LinkArticle(FormCollection form)
        {
            var idPerformance = int.Parse(form["Id"]);
            var idArticle = form["LinkedArticle"];
            var idPhotoAlbum = form["LinkedPhotoAlbum"];

            var performance = Database.Articles.SingleOrDefault(a => a.Id == idPerformance);
            if (performance != null)
            {
                Database.Entry(performance).Property(x => x.LinkedArticle1Id).IsModified = true;
                Database.Entry(performance).Property(x => x.LinkedArticle2Id).IsModified = true;
                if (!string.IsNullOrEmpty(idArticle))
                    performance.LinkedArticle1Id = int.Parse(idArticle);
                else
                    performance.LinkedArticle1Id = null;

                if (!string.IsNullOrEmpty(idPhotoAlbum))
                    performance.LinkedArticle2Id = int.Parse(idPhotoAlbum);
                else
                    performance.LinkedArticle2Id = null;

                Database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
    }
}
