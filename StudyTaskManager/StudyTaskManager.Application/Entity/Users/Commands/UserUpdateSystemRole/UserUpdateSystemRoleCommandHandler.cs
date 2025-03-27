using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.User.Commands.UserUpdateSystemRole
{
	internal sealed class UserUpdateSystemRoleCommandHandler : ICommandHandler<UserUpdateSystemRoleCommand, Guid>
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

		public async Task<Result<Guid>> Handle(UserUpdateSystemRoleCommand request, CancellationToken cancellationToken)
		{
			var foundUserResult = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
			if (foundUserResult.IsFailure)
			{
				return Result.Failure<Guid>(foundUserResult.Error);
			}

			var foundSystemRoleResult = await _systemRoleRepository.GetByIdAsync(request.SystemRoleId, cancellationToken);
			if (foundSystemRoleResult.IsFailure)
			{
				return Result.Failure<Guid>(foundSystemRoleResult.Error);
			}

			var user = foundUserResult.Value;
			var systemRole = foundSystemRoleResult.Value;

			user.ChangeSystemRole(systemRole);

			await _userRepository.UpdateAsync(user, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return user.Id;
		}
	}
}
