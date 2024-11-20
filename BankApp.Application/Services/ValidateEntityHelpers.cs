using BankApp.Database.Repositories;
using FluentValidation;

internal static class ValidateEntityHelpers
{
    public static Result<T> ValidateEntity<T>(T entity, AbstractValidator<T> validator)
    {
        var validationResult = validator.Validate(entity);
        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return Result<T>.Failure(errorMessages, "Doğrulamalar başarısız.");
        }
        return Result<T>.Success(entity);
    }
}