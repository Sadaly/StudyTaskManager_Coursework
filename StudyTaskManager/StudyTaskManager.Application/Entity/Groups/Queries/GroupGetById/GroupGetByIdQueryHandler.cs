using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Groups.Queries.GroupGetById
{
    public class GroupGetByIdQueryHandler : IQueryHandler<GroupGetByIdQuery, GroupGetByIdResponse>
    {
        private readonly IGroupRepository _groupRepository;

        public GroupGetByIdQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Result<GroupGetByIdResponse>> Handle(GroupGetByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.Id, cancellationToken);
            if (group.IsFailure) return Result.Failure<GroupGetByIdResponse>(group);

            return new GroupGetByIdResponse(group.Value);
        }
    }
}
