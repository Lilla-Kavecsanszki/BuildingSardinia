using Microsoft.AspNetCore.Mvc;
using BuildingSardinia.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildingSardinia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PropertiesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Properties?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PropertyListViewModel>> GetProperties(int pageNumber = 1, int pageSize = 10)
        {
            var totalCount = await _context.Properties.CountAsync();
            var properties = await _context.Properties
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var model = new PropertyListViewModel
            {
                Properties = properties,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            return Ok(model);
        }
    }
}
