using BankApp.Database.Context;
using BankApp.Domain.Entities;

namespace BankApp.Database.Repositories.CurrencyRepo
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Result<Currency>> GetByShortNameAsync(string shortName)
        {
            var currency = await base.GetQueryAsync(x => x.ShortName == shortName);
            if (currency == null)
            {
                return Result<Currency>.Failure(new List<string> { "Kayıt bulunamadı." });
            }
            return Result<Currency>.Success(currency.Data, "Kayıt bulundu.");
        }
    }
}
