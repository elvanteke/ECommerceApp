using DataAccess.Abstract;
using Entities;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserRepository:IUserRepository
    {
        private readonly AppDbContext _context;

        public EfUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null) return null;

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new AppDbContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public User Get(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.FirstOrDefault(predicate);
        }
    }
}
