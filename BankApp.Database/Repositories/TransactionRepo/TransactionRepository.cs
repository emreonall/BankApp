using BankApp.Database.Context;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.TransactionRepo
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Result<List<Transaction>>> GetAllWitRelationshipsAsync(Expression<Func<Transaction, bool>>? filter = null)
        {
            IQueryable<Transaction> query = _dbSet.Include(t => t.Company).Include(t=>t.Currency).Include(t=>t.Bank).Include(t=>t.TransactionType).Include(t => t.TransactionType.ProcessType);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var entities = await query.ToListAsync();
            return entities.Any()
                ? Result<List<Transaction>>.Success(entities, $"{entities.Count} adet kayıt listelendi.")
                : Result<List<Transaction>>.Failure(new List<string> { "Koşula uygun kayıt bulunamadı." });
        }
    }
}
