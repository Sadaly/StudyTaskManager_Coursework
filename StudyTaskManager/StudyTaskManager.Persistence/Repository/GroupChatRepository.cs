using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatRepository : Generic.TWithIdRepository<GroupChat>, IGroupChatRepository
    {
        public GroupChatRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
