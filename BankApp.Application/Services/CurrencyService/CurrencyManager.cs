using BankApp.Database.Repositories;
using BankApp.Database.Repositories.CurrencyRepo;
using BankApp.Database.Validators;
using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Application.Services.CurrencyService
{
    public class CurrencyManager : ICurrencyService
    {
        private readonly ICurrencyRepository _repo;

        public CurrencyManager(ICurrencyRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Currency>> CreateCurrency(Currency currency)
        {
            var validationResult = ValidateCurrency(currency);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var result = await _repo.AddAsync(currency);
            return result.IsSuccess
                ? Result<Currency>.Success(result.Data,result.Message)
                : Result<Currency>.Failure(result.Errors,result.Message);
        }

        public async Task<Result<Currency>> DeleteCurrency(int id)
        {
            var result = await _repo.Delete(id);
            return result.IsSuccess
                ? Result<Currency>.Success(result.Data,result.Message)
                : Result<Currency>.Failure(result.Errors,result.Message);
        }

        public async Task<Result<List<Currency>>> GetAllCurrencies(Expression<Func<Currency, bool>>? filter = null)
        {
            var result = filter == null
                ? await _repo.GetAllAsync()
                : await _repo.GetAllAsync(filter);

            return result.IsSuccess
                ? Result<List<Currency>>.Success(result.Data,result.Message)
                : Result<List<Currency>>.Failure(result.Errors, result.Message);
        }

        public async Task<Result<Currency>> GetCurrencyById(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            return result.IsSuccess
                ? Result<Currency>.Success(result.Data,result.Message)
                : Result<Currency>.Failure(result.Errors, result.Message);
        }

        public async Task<Result<Currency>> UpdateCurrency(Currency currency)
        {
            var validationResult = ValidateCurrency(currency);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var existingCurrencyResult = await _repo.GetByIdAsync(currency.Id);
            if (!existingCurrencyResult.IsSuccess || existingCurrencyResult.Data == null)
            {
                return Result<Currency>.Failure(existingCurrencyResult.Errors,existingCurrencyResult.Message);
            }

            try
            {
                existingCurrencyResult.Data.Name = currency.Name;
                existingCurrencyResult.Data.ShortName = currency.ShortName;

                var updateResult = _repo.Update(currency.Id, currency);
                return updateResult.IsSuccess
                    ? Result<Currency>.Success(updateResult.Data,updateResult.Message)
                    : Result<Currency>.Failure(updateResult.Errors,updateResult.Message);
            }
            catch (Exception ex)
            {
                
                return Result<Currency>.Failure(new List<string> { ex.Message }, "Kayıt güncellenirken hata oluştu.");
            }
        }

        private Result<Currency> ValidateCurrency(Currency currency)
        {
            var validator = new CurrencyValidator();
            var validationResult = validator.Validate(currency);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Currency>.Failure(errorMessages, "Doğrulamalar başarısız.");
            }
            return Result<Currency>.Success(currency);
        }
    }
}
