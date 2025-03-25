using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Log;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class LogRepository : Generic.TWithIdRepository<Log>, ILogRepository
    {
        public LogRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<Result<Log>> GetByIdWithLogActionAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
