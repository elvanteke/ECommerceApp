using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICartRepository : IEntityRepository<Cart>
    {
        //Cart GetByUserId(int userId);
        public Task<Cart> GetByIdAsync(int id);
    }
}
