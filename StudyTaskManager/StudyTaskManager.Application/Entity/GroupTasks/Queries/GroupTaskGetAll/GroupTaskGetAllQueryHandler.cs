using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetByGroup
{
    class GroupTaskGetAllQueryHandler : IQueryHandler<GroupTaskGetAllQuery, List<GroupTaskResponse>>
    {
        private readonly IGroupTaskRepository _groupTaskRepository;

        public GroupTaskGetAllQueryHandler(IGroupTaskRepository groupTaskRepository)
        {
            _groupTaskRepository = groupTaskRepository;
        }

        public async Task<Result<List<GroupTaskResponse>>> Handle(GroupTaskGetAllQuery request, CancellationToken cancellationToken)
        {
			var listUIG = request.Predicate == null
				? await _groupTaskRepository.GetAllAsync(cancellationToken)
				: await _groupTaskRepository.GetAllAsync(request.Predicate, cancellationToken);
			if (listUIG.IsFailure) return Result.Failure<List<GroupTaskResponse>>(listUIG);

			var listRes = listUIG.Value.Select(uig => new GroupTaskResponse(uig)).ToList();

			return listRes;
		}
    }
}
