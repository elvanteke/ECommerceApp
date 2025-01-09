using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly ICartService _cartService;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        //private readonly AppDbContext _dbContext;

        public OrderService(ICartService cartService, IOrderRepository orderRepository, IProductRepository productRepository)// AppDbContext dbContext)
        {
            _cartService = cartService;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            //_dbContext = dbContext;
        }

        //public async Task PlaceOrderAsync(int userId)
        //{
        //    var cart = await _cartRepository.GetByIdAsync(userId);
        //    if (cart == null || !cart.CartItems.Any())
        //    {
        //        throw new Exception("Cart is empty.");
        //    }

        //    var order = new Order
        //    {
        //        UserId = userId,
        //        OrderDate = DateTime.UtcNow,
        //        TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price),
        //        Status = "Pending",
        //        OrderItems = cart.CartItems.Select(ci => new OrderItem
        //        {
        //            ProductId = ci.ProductId,
        //            Quantity = ci.Quantity,
        //            UnitPrice = ci.Product.Price
        //        }).ToList()
        //    };

        //    await _orderRepository.AddAsync(order);
        //    cart.CartItems.Clear();
        //    await _cartRepository.SaveChangesAsync();
        //}

        public async Task PlaceOrderAsync(int userId)
        {
            var cart = await _cartService.GetByUserIdAsync(userId);
            if (cart == null || !cart.CartItems.Any())
            {
                throw new Exception("Cart is empty.");
            }

            using (var transaction = await _orderRepository.BeginTransactionAsync())
            {
                try
                {
                    // Check stock and update it
                    foreach (var cartItem in cart.CartItems)
                    {
                        var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
                        if (product == null)
                        {
                            throw new Exception($"Product with ID {cartItem.ProductId} not found.");
                        }

                        if (product.Stock < cartItem.Quantity)
                        {
                            throw new Exception($"Insufficient stock for product {product.Name}.");
                        }

                        product.Stock -= cartItem.Quantity; // Deduct stock
                        await _productRepository.UpdateAsync(product); // Save changes
                    }

                    // Create the order
                    var order = new Order
                    {
                        UserId = userId,
                        OrderDate = DateTime.Now,
                        Status = "Pending",
                        TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price),
                        OrderItems = cart.CartItems.Select(ci => new OrderItem
                        {
                            ProductId = ci.ProductId,
                            Quantity = ci.Quantity,
                            UnitPrice = ci.Product.Price
                        }).ToList()
                    };

                    await _orderRepository.AddAsync(order);

                    await _cartService.ClearCartAsync(userId);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Order placement failed: {ex.Message}");
                }
            }
        }


        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }
    }
}
