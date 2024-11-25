using BankApp.Domain.Entities;
using FluentValidation;

namespace BankApp.Database.Validators
{
    public class ProcessTypeValidator:AbstractValidator<ProcessType>
    {
        public ProcessTypeValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("İşlem adı boş geçilemez.").MaximumLength(10).WithMessage("Adı alanı en fazla 10 karakter olabilir.");
            RuleFor(x => x.Symbol).MinimumLength(1).WithMessage("İşlem sembolü en az 1 karakter olabilir.").MaximumLength(1).WithMessage("İşlem sembolü en fazla 1 karakter olabilir.").NotNull().WithMessage("İşlem sembolü boş geçilemez.").Must(value => value == "+" || value == "-").WithMessage("İşlem sembolü + veya - olabilir.");
            RuleFor(x => x.Multiplier).NotNull().WithMessage("Çarpan boş geçilemez.").Must(value=>value==1 || value==-1).WithMessage("Çarpan girişler için 1, çıkışlar için -1 olmalıdır.");
        }
    }
}
