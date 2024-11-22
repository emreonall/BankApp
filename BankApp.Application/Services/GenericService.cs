using BankApp.Database.Repositories;
using BankApp.Domain.Entities;
using FluentValidation;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BankApp.Application.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IValidator<T> _validator;

        public GenericService(IBaseRepository<T> repository, IValidator<T> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<T>> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result.IsSuccess
                ? Result<T>.Success(result.Data, result.Message)
                : Result<T>.Failure(result.Errors, result.Message);
        }

        public async Task<Result<List<T>>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            var result = filter == null
                ? await _repository.GetAllAsync()
                : await _repository.GetAllAsync(filter);

            return result.IsSuccess
                ? Result<List<T>>.Success(result.Data, result.Message)
                : Result<List<T>>.Failure(result.Errors, result.Message);
        }

        public async Task<Result<T>> AddAsync(T entity, IValidator<T> validator)
        {
            var validationResult =ValidateEntity(entity,validator);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var result = await _repository.AddAsync(entity);
            return result.IsSuccess
                ? Result<T>.Success(result.Data, result.Message)
                : Result<T>.Failure(result.Errors, result.Message);
        }

        public async Task<Result<T>> Update(int id, T entity, IValidator<T> validator)
        {
            var validationResult = ValidateEntity(entity,validator);

            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var existingEntity = await _repository.GetByIdAsync(id);
            if (!existingEntity.IsSuccess || existingEntity.Data == null)
            {
                return Result<T>.Failure(existingEntity.Errors, existingEntity.Message);
            }

            try
            {
                var updateResult = _repository.Update(id, entity);
                return updateResult.IsSuccess
                    ? Result<T>.Success(updateResult.Data, updateResult.Message)
                    : Result<T>.Failure(updateResult.Errors, updateResult.Message);
            }
            catch (Exception ex)
            {

                return Result<T>.Failure(new List<string> { ex.Message }, "Kayıt güncellenirken hata oluştu.");
            }
          
        }

        public async Task<Result<T>> DeleteAsync(int id)
        {
            var result = await _repository.Delete(id);
            return result.IsSuccess
                ? Result<T>.Success(result.Data, result.Message)
                : Result<T>.Failure(result.Errors, result.Message);
        }
        private Result<T> ValidateEntity(T entity, IValidator<T> validator)
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

}
