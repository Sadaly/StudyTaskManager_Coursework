using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetById
{
    class GroupTaskGetByIdQeuryHandler : IQueryHandler<GroupTaskGetByIdQeury, GroupTaskGetByIdResponse>
    {
        private readonly IGroupTaskRepository _groupTaskRepository;

        public GroupTaskGetByIdQeuryHandler(IGroupTaskRepository groupTaskRepository)
        {
            _groupTaskRepository = groupTaskRepository;
        }

        public async Task<Result<GroupTaskGetByIdResponse>> Handle(GroupTaskGetByIdQeury request, CancellationToken cancellationToken)
        {
            var task = await _groupTaskRepository.GetByIdAsync(request.Id, cancellationToken);
            if (task.IsFailure) return Result.Failure<GroupTaskGetByIdResponse>(task);

            return new GroupTaskGetByIdResponse(task.Value);
        }
    }
}
