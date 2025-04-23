using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Queries.GroupTaskUpdateGetByITaskd
{
    public class GroupTaskUpdateGetByITaskdQueryHandler : IQueryHandler<GroupTaskUpdateGetByITaskdQuery, List<GroupTaskUpdateResponse>>
    {
        private readonly IGroupTaskUpdateRepository _groupTaskUpdateRepository;
        private readonly IGroupTaskRepository _groupTaskRepository;

        public GroupTaskUpdateGetByITaskdQueryHandler(IGroupTaskUpdateRepository groupTaskUpdateRepository, IGroupTaskRepository groupTaskRepository)
        {
            _groupTaskUpdateRepository = groupTaskUpdateRepository;
            _groupTaskRepository = groupTaskRepository;
        }

        public async Task<Result<List<GroupTaskUpdateResponse>>> Handle(GroupTaskUpdateGetByITaskdQuery request, CancellationToken cancellationToken)
        {
            var task = await _groupTaskRepository.GetByIdAsync(request.TaskId, cancellationToken);
            if (task.IsFailure) return Result.Failure<List<GroupTaskUpdateResponse>>(task);

            var updates = await _groupTaskUpdateRepository.GetByTaskAsync(request.StartIndex, request.Count, task.Value, cancellationToken);
            if (updates.IsFailure) return Result.Failure<List<GroupTaskUpdateResponse>>(updates);

            var listRes = updates.Value.Select(gtu => new GroupTaskUpdateResponse(gtu)).ToList();

            return listRes;
        }
    }
}