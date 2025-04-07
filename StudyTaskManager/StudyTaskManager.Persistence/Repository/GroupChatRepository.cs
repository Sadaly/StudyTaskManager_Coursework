using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatRepository : Generic.TWithIdRepository<GroupChat>, IGroupChatRepository
    {
        public GroupChatRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(GroupChat groupChat, CancellationToken cancellationToken = default)
        {
            Group? group = await _dbContext.Set<Group>().FirstOrDefaultAsync(g => g.Id == groupChat.GroupId, cancellationToken);
            if (group == null) return Result.Failure(PersistenceErrors.Group.NotFound);

            bool notUniqueName = await _dbContext.Set<GroupChat>().AnyAsync(gc => gc.Name.Value == groupChat.Name.Value, cancellationToken);
            if (notUniqueName) return Result.Failure(PersistenceErrors.GroupChat.NotUniqueName);

            await _dbContext.Set<GroupChat>().AddAsync(groupChat, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
