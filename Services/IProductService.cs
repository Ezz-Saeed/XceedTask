using Microsoft.AspNetCore.Mvc.Rendering;
using XceedTask.Models;
using XceedTask.ViewModels;

namespace XceedTask.Services
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(AddProductViewModel viewModel, AppUser appUser);
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<SelectListItem>> GetCategories();
        Task<Product> GetProductById(string id);
        Task<Product> UpdateProduct(UpdateProductViewModel viewModel);
    }
}
