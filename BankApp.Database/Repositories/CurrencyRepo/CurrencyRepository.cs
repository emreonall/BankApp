using BankApp.Database.Context;
using BankApp.Domain.Entities;

namespace BankApp.Database.Repositories.CurrencyRepo
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(AppDbContext context) : base(context)
        {
        }
    }
}
