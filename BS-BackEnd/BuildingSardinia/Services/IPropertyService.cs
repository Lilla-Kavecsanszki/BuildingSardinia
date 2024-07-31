using BuildingSardinia.Models;
using System.Threading.Tasks;

namespace BuildingSardinia.Services
{
    public interface IPropertyService
    {
        Task<PropertyListViewModel> GetPropertiesAsync(int page, int pageSize);
    }
}
