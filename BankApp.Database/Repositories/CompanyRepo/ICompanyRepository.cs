using BankApp.Domain.Entities;

namespace BankApp.Database.Repositories.CompanyRepo
{
    public interface ICompanyRepository:IBaseRepository<Company>
    {
        Task<Company>  GetByCompanyCodeAsync(string companyCode);
    }
    
}
