using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Models
{
    public class DbInitializer
    {
        public void Seed(AppDbContext context)
        {
            if (context.Products == null)
            {
                context.AddRange(
                        new Product { Id = 1, Name = "Ruler", Price = 1 },
                        new Product { Id = 2, Name = "Pencilecase", Price = 2 },
                        new Product { Id = 3, Name = "Pen", Price = 1 },
                        new Product { Id = 4, Name = "Book", Price = 10 },
                        new Product { Id = 5, Name = "Box", Price = 5 }
                    );
            }
            context.SaveChanges();
        }
       
    }
}
