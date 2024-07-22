using Microsoft.AspNetCore.Mvc;

namespace BuildingSardinia.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
