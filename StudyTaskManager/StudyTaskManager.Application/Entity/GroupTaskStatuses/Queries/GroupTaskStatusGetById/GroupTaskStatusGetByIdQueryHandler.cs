using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetById
{
    public class GroupTaskStatusGetByIdQueryHandler : IQueryHandler<GroupTaskStatusGetByIdQuery, GroupTaskStatusRepsonse>
    {
        private readonly IGroupTaskStatusRepository _groupTaskStatusRepository;

        public GroupTaskStatusGetByIdQueryHandler(IGroupTaskStatusRepository groupTaskStatusRepository)
        {
            _groupTaskStatusRepository = groupTaskStatusRepository;
        }

        public async Task<Result<GroupTaskStatusRepsonse>> Handle(GroupTaskStatusGetByIdQuery request, CancellationToken cancellationToken)
        {
            var status = await _groupTaskStatusRepository.GetByIdAsync(request.Id, cancellationToken);
            if (status.IsFailure) return Result.Failure<GroupTaskStatusRepsonse>(status);

            return new GroupTaskStatusRepsonse(status.Value);
        }
    }
}
