using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserDelete
{
    internal sealed class UserDeleteCommandHandler : ICommandHandler<UserDeleteCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ISystemRoleRepository _systemRoleRepository;

        public UserDeleteCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, ISystemRoleRepository systemRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _systemRoleRepository = systemRoleRepository;
        }

        public async Task<Result<Guid>> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
			var foundUserResult = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
			if (foundUserResult.IsFailure)
			{
				return Result.Failure<Guid>(foundUserResult.Error);
			}

			var user = foundUserResult.Value;
			user.Delete();

			await _userRepository.UpdateAsync(user, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return user.Id;
		}
    }
}
