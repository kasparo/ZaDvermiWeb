
using System.Web.Mvc;
using ZaDvermi.Data;

namespace ZaDvermi.Controllers
{
    public class BaseController : Controller
    {
        private readonly DataContext _database = new DataContext();
        protected DataContext Database { get { return _database; } }
    }
}