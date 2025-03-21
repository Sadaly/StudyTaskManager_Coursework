using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class PersonalCharRepository : Generic.TWithIdRepository<PersonalChat>, IPersonalChatRepository
    {
        public PersonalCharRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<PersonalChat> GetChatByUsersAsync(User user1, User user2, CancellationToken cancellationToken = default)
        {
            PersonalChat? res = null;
            res = await _dbContext.Set<PersonalChat>()
                .FirstOrDefaultAsync(
                    pc =>
                        (pc.UserId1 == user1.Id && pc.UserId2 == user2.Id) ||
                        (pc.UserId1 == user2.Id && pc.UserId2 == user1.Id)
                    ,cancellationToken
                );
            if (res != null) return res;

            Result<PersonalChat> resultPersonalChat = PersonalChat.Create(user1, user2);
            if (resultPersonalChat.IsSuccess)
            {
                res = resultPersonalChat.Value;
            }
            else
            {
                throw new Exception();
            }
            await AddAsync(res, cancellationToken);
            return res;
        }
    }
}
