using BankApp.Database.Repositories;
using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Application.Services.CompanyService
{
    public interface ICompanyService
    {
        Task<Result<Company>> GetCompanyById(int id);
        Task<Result<List<Company>>> GetAllCompanies(Expression<Func<Company, bool>> filter = null);
        Task<Result<Company>> CreateCompany(Company company);
        Task<Result<Company>> UpdateCompanyy(Company company);
        Task<Result<Company>> DeleteCompany(int id);
    }
}
