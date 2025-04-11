using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleCreate
{
    internal class SystemRoleCreateCommandHandler : ICommandHandler<SystemRoleCreateCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISystemRoleRepository _systemRoleRepository;

        public SystemRoleCreateCommandHandler(IUnitOfWork unitOfWork, ISystemRoleRepository systemRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _systemRoleRepository = systemRoleRepository;
        }

        public async Task<Result<Guid>> Handle(SystemRoleCreateCommand request, CancellationToken cancellationToken)
        {
            Result<Title> name = Title.Create(request.Name);
            if (name.IsFailure) return Result.Failure<Guid>(name.Error);

            Result<SystemRole> systemRoleInDB;
            systemRoleInDB = await _systemRoleRepository.GetByTitleAsync(name.Value, cancellationToken);
            if (systemRoleInDB.IsFailure) return Result.Failure<Guid>(systemRoleInDB.Error);
            if (systemRoleInDB.Value != null) return Result.Failure<Guid>(PersistenceErrors.SystemRole.TitleAlreadyInUse);

            Result<SystemRole> systemRole = SystemRole.Create(
                name.Value,
                request.CanViewPeoplesGroups,
                request.CanChangeSystemRoles,
                request.CanBlockUsers,
                request.CanDeleteChats);
            if (systemRole.IsFailure) return Result.Failure<Guid>(systemRole.Error);

            Result add = await _systemRoleRepository.AddAsync(systemRole.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<Guid>(add.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(systemRole.Value.Id);
        }
    }
}
