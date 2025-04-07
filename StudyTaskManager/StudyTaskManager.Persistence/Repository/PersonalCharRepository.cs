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

        public override async Task<Result> AddAsync(PersonalChat personalChat, CancellationToken cancellationToken = default)
        {
            if (personalChat.User1Id == personalChat.User2Id) return Result.Failure(PersistenceErrors.PersonalChat.SameUser);

            User? user_1 = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == personalChat.User1Id, cancellationToken);
            if (user_1 == null) return Result.Failure(PersistenceErrors.User.NotFound);
            User? user_2 = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == personalChat.User2Id, cancellationToken);
            if (user_2 == null) return Result.Failure(PersistenceErrors.User.NotFound);

            PersonalChat? personalChat_old = await _dbContext.Set<PersonalChat>()
                .FirstOrDefaultAsync(
                    pc =>
                        (pc.User1Id == personalChat.User1Id && pc.User2Id == personalChat.User2Id) ||
                        (pc.User1Id == personalChat.User2Id && pc.User2Id == personalChat.User1Id)
                    , cancellationToken
                );
            if (personalChat_old != null) return Result.Failure(PersistenceErrors.PersonalChat.AlreadyExists);

            await _dbContext.Set<PersonalChat>().AddAsync(personalChat, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<PersonalChat>> GetChatByUsersAsync(User user1, User user2, CancellationToken cancellationToken = default)
        {
            PersonalChat? res = null;
            res = await _dbContext.Set<PersonalChat>()
                .FirstOrDefaultAsync(
                    pc =>
                        (pc.User1Id == user1.Id && pc.User2Id == user2.Id) ||
                        (pc.User1Id == user2.Id && pc.User2Id == user1.Id)
                    , cancellationToken
                );
            if (res != null) return res;

            Result<PersonalChat> resultPersonalChat = PersonalChat.Create(user1, user2);
            if (resultPersonalChat.IsSuccess)
            {
                res = resultPersonalChat.Value;
                await AddAsync(res, cancellationToken);
                return res;
            }
            return resultPersonalChat;
        }
    }
}
