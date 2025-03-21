using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Persistence.Repository
{
    class PersonalMessageRepository : Generic.TWithIdRepository<PersonalMessage>, IPresonalMessageRepository
    {
        public PersonalMessageRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<PersonalMessage>> GetMessageByChatAsync(PersonalChat personalChat, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<PersonalMessage>()
                .AsNoTracking()
                .Where(pm => pm.PersonalChatId == personalChat.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
