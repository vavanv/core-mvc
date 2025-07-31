using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class LLM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Specialization { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CompanyId { get; set; }
        public virtual Company? Company { get; set; }
    }
}