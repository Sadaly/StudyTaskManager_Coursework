using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Log;

namespace StudyTaskManager.Persistence.Repository
{
    class LogActionRepository : Generic.TWithIdRepository<LogAction>, ILogActionRepository
    {
        public LogActionRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
