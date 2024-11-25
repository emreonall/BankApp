using BankApp.Database.Context;
using BankApp.Domain.Entities;

namespace BankApp.Database.Repositories.ProcessTypeRepo
{
    public class ProcessTypeRepository : BaseRepository<ProcessType>, IProcessTypeRepository
    {
        public ProcessTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
