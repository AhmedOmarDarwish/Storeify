using Microsoft.AspNetCore.Mvc;

namespace Storeify.Web.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
