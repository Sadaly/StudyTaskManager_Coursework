using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Result<Email> emailResult = Email.Create(request.Email.Value);
            Result<UserName> userName = UserName.Create(request.UserName.Value);
            Result<Password> password = Password.Create(request.Password.Value);

            if (request.PhoneNumber != null)
            {
                Result<PhoneNumber> phoneNumber = PhoneNumber.Create(request.PhoneNumber.Value);
                if (!await _userRepository.IsPhoneNumberUniqueAsync(phoneNumber.Value, cancellationToken))
                {
                    return Result.Failure<Guid>(DomainErrors.User.PhoneNumberAlreadyInUse);
                }
            }

            if (!await _userRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
            {
                return Result.Failure<Guid>(DomainErrors.User.EmailAlreadyInUse);
            }

            if (!await _userRepository.IsUserNameUniqueAsync(userName.Value, cancellationToken))
            {
                return Result.Failure<Guid>(DomainErrors.User.UserNameAlreadyInUse);
            }

            var user = User.Create(Guid.NewGuid(), request.UserName, request.Email, request.Password, request.PhoneNumber, request.SystemRoleId);

            _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
