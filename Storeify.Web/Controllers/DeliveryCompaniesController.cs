using Microsoft.AspNetCore.Mvc;

namespace Storeify.Web.Controllers
{
    public class DeliveryCompaniesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
