using BuildingSardinia.Models; // Correct namespace
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingSardinia.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly ApplicationDbContext _context;

        public PropertyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PropertyListViewModel> GetPropertiesAsync(int page, int pageSize)
        {
            var query = _context.Properties.AsQueryable();

            var totalCount = await query.CountAsync();
            var properties = await query
                .OrderBy(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PropertyListViewModel
            {
                Properties = properties,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize
            };
        }
    }
}
