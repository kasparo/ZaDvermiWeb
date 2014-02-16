using System.Web.Security;
using ZaDvermi.Data;
using System.Linq;

namespace ZaDvermi.Security
{
    public class RoleValidator : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            using (var dataContext = new DataContext())
            {
                var user = dataContext.Users.SingleOrDefault(u => u.Email == username);
                if (user == null)
                    return false;
                return user.MemberShips != null && user.MemberShips.Select(
                     u => u.UserGroup).Any(r => r.Name == roleName);
            }
        }
        
        public override string[] GetRolesForUser(string username)
        {
            using (var dataContext = new DataContext())
            {
                var user = dataContext.Users.SingleOrDefault(u => u.Email == username);
                if (user == null)
                    return new string[] { };
                return user.MemberShips == null ? new string[] { } :
                  user.MemberShips.Select(u => u.UserGroup).Select(u => u.Name).ToArray();
            }
        }

        public override string[] GetAllRoles()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.UserGroups.Select(r => r.Name).ToArray();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }



    }
}