using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
