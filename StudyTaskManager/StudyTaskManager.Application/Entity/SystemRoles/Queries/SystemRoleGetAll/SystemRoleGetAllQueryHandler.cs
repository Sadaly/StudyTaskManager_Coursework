using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.SystemRoles.Queries.SystemRoleGetAll
{
    public class SystemRoleGetAllQueryHandler : IQueryHandler<SystemRoleGetAllQuery, List<SystemRoleResponse>>
    {
        private readonly ISystemRoleRepository _systemRoleRepository;

        public SystemRoleGetAllQueryHandler(ISystemRoleRepository systemRoleRepository)
        {
            _systemRoleRepository = systemRoleRepository;
        }

        public async Task<Result<List<SystemRoleResponse>>> Handle(SystemRoleGetAllQuery request, CancellationToken cancellationToken)
        {
            var roles = request.Predicate == null
                ? await _systemRoleRepository.GetAllAsync(cancellationToken)
                : await _systemRoleRepository.GetAllAsync(request.Predicate, cancellationToken);

            if (roles.IsFailure) return Result.Failure<List<SystemRoleResponse>>(roles);

            var listRes = roles.Value.Select(r => new SystemRoleResponse(r)).ToList();

            return listRes;
        }
    }
}
