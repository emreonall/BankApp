using BankApp.Domain.Entities;
using FluentValidation;

namespace BankApp.Database.Validators
{
    public class CurrencyValidator : AbstractValidator<Currency>
    {
        public CurrencyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Para birimi adı boş geçilemez.");
            RuleFor(x => x.ShortName).MaximumLength(3).WithMessage("Para birimi adı en fazla 3 karakter olabilir.").NotEmpty();
        }
    }
}
