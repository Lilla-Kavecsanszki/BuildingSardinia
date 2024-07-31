namespace BuildingSardinia.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string PropertyType { get; set; } = string.Empty;
        public string Features { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Bedrooms { get; set; } = 0;
        public int Bathrooms { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public decimal Price { get; set; }
        public int Size { get; set; } // Assuming size in square feet/meters
    }
}
