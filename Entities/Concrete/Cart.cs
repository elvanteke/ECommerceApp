using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; } // To track whether the cart is active or checked out
        public List<CartItem> CartItems { get; set; }
    }
}
