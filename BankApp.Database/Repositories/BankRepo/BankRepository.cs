using BankApp.Database.Context;
using BankApp.Domain.Entities;

namespace BankApp.Database.Repositories.BankRepo
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        public BankRepository(AppDbContext context) : base(context)
        {
        }

        Task<Bank> IBankRepository.GetByName(string name)
        {
            //var result = await base.Queryable<Bank>().Where(x = x => x.Name == name);
            //return result;
            return null;
        }
    }
}
