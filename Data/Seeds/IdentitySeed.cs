using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XceedTask.Models;

namespace XceedTask.Data.Seeds
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager,
            AppDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    var roles = new List<IdentityRole>
                    {
                         new IdentityRole()
                         {
                             Id = Guid.NewGuid().ToString(),
                             Name = "admin",
                             NormalizedName = "admin".ToUpper(),
                             ConcurrencyStamp = Guid.NewGuid().ToString(),
                         },
                          new IdentityRole()
                         {
                             Id = Guid.NewGuid().ToString(),
                             Name = "user",
                             NormalizedName = "user".ToUpper(),
                             ConcurrencyStamp = Guid.NewGuid().ToString(),
                         }
                    };

                    foreach (var role in roles)
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

                if (!userManager.Users.Any())
                {
                    var appUser = new AppUser { Id = Guid.NewGuid().ToString(), Email = "admin@test.com", UserName = "Admin" };
                    await userManager.CreateAsync(appUser, "Ab$$123");

                    var adminRole = await roleManager.Roles.SingleOrDefaultAsync(r => r.Name == "admin");
                    var userRole = new IdentityUserRole<string> { RoleId = adminRole!.Id, UserId = appUser.Id };
                    await context.UserRoles.AddAsync(userRole);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var roleLogger = loggerFactory.CreateLogger<RoleManager<IdentityRole>>();
                var userLogger = loggerFactory.CreateLogger<UserManager<AppUser>>();
                var logger = loggerFactory.CreateLogger<AppDbContext>();

                logger.LogError(ex.Message);
                roleLogger.LogError(ex.Message);
                userLogger.LogError(ex.Message);
            }
        }
    }
}
