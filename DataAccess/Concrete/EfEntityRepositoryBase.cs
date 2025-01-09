using DataAccess.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfEntityRepositoryBase<T, TContext> : IEntityRepository<T>
        where T : class
        
    {
        public AppDbContext _context;

        public EfEntityRepositoryBase(AppDbContext context)
        {
            _context = context;
        }

        //public void Add(T entity)
        //{
        //    _context.Set<T>().Add(entity);
        //    SaveChanges();
        //}
        public async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null
                ? await _context.Set<T>().ToListAsync()
                : await _context.Set<T>().Where(predicate).ToListAsync();
        }

        

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
