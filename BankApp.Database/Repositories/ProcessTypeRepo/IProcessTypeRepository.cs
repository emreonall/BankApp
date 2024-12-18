using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.ProcessTypeRepo
{
    public interface IProcessTypeRepository 
    {
        Task<Result<ProcessType>> CreateProcessType(ProcessType entity);
        Result<ProcessType> UpdateProcessType(int id, ProcessType entity);
        Task<Result<ProcessType>> DeleteProcessType(int id);
        Task<Result<ProcessType>> GetProcessTypeByIdAsync(int id);
        Task<Result<List<ProcessType>>> GetAllProcessTypeAsync(Expression<Func<ProcessType, bool>>? filter = null);
        Task<Result<ProcessType>> GetProcessTypeQueryAsync(Expression<Func<ProcessType, bool>>? filter = null);
    }
}
