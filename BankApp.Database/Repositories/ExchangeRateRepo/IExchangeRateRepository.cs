using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.ExchangeRateRepo
{
    public interface IExchangeRateRepository
    {
        Task<Result<ExchangeRate>> CreateExchRate(ExchangeRate entity);
        Result<ExchangeRate> UpdateExchRate(int id, ExchangeRate entity);
        Task<Result<ExchangeRate>> DeleteExchRate(int id);
        Task<Result<ExchangeRate>> GetExchRateByIdAsync(int id);
        Task<Result<List<ExchangeRate>>> GetAllExchRateAsync(Expression<Func<ExchangeRate, bool>>? filter = null);
        Task<Result<ExchangeRate>> GetExchRateQueryAsync(Expression<Func<ExchangeRate, bool>>? filter = null);
        Task<Result<List<ExchangeRate>>> GetAllWithCurrencyAsync(Expression<Func<ExchangeRate, bool>>? filter = null);
    }
}
