using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleDelete
{
    internal class SystemRoleDeleteCommandHandler : ICommandHandler<SystemRoleDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISystemRoleRepository _systemRoleRepository;

        public SystemRoleDeleteCommandHandler(IUnitOfWork unitOfWork, ISystemRoleRepository systemRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _systemRoleRepository = systemRoleRepository;
        }

        public async Task<Result> Handle(SystemRoleDeleteCommand request, CancellationToken cancellationToken)
        {
            Result<SystemRole> systemRole = await _systemRoleRepository.GetByIdAsync(request.SystemRoleId, cancellationToken);
            if (systemRole.IsFailure) return systemRole;

            Result delete = await _systemRoleRepository.RemoveAsync(systemRole.Value, cancellationToken);
            if (delete.IsFailure) return delete;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
