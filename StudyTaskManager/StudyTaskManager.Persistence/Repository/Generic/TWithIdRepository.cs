using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories.Generic;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository.Generic
{
    public abstract class TWithIdRepository<T> : TRepository<T>, IRepositoryWithID<T> where T : BaseEntityWithID
    {
        public TWithIdRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<T?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            T? res = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            //if (res == null)
            //    return Result.Failure<T?>(new Error(
            //        $"{typeof(T)}.NotFound",
            //        $"Элемент {typeof(T)} с указанным id: {id} не найден"));
            return Result.Success(res);
        }
    }
}
