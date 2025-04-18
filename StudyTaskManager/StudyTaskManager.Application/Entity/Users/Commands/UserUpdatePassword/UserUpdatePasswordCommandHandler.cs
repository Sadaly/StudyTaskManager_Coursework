using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserUpdatePassword
{
    internal sealed class UserUpdatePasswordCommandHandler : ICommandHandler<UserUpdatePasswordCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserUpdatePasswordCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(UserUpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var password = Password.Create(request.NewPassword);
            if (password.IsFailure) return Result.Failure(password.Error);

            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsFailure) return Result.Failure(user.Error);

            var changePasswordResult = user.Value.ChangePassword(password.Value);
            if (changePasswordResult.IsFailure) return Result.Failure<Guid>(changePasswordResult.Error);

            var update = await _userRepository.UpdateAsync(user.Value, cancellationToken);
            if (update.IsFailure) return Result.Failure(update.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
