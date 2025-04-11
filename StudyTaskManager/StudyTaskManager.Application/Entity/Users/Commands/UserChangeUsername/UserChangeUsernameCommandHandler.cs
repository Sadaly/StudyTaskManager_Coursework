using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserChangeUsername
{
    internal sealed class UserChangeUsernameCommandHandler : ICommandHandler<UserChangeUsernameCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserChangeUsernameCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(UserChangeUsernameCommand request, CancellationToken cancellationToken)
        {
            var username = Username.Create(request.NewUsername);
            if (username.IsFailure) return username;

            var isUsernameUniqueAsync = await _userRepository.IsUsernameUniqueAsync(username.Value, cancellationToken);
            if (isUsernameUniqueAsync.IsFailure) return isUsernameUniqueAsync;
            if (!isUsernameUniqueAsync.Value) return Result.Failure(PersistenceErrors.User.NotUniqueUsername);

            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsFailure) return user;

            user.Value.ChangeUsername(username.Value);

            await _userRepository.UpdateAsync(user.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
