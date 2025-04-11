using Microsoft.AspNetCore.Mvc;

namespace Storeify.Web.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
