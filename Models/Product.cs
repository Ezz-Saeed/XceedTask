using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XceedTask.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        [ForeignKey(nameof(Category))]
        public string CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
