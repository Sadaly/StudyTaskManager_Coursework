using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Persistence.DB;

namespace StudyTaskManager.Persistence.Repository
{
    class PersonalCharRepository : Generic.TWithIdRepository<PersonalChat>, IPersonalChatRepository
    {
        public PersonalCharRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<PersonalChat> GetChatByUsersAsync(User user1, User user2, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
