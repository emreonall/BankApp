using BankApp.Database.Repositories;
using BankApp.Database.Repositories.CurrencyRepo;
using BankApp.Database.Validators;
using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Application.Services.CurrencyService
{
    public class CurrencyMenager : ICurrencyService
    {
        private readonly ICurrencyRepository _repo;

        public CurrencyMenager(ICurrencyRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<Currency>> CreateCurrency(Currency currency)
        {
            var validator = new CurrencyValidator();
            var validResult = validator.Validate(currency);

            if (validResult.IsValid)
            {
                var r = await _repo.AddAsync(currency);
                // return Result<Currency>.Success(currency, "Yeni para birimi yaratıldı.");
                return r;
            }
            else
            {
                var errorMessages = validResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Currency>.Failure(errorMessages);
            }
        }

        public async Task<Result<Currency>> DeletCurrency(int id)
        {
            var r = await _repo.Delete(id);
            return r;
        }

        public async Task<Result<List<Currency>>> GetAllCurrencies(Expression<Func<Currency, bool>> filter = null)
        {

            if (filter == null)
            {
                var r = await _repo.GetAllAsync();
                return r;
            }
            else
            {
                var r = await _repo.GetAllAsync(filter);
                return r;
            }



        }

        public async Task<Result<Currency>> GetCurrencyById(int id)
        {
            var r = await _repo.GetByIdAsync(id);
            return r;
        }

        public async Task<Result<Currency>> UpdateCurrency(Currency currency)
        {
            var validator = new CurrencyValidator();
            var validResult = validator.Validate(currency);
            var response = null as Result<Currency>;

            if (validResult.IsValid)
            {
                var existingCurrency = await _repo.GetByIdAsync(currency.Id);

                if (existingCurrency.Data == null || existingCurrency.IsSuccess == false)
                {
                    response = Result<Currency>.Failure(new List<string> { "Kayıt bulunamadı." });
                }
                else
                {
                    try
                    {
                        existingCurrency.Data.Name = currency.Name;
                        existingCurrency.Data.ShortName = currency.ShortName;
                        var r = await _repo.Update(currency.Id, currency);
                    }
                    catch (Exception ex)
                    {
                        var errorMessages = validResult.Errors.Select(e => e.ErrorMessage).ToList();
                        response = Result<Currency>.Failure(errorMessages);
                    }
                }
            }
            else
            {
                var errorMessages = validResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Currency>.Failure(errorMessages);
            }
            return response;
        }
    }
}
