using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZaDvermi.Controllers
{
    public class PerformanceController : BaseController
    {
        //
        // GET: /Performance/

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Add()
        {
            return View();
        }

    }
}
