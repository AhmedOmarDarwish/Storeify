using Microsoft.AspNetCore.Mvc;

namespace Storeify.Web.Controllers
{
    public class SuppliersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
