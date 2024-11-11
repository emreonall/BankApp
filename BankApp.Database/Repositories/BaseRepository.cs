using BankApp.Database.Context;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;// => _context.Set<T>();

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null)
            {
                throw new Exception($"{id} id'li kayıt bulunamadı.");
            }
            return result;
        }

        public IQueryable<T> Queryable(Expression<Func<T, bool>> predicate)
        {
            return (IQueryable<T>)_dbSet.Where(predicate).AsQueryable().AsNoTracking().ToList();


        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception) { }

        }

        public async Task UpdateAsync(int id, T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

        }

        public IQueryable<T> GetAll()
        {
            var result = _dbSet.AsNoTracking();

            return result;
        }

        public List<T> Hepsi()
        {
            return _dbSet.ToList();
        }
    }
}
