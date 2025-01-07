using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICartService
    {
        Task<Cart> GetByUserIdAsync(int userId);
        Task AddToCart(int userId, int productId, int quantity); // Add item to the cart
        Task RemoveFromCart(int userId, int productId); // Remove item from the cart
        Task ClearCartAsync(int userId); // Clear all items from the cart
    }
}
