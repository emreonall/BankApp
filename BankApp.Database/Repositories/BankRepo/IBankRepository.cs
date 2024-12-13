using BankApp.Database.ViewModels.Bank;
using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.BankRepo
{
    public interface IBankRepository
    {
        Task<Result<Bank>> CreateBank(Bank entity);
        Result<Bank> UpdateBank(int id, Bank entity);
        Task<Result<Bank>> DeleteBank(int id);
        Task<Result<Bank>> GetBankByIdAsync(int id);
        Task<Result<List<Bank>>> GetAllBankAsync(Expression<Func<Bank, bool>>? filter = null);
        Task<Result<Bank>> GetBankQueryAsync(Expression<Func<Bank, bool>>? filter = null);
        Task<Result<List<Bank>>> GetAllBankWithTransactionsAsync(Expression<Func<Bank, bool>>? filter = null);
        Task<Result<List<BankTransactionsViewModel>>> GetAllBalanceSummaryWithCurrencyAsync();
    }
}
