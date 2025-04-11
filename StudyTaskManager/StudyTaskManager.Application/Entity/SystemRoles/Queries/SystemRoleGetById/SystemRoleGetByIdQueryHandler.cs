using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.SystemRoles.Queries.SystemRoleGetById
{
    public class SystemRoleGetByIdQueryHandler : IQueryHandler<SystemRoleGetByIdQuery, SystemRole>
    {
        private readonly ISystemRoleRepository _systemRoleRepository;

        public SystemRoleGetByIdQueryHandler(ISystemRoleRepository systemRoleRepository)
        {
            _systemRoleRepository = systemRoleRepository;
        }

        public async Task<Result<SystemRole>> Handle(SystemRoleGetByIdQuery request, CancellationToken cancellationToken)
        {
            var systemRole = await _systemRoleRepository.GetByIdAsync(request.SystemRoleId, cancellationToken);
            return systemRole;
        }
    }
}
