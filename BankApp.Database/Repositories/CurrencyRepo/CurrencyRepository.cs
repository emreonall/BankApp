using BankApp.Database.Context;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.CurrencyRepo
{
    public class CurrencyRepository :  ICurrencyRepository
    {
        private readonly IBaseRepository<Currency> _repo;
        private readonly AppDbContext _context;
        public CurrencyRepository(IBaseRepository<Currency> repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<Result<Currency>> CreateCurrency(Currency entity)
        {
            return await _repo.AddAsync(entity);
        }

        public async Task<Result<Currency>> DeleteCurrency(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<Result<List<Currency>>> GetAllCurrencyAsync(Expression<Func<Currency, bool>>? filter = null)
        {
            return await _repo.GetAllAsync(filter);
        }


        public Task<Result<Currency>> GetCurrencyByIdAsync(int id)
        {
            return _repo.GetByIdAsync(id);
        }

        public async Task<Result<Currency>> GetCurrencyByShortNameAsync(string shortName)
        {
            var currency = await _context.Currencies.FirstOrDefaultAsync(x => x.ShortName == shortName);
            if (currency == null)
            {
                return Result<Currency>.Failure(new List<string> { "Kayıt bulunamadı." });
            }
            return Result<Currency>.Success(currency, "Kayıt bulundu.");
        }

        public Task<Result<Currency>> GetCurrencyQueryAsync(Expression<Func<Currency, bool>>? filter = null)
        {
            return _repo.GetQueryAsync(filter);
        }

        public Result<Currency> UpdateCurrency(int id, Currency entity)
        {
            return _repo.Update(id, entity);
        }
    }
}
