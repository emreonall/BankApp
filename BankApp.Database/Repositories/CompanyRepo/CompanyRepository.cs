using BankApp.Database.Context;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Database.Repositories.CompanyRepo
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Company> GetByCompanyCodeAsync(string companyCode)
        {
            var companiesResult = await base.GetAllAsync(x => x.CompanyCode == companyCode);
            return companiesResult.Data!.FirstOrDefault();
        }
    }
}
