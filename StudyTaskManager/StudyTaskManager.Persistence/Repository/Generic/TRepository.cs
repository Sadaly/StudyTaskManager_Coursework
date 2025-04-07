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

        public abstract Task<Result> AddAsync(T entity, CancellationToken cancellationToken = default);

        public async Task<Result<List<T>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<Result> RemoveAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity.Delete();
            await UpdateAsync(entity, cancellationToken);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
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
