using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetByGroupWithBase
{

    public class GroupTaskStatusGetByGroupIdQueryHandler : IQueryHandler<GroupTaskStatusGetByGroupIdQuery, List<GroupTaskStatusRepsonse>>
    {
        private readonly IGroupTaskStatusRepository _groupTaskStatusRepository;
        private readonly IGroupRepository _groupRepository;

        public GroupTaskStatusGetByGroupIdQueryHandler(IGroupTaskStatusRepository groupTaskStatusRepository, IGroupRepository groupRepository)
        {
            _groupTaskStatusRepository = groupTaskStatusRepository;
            _groupRepository = groupRepository;
        }

        public async Task<Result<List<GroupTaskStatusRepsonse>>> Handle(GroupTaskStatusGetByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return Result.Failure<List<GroupTaskStatusRepsonse>>(group);

            var listStatus = await _groupTaskStatusRepository.GetByGroupWithBaseAsync(group.Value, cancellationToken);
            if (listStatus.IsFailure) return Result.Failure<List<GroupTaskStatusRepsonse>>(listStatus);

            var listRes = listStatus.Value.Select(s => new GroupTaskStatusRepsonse(s)).ToList();

            return listRes;
        }
    }
}