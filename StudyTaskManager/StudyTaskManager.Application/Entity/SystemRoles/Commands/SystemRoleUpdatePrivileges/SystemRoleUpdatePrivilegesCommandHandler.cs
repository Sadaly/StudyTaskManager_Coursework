using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleUpdatePrivileges
{
    internal sealed class SystemRoleUpdatePrivilegesCommandHandler : ICommandHandler<SystemRoleUpdatePrivilegesCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISystemRoleRepository _systemRoleRepository;

        public SystemRoleUpdatePrivilegesCommandHandler(IUnitOfWork unitOfWork, ISystemRoleRepository systemRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _systemRoleRepository = systemRoleRepository;
        }

        public async Task<Result> Handle(SystemRoleUpdatePrivilegesCommand request, CancellationToken cancellationToken)
        {
            var systemRole = await _systemRoleRepository.GetByIdAsync(request.SystemRoleId, cancellationToken);
            if (systemRole.IsFailure) return systemRole;

            var updatePrivileges = systemRole.Value.UpdatePrivileges(request.CanViewPeoplesGroups, request.CanChangeSystemRoles, request.CanBlockUsers, request.CanDeleteChats);
            if (updatePrivileges.IsFailure) return updatePrivileges;

            var update = await _systemRoleRepository.UpdateAsync(systemRole.Value, cancellationToken);
            if (update.IsFailure) return update;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
