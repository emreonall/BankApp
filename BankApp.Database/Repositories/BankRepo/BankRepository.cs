using BankApp.Database.Context;
using BankApp.Domain.Entities;

namespace BankApp.Database.Repositories.BankRepo
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        public BankRepository(AppDbContext context) : base(context)
        {
        }
                      
    }
}
