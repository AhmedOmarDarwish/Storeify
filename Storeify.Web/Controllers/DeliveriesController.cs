using Microsoft.AspNetCore.Mvc;

namespace Storeify.Web.Controllers
{
    public class DeliveriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
