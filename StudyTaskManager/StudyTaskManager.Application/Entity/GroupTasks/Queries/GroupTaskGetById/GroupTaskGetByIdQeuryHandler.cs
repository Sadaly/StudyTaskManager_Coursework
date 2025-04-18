using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetById
{
    class GroupTaskGetByIdQeuryHandler : IQueryHandler<GroupTaskGetByIdQeury, GroupTask>
    {
        private readonly IGroupTaskRepository _groupTaskRepository;

        public GroupTaskGetByIdQeuryHandler(IGroupTaskRepository groupTaskRepository)
        {
            _groupTaskRepository = groupTaskRepository;
        }

        public async Task<Result<GroupTask>> Handle(GroupTaskGetByIdQeury request, CancellationToken cancellationToken)
        {
            return await _groupTaskRepository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
