using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories.Generic;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository.Generic
{
    public abstract class TRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext = null!;

        public TRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Получение сущности из базы данных, если её нет, то должна быть ошибка, необходимо для проверки
        /// </summary>
        protected abstract Result<T> GetFromDatabase(T entity);

        public abstract Task<Result> AddAsync(T entity, CancellationToken cancellationToken = default);

        public async Task<Result<List<T>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<Result> RemoveAsync(T entity, CancellationToken cancellationToken = default)
        {
            Result<T> entityInDatabase = GetFromDatabase(entity);
            if (entityInDatabase.IsFailure) return entityInDatabase;

            entity.Delete();
            await UpdateAsync(entity, cancellationToken);
            //_dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            Result<T> entityInDatabase = GetFromDatabase(entity);
            if (entityInDatabase.IsFailure) return entityInDatabase;

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
