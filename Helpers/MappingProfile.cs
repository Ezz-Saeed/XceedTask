using AutoMapper;
using XceedTask.Models;
using XceedTask.ViewModels;

namespace XceedTask.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, UpdateProductViewModel>().
                ForMember(dist=>dist.Categories, options=>options.MapFrom<CategoriesResolver>()).
                ReverseMap().ForMember(dist=>dist.UserId, options=>options.MapFrom<UserIdResolver>());
        }
    }
}
