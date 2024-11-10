using BankApp.Domain.Entities;

namespace BankApp.Database.Repositories.BankRepo
{
    public interface IBankRepository:IBaseRepository<Bank>
    {
        Task<Bank> GetByName(string name);
    }
}
