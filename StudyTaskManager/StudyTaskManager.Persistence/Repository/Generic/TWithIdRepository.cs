using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories.Generic;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository.Generic
{
    public abstract class TWithIdRepository<T> : TRepository<T>, IRepositoryWithID<T> where T : BaseEntityWithID
    {
        public TWithIdRepository(AppDbContext dbContext) : base(dbContext) { }

        protected abstract Error GetErrorNotFound();
        protected abstract Error GetErrorMissingId();

        public async Task<Result<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) return Result.Failure<T>(GetErrorMissingId());
            T? entity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (entity == null) return Result.Failure<T>(GetErrorNotFound());
            return Result.Success(entity);
        }

        public async Task<Result> RemoveAsync(Guid entityId, CancellationToken cancellationToken = default)
        {
            if (entityId == Guid.Empty) return Result.Failure<T>(GetErrorMissingId());
            T? entity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
            if (entity == null) return Result.Failure(GetErrorNotFound());

            entity.Delete();                                        // Код аналогичен из родительского класса
            await UpdateAsync(entity, cancellationToken);           // 
            //_dbContext.Set<T>().Remove(entity);                     // 
            await _dbContext.SaveChangesAsync(cancellationToken);   // 
            return Result.Success();                                // 
        }
    }
}
