using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class PersonalCharRepository : Generic.TWithIdRepository<PersonalChat>, IPersonalChatRepository
    {
        public PersonalCharRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<PersonalChat>> GetChatByUsersAsync(User user1, User user2, CancellationToken cancellationToken = default)
        {
            Result<PersonalChat> personalChat;

            personalChat = await GetFromDBAsync(
                pc =>
                    (pc.User1Id == user1.Id && pc.User2Id == user2.Id) ||
                    (pc.User1Id == user2.Id && pc.User2Id == user1.Id)
                , cancellationToken);
            if (personalChat.IsFailure)
            {
                if (personalChat.Error != GetErrorNotFound()) { return personalChat; }
            }
            else { return personalChat; }

            personalChat = PersonalChat.Create(user1, user2);
            if (personalChat.IsSuccess)
            {
                await AddAsync(personalChat.Value, cancellationToken);
            }
            return personalChat;
        }

        protected override Error GetErrorIdEmpty()
        {
            return PersistenceErrors.PersonalChat.IdEmpty;
        }

        protected override Error GetErrorNotFound()
        {
            return PersistenceErrors.PersonalChat.NotFound;
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(PersonalChat entity, CancellationToken cancellationToken)
        {
            if (entity.User1Id == entity.User2Id) return Result.Failure(PersistenceErrors.PersonalChat.SameUser);

            Result<object> obj;

            Error idEmpty = PersistenceErrors.User.IdEmpty;
            Error notFound = PersistenceErrors.User.NotFound;
            obj = await GetFromDBAsync<User>(entity.User1Id, idEmpty, notFound, cancellationToken);
            if (obj.IsFailure) return obj;
            obj = await GetFromDBAsync<User>(entity.User2Id, idEmpty, notFound, cancellationToken);
            if (obj.IsFailure) return obj;

            obj = await GetFromDBAsync(entity.Id, cancellationToken);
            if (obj.IsFailure)
            {
                if (obj.Error != GetErrorNotFound()) { return obj; }
            }
            else { return Result.Failure(PersistenceErrors.PersonalChat.AlreadyExists); }

            obj = await GetFromDBAsync(
                pc =>
                    (pc.User1Id == entity.User1Id && pc.User2Id == entity.User2Id) ||
                    (pc.User1Id == entity.User2Id && pc.User2Id == entity.User1Id)
                , cancellationToken);
            if (obj.IsFailure) { return Result.Success(); }
            return Result.Failure(PersistenceErrors.PersonalChat.AlreadyExists);
        }
    }
}
