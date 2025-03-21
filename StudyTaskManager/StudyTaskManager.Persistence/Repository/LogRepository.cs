using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Log;

namespace StudyTaskManager.Persistence.Repository
{
    class LogRepository : Generic.TWithIdRepository<Log>, ILogRepository
    {
        public LogRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<Log> GetByIdWithLogActionAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
