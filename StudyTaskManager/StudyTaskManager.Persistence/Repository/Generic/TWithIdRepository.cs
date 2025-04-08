using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories.Generic;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository.Generic
{
    public abstract class TWithIdRepository<T> : TRepository<T>, IRepositoryWithID<T> where T : BaseEntityWithID
    {
        public TWithIdRepository(AppDbContext dbContext) : base(dbContext) { }

        protected abstract Error GetErrorNotFound();

        public async Task<Result<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            T? res = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (res == null)
                return Result.Failure<T>(GetErrorNotFound());
            return Result.Success(res);
        }

        public async Task<Result> RemoveAsync(Guid entityId, CancellationToken cancellationToken = default)
        {
            T? entity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == entityId);
            if (entity == null) return Result.Failure(GetErrorNotFound());

            return await RemoveAsync(entity, cancellationToken);
        }
    }
}
