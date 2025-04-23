using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.SystemRoles.Queries.SystemRoleGetById
{
    public class SystemRoleGetByIdQueryHandler : IQueryHandler<SystemRoleGetByIdQuery, SystemRoleResponse>
    {
        private readonly ISystemRoleRepository _systemRoleRepository;

        public SystemRoleGetByIdQueryHandler(ISystemRoleRepository systemRoleRepository)
        {
            _systemRoleRepository = systemRoleRepository;
        }

        public async Task<Result<SystemRoleResponse>> Handle(SystemRoleGetByIdQuery request, CancellationToken cancellationToken)
        {
            var systemRole = await _systemRoleRepository.GetByIdAsync(request.SystemRoleId, cancellationToken);
            if(systemRole.IsFailure) return Result.Failure<SystemRoleResponse>(systemRole);
            return new SystemRoleResponse(systemRole.Value);
        }
    }
}
