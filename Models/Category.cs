using System.ComponentModel.DataAnnotations;

namespace XceedTask.Models
{
    public class Category
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
