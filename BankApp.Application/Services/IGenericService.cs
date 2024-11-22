using BankApp.Database.Repositories;
using FluentValidation;
using System.Linq.Expressions;

namespace BankApp.Application.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<Result<T>> GetByIdAsync(int id);
        Task<Result<List<T>>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<Result<T>> AddAsync(T entity, IValidator<T> validator);
        Task<Result<T>> Update(int id, T entity, IValidator<T> validator );
        Task<Result<T>> DeleteAsync(int id);
    }
}
