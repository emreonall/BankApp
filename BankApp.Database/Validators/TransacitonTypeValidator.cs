using BankApp.Domain.Entities;
using FluentValidation;

namespace BankApp.Database.Validators
{
    public class TransacitonTypeValidator:AbstractValidator<TransactionType>
    {
        public TransacitonTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Hareket türü adı boş geçilemez").MaximumLength(50).WithMessage("Hareket türü adı alanı en fazla 50 karakter olaiblir.");
            
        }
    }
}
