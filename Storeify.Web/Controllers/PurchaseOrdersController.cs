using Microsoft.AspNetCore.Mvc;

namespace Storeify.Web.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
