using StudyTaskManager.Application.Abstractions;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserLogin
{
    internal sealed class UserLoginCommandHandler : ICommandHandler<UserLoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public UserLoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<string>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);
            if (email.IsFailure) return Result.Failure<string>(email.Error);

            var password = Password.Create(request.Password);
            if (password.IsFailure) return Result.Failure<string>(password.Error);

            var passwordHash = PasswordHash.Create(password.Value);
            if (passwordHash.IsFailure) return Result.Failure<string>(passwordHash.Error);

            var user = await _userRepository.GetByEmailAsync(email.Value, cancellationToken);
            if (user.IsFailure) return Result.Failure<string>(email.Error);

            if (user.Value.PasswordHash.Value != passwordHash.Value.Value)
            {
                return Result.Failure<string>(PersistenceErrors.User.IncorrectUsernameOrPassword);
            }

            string token = _jwtProvider.Generate(user.Value);

            return Result.Success(token);
        }
    }
}
