using BankApp.Domain.Entities;
using FluentValidation;

namespace BankApp.Database.Validators
{
    public class ExchangeRateValidator:AbstractValidator<ExchangeRate>
    {
        public ExchangeRateValidator()
        {
            RuleFor(x => x.CurrencyId).NotEmpty().WithMessage("Para birimi boş olamaz");
            RuleFor(x => x.CurrentDate).NotEmpty().WithMessage("Tarih boş olamaz");
            RuleFor(x => x.Rate).NotEmpty().WithMessage("Kur boş olamaz");
        }
    }
}
