using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using XceedTask.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace XceedTask.ViewModels
{
    public class AddProductViewModel
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        [ForeignKey(nameof(Category))]
        public string CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
