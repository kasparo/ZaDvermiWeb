
using System.Web.Mvc;
using ZaDvermi.Data;
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
    }
}