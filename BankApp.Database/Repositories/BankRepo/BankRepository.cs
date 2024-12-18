using BankApp.Database.Context;
using BankApp.Database.ViewModels.Bank;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.BankRepo
{
    public class BankRepository : IBankRepository
    {
        private readonly IBaseRepository<Bank> _repo;
        private readonly AppDbContext _context;


        public BankRepository(IBaseRepository<Bank> baseRepository, AppDbContext context)
        {
            _repo = baseRepository;
            _context = context;
        }

        public async Task<Result<Bank>> CreateBank(Bank entity)
        {
            return await _repo.AddAsync(entity);
        }


        public async Task<Result<Bank>> DeleteBank(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public Task<Result<List<Bank>>> GetAllBankAsync(Expression<Func<Bank, bool>>? filter = null)
        {
            return _repo.GetAllAsync(filter);
        }

        public async Task<Result<List<BankTransactionsViewModel>>> GetAllBalanceSummaryWithCurrencyAsync()
        {
            var bankTransactionViewModels = await _context.Set<Bank>()
                .Include(b => b.Transactions!)
                .ThenInclude(t => t.Currency)
                .Select(b => new BankTransactionsViewModel
                {
                    BankId = b.Id,
                    Name = b.Name,
                    IconURL = b.IconUrl ?? string.Empty, // Fix for CS8601
                    CurrencyTotals = b.Transactions != null && b.Transactions.Any()
                        ? b.Transactions
                            .GroupBy(t => t.CurrencyId)
                            .Select(g => new CurrencyAmount
                            {
                                CurrencyName = g.First().Currency != null ? g.First().Currency!.ShortName : "Tanımsız PB",
                                TotalAmount = g.Sum(t => t.Amount)
                            })
                            .ToList()
                        : new List<CurrencyAmount>()
                })
                .ToListAsync();

            return Result<List<BankTransactionsViewModel>>.Success(bankTransactionViewModels);
        }

        public async Task<Result<List<Bank>>> GetAllBankWithTransactionsAsync(Expression<Func<Bank, bool>>? filter = null)
        {
            IQueryable<Bank> query = _context.Set<Bank>().Include(t => t.Transactions);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var entities = await query.ToListAsync();
            return entities.Any()
                ? Result<List<Bank>>.Success(entities, $"{entities.Count} adet kayıt listelendi.")
                : Result<List<Bank>>.Failure(new List<string> { "Koşula uygun kayıt bulunamadı." });
        }

        public async Task<Result<Bank>> GetBankByIdAsync(int id)
        {
          return await  _repo.GetByIdAsync(id);
        }

        public async Task<Result<Bank>> GetBankQueryAsync(Expression<Func<Bank, bool>>? filter = null)
        {
            return await _repo.GetQueryAsync(filter);
        }

        public Result<Bank> UpdateBank(int id, Bank entity)
        {
            return _repo.Update(id, entity);
        }

       
    }
}
