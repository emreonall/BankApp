using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task<List<T>> Hepsi();
        IQueryable<T> Queryable(Expression<Func<T,bool>> predicate);
        Task AddAsync(T entity);
        Task  UpdateAsync(int id, T entity);
        Task  DeleteAsync(int id);

    }
}
