using BankApp.Database.Context;
using BankApp.Domain.Entities;
using System.Linq.Expressions;
using System.Security.AccessControl;

namespace BankApp.Database.Repositories.ProcessTypeRepo
{
    public class ProcessTypeRepository : IProcessTypeRepository
    {
        private readonly AppDbContext _context;
        private readonly IBaseRepository<ProcessType> _repo;

        public ProcessTypeRepository(AppDbContext context, IBaseRepository<ProcessType> repo) 
        {
            _context = context;
            _repo = repo;
        }

        public async Task<Result<ProcessType>> CreateProcessType(ProcessType entity)
        {
            return await _repo.AddAsync(entity);
        }

        public async Task<Result<ProcessType>> DeleteProcessType(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<Result<List<ProcessType>>> GetAllProcessTypeAsync(Expression<Func<ProcessType, bool>>? filter = null)
        {
            return await _repo.GetAllAsync(filter);
        }

        public async Task<Result<ProcessType>> GetProcessTypeByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Result<ProcessType>> GetProcessTypeQueryAsync(Expression<Func<ProcessType, bool>>? filter = null)
        {
            return await _repo.GetQueryAsync(filter);
        }

        public Result<ProcessType> UpdateProcessType(int id, ProcessType entity)
        {
            return _repo.Update(id, entity);
        }
    }
}
