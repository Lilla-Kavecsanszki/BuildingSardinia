using Microsoft.AspNetCore.Mvc;
using BuildingSardinia.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildingSardinia.Controllers
{
    public class HomeController : Controller
    {
        // Removed DbContext dependency
        // private readonly ApplicationDbContext _context;

        // Constructor without DbContext
        // public HomeController(ApplicationDbContext context)
        // {
        //     _context = context;
        // }

        // Default constructor
        public HomeController()
        {
        }

        public async Task<IActionResult> Index()
        {
            // Static data instead of database call
            var properties = new List<Property>
            {
                new Property { Id = 1, Name = "Sample Property 1", Location = "Location 1", Description = "Description 1" },
                new Property { Id = 2, Name = "Sample Property 2", Location = "Location 2", Description = "Description 2" },
                new Property { Id = 3, Name = "Sample Property 3", Location = "Location 3", Description = "Description 3" }
            };

            var model = new PropertyListViewModel
            {
                Properties = properties,
                TotalCount = properties.Count
            };

            return View(model);
        }
    }
}
