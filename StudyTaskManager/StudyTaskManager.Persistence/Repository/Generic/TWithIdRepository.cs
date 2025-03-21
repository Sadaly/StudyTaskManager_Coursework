using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories.Generic;
using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Persistence.Repository.Generic
{
    public class TWithIdRepository<T> : TRepository<T>, IRepositoryWithID<T> where T : BaseEntityWithID
    {
        public TWithIdRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
