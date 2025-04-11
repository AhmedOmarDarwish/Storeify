using Microsoft.AspNetCore.Mvc;

namespace Storeify.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
