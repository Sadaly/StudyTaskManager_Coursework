using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatParticipantLastReadRepository : Generic.TRepository<GroupChatParticipantLastRead>, IGroupChatParticipantLastReadRepository
    {
        public GroupChatParticipantLastReadRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
