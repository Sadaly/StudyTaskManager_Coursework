using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetByGroup
{
    class GroupTaskGetByGroupQueryHandler : IQueryHandler<GroupTaskGetByGroupQuery, List<GroupTaskGetByGroupResponseElements>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTaskRepository _groupTaskRepository;

        public GroupTaskGetByGroupQueryHandler(IGroupRepository groupRepository, IGroupTaskRepository groupTaskRepository)
        {
            _groupRepository = groupRepository;
            _groupTaskRepository = groupTaskRepository;
        }

        public async Task<Result<List<GroupTaskGetByGroupResponseElements>>> Handle(GroupTaskGetByGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return Result.Failure<List<GroupTaskGetByGroupResponseElements>>(group);

            var tasks = await _groupTaskRepository.GetByGroupAsync(group.Value, cancellationToken);
            if (tasks.IsFailure) return Result.Failure<List<GroupTaskGetByGroupResponseElements>>(tasks);

            var listRes = tasks.Value.Select(t => new GroupTaskGetByGroupResponseElements(t)).ToList();

            return listRes;
        }
    }
}
