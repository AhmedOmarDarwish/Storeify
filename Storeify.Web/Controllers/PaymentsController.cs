using Microsoft.AspNetCore.Mvc;

namespace Storeify.Web.Controllers
{
    public class PaymentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
