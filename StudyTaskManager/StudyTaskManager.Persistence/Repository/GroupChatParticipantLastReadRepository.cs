using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatParticipantLastReadRepository : Generic.TRepository<GroupChatParticipantLastRead>
    {
        public GroupChatParticipantLastReadRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
