using Microsoft.AspNetCore.Mvc;

namespace Storeify.Web.Controllers
{
    public class DiscountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
