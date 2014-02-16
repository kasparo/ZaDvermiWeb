using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZaDvermi.Data;
using ZaDvermi.Models;
using ZaDvermi.Security;

namespace ZaDvermi.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult LogOnPanel()
        {
            var user = UserProvider.GetCurrentUser();
            if (user == null)
            {
                return PartialView();
            }
            return PartialView("UserStatus", user);
        }

        public ActionResult LogOn()
        {
            TempData["VisibleLoginPanel"] = false;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogOn(UserLogin model, string returnUrl)
        {
            if (this.ModelState.IsValid && UserProvider.ValidateUser(model))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                if (this.Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Board");
                }
            }
            this.ModelState.AddModelError("", "Chybný email nebo heslo");

            TempData["VisibleLoginPanel"] = false;

            return View(model);
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserProvider.GetUser(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID, FirstName, LastName, NickName, Email, Password, Address, PhoneNumber, Birthday, Color")]User user)
        {
            if (this.ModelState.IsValid)
            {
                Database.Entry(user).State = EntityState.Modified;
                if (String.IsNullOrEmpty(user.Password))
                    Database.Entry(user).Property(x => x.Password).IsModified = false;
                Database.Entry(user).Property(x => x.LastActivity).IsModified = false;
                Database.SaveChanges();

                return RedirectToAction("Index", "Board");
            }
            return View(user);
        }

        [Authorize]
        public ActionResult List()
        {
            return View(GetUsers());
        }

        [Authorize(Roles="Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserProvider.GetUser(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            Database.Users.Remove(user);
            Database.SaveChanges();

            return View("List", GetUsers());
        }

        private List<User> GetUsers()
        {
            var users = from u in Database.Users
                        orderby u.LastName
                        select u;
            return users.ToList();
        }

    }
}
