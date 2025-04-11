using Microsoft.AspNetCore.Mvc;

namespace Storeify.Web.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
