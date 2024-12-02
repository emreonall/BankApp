using BankApp.Database.Context;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.BankRepo
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        public BankRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Result<List<Bank>>> GetAllWithTransactionsAsync(Expression<Func<Bank, bool>>? filter = null)
        {
            IQueryable<Bank> query = _dbSet.Include(t => t.Transactions);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var entities = await query.ToListAsync();
            return entities.Any()
                ? Result<List<Bank>>.Success(entities, $"{entities.Count} adet kayıt listelendi.")
                : Result<List<Bank>>.Failure(new List<string> { "Koşula uygun kayıt bulunamadı." });
        }
    }
}
