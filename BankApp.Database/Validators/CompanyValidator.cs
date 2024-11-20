using BankApp.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Database.Validators
{
    public class CompanyValidator:AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(x=>x.CompanyCode).NotNull().WithMessage("Şirket kodu boş geçilemez.").NotEmpty().WithMessage("Şirket kodu boş geçilemez.").MaximumLength(5).WithMessage("Şirket kodu en fazla 5 karakter olabilir.");

            RuleFor(x => x.CompanyName).NotNull().WithMessage("Şirket adı boş geçilemez.").NotEmpty().WithMessage("Şirket adı boş geçilemez.").MaximumLength(50).WithMessage("Şirket kodu en fazla 50 karakter olabilir.");
        }
    }
}
