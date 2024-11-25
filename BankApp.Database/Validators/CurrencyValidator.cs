using BankApp.Domain.Entities;
using FluentValidation;

namespace BankApp.Database.Validators
{
    public class CurrencyValidator : AbstractValidator<Currency>
    {
        public CurrencyValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Para birimi adı boş geçilemez.").MaximumLength(50).WithMessage("Adı alanı en fazla 50 karakter olabilir.");
            RuleFor(x => x.ShortName).MinimumLength(3).WithMessage("Para birimi en az 3 karakter olabilir.").NotEmpty().MaximumLength(3).WithMessage("Para birimi en fazla 3 karakter olabilir.").NotNull().WithMessage("Para birimi adı boş geçilemez.");
        }
    }
}
