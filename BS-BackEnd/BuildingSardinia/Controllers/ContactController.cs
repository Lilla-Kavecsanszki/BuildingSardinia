using Microsoft.AspNetCore.Mvc;

namespace BuildingSardinia.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            // You can add code here to fetch data if needed
            return View();
        }
    }
}
