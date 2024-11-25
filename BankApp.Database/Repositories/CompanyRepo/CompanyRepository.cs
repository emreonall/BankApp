using BankApp.Database.Context;
using BankApp.Domain.Entities;

namespace BankApp.Database.Repositories.CompanyRepo
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Company> GetByCompanyCodeAsync(string companyCode)
        {
            var companiesResult= await base.GetQueryAsync(x => x.CompanyCode == companyCode);
            return companiesResult.Data;
        }
    }
}
