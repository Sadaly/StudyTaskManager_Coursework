using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetByGroup
{
    class GroupTaskGetAllByGroupIdQueryHandler : IQueryHandler<GroupTaskGetAllByGroupIdQuery, List<GroupTaskResponse>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTaskRepository _groupTaskRepository;

        public GroupTaskGetAllByGroupIdQueryHandler(IGroupRepository groupRepository, IGroupTaskRepository groupTaskRepository)
        {
            _groupRepository = groupRepository;
            _groupTaskRepository = groupTaskRepository;
        }

        public async Task<Result<List<GroupTaskResponse>>> Handle(GroupTaskGetAllByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return Result.Failure<List<GroupTaskResponse>>(group);

            var tasks = await _groupTaskRepository.GetByGroupAsync(group.Value, cancellationToken);
            if (tasks.IsFailure) return Result.Failure<List<GroupTaskResponse>>(tasks);

            var listRes = tasks.Value.Select(t => new GroupTaskResponse(t)).ToList();

            return listRes;
        }
    }
}
