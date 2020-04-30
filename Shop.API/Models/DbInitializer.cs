using System.Linq;

namespace Shop.API.Models
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Products.Any())
            {
                context.AddRange(
                        new Product { Name = "Ruler", Price = 1 },
                        new Product { Name = "Pencilecase", Price = 2 },
                        new Product { Name = "Pen", Price = 1 },
                        new Product { Name = "Book", Price = 10 },
                        new Product { Name = "Box", Price = 5 }
                    );
            }
            context.SaveChanges();
        }       
    }
}
