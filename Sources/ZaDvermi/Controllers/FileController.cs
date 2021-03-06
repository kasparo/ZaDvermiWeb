﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ZaDvermi.Data;
using ZaDvermi.Models;


namespace ZaDvermi.Controllers
{
    public class FileController : BaseController
    {
        #region Private Properties

        private string StorageRoot(MediaType type)
        {
            if (type == MediaType.Photo)
                return Path.Combine(Server.MapPath("~/App_Data/Pictures"));

            return Path.Combine(Server.MapPath("~/App_Data/Documents"));
        }

        #endregion

        [HttpPost]
        [Authorize]
        public ActionResult Upload(MediaType? type)
        {
            MediaType mediaType = type != null ? type.Value : MediaType.Photo;

            var statuses = new List<FileDto>();
            var httpRequest = Request;
            foreach (var file in httpRequest.Files)
            {
                var headers = httpRequest.Headers;
                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {
                    UploadWholeFile(httpRequest, statuses, mediaType);
                }
                else
                {
                    UploadPartialFile(headers["X-File-Name"], httpRequest, statuses, mediaType);
                }

                // database
                foreach (var status in statuses)
                {
                    var mediaItem = new MediaItem()
                        {
                            CreateDate = DateTime.Now,
                            CreatedById = UserProvider.GetCurrentUser(false).Id,
                            MediaType = mediaType,
                            OriginialFileName = status.name,
                            FileName = status.name
                        };
                    Database.MediaItems.Add(mediaItem);
                    Database.SaveChanges();

                    status.id = mediaItem.Id;
                    status.url += String.Format("/{0}", mediaItem.Id);
                    status.deleteUrl += String.Format("/{0}", mediaItem.Id);
                }

                var returnObject = new {files = statuses};
                return Json(returnObject);
            }
            var emptyResult = new {files = statuses};
            return Json(emptyResult);
        }

        [HttpGet]
        public ActionResult Download(int id, bool? thumb)
        {
            var context = HttpContext;
            var mediaItem = Database.MediaItems.SingleOrDefault(m => m.Id == id);
            if (mediaItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var filePath = Path.Combine(StorageRoot(mediaItem.MediaType), mediaItem.FileName);
            if (!System.IO.File.Exists(filePath))
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            const string contentType = "application/octet-stream";

            if (thumb.HasValue && thumb == true)
            {
                var img = new WebImage(filePath);
                if (img.Width > 120)
                    img.Resize(120, 110).Crop(1, 1);
                return new FileContentResult(img.GetBytes(), contentType)
                    {
                        FileDownloadName = mediaItem.FileName
                    };
            }
            else
            {
                return new FilePathResult(filePath, contentType)
                    {
                        FileDownloadName = mediaItem.FileName
                    };
            }

        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            var mediaItem = Database.MediaItems.SingleOrDefault(m => m.Id == id);
            if (mediaItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var filePath = Path.Combine(StorageRoot(mediaItem.MediaType), mediaItem.FileName);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            Database.MediaItems.Remove(mediaItem);
            Database.SaveChanges();

            return Json(new FileDto(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PicturesList()
        {
            var photos = Database.MediaItems.Where(m => m.MediaType == MediaType.Photo);

            var list = from f in photos.ToList()
                       select new
                           {
                               image = Url.Action("Download", "File", new {id = f.Id}),
                               thumb = Url.Action("Download", "File", new {id = f.Id, thumb = true}),
                               folder = ""
                           };

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #region Private
        private void UploadPartialFile(string fileName, HttpRequestBase request, List<FileDto> statuses, MediaType type)
        {
            if (request.Files.Count != 1)
                throw new HttpRequestValidationException(
                    "Attempt to upload chunked file containing more than one fragment per request");
            var file = request.Files[0];
            var inputStream = file.InputStream;

            var fullName = Path.Combine(StorageRoot(type), Path.GetFileName(fileName));

            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }
                fs.Flush();
                fs.Close();
            }
            statuses.Add(new FileDto()
                {
                    name = fileName,
                    size = file.ContentLength,
                    type = file.ContentType,
                    url = Url.Action("Download", "File"),
                    deleteUrl = Url.Action("Delete", "File"),
                    thumbnailUrl = type == MediaType.Photo ? @"data:image/png;base64," + EncodeFile(fullName) : string.Empty,
                    deleteType = "GET",
                });
        }

        private void UploadWholeFile(HttpRequestBase request, List<FileDto> statuses, MediaType type)
        {
            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];

                var fullPath = Path.Combine(StorageRoot(type), Path.GetFileName(file.FileName));

                file.SaveAs(fullPath);

                statuses.Add(new FileDto()
                    {
                        name = file.FileName,
                        size = file.ContentLength,
                        type = file.ContentType,
                        url = Url.Action("Download", "File"),
                        deleteUrl = Url.Action("Delete", "File"),
                        thumbnailUrl = type == MediaType.Photo ? @"data:image/png;base64," + EncodeFile(fullPath) : string.Empty,
                        deleteType = "GET",
                    });
            }
        }

        private string EncodeFile(string fileName)
        {
            WebImage img = new WebImage(fileName);
            if (img.Width > 80)
                img.Resize(80, 60).Crop(1, 1);
            
            return Convert.ToBase64String(img.GetBytes());
        }
        #endregion



    }


   
}
