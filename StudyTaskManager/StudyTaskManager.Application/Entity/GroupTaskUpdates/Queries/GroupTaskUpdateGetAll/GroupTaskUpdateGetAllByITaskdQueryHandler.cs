using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.UsersInGroup.Queries;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Queries.GroupTaskUpdateGetAll
{
    public class GroupTaskUpdateGetAllQueryHandler : IQueryHandler<GroupTaskUpdateGetAllQuery, List<GroupTaskUpdateResponse>>
    {
        private readonly IGroupTaskUpdateRepository _groupTaskUpdateRepository;

		public GroupTaskUpdateGetAllQueryHandler(IGroupTaskUpdateRepository groupTaskUpdateRepository)
		{
			_groupTaskUpdateRepository = groupTaskUpdateRepository;
		}

		public async Task<Result<List<GroupTaskUpdateResponse>>> Handle(GroupTaskUpdateGetAllQuery request, CancellationToken cancellationToken)
        {
			var listGTU = request.Predicate == null
				 ? await _groupTaskUpdateRepository.GetAllAsync(cancellationToken)
				 : await _groupTaskUpdateRepository.GetAllAsync(request.Predicate, cancellationToken);
			if (listGTU.IsFailure) return Result.Failure<List<GroupTaskUpdateResponse>>(listGTU);

			var listRes = listGTU.Value.Select(gtu => new GroupTaskUpdateResponse(gtu)).ToList();

			return listRes;
		}
    }
}