using BankApp.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Database.Validators
{
    public class BankValidator : AbstractValidator<Bank>
    {
        public BankValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Banka adı boş olamaz").MaximumLength(20).WithMessage("Banka adı en fazla 20 karakter olabilir.");
        }
    }
}
