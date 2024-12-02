﻿using BankApp.Database.Repositories;
using BankApp.Domain.Entities;
using System.Linq.Expressions;

namespace BankApp.Application.Services.CurrencyService
{
    public interface ICurrencyService:IGenericService<Currency>
    {
        Task<Result<List<Currency>>> GetAllCurrencies(Expression<Func<Currency, bool>>? filter = null);
        Task<Result<Currency>> GetCurrencyById(int id);
        Task<Result<Currency>> CreateCurrency(Currency currency);
        Task<Result<Currency>> DeleteCurrency(int id);
        Task<Result<Currency>> UpdateCurrency(Currency currency);
    }
}
