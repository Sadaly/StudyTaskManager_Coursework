using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetByGroupWithBase
{

    public class GroupTaskStatusGetByGroupWithBaseQueryHandler : IQueryHandler<GroupTaskStatusGetByGroupWithBaseQuery, List<GroupTaskStatusGetByGroupWithBaseRepsonseElements>>
    {
        private readonly IGroupTaskStatusRepository _groupTaskStatusRepository;
        private readonly IGroupRepository _groupRepository;

        public GroupTaskStatusGetByGroupWithBaseQueryHandler(IGroupTaskStatusRepository groupTaskStatusRepository, IGroupRepository groupRepository)
        {
            _groupTaskStatusRepository = groupTaskStatusRepository;
            _groupRepository = groupRepository;
        }

        public async Task<Result<List<GroupTaskStatusGetByGroupWithBaseRepsonseElements>>> Handle(GroupTaskStatusGetByGroupWithBaseQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return Result.Failure<List<GroupTaskStatusGetByGroupWithBaseRepsonseElements>>(group);

            var listStatus = await _groupTaskStatusRepository.GetByGroupWithBaseAsync(group.Value, cancellationToken);
            if (listStatus.IsFailure) return Result.Failure<List<GroupTaskStatusGetByGroupWithBaseRepsonseElements>>(listStatus);

            var listRes = listStatus.Value.Select(s => new GroupTaskStatusGetByGroupWithBaseRepsonseElements(s)).ToList();

            return listRes;
        }
    }
}