using Microsoft.AspNetCore.Mvc;

namespace Storeify.Web.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
