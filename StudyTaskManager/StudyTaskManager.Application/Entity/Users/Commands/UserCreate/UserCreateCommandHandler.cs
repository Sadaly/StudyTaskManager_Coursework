using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
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
            Result<Email> emailResult = Email.Create(request.Email);
            Result<Username> username = Username.Create(request.Username);
            Result<Password> password = Password.Create(request.Password);
            Result<PhoneNumber>? phoneNumber = null;
            SystemRole? role = null;

            if (request.SystemRoleId != null)
            {
                var foundRoleResult = _systemRoleRepository.GetByIdAsync(request.SystemRoleId.Value, cancellationToken).Result;
                if (foundRoleResult.IsSuccess)
                {
                    role = foundRoleResult.Value;
                }
                else
                {
                    return Result.Failure<Guid>(foundRoleResult.Error);
                }
            }

            if (request.PhoneNumber != null)
            {
                //Перед созданием экземпляра мы проверяем, что он не равен null
                phoneNumber = PhoneNumber.Create(request.PhoneNumber);
                if (!_userRepository.IsPhoneNumberUniqueAsync(phoneNumber.Value, cancellationToken).Result.Value)
                {
                    return Result.Failure<Guid>(PersistenceErrors.User.PhoneNumberAlreadyInUse);
                }
            }

            if (!_userRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken).Result.Value)
            {
                return Result.Failure<Guid>(PersistenceErrors.User.EmailAlreadyInUse);
            }

            if (!_userRepository.IsUsernameUniqueAsync(username.Value, cancellationToken).Result.Value)
            {
                return Result.Failure<Guid>(PersistenceErrors.User.UsernameAlreadyInUse);
            }

            var user = Domain.Entity.User.User.Create(username.Value, emailResult.Value, password.Value, phoneNumber?.Value, role).Value;

            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
