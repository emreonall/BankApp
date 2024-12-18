using BankApp.Database.Context;
using BankApp.Database.Repositories.BankRepo;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.TransactionTypeRepo
{
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly DbSet<TransactionType> _dbSet;
        private readonly AppDbContext _context;
        private readonly IBaseRepository<TransactionType> _repo;
        public TransactionTypeRepository(AppDbContext context, IBaseRepository<TransactionType> repo)
        {
            _repo = repo;
            _context = context;
            _dbSet = context.Set<TransactionType>();
        }

        public async Task<Result<TransactionType>> CreateTransactionType(TransactionType entity)
        {
            return await _repo.AddAsync(entity);
        }

        public async Task<Result<TransactionType>> DeleteTransactionTypeAsync(int id)
        {
           return await _repo.DeleteAsync(id);
        }

        public async Task<Result<List<TransactionType>>> GetAllTransactionTypeAsync(Expression<Func<TransactionType, bool>>? filter = null)
        {
            return await _repo.GetAllAsync(filter);
        }

        public async Task<Result<List<TransactionType>>> GetAllWithProcessTypeAsync(Expression<Func<TransactionType, bool>>? filter = null)
        {
            IQueryable<TransactionType> query = _dbSet.Include(t => t.ProcessType);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var entities = await query.ToListAsync();
            return entities.Any()
                ? Result<List<TransactionType>>.Success(entities, $"{entities.Count} adet kayıt listelendi.")
                : Result<List<TransactionType>>.Failure(new List<string> { "Koşula uygun kayıt bulunamadı." });
        }

        public async Task<Result<TransactionType>> GetTransactionTypeByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Result<TransactionType>> GetTransactionTypeQueryAsync(Expression<Func<TransactionType, bool>>? filter = null)
        {
           return await _repo.GetQueryAsync(filter);
        }

        public Result<TransactionType> UpdateTransactionType(int id, TransactionType entity)
        {
            return _repo.Update(id, entity);
        }
    }
}
