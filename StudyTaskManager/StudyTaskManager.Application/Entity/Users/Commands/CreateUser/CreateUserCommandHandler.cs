using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository) : ICommandHandler<CreateUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserRepository _userRepository = userRepository;

		public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Result<Email> emailResult = Email.Create(request.Email);
            Result<UserName> userNameResult = UserName.Create(request.UserName);
            Result<Password> passwordResult = Password.Create(request.Password);
            Result<PhoneNumber>? phoneNumberResult = null;

			if (request.PhoneNumber != null && request.PhoneNumber != String.Empty)
			{
				//Перед созданием экземпляра мы проверяем, что он не равен null
				phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
                if (!await _userRepository.IsPhoneNumberUniqueAsync(phoneNumberResult.Value, cancellationToken))
                {
                    return Result.Failure<Guid>(DomainErrors.User.PhoneNumberAlreadyInUse);
                }
            }

            if (!await _userRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
            {
                return Result.Failure<Guid>(DomainErrors.User.EmailAlreadyInUse);
            }

            if (!await _userRepository.IsUserNameUniqueAsync(userNameResult.Value, cancellationToken))
            {
                return Result.Failure<Guid>(DomainErrors.User.UserNameAlreadyInUse);
            }

            var user = User.Create(
                Guid.NewGuid(), 
                userNameResult.Value, 
                emailResult.Value, 
                passwordResult.Value, 
                phoneNumberResult?.Value, 
                request.SystemRole);

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
