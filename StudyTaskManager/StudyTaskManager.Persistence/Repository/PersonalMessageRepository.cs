using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class PersonalMessageRepository : Generic.TWithIdRepository<PersonalMessage>, IPresonalMessageRepository
    {
        public PersonalMessageRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(PersonalMessage personalMessage, CancellationToken cancellationToken = default)
        {
            User? sender = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == personalMessage.SenderId, cancellationToken);
            if (sender == null) return Result.Failure(PersistenceErrors.User.NotFound);

            PersonalChat? personalChat = await _dbContext.Set<PersonalChat>().FirstOrDefaultAsync(pc => pc.Id == personalMessage.PersonalChatId);
            if (personalChat == null) return Result.Failure(PersistenceErrors.PersonalChat.NotFound);

            await _dbContext.Set<PersonalMessage>().AddAsync(personalMessage, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<List<PersonalMessage>>> GetMessageByChatAsync(PersonalChat personalChat, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<PersonalMessage>()
                .Where(pm => pm.PersonalChatId == personalChat.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
