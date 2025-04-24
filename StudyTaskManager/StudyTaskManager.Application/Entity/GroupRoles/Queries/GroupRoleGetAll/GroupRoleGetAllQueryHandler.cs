using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupRoles.Queries.GroupRoleGetAll
{
    public class GroupRoleGetAllQueryHandler : IQueryHandler<GroupRoleGetAllQuery, List<GroupRoleResponse>>
    {
        private readonly IGroupRoleRepository _groupRoleRepository;

        public GroupRoleGetAllQueryHandler(IGroupRoleRepository groupRoleRepository)
        {
            _groupRoleRepository = groupRoleRepository;
        }

        public async Task<Result<List<GroupRoleResponse>>> Handle(GroupRoleGetAllQuery request, CancellationToken cancellationToken)
        {
            var roles = request.Perdicate == null
                ? await _groupRoleRepository.GetAllAsync(cancellationToken)
                : await _groupRoleRepository.GetAllAsync(request.Perdicate, cancellationToken);

            if (roles.IsFailure) return Result.Failure<List<GroupRoleResponse>>(roles);

            return roles.Value.Select(r => new GroupRoleResponse(r)).ToList();
        }
    }
}
