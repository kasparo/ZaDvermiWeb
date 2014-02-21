using System.Collections.Generic;
using System.Linq;
using System;
using System.Web.Mvc;
using ZaDvermi.Data;
using ZaDvermi.Models;
using ZaDvermi.Security;

namespace ZaDvermi.Controllers
{
    public class BaseController : Controller
    {
        private readonly DataContext _database = new DataContext();

        protected DataContext Database
        {
            get { return _database; }
        }

        private UserProvider _userProvider;

        protected UserProvider UserProvider
        {
            get
            {
                if (_userProvider == null)
                    _userProvider = new UserProvider(Database);
                return _userProvider;
            }
        }

        public ActionResult PhotoSelector()
        {
            var list = new List<MediaItem>();
            var random = new Random();
            int max = Database.MediaItems.Count();
            do
            {
                var number = random.Next(0, max - 1);
                var item = Database.MediaItems
                                   .Where(m => m.MediaType == MediaType.Photo)
                                   .OrderBy(m => m.CreateDate)
                                   .Skip(number)
                                   .Take(1)
                                   .First();
                if (!list.Exists(m => m.Id == item.Id))
                    list.Add(item);
                if (list.Count() >= 8 || list.Count() == max)
                    break;
            } while (true);

            return View("PhotoSelector", list);
        }
    }
}