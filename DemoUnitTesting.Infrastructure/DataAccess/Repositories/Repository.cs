using DemoUnitTesting.Core.Entities;
using DemoUnitTesting.Core.Interfaces;
using DemoUnitTesting.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoUnitTesting.Infrastructure.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity).ConfigureAwait(false);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities)
        {
            await _db.Set<T>().AddRangeAsync(entities).ConfigureAwait(false);
            await _db.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            _db.Set<T>().RemoveRange(entities);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<T> entities)
        {
            foreach (T entity in entities) _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var result = Where(predicate).AsEnumerable();
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = _db.Set<T>().AsEnumerable();
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _db.Set<T>().FindAsync(id).ConfigureAwait(false);
        }

        private IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _db.Set<T>().Where(predicate);
        }

        public async Task<T> First(Expression<Func<T, bool>> predicate)
        {
            return await _db.Set<T>().FirstOrDefaultAsync(predicate);
        }
    }
}
