using BankApp.Domain.Entities;
using System.Linq.Expressions;


namespace BankApp.Database.Repositories.TransactionRepo
{
    public interface ITransactionRepository
    {
        Task<Result<Transaction>> CreateTransaction(Transaction entity);
        Result<Transaction> UpdateTransaction(int id, Transaction entity);
        Task<Result<Transaction>> DeleteTransaction(int id);
        Task<Result<Transaction>> GetTransactionByIdAsync(int id);
        Task<Result<List<Transaction>>> GetAllTransactionAsync(Expression<Func<Transaction, bool>>? filter = null);
        Task<Result<Transaction>> GetTransactionQueryAsync(Expression<Func<Transaction, bool>>? filter = null);
        Task<Result<List<Transaction>>> GetAllWitRelationshipsAsync(Expression<Func<Transaction, bool>>? filter = null);
    }
}
