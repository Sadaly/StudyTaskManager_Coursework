using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Persistence.DB;

namespace StudyTaskManager.Persistence.Repository
{
    class PersonalMessageRepository : Generic.TWithIdRepository<PersonalMessage>, IPresonalMessageRepository
    {
        public PersonalMessageRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<List<PersonalMessage>> GetMessageByChatAsync(PersonalChat personalChat, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
