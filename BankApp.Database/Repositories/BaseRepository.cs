using BankApp.Database.Context;
using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankApp.Database.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<Result<T>> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return Result<T>.Success(entity, "Kayıt başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return Result<T>.Failure(new List<string> { ex.Message }, "Kayıt eklenirken bir hata oluştu.");
            }
        }

        public async Task<Result<T>> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return Result<T>.Failure(new List<string> { "Kayıt bulunamadı." });
            }

            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return Result<T>.Success(entity, "Kayıt silindi.");
            }
            catch (Exception ex)
            {
                return Result<T>.Failure(new List<string> { ex.Message }, "Kayıt silme işleminde hata oluştu");
            }
        }

        public async Task<Result<List<T>>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            Result<List<T>> sonuc = new();
            if (filter == null)
            {
                var entities = await _dbSet.ToListAsync(); // Veriyi çekmek için `ToList()` çağrısı yapıldı.
                sonuc = Result<List<T>>.Success(entities, $"{entities.Count} adet kayıt listelendi.");
            }
            else
            {
                var entities = await _dbSet.Where(filter).ToListAsync(); // Veriyi çekmek için `ToList()` çağrısı yapıldı.
                if (entities.Any())
                {
                    sonuc = Result<List<T>>.Success(entities, $"{entities.Count} adet kayıt listelendi.");
                }
            }
            if (sonuc.IsSuccess)
            {
                return sonuc;
            }
            return Result<List<T>>.Failure(new List<string> { "Koşula uygun kayıt bulunamadı." });
        }

        public async Task<Result<T>> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                return Result<T>.Success(entity, "Kayıt bulundu.");
            }
            return Result<T>.Failure(new List<string> { "Kayıt bulunamadı." });
        }

        public  Result<T> Update(int id, T entity)
        {
            Result<T> response = new();
            Result<T> result = GetByIdAsync(id).Result;
            if (result.IsSuccess)
            {
                try
                {
                    _dbSet.Update(entity);
                    _context.SaveChanges();
                    // return Task.FromResult(Result<T>.Success(entity, "Kayıt başarıyla güncellendi."));
                    response= Result<T>.Success(entity, "Kayıt başarıyla güncellendi.");
                }
                catch (Exception ex)
                {
                    //  return Task.FromResult(Result<T>.Failure(new List<string> { ex.Message }, "Kayıt güncellenirken bir hata oluştu."));
                    response= Result<T>.Failure(new List<string> { ex.Message }, "Kayıt günceleme işleminde hata oluştu.");
                }

            }
            //return Task.FromResult(Result<T>.Failure(result.Errors, result.Message));
            // return Result<T>.Failure(result.Errors, result.Message);
            return response;
        }
    }
}
