using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Services.API.Entities;


namespace Services.API.Data;

public class MainDbContext : IdentityDbContext<User>
{
    public MainDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Product> Products { get; set; }

    public DbSet<Basket> Baskets { get; set; }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>()
            .HasData(
              new IdentityRole { Name = "Member", NormalizedName = "MEMBER" },
              new IdentityRole { Name = "Admin", NormalizedName = "Admin" }
            );

        builder.Entity<Category>()
            .HasData(
             new Category { Id = Guid.NewGuid(), Name = "Electronics & Gadgets" },
             new Category { Id = Guid.NewGuid(), Name = "Health & Wellness" },
             new Category { Id = Guid.NewGuid(), Name = "Fashion & Apparel" },
             new Category { Id = Guid.NewGuid(), Name = "Electronics & Gadgets" },
             new Category { Id = Guid.NewGuid(), Name = "Home & Kitchen Essentials" },
             new Category { Id = Guid.NewGuid(), Name = "Beauty & Personal Care" },
             new Category { Id = Guid.NewGuid(), Name = "Sports & Fitness Gear" },
             new Category { Id = Guid.NewGuid(), Name = "Toys & Games" },
             new Category { Id = Guid.NewGuid(), Name = "Books & Stationery" }
            );


    }
}

