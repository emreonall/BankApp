using BankApp.Database.Context;
using BankApp.Database.ViewModels.Bank;
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

        public async Task<Result<List<BankTransactionsViewModel>>> GetAllBalanceSummaryWithCurrencyAsync()
        {
            var bankTransactionViewModels = _dbSet.Include(b => b.Transactions).ThenInclude(t => t.Currency).Select(b => new BankTransactionsViewModel
            {
                BankId = b.Id,
                Name = b.Name,
                IconURL = b.IconUrl,
                CurrencyTotals = b.Transactions != null && b.Transactions.Any()
             ? b.Transactions
                 .GroupBy(t => t.CurrencyId)
                 .Select(g => new CurrencyAmount
                 {
                     CurrencyName = g.First().Currency != null ? g.First().Currency.ShortName : "Tanımsız PB",
                     TotalAmount = g.Sum(t => t.Amount)
                 })
                 .ToList()
             : new List<CurrencyAmount>() // Eğer transaction yoksa boş bir liste döndür
            })
     .ToList();


            return Result<List<BankTransactionsViewModel>>.Success(bankTransactionViewModels);
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
