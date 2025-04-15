using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.User.Commands.UserUpdateSystemRole
{
    internal sealed class UserUpdateSystemRoleCommandHandler : ICommandHandler<UserUpdateSystemRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ISystemRoleRepository _systemRoleRepository;

        public UserUpdateSystemRoleCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, ISystemRoleRepository systemRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _systemRoleRepository = systemRoleRepository;
        }

        public async Task<Result> Handle(UserUpdateSystemRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsFailure) return Result.Failure<Guid>(user.Error);

            var newSystemRole = await _systemRoleRepository.GetByIdAsync(request.SystemRoleId, cancellationToken);
            if (newSystemRole.IsFailure) return Result.Failure<Guid>(newSystemRole.Error);

            var change = user.Value.ChangeSystemRole(newSystemRole.Value);
            if (change.IsFailure) return change;

            var update = await _userRepository.UpdateAsync(user.Value, cancellationToken);
            if (update.IsFailure) return update;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
