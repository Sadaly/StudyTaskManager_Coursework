using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserChangeUsername
{
    internal sealed class UserChangeUsernameCommandHandler : ICommandHandler<UserChangeUsernameCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserChangeUsernameCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(UserChangeUsernameCommand request, CancellationToken cancellationToken)
        {
            Result<Username> usernameResult = Username.Create(request.NewUsername);

			if (!(await _userRepository.IsUsernameUniqueAsync(usernameResult.Value, cancellationToken)).Value)
			{
				return Result.Failure<Guid>(PersistenceErrors.User.UsernameAlreadyInUse);
			}

			var foundUserResult = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
			if (foundUserResult.IsFailure)
			{
				return Result.Failure<Guid>(foundUserResult.Error);
			}

            var user = foundUserResult.Value;
            user.ChangeUsername(usernameResult.Value);

			await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
