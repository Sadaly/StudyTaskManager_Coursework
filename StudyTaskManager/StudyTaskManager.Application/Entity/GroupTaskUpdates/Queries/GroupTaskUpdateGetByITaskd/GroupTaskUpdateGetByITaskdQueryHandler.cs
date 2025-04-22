using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Queries.GroupTaskUpdateGetByITaskd
{
    public class GroupTaskUpdateGetByITaskdQueryHandler : IQueryHandler<GroupTaskUpdateGetByITaskdQuery, List<GroupTaskUpdateGetByITaskdResponseElements>>
    {
        private readonly IGroupTaskUpdateRepository _groupTaskUpdateRepository;
        private readonly IGroupTaskRepository _groupTaskRepository;

        public GroupTaskUpdateGetByITaskdQueryHandler(IGroupTaskUpdateRepository groupTaskUpdateRepository, IGroupTaskRepository groupTaskRepository)
        {
            _groupTaskUpdateRepository = groupTaskUpdateRepository;
            _groupTaskRepository = groupTaskRepository;
        }

        public async Task<Result<List<GroupTaskUpdateGetByITaskdResponseElements>>> Handle(GroupTaskUpdateGetByITaskdQuery request, CancellationToken cancellationToken)
        {
            var task = await _groupTaskRepository.GetByIdAsync(request.TaskId, cancellationToken);
            if (task.IsFailure) return Result.Failure<List<GroupTaskUpdateGetByITaskdResponseElements>>(task);

            var updates = await _groupTaskUpdateRepository.GetByTaskAsync(request.StartIndex, request.Count, task.Value, cancellationToken);
            if (updates.IsFailure) return Result.Failure<List<GroupTaskUpdateGetByITaskdResponseElements>>(updates);

            var listRes = updates.Value.Select(gtu => new GroupTaskUpdateGetByITaskdResponseElements(gtu)).ToList();

            return listRes;
        }
    }
}