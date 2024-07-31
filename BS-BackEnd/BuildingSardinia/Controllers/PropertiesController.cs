using Microsoft.AspNetCore.Mvc;
using BuildingSardinia.Models;
using BuildingSardinia.Services;
using System.Threading.Tasks;

namespace BuildingSardinia.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 9)
        {
            var properties = await _propertyService.GetPropertiesAsync(page, pageSize);
            var model = new PropertyListViewModel
            {
                Properties = properties.Properties,
                TotalCount = properties.TotalCount,
                CurrentPage = page,
                PageSize = pageSize
            };

            return View("PropertyListing", model); // Use PropertyListing view
        }
    }
}
