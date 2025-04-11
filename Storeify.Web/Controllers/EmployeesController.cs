using Microsoft.AspNetCore.Mvc;

namespace Storeify.Web.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
