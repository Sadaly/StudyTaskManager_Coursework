using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class PersonalMessageRepository : Generic.TWithIdRepository<PersonalMessage>, IPresonalMessageRepository
    {
        public PersonalMessageRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<PersonalMessage>>> GetMessageByChatAsync(PersonalChat personalChat, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<PersonalMessage>()
                .Where(pm => pm.PersonalChatId == personalChat.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        protected override Error GetErrorIdEmpty()
        {
            return PersistenceErrors.PersonalMessage.IdEmpty;
        }

        protected override Error GetErrorNotFound()
        {
            return PersistenceErrors.PersonalMessage.IdEmpty;
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(PersonalMessage entity, CancellationToken cancellationToken)
        {
            Result<object> obj;

            obj = await GetFromDBAsync<User>(entity.SenderId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (obj.IsFailure) { return obj; }

            obj = await GetFromDBAsync<PersonalChat>(entity.PersonalChatId, PersistenceErrors.PersonalChat.IdEmpty, PersistenceErrors.PersonalChat.NotFound, cancellationToken);
            if (obj.IsFailure) { return obj; }

            obj = await GetFromDBAsync(entity.Id, cancellationToken);
            if (obj.IsFailure) { return Result.Success(); }
            return Result.Failure(PersistenceErrors.PersonalMessage.AlreadyExists);
        }
    }
}
