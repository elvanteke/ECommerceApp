using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        Task PlaceOrderAsync(int userId);
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    }
}
