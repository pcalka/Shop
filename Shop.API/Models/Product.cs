using System.Collections.Generic;

namespace Shop.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Descrption { get; set; }
        public float Price { get; set; }
        public List<string> PhotoUrl { get; set; }
    }
}
