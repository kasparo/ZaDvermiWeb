using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZaDvermi.Models;

namespace ZaDvermi.Controllers
{
    public class ProductionController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Manager,Editor")]
        public ActionResult Add()
        {
            var productionItem = new Article();
            return View("Edit", productionItem);
        }
    }
}
