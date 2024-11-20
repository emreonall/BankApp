using BankApp.Database.Repositories;
using BankApp.Database.Repositories.CompanyRepo;
using BankApp.Database.Validators;
using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Application.Services.CompanyService
{
    public class CompanyManager:ICompanyService
    {
        private readonly ICompanyRepository _repo;

        public CompanyManager(ICompanyRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Company>> CreateCompany(Company company)
        {
            var validationResult = ValidateEntity(company);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var result = await _repo.AddAsync(company);
            return result.IsSuccess
                ? Result<Company>.Success(result.Data, result.Message)
                : Result<Company>.Failure(result.Errors, result.Message);
        }

        public async Task<Result<Company>> DeleteCompany(int id)
        {
            var result = await _repo.Delete(id);
            return result.IsSuccess
                ? Result<Company>.Success(result.Data, result.Message)
                : Result<Company>.Failure(result.Errors, result.Message);
        }

        public async Task<Result<List<Company>>> GetAllCompanies(Expression<Func<Company, bool>> filter = null)
        {
            var result = filter == null
                ? await _repo.GetAllAsync()
                : await _repo.GetAllAsync(filter);

            return result.IsSuccess
                ? Result<List<Company>>.Success(result.Data, result.Message)
                : Result<List<Company>>.Failure(result.Errors, result.Message);
        }

        public async Task<Result<Company>> GetCompanyById(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            return result.IsSuccess
                ? Result<Company>.Success(result.Data, result.Message)
                : Result<Company>.Failure(result.Errors, result.Message);
        }

        public async Task<Result<Company>> UpdateCompanyy(Company company)
        {
            var validationResult = ValidateEntity(company);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var existingCompanyResult = await _repo.GetByIdAsync(company.Id);
            if (!existingCompanyResult.IsSuccess || existingCompanyResult.Data == null)
            {
                return Result<Company>.Failure(existingCompanyResult.Errors, existingCompanyResult.Message);
            }

            try
            {
                existingCompanyResult.Data.CompanyCode = company.CompanyCode;
                existingCompanyResult.Data.CompanyName = company.CompanyName;

                var updateResult = _repo.Update(company.Id, company);
                return updateResult.IsSuccess
                    ? Result<Company>.Success(updateResult.Data, updateResult.Message)
                    : Result<Company>.Failure(updateResult.Errors, updateResult.Message);
            }
            catch (Exception ex)
            {

                return Result<Company>.Failure(new List<string> { ex.Message }, "Kayıt güncellenirken hata oluştu.");
            }
        }
        private Result<Company> ValidateEntity(Company company)
        {
            var validator = new CompanyValidator();
            var validationResult = validator.Validate(company);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Company>.Failure(errorMessages, "Doğrulamalar başarısız.");
            }
            return Result<Company>.Success(company);
        }
    }
}
