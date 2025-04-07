using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatParticipantRepository : Generic.TRepository<GroupChatParticipant>, IGroupChatParticipantRepository
    {
        public GroupChatParticipantRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(GroupChatParticipant groupChatParticipant, CancellationToken cancellationToken = default)
        {
            User? user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == groupChatParticipant.UserId, cancellationToken);
            if (user == null) return Result.Failure(PersistenceErrors.User.NotFound);

            GroupChat? groupChat = await _dbContext.Set<GroupChat>().FirstOrDefaultAsync(gc => gc.Id == groupChatParticipant.GroupChatId, cancellationToken);
            if (groupChat == null) return Result.Failure(PersistenceErrors.GroupChat.NotFound);
            if (groupChat.IsPublic) return Result.Failure(PersistenceErrors.GroupChatParticipant.AddingToAPublicChat);

            await _dbContext.Set<GroupChatParticipant>().AddAsync(groupChatParticipant, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
