using Microsoft.EntityFrameworkCore;
using TechWave.Models.DomainModel;

namespace TechWave.Models.SeedData
{
    public class SeedTechWaveData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TechWaveDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<TechWaveDBContext>>()))
            {
                // Look for any existing data
                if (context.Products.Any() || context.Categories.Any())
                {
                    return;   // DB has been seeded
                }

                // Seed Categories
                var categories = new Category[]
                {
                    new Category { Name = "Laptops", Description = "Portable computers" },
                    new Category { Name = "Smartphones", Description = "Mobile phones with advanced features" },
                    new Category { Name = "Tablets", Description = "Touchscreen devices" },
                    new Category { Name = "Accessories", Description = "Electronic accessories" }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();

                // Seed Products
                var products = new Product[]
                {
                    new Product { Name = "Dell XPS 13", Description = "13-inch laptop", Specifications = "8GB RAM, 256GB SSD", Price = 999.99M, CategoryID = categories[0].CategoryID, ImageURL = "dell-xps-13.jpg" },
                    new Product { Name = "MacBook Pro", Description = "16-inch laptop", Specifications = "16GB RAM, 512GB SSD", Price = 2399.99M, CategoryID = categories[0].CategoryID, ImageURL = "macbook-pro.jpg" },
                    new Product { Name = "iPhone 14", Description = "Latest Apple smartphone", Specifications = "128GB Storage", Price = 999.99M, CategoryID = categories[1].CategoryID, ImageURL = "iphone-14.jpg" },
                    new Product { Name = "Samsung Galaxy S21", Description = "Latest Samsung smartphone", Specifications = "128GB Storage", Price = 799.99M, CategoryID = categories[1].CategoryID, ImageURL = "samsung-galaxy-s21.jpg" },
                    new Product { Name = "iPad Pro", Description = "12.9-inch tablet", Specifications = "128GB Storage", Price = 1099.99M, CategoryID = categories[2].CategoryID, ImageURL = "ipad-pro.jpg" },
                    new Product { Name = "AirPods Pro", Description = "Wireless earbuds", Specifications = "Noise Cancelling", Price = 249.99M, CategoryID = categories[3].CategoryID, ImageURL = "airpods-pro.jpg" }
                };

                context.Products.AddRange(products);
                context.SaveChanges();

            }
        }
     }
}
