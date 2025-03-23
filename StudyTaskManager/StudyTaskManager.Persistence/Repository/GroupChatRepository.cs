using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatRepository : Generic.TWithIdRepository<GroupChat>
    {
        public GroupChatRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
