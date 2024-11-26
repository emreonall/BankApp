using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.ExchangeRateRepo
{
    public interface IExchangeRateRepository:IBaseRepository<ExchangeRate>
    {
        Task<Result<List<ExchangeRate>>> GetAllWithCurrencyAsync(Expression<Func<ExchangeRate, bool>>? filter = null);
    }
}
