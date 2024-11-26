using BankApp.Database.Context;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.ExchangeRateRepo
{
    public class ExchangeRateRepository : BaseRepository<ExchangeRate>, IExchangeRateRepository
    {
        public ExchangeRateRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Result<List<ExchangeRate>>> GetAllWithCurrencyAsync(Expression<Func<ExchangeRate, bool>>? filter = null)
        {
            IQueryable<ExchangeRate> query = _dbSet.Include(t => t.Currency);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var entities = await query.ToListAsync();
            return entities.Any()
                ? Result<List<ExchangeRate>>.Success(entities, $"{entities.Count} adet kayıt listelendi.")
                : Result<List<ExchangeRate>>.Failure(new List<string> { "Koşula uygun kayıt bulunamadı." });
        }
    }
  
}
