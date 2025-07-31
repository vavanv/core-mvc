using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<LLM> LLMs { get; set; } = new List<LLM>();
        public virtual ICollection<Chatbot> Chatbots { get; set; } = new List<Chatbot>();
    }
}