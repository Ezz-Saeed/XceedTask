using Microsoft.AspNetCore.Mvc.Rendering;
using XceedTask.Models;
using XceedTask.ViewModels;

namespace XceedTask.Services
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(AddProductViewModel viewModel, AppUser appUser);
        Task<IEnumerable<SelectListItem>> GetCategories();
    }
}
