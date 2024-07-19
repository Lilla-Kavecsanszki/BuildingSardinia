using System.Collections.Generic;

namespace BuildingSardinia.Models
{
    public class PropertyListViewModel
    {
        public List<Property> Properties { get; set; } = new List<Property>();
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
