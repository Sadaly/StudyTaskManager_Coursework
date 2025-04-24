using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupInvites.Queries.GroupInviteGetAll
{
    public class GroupInviteGetAllQueryHandler : IQueryHandler<GroupInviteGetAllQuery, List<GroupInviteResponse>>
    {
        private readonly IGroupInviteRepository _groupInviteRepository;

        public GroupInviteGetAllQueryHandler(IGroupInviteRepository groupInviteRepository)
        {
            _groupInviteRepository = groupInviteRepository;
        }

        public async Task<Result<List<GroupInviteResponse>>> Handle(GroupInviteGetAllQuery request, CancellationToken cancellationToken)
        {
            var invites = request.Perdicate == null
                ? await _groupInviteRepository.GetAllAsync(cancellationToken)
                : await _groupInviteRepository.GetAllAsync(request.Perdicate, cancellationToken);

            if (invites.IsFailure) return Result.Failure<List<GroupInviteResponse>>(invites);

            return invites.Value.Select(gi => new GroupInviteResponse(gi)).ToList();
        }
    }
}
