using Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User product);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(int id);
        List<OperationClaim> GetClaims(User user);
        User Get(Expression<Func<User, bool>> predicate);
    }
}
