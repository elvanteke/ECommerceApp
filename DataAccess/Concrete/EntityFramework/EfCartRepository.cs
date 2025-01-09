using DataAccess.Abstract;
using Entities;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCartRepository : EfEntityRepositoryBase<Cart, AppDbContext>, ICartRepository
    {
        public EfCartRepository(AppDbContext context) : base(context)
        {
        }

        //public Cart GetByUserId(int userId)
        //{
        //    return _context.Carts.Include(c => c.CartItems)
        //                         .ThenInclude(ci => ci.Product)
        //                         .FirstOrDefault(c => c.UserId == userId && c.IsActive);
        //}

        public async new Task<Cart> GetByIdAsync(int id)
        {
            return await _context.Carts.Include(c => c.CartItems)
                                       .ThenInclude(ci => ci.Product)
                                       .FirstOrDefaultAsync(c => c.UserId == id && c.IsActive);
        }
    }
}
