using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserCreate
{
    internal sealed class UserCreateCommandHandler : ICommandHandler<UserCreateCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ISystemRoleRepository _systemRoleRepository;

        public UserCreateCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, ISystemRoleRepository systemRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _systemRoleRepository = systemRoleRepository;
        }

        public async Task<Result<Guid>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);
            if (email.IsFailure) return Result.Failure<Guid>(email);

            var username = Username.Create(request.Username);
            if (username.IsFailure) return Result.Failure<Guid>(username);

            var password = Password.Create(request.Password);
            if (password.IsFailure) return Result.Failure<Guid>(password);

            PhoneNumber? phoneNumber = null;
            SystemRole? role = null;

            if (request.PhoneNumber != null)
            {
                var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
                if (phoneNumberResult.IsFailure) return Result.Failure<Guid>(phoneNumberResult);
                phoneNumber = phoneNumberResult.Value;
            }

            if (request.SystemRoleId != null)
            {
                var foundRoleResult = _systemRoleRepository.GetByIdAsync(request.SystemRoleId.Value, cancellationToken).Result;
                if (foundRoleResult.IsFailure) return Result.Failure<Guid>(foundRoleResult);
                role = foundRoleResult.Value;
            }

            var user = Domain.Entity.User.User.Create(username.Value, email.Value, password.Value, phoneNumber, role);
            if (user.IsFailure) return Result.Failure<Guid>(user);

            var add = await _userRepository.AddAsync(user.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<Guid>(add);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Value.Id;
        }
    }
}
