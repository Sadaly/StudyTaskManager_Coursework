using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Groups.Queries.GroupGetAll
{
    public class GroupGetAllQueryHandler : IQueryHandler<GroupGetAllQuery, List<GroupResponse>>
    {
        private readonly IGroupRepository _groupRepository;

        public GroupGetAllQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Result<List<GroupResponse>>> Handle(GroupGetAllQuery request, CancellationToken cancellationToken)
        {
            var groups = request.Predicate == null
                ? await _groupRepository.GetAllAsync(cancellationToken)
                : await _groupRepository.GetAllAsync(request.Predicate, cancellationToken);
            
            if (groups.IsFailure) return Result.Failure<List<GroupResponse>>(groups);
            
            return groups.Value.Select(g => new GroupResponse(g)).ToList();
        }
    }
}
