using BankApp.Database.Context;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.TransactionTypeRepo
{
    public class TransactionTypeRepository : BaseRepository<TransactionType>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Result<List<TransactionType>>> GetAllWithProcessTypeAsync(Expression<Func<TransactionType, bool>>? filter = null)
        {
            IQueryable<TransactionType> query = _dbSet.Include(t => t.ProcessType);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var entities = await query.ToListAsync();
            return entities.Any()
                ? Result<List<TransactionType>>.Success(entities, $"{entities.Count} adet kayıt listelendi.")
                : Result<List<TransactionType>>.Failure(new List<string> { "Koşula uygun kayıt bulunamadı." });
        }

    }
}
