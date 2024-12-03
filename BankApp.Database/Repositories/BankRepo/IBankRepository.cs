using BankApp.Database.ViewModels.Bank;
using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.BankRepo
{
    public interface IBankRepository:IBaseRepository<Bank>
    {
        Task<Result<List<Bank>>> GetAllWithTransactionsAsync(Expression<Func<Bank, bool>>? filter = null);
        Task<Result<List<BankTransactionsViewModel>>> GetAllBalanceSummaryWithCurrencyAsync();
    }
}
