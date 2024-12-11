using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using XceedTask.Data;
using XceedTask.Models;
using XceedTask.ViewModels;

namespace XceedTask.Services
{
    public class ProductService(AppDbContext context, IMapper mapper) : IProductService
    {
        public async Task<Product> AddProductAsync(AddProductViewModel viewModel, AppUser appUser)
        {
            Product product = new()
            {
                Name = viewModel.Name,
                Price = viewModel.Price,
                StartDate = viewModel.StartDate,
                Duration = viewModel.Duration,
                CategoryId = viewModel.CategoryId,
                UserId = appUser.Id,
            };
            await context.Products.AddAsync(product);

            var affectedRows = await context.SaveChangesAsync();
            if (affectedRows <= 0)
                return null;
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await context.Products.Include(p=>p.Category).Include(p=> p.User).ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetCategories()
        {
            var categoties = context.Categories.AsQueryable();
            return await categoties.Select(c=> new SelectListItem { Text = c.Name, Value=c.Id}).
                OrderBy(c=>c.Text).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            var product = await context.Products.FindAsync(id);
            if (product is null) return null;
            
            return product;
        }

        public async Task<Product> UpdateProduct(UpdateProductViewModel viewModel)
        {
            var product = mapper.Map<UpdateProductViewModel, Product>(viewModel);

            context.Products.Update(product);
            var result = await context.SaveChangesAsync();

            if (result <= 0) return null;
            return product;
        }
    }
}
