using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.CurrencyRepo
{
    public interface ICurrencyRepository
    {
        Task<Result<Currency>> CreateCurrency(Currency entity);
        Result<Currency> UpdateCurrency(int id, Currency entity);
        Task<Result<Currency>> DeleteCurrency(int id);
        Task<Result<Currency>> GetCurrencyByIdAsync(int id);
        Task<Result<List<Currency>>> GetAllCurrencyAsync(Expression<Func<Currency, bool>>? filter = null);
        Task<Result<Currency>> GetCurrencyQueryAsync(Expression<Func<Currency, bool>>? filter = null);
        Task<Result<Currency>> GetCurrencyByShortNameAsync(string shortName);
    }
}
