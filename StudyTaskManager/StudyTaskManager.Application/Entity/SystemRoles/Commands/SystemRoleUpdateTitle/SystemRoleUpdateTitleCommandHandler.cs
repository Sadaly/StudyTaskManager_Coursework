using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleUpdateTitle
{
    class SystemRoleUpdateTitleCommandHandler : ICommandHandler<SystemRoleUpdateTitleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISystemRoleRepository _systemRoleRepository;

        public SystemRoleUpdateTitleCommandHandler(IUnitOfWork unitOfWork, ISystemRoleRepository systemRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _systemRoleRepository = systemRoleRepository;
        }

        public async Task<Result> Handle(SystemRoleUpdateTitleCommand request, CancellationToken cancellationToken)
        {
            var newTitle = Title.Create(request.NewTitle);
            if (newTitle.IsFailure) return newTitle;

            var systemRole = await _systemRoleRepository.GetByIdAsync(request.SystemRoleId, cancellationToken);
            if (systemRole.IsFailure) return systemRole;

            var updateTitle = systemRole.Value.UpdateTitle(newTitle.Value);
            if (updateTitle.IsFailure) return updateTitle;

            var update = await _systemRoleRepository.UpdateAsync(systemRole.Value, cancellationToken);
            if (update.IsFailure) return update;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
