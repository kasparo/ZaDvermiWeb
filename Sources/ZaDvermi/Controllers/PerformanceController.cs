﻿using System;
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
        //
        // GET: /Performance/

        public ActionResult Index()
        {
            var performance = Database.Articles
                .Where(a => a.ArticleType == ArticleType.Performance);
                
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
        [Authorize]
        public ActionResult Add([Bind(Include = "Title,ValidFrom,Content")]Article performance, int selectedPhotoId)
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
    }
}
