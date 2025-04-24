using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetAll
{

    public class GroupTaskStatusGetByGroupIdQueryHandler : IQueryHandler<GroupTaskStatusGetAllQuery, List<GroupTaskStatusRepsonse>>
    {
        private readonly IGroupTaskStatusRepository _groupTaskStatusRepository;

        public GroupTaskStatusGetByGroupIdQueryHandler(IGroupTaskStatusRepository groupTaskStatusRepository)
        {
            _groupTaskStatusRepository = groupTaskStatusRepository;
        }

        public async Task<Result<List<GroupTaskStatusRepsonse>>> Handle(GroupTaskStatusGetAllQuery request, CancellationToken cancellationToken)
        {
			var listStatus = request.Predicate == null
			   ? await _groupTaskStatusRepository.GetAllAsync(cancellationToken)
			   : await _groupTaskStatusRepository.GetAllAsync(request.Predicate, cancellationToken);
            if (listStatus.IsFailure) return Result.Failure<List<GroupTaskStatusRepsonse>>(listStatus);

            var listRes = listStatus.Value.Select(s => new GroupTaskStatusRepsonse(s)).ToList();

            return listRes;
        }
    }
}