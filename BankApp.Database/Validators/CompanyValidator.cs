using BankApp.Domain.Entities;
using FluentValidation;

namespace BankApp.Database.Validators
{
    public class CompanyValidator:AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(x=>x.CompanyCode).NotNull().WithMessage("Şirket kodu boş geçilemez.").MaximumLength(5).WithMessage("Şirket kodu en fazla 5 karakter olabilir.");

            RuleFor(x => x.CompanyName).NotNull().WithMessage("Şirket adı boş geçilemez.").MaximumLength(50).WithMessage("Şirket kodu en fazla 50 karakter olabilir.");
        }
    }
}
