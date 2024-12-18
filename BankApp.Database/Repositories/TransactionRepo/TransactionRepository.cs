using BankApp.Database.Context;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.TransactionRepo
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DbSet<Transaction> _dbSet;
        private readonly AppDbContext _context;
        private readonly IBaseRepository<Transaction> _repo;

        public TransactionRepository(DbSet<Transaction> dbSet, AppDbContext context, IBaseRepository<Transaction> repo)
        {
            _dbSet = dbSet;
            _context = context;
            _repo = repo;
        }

        public Task<Result<Transaction>> CreateTransaction(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Transaction>> DeleteTransaction(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Transaction>>> GetAllTransactionAsync(Expression<Func<Transaction, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<Transaction>>> GetAllWitRelationshipsAsync(Expression<Func<Transaction, bool>>? filter = null)
        {
            IQueryable<Transaction> query = _dbSet
                .Include(t => t.Company!)
                .Include(t => t.Currency!)
                .Include(t => t.Bank!)
                .Include(t => t.TransactionType!)
                .ThenInclude(tt => tt.ProcessType!);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var entities = await query.ToListAsync();
            return entities.Any()
                ? Result<List<Transaction>>.Success(entities, $"{entities.Count} adet kayıt listelendi.")
                : Result<List<Transaction>>.Failure(new List<string> { "Koşula uygun kayıt bulunamadı." });
        }

        public Task<Result<Transaction>> GetTransactionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Transaction>> GetTransactionQueryAsync(Expression<Func<Transaction, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Result<Transaction> UpdateTransaction(int id, Transaction entity)
        {
            throw new NotImplementedException();
        }
    }
}
