using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        //public Cart GetCartByUserId(int userId)
        //{
        //    return _cartRepository.GetByUserId(userId);
        //}

        public async Task<Cart> GetByUserIdAsync(int userId)
        {
            var cart = await _cartRepository.GetByIdAsync(userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId, CartItems = new List<CartItem>() };
                await _cartRepository.AddAsync(cart);
            }

            return cart;

        }

        public async Task AddToCart(int userId, int productId, int quantity)
        {
            var cart = await _cartRepository.GetByIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    IsActive = true,
                    CartItems = new List<CartItem>()
                };
                await _cartRepository.AddAsync(cart);
            }

            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            await _cartRepository.SaveChangesAsync();
        }

        public async Task RemoveFromCart(int userId, int productId)
        {
            var cart = await _cartRepository.GetByIdAsync(userId);
            if (cart != null)
            {
                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (cartItem != null)
                {
                    cart.CartItems.Remove(cartItem);
                    await _cartRepository.SaveChangesAsync();
                }
            }
        }

        

        public async Task ClearCartAsync(int userId)
        {
            var cart = await _cartRepository.GetByIdAsync(userId); // Await the asynchronous method
            if (cart != null) // Check if the cart exists
            {
                cart.CartItems.Clear(); // Clear the items in the cart
                await _cartRepository.UpdateAsync(cart); // Update the cart
            }
            else
            {
                throw new Exception("Cart not found."); // Handle the case where the cart does not exist
            }
        }
    }
}
