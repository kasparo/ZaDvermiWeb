using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZaDvermi.Data;
using ZaDvermi.Models;

namespace ZaDvermi.Controllers
{
    public class UserController : BaseController
    {
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogOn(User model, string returnUrl)
        {
            if (this.ModelState.IsValid && Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                if (this.Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            this.ModelState.AddModelError("", "Incorrect user name or password.");
            return View(model);
        }


    }
}
