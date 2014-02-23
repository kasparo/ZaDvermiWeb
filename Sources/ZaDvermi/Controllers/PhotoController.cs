using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZaDvermi.Models;

namespace ZaDvermi.Controllers
{
    public class PhotoController : BaseController
    {
        private string StorageRoot
        {
            get { return Path.Combine(Server.MapPath("~/App_Data/Pictures")); }
        }

        public ActionResult Index()
        {
            return View(GetAlbums());
        }

        private List<Article> GetAlbums()
        {
            var albums = Database.Articles
                                 .Where(a => a.ArticleType == ArticleType.PhotoAlbum)
                                 .OrderByDescending(a => a.CreateDate);
            return albums.ToList();
        }

        [Authorize(Roles = "Administrator,Manager,Editor")]
        public ActionResult Add(int? id)
        {
            var article = new Article();
            if (id != null)
            {
                article = Database.Articles.SingleOrDefault(a => a.Id == id);
                if (article == null)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(article);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Manager,Editor")]
        public ActionResult Add([Bind(Include = "ID, Title, Content")]Article article, string listOfPictures, string album)
        {
            if (String.IsNullOrEmpty(article.Title))
                article.Title = "---";

            bool isNewAlbum = article.Id == 0;
            if (isNewAlbum)
            {
                article.CreateDate = DateTime.Now;
                article.CreatedById = UserProvider.GetCurrentUser(false).Id;
                article.ArticleType = ArticleType.PhotoAlbum;

                Database.Entry(article).State = EntityState.Added;
                Database.Articles.Add(article);
            }
            else
            {
                Database.Configuration.ValidateOnSaveEnabled = false;

                Database.Articles.Attach(article);
                Database.Entry(article).Property(a => a.Title).IsModified = true;
                Database.Entry(article).Property(a => a.Content).IsModified = true;

                Database.SaveChanges();
                Database.Configuration.ValidateOnSaveEnabled = true;
            }

            string[] picturesId = listOfPictures.Split(';');
            foreach (var pictureId in picturesId)
            {
                if(String.IsNullOrEmpty(pictureId))
                    continue;
                
                var articleMedia = new ArticleMedia()
                    {
                        ArticleId = article.Id,
                        MediaItemId = int.Parse(pictureId),
                        Thumbnail = false,
                        Description = String.Empty,
                        Title = String.Empty
                    };
                Database.Entry(articleMedia).State = EntityState.Added;
                Database.ArticleMedias.Add(articleMedia);
            }
            Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator,Manager,Editor")]
        public ActionResult Cancel()
        {
            var orphans =
                Database.MediaItems.Where(
                    p => p.MediaType == MediaType.Photo &&  p.ArticleMedias.Count == 0);
            if (orphans.Any())
            {
                Database.MediaItems.RemoveRange(orphans);
                Database.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult List(int id)
        {
            var article = Database.Articles.SingleOrDefault(m => m.Id == id);
            if (article == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var photos = from m in article.ArticleMedias
                         select m.MediaItem;
            return View(photos.ToList());
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteAlbum(int id)
        {
            var article = Database.Articles.SingleOrDefault(m => m.Id == id);
            if (article == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var photos = from m in article.ArticleMedias
                         select m.MediaItem;

            foreach (var photo in photos)
            {
                var filePath = Path.Combine(StorageRoot, photo.FileName);
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);  
            }
           
            Database.MediaItems.RemoveRange(photos);
            Database.ArticleMedias.RemoveRange(article.ArticleMedias);
            Database.Articles.Remove(article);

            Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            var photo = Database.MediaItems.SingleOrDefault(m => m.Id == id);
            if (photo == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var filePath = Path.Combine(StorageRoot, photo.FileName);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);  

            Database.MediaItems.Remove(photo);
            Database.ArticleMedias.RemoveRange(Database.ArticleMedias.Where(m => m.MediaItemId == id));

            Database.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
