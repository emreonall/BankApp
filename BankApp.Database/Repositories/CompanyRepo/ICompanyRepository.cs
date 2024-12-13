using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.CompanyRepo
{
    public interface ICompanyRepository
    {
        Task<Result<Company>> CreateCompany(Company entity);
        Result<Company> UpdateCompany(int id, Company entity);
        Task<Result<Company>> DeleteCompany(int id);
        Task<Result<Company>> GetCompanyByIdAsync(int id);
        Task<Result<List<Company>>> GetAllCompanyAsync(Expression<Func<Company, bool>>? filter = null);
        Task<Result<Company>> GetCompanyQueryAsync(Expression<Func<Company, bool>>? filter = null);
        Task<Result<Company>> GetByCompanyCodeAsync(string companyCode);
    }
    
}
