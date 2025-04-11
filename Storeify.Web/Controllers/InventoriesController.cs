using Microsoft.AspNetCore.Mvc;

namespace Storeify.Web.Controllers
{
    public class InventoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
