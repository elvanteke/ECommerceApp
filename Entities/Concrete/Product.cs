using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } // Product Name
        public string Description { get; set; } // Brief Description
        public decimal Price { get; set; } // Product Price
        public int Stock { get; set; } // Stock Count
        public int CategoryId { get; set; } // Foreign Key to Category
        // Navigation Property
        //[JsonIgnore]
        //public Category Category { get; set; }
    }
}
