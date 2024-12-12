using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using XceedTask.Models;
using XceedTask.ViewModels;

namespace XceedTask.Services
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(AddProductViewModel viewModel, AppUser appUser);
        Task<IEnumerable<Product>> GetProducts(Expression<Func<Product, bool>>? expression);
        Task<IEnumerable<SelectListItem>> GetCategories();
        Task<Product> GetProductById(string id);
        Task<Product> UpdateProduct(Product product);
        Task<bool> Delete(UpdateProductViewModel viewModel);
    }
}
