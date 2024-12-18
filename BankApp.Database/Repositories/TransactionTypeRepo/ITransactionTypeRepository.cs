using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.TransactionTypeRepo
{
    public interface ITransactionTypeRepository 
    {
        Task<Result<TransactionType>> CreateTransactionType(TransactionType entity);
        Result<TransactionType> UpdateTransactionType(int id, TransactionType entity);
        Task<Result<TransactionType>> DeleteTransactionTypeAsync(int id);
        Task<Result<TransactionType>> GetTransactionTypeByIdAsync(int id);
        Task<Result<List<TransactionType>>> GetAllTransactionTypeAsync(Expression<Func<TransactionType, bool>>? filter = null);
        Task<Result<TransactionType>> GetTransactionTypeQueryAsync(Expression<Func<TransactionType, bool>>? filter = null);
        Task<Result<List<TransactionType>>> GetAllWithProcessTypeAsync(Expression<Func<TransactionType, bool>>? filter = null);
    }

}
