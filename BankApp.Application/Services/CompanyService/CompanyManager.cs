using BankApp.Database.Repositories;
using BankApp.Database.Repositories.CompanyRepo;
using BankApp.Database.Validators;
using BankApp.Domain.Entities;
using FluentValidation;
using System.Linq.Expressions;

namespace BankApp.Application.Services.CompanyService
{
    public class CompanyManager : GenericService<Company>, ICompanyService
    {
        private readonly ICompanyRepository _repo;

        public CompanyManager(ICompanyRepository repo, IValidator<Company> validator) : base(repo, validator)
        {
            _repo = repo;
        }
    }
}
