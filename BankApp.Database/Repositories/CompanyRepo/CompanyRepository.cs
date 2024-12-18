using BankApp.Database.Context;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories.CompanyRepo
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IBaseRepository<Company> _repo;
        private readonly AppDbContext _context;
        public CompanyRepository(IBaseRepository<Company> repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<Result<Company>> CreateCompany(Company entity)
        {
            return await _repo.AddAsync(entity);
        }

        public async Task<Result<Company>> DeleteCompany(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<Result<List<Company>>> GetAllCompanyAsync(Expression<Func<Company, bool>>? filter = null)
        {
            return await _repo.GetAllAsync(filter);
        }

        public async Task<Result<Company>> GetByCompanyCodeAsync(string companyCode)
        {
           
            var result = await _context.Companies.FirstOrDefaultAsync(c => c.CompanyCode == companyCode);
            if (result is not null)
            {
                return Result<Company>.Success(result);
            }
            return Result<Company>.Failure(new List<string> { $"{companyCode} koduna sahip bir şirket bulunamadı." });
        }

        public Task<Result<Company>> GetCompanyByIdAsync(int id)
        {
            return _repo.GetByIdAsync(id);
        }

        public Task<Result<Company>> GetCompanyQueryAsync(Expression<Func<Company, bool>>? filter = null)
        {
            return _repo.GetQueryAsync(filter);
        }

        public Result<Company> UpdateCompany(int id, Company entity)
        {
            return _repo.Update(id, entity);
        }
    }
}
