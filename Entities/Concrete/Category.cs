using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } // Category Name
        // Navigation Property
        //public ICollection<Product> Products { get; set; }
    }
}
