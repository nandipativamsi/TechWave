using Microsoft.EntityFrameworkCore;
using TechWave.Models.DomainModel;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

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

                // Fetch the categories from the database to get the correct IDs
                var categoriesFromDb = context.Categories.ToList();

                // Seed Products
                var products = new Product[]
                {
                    new Product
                    {
                        Name = "Dell XPS 13",
                        Description = "13-inch laptop with high performance",
                        Brand = "Dell",
                        LongDescription = "The Dell XPS 13 is a compact laptop featuring an 8GB RAM and a 256GB SSD. Its sleek design and powerful performance make it ideal for both professional and personal use.",
                        Specifications = "8GB RAM, 256GB SSD",
                        Price = 999.99M,
                        CategoryID = categoriesFromDb.First(c => c.Name == "Laptops").CategoryID,
                        ImageURL = "/images/dell-xps-13.jpg"
                    },
                    new Product
                    {
                        Name = "MacBook Pro",
                        Description = "16-inch laptop with exceptional power",
                        Brand = "Apple",
                        LongDescription = "The MacBook Pro features a 16-inch Retina display and powerful performance with 16GB RAM and a 512GB SSD. Perfect for professionals needing high processing power.",
                        Specifications = "16GB RAM, 512GB SSD",
                        Price = 2399.99M,
                        CategoryID = categoriesFromDb.First(c => c.Name == "Laptops").CategoryID,
                        ImageURL = "/images/macbook-pro.jpg"
                    },
                    new Product
                    {
                        Name = "iPhone 14",
                        Description = "Latest Apple smartphone with advanced features",
                        Brand = "Apple",
                        LongDescription = "The iPhone 14 comes with a 128GB storage capacity, a powerful A15 Bionic chip, and a sleek design. It offers a great camera system and a high-resolution display.",
                        Specifications = "128GB Storage",
                        Price = 999.99M,
                        CategoryID = categoriesFromDb.First(c => c.Name == "Smartphones").CategoryID,
                        ImageURL = "/images/iphone-14.jpg"
                    },
                    new Product
                    {
                        Name = "Samsung Galaxy S21",
                        Description = "Latest Samsung smartphone with a stunning display",
                        Brand = "Samsung",
                        LongDescription = "The Samsung Galaxy S21 features a 128GB storage option and a dynamic AMOLED display. It offers excellent performance with a high refresh rate and advanced camera capabilities.",
                        Specifications = "128GB Storage",
                        Price = 799.99M,
                        CategoryID = categoriesFromDb.First(c => c.Name == "Smartphones").CategoryID,
                        ImageURL = "/images/samsung-galaxy-s21.jpg"
                    },
                    new Product
                    {
                        Name = "iPad Pro",
                        Description = "12.9-inch tablet with high resolution",
                        Brand = "Apple",
                        LongDescription = "The iPad Pro 12.9-inch model comes with 128GB storage and a Liquid Retina display. It is perfect for professionals and creatives needing a powerful and versatile device.",
                        Specifications = "128GB Storage",
                        Price = 1099.99M,
                        CategoryID = categoriesFromDb.First(c => c.Name == "Tablets").CategoryID,
                        ImageURL = "/images/ipad-pro.jpg"
                    },
                    new Product
                    {
                        Name = "AirPods Pro",
                        Description = "Wireless earbuds with noise cancellation",
                        Brand = "Apple",
                        LongDescription = "AirPods Pro offer active noise cancellation and a comfortable fit. With a powerful H1 chip and adaptive EQ, they deliver high-quality sound and seamless integration with Apple devices.",
                        Specifications = "Noise Cancelling",
                        Price = 249.99M,
                        CategoryID = categoriesFromDb.First(c => c.Name == "Accessories").CategoryID,
                        ImageURL = "/images/airpods-pro.jpg"
                    },

                    // New Products
                    new Product
                    {
                        Name = "Google Pixel 6",
                        Description = "Latest Google smartphone with an excellent camera",
                        Brand = "Google",
                        LongDescription = "The Google Pixel 6 features a 128GB storage capacity and a new Tensor chip for advanced performance. It offers an exceptional camera system and a clean Android experience.",
                        Specifications = "128GB Storage",
                        Price = 899.99M,
                        CategoryID = categoriesFromDb.First(c => c.Name == "Smartphones").CategoryID,
                        ImageURL = "/images/google-pixel-6.jpg"
                    },
                    new Product
                    {
                        Name = "Microsoft Surface Pro 7",
                        Description = "2-in-1 laptop with versatile functionality",
                        Brand = "Microsoft",
                        LongDescription = "The Microsoft Surface Pro 7 combines the power of a laptop with the portability of a tablet. It includes 8GB RAM and a 256GB SSD, offering great flexibility for productivity on the go.",
                        Specifications = "8GB RAM, 256GB SSD",
                        Price = 1199.99M,
                        CategoryID = categoriesFromDb.First(c => c.Name == "Laptops").CategoryID,
                        ImageURL = "/images/surface-pro-7.jpg"
                    },
                    new Product
                    {
                        Name = "Samsung Galaxy Tab S7",
                        Description = "11-inch tablet with high performance",
                        Brand = "Samsung",
                        LongDescription = "The Samsung Galaxy Tab S7 features a high-resolution 11-inch display and 128GB storage. It offers powerful performance and is ideal for both entertainment and productivity.",
                        Specifications = "128GB Storage",
                        Price = 649.99M,
                        CategoryID = categoriesFromDb.First(c => c.Name == "Tablets").CategoryID,
                        ImageURL = "/images/galaxy-tab-s7.jpg"
                    },
                    new Product
                    {
                        Name = "Bose QuietComfort 35",
                        Description = "Wireless noise-cancelling headphones with long battery life",
                        Brand = "Bose",
                        LongDescription = "The Bose QuietComfort 35 offers world-class noise cancellation and up to 20 hours of battery life. It provides a comfortable fit and superior sound quality for an immersive audio experience.",
                        Specifications = "20 hours battery life",
                        Price = 299.99M,
                        CategoryID = categoriesFromDb.First(c => c.Name == "Accessories").CategoryID,
                        ImageURL = "/images/bose-quietcomfort-35.jpg"
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
