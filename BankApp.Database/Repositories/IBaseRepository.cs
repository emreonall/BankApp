using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<Result<T>> GetByIdAsync(int id);
        Task<Result<List<T>>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<Result<T>> AddAsync(T entity);
        Result<T> Update(int id, T entity);
        Task<Result<T>> DeleteAsync(int id);
        Task<Result<T>> GetQueryAsync(Expression<Func<T, bool>>? filter = null);

    }
}
