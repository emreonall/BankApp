using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.TransactionTypeRepo
{
    public interface ITransactionTypeRepository : IBaseRepository<TransactionType>
    {
        Task<Result<List<TransactionType>>> GetAllWithProcessTypeAsync(Expression<Func<TransactionType, bool>>? filter = null);
    }

}
