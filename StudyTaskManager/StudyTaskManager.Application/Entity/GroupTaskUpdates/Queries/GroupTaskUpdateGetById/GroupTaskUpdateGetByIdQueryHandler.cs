using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Queries.GroupTaskUpdateGetById
{
    public class GroupTaskUpdateGetByIdQueryHandler : IQueryHandler<GroupTaskUpdateGetByIdQuery, GroupTaskUpdateResponse>
    {
        private readonly IGroupTaskUpdateRepository _groupTaskUpdateRepository;

        public GroupTaskUpdateGetByIdQueryHandler(IGroupTaskUpdateRepository groupTaskUpdateRepository)
        {
            _groupTaskUpdateRepository = groupTaskUpdateRepository;
        }

        public async Task<Result<GroupTaskUpdateResponse>> Handle(GroupTaskUpdateGetByIdQuery request, CancellationToken cancellationToken)
        {
            var update = await _groupTaskUpdateRepository.GetByIdAsync(request.Id, cancellationToken);
            if (update.IsFailure) return Result.Failure<GroupTaskUpdateResponse>(update);

            return new GroupTaskUpdateResponse(update.Value);
        }
    }
}
