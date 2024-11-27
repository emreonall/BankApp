using BankApp.Domain.Entities;
using System.Linq.Expressions;


namespace BankApp.Database.Repositories.TransactionRepo
{
    public interface ITransactionRepository:IBaseRepository<Transaction>
    {
        Task<Result<List<Transaction>>> GetAllWitRelationshipsAsync(Expression<Func<Transaction, bool>>? filter = null);
    }
}
