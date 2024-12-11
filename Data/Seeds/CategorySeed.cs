using XceedTask.Models;

namespace XceedTask.Data.Seeds
{
    public static class CategorySeed
    {
        public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory)
        {
            if(!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category{Name="C1"},
                    new Category{Name="C2"},
                    new Category{Name="C3"},
                    new Category{Name="C4"}
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
        }
    }
}
