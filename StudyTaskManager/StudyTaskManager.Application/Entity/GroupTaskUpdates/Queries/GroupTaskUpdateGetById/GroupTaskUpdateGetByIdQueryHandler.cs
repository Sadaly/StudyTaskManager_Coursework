using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Queries.GroupTaskUpdateGetById
{
    public class GroupTaskUpdateGetByIdQueryHandler : IQueryHandler<GroupTaskUpdateGetByIdQuery, GroupTaskUpdateGetByIdResponse>
    {
        private readonly IGroupTaskUpdateRepository _groupTaskUpdateRepository;

        public GroupTaskUpdateGetByIdQueryHandler(IGroupTaskUpdateRepository groupTaskUpdateRepository)
        {
            _groupTaskUpdateRepository = groupTaskUpdateRepository;
        }

        public async Task<Result<GroupTaskUpdateGetByIdResponse>> Handle(GroupTaskUpdateGetByIdQuery request, CancellationToken cancellationToken)
        {
            var update = await _groupTaskUpdateRepository.GetByIdAsync(request.Id, cancellationToken);
            if (update.IsFailure) return Result.Failure<GroupTaskUpdateGetByIdResponse>(update);

            return new GroupTaskUpdateGetByIdResponse(update.Value);
        }
    }
}
