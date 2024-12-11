using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc.Rendering;
using XceedTask.Models;
using XceedTask.Services;
using XceedTask.ViewModels;

namespace XceedTask.Helpers
{

    public class CategoriesResolver(IProductService productService) : IValueResolver<Product, AddProductViewModel, IEnumerable<SelectListItem>>
    {
        public  IEnumerable<SelectListItem> Resolve(Product source, AddProductViewModel destination, 
            IEnumerable<SelectListItem> destMember, ResolutionContext context)
        {
            return  productService.GetCategories().Result;
        }
    }
}
