//using StudyTaskManager.Application.Abstractions.Messaging;
//using StudyTaskManager.Domain.Abstractions;
//using StudyTaskManager.Domain.Abstractions.Repositories;
//using StudyTaskManager.Domain.Entity.User;
//using StudyTaskManager.Domain.Errors;
//using StudyTaskManager.Domain.Shared;
//using StudyTaskManager.Domain.ValueObjects;

//namespace StudyTaskManager.Application.Entity.Users.Commands.UserChangePhoneNumber
//{
//    internal sealed class UserChangePhoneNumberCommandHandler : ICommandHandler<UserChangePhoneNumberCommand, Guid>
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IUserRepository _userRepository;

//        public UserChangePhoneNumberCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
//        {
//            _unitOfWork = unitOfWork;
//            _userRepository = userRepository;
//        }

//        public async Task<Result<Guid>> Handle(UserChangePhoneNumberCommand request, CancellationToken cancellationToken)
//        {
//            Result<Email> emailResult = Email.Create(request.Email);
//            Result<Username> username = Username.Create(request.Username);
//            Result<Password> password = Password.Create(request.Password);
//            Result<PhoneNumber>? phoneNumber = null;
//            SystemRole? role = request.SystemRole;

//            if (request.PhoneNumber != null)
//            {
//                //Перед созднием экземпляра мы проверяем, что он не равен null
//                phoneNumber = PhoneNumber.Create(request.PhoneNumber);
//                if (!_userRepository.IsPhoneNumberUniqueAsync(phoneNumber.Value, cancellationToken).Result.Value)
//                {
//                    return Result.Failure<Guid>(DomainErrors.User.PhoneNumberAlreadyInUse);
//                }
//            }

//            if (!_userRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken).Result.Value)
//            {
//                return Result.Failure<Guid>(DomainErrors.User.EmailAlreadyInUse);
//            }

//            if (!_userRepository.IsUsernameUniqueAsync(username.Value, cancellationToken).Result.Value)
//            {
//                return Result.Failure<Guid>(DomainErrors.User.UsernameAlreadyInUse);
//            }

//            var user = User.Create(Guid.NewGuid(), username.Value, emailResult.Value, password.Value, phoneNumber?.Value, role).Value;

//            await _userRepository.AddAsync(user, cancellationToken);
//            await _unitOfWork.SaveChangesAsync(cancellationToken);

//            return user.Id;
//        }
//    }
//}
