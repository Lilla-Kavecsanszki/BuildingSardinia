using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildingSardinia.Models;
using System.Threading.Tasks;

namespace BuildingSardinia.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Properties
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var properties = await _context.Properties
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await _context.Properties.CountAsync();

            var model = new PropertyListViewModel
            {
                Properties = properties,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize
            };

            return View(model);
        }
    }
}

