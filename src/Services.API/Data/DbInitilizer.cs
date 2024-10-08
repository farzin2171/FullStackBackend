using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.API.Entities;
using static System.Net.WebRequestMethods;

namespace Services.API.Data;

    public class DbInitilizer
    {
    public static async Task InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        await seedData(scope.ServiceProvider.GetService<MainDbContext>(), scope.ServiceProvider.GetService<UserManager<User>>());
    }

    private static async Task seedData(MainDbContext context, UserManager<User> userManager)
    {
        await context.Database.MigrateAsync();

        if (!userManager.Users.Any())
        {
            var user = new User
            {
                UserName = "bob",
                Email = "bob@gmail.com"
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Member");

            //var admin = new User
            //{
            //    UserName = "jon",
            //    Email = "jon@gmail.com"
            //};

            //await userManager.CreateAsync(admin, "Pa$$w0rd");
            //await userManager.AddToRolesAsync(admin, new[] { "Admin", "Member" });
        }

        if (context.Products.Any())
        {
            return;
        }

        var categories = await context.Categories.ToListAsync();

        var products =new  List<Product>();

        var productFaker = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription());
           
           


        foreach (var category in categories)
        {
            for (int i = 0; i < 50; i++)
            {
                var randomImage = new Random().Next(1, 1000);
                var product = productFaker.Generate();
                product.Category = category;
                product.CategoryId = category.Id;
                product.Price = 200;
                product.PictureUrl = $"https://picsum.photos/id/{randomImage}/200/300";
                product.Id = Guid.NewGuid();
                product.Type = string.Empty;
                product.Brand = string.Empty;
                product.QuantityInStock = 10;
                products.Add(product);
            }
        }
       

        await context.AddRangeAsync(products);
        context.SaveChanges();
    }
}

