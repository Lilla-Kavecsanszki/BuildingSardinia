namespace BuildingSardinia.Models
{
    public class ContactForm
    {
        public int Id { get; set; }  // Primary key
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
