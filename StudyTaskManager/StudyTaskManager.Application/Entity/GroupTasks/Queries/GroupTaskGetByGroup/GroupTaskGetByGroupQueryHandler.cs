using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetByGroup
{
    class GroupTaskGetByGroupQueryHandler : IQueryHandler<GroupTaskGetByGroupQuery, List<GroupTask>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTaskRepository _groupTaskRepository;

        public GroupTaskGetByGroupQueryHandler(IGroupRepository groupRepository, IGroupTaskRepository groupTaskRepository)
        {
            _groupRepository = groupRepository;
            _groupTaskRepository = groupTaskRepository;
        }

        public async Task<Result<List<GroupTask>>> Handle(GroupTaskGetByGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return Result.Failure<List<GroupTask>>(group);

            return await _groupTaskRepository.GetByGroupAsync(group.Value, cancellationToken);
        }
    }
}
