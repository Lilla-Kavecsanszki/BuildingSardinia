namespace BuildingSardinia.Models
{
    public class Property
    {
        // Primary key
        public int Id { get; set; }  // No default value needed for primary key

        public string Name { get; set; } = string.Empty;  // Default to empty string
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string[] ImageUrls { get; set; } = Array.Empty<string>();  // Default to empty array
        public string Location { get; set; } = string.Empty;
        public string PropertyType { get; set; } = string.Empty;
        public string Features { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
    }
}
