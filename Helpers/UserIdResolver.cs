using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using XceedTask.Models;
using XceedTask.ViewModels;

namespace XceedTask.Helpers
{
    public class UserIdResolver(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor) 
        : IValueResolver<AddProductViewModel, Product, string>
    {
        public string Resolve(AddProductViewModel source, Product destination, string destMember, ResolutionContext context)
        {
            var email =  httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c=>c.Type==ClaimTypes.Email).Value;

            var user = userManager.Users.SingleOrDefault(u=>u.Email == email);
            return user.Id;
        }
    }
}
