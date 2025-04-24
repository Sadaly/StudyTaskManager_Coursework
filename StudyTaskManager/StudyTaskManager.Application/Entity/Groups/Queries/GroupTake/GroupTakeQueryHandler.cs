using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Groups.Queries.GroupTake
{
    public class GroupTakeQueryHandler : IQueryHandler<GroupTakeQuery, List<GroupResponse>>
    {
        private readonly IGroupRepository _groupRepository;

        public GroupTakeQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Result<List<GroupResponse>>> Handle(GroupTakeQuery request, CancellationToken cancellationToken)
        {
            var groups = request.Perdicate == null
                ? await _groupRepository.GetAllAsync(request.StartIndex, request.Count, cancellationToken)
                : await _groupRepository.GetAllAsync(request.StartIndex, request.Count, request.Perdicate, cancellationToken);

            if (groups.IsFailure) return Result.Failure<List<GroupResponse>>(groups);

            return groups.Value.Select(g => new GroupResponse(g)).ToList();
        }
    }
}
