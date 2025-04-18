using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    public class PersonalCharRepository : Generic.TWithIdRepository<PersonalChat>, IPersonalChatRepository
    {
        public PersonalCharRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<PersonalChat>>> GetChatByUserAsync(User user, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(
                    pc =>
                        pc.User1Id == user.Id ||
                        pc.User2Id == user.Id)
                .Include(pc => pc.Messages) // подгрузка сообщений, думаю стоит переделать, чтобы возвращать определенное кол-во последних сообщений из чата
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<PersonalChat>> GetChatByUsersAsync(User user1, User user2, CancellationToken cancellationToken = default)
        {
            Result<PersonalChat> personalChat;
            personalChat = await GetFromDBAsync(
                pc =>
                    (pc.User1Id == user1.Id && pc.User2Id == user2.Id) ||
                    (pc.User1Id == user2.Id && pc.User2Id == user1.Id)
                , cancellationToken);
            //if (personalChat.IsSuccess) { return personalChat; }
            //if (personalChat.Error != GetErrorNotFound()) { return personalChat; }

            //personalChat = PersonalChat.Create(user1, user2);
            //if (personalChat.IsSuccess)
            //{
            //    await AddAsync(personalChat.Value, cancellationToken);
            //}
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

            Error idEmpty = PersistenceErrors.User.IdEmpty;
            Error notFound = PersistenceErrors.User.NotFound;
            var user1 = await GetFromDBAsync<User>(entity.User1Id, idEmpty, notFound, cancellationToken);
            if (user1.IsFailure) return user1;
            var user2 = await GetFromDBAsync<User>(entity.User2Id, idEmpty, notFound, cancellationToken);
            if (user2.IsFailure) return user2;

            var personalChat = await GetFromDBAsync(entity.Id, cancellationToken);
            if (personalChat.IsSuccess) return Result.Failure(PersistenceErrors.PersonalChat.AlreadyExists);

            personalChat = await GetFromDBAsync(
                pc =>
                    (pc.User1Id == entity.User1Id && pc.User2Id == entity.User2Id) ||
                    (pc.User1Id == entity.User2Id && pc.User2Id == entity.User1Id)
                , cancellationToken);
            if (personalChat.IsSuccess) return Result.Failure(PersistenceErrors.PersonalChat.AlreadyExists);
            return Result.Success();
        }
    }
}
