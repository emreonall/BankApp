using BankApp.Database.Context;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.ExchangeRateRepo
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly IBaseRepository<ExchangeRate> _repo;
        private readonly AppDbContext _context;
        public ExchangeRateRepository(AppDbContext context, IBaseRepository<ExchangeRate> repo)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<Result<ExchangeRate>> CreateExchRate(ExchangeRate entity)
        {
           return await _repo.AddAsync(entity);
        }

        public Task<Result<ExchangeRate>> DeleteExchRate(int id)
        {
            return _repo.DeleteAsync(id);
        }

        public Task<Result<List<ExchangeRate>>> GetAllExchRateAsync(Expression<Func<ExchangeRate, bool>>? filter = null)
        {
            return _repo.GetAllAsync(filter);
        }

        public async Task<Result<List<ExchangeRate>>> GetAllWithCurrencyAsync(Expression<Func<ExchangeRate, bool>>? filter = null)
        {
            IQueryable<ExchangeRate> query = _context.Set<ExchangeRate>().Include(t => t.Currency);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var entities = await query.ToListAsync();
            return entities.Any()
                ? Result<List<ExchangeRate>>.Success(entities, $"{entities.Count} adet kayıt listelendi.")
                : Result<List<ExchangeRate>>.Failure(new List<string> { "Koşula uygun kayıt bulunamadı." });
        }

        public async Task<Result<ExchangeRate>> GetExchRateByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Result<ExchangeRate>> GetExchRateQueryAsync(Expression<Func<ExchangeRate, bool>>? filter = null)
        {
            return await _repo.GetQueryAsync(filter);
        }

        public Result<ExchangeRate> UpdateExchRate(int id, ExchangeRate entity)
        {
            return _repo.Update(id, entity);
        }
    }
  
}
