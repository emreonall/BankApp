using BankApp.Database.Repositories;
using BankApp.Database.Repositories.CurrencyRepo;
using BankApp.Domain.Entities;
using FluentValidation;
using System.Linq.Expressions;

namespace BankApp.Application.Services.CurrencyService
{
    public class CurrencyManager : GenericService<Currency>, ICurrencyService
    {
        private readonly ICurrencyRepository _repo;

        public CurrencyManager(ICurrencyRepository repo, IValidator<Currency> validator) : base(repo, validator)
        {
            _repo = repo;
        }

        public async Task<Result<Currency>> CreateCurrency(Currency currency)
        {
            var response = await _repo.AddAsync(currency);
            return response;
        }

        public async Task<Result<Currency>> DeleteCurrency(int id)
        {
            var response = await _repo.Delete(id);
            return response;
        }

        public async Task<Result<List<Currency>>> GetAllCurrencies(Expression<Func<Currency, bool>>? filter = null)
        {
            var response = await _repo.GetAllAsync(filter);
            return response;
        }

        public async Task<Result<Currency>> GetCurrencyById(int id)
        {
            var response = await _repo.GetByIdAsync(id);
            return response;
        }

        public async Task<Result<Currency>> UpdateCurrency(Currency currency)
        {
            var existingEntity = await _repo.GetByIdAsync(currency.Id);
            if (!existingEntity.IsSuccess || existingEntity.Data == null)
            {
                return Result<Currency>.Failure(existingEntity.Errors,"Kayıt bulunamadı");
            }
            existingEntity.Data.Name = currency.Name;
            existingEntity.Data.ShortName = currency.ShortName;

            var response = _repo.Update(currency.Id, existingEntity.Data);
            return response;
        }


    }
}
