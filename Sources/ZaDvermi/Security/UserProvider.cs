using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using ZaDvermi.Data;
using ZaDvermi.Models;

namespace ZaDvermi.Security
{
    public class UserProvider
    {
        private readonly DataContext _database;

        public UserProvider(DataContext database)
        {
            _database = database;
        }

        public bool ValidateUser(UserLogin userLogin)
        {
            var user = _database.Users.SingleOrDefault(u => u.Email.ToLower() == userLogin.UserName.ToLower());
            if (user == null)
                return false;

            if (!userLogin.Password.Equals(user.Password))
                return false;

            return true;
        }

        public User GetCurrentUser(bool setActivity)
        {
            IPrincipal userPrincipal = HttpContext.Current.User;
            if (!userPrincipal.Identity.IsAuthenticated)
            {
                return null;
            }
            var user =
                _database.Users.SingleOrDefault(u => u.Email.Trim().ToLower() == userPrincipal.Identity.Name.ToLower());
            if (user == null)
            {
                FormsAuthentication.SignOut();
                return null;
            }

            if (setActivity)
            {
                _database.Configuration.ValidateOnSaveEnabled = false;
                user.LastActivity = DateTime.Now;
                _database.Entry(user).Property(x => x.LastActivity).IsModified = true;

                _database.SaveChanges();
                _database.Configuration.ValidateOnSaveEnabled = true;
            }

            if (HttpContext.Current.Session != null)
                HttpContext.Current.Session["CurrentUser"] = user;

            return user;
        }

        public static User GetStoredUser()
        {
            var user = HttpContext.Current.Session["CurrentUser"] as User;
            return user;
        }

        public User GetUser(int id)
        {
            var user = _database.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
                return null;
            return user;
        }

        public static bool IsAuthenticated
        {
            get
            {
                IPrincipal userPrincipal = HttpContext.Current.User;
                return userPrincipal.Identity.IsAuthenticated;

            }
        }

    }
}