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

        public async Task<Result<string>> Handle(
            UserLoginCommand request,
            CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(request.Email);
            var password = Password.Create(request.Password);

            var phResult = PasswordHash.Create(password.Value);

            var userResult = await _userRepository.GetByEmailAsync(
                emailResult.Value,
                cancellationToken);

            if (userResult.IsFailure)
            {
                return Result.Failure<string>(userResult.Error);
            }
            if (userResult.Value == null)
            {
                return Result.Failure<string>(PersistenceErrors.User.IncorrectUsernameOrPassword);
            }

            var user = userResult.Value;

            if (user.PasswordHash.Value != phResult.Value.Value)
            {
                return Result.Failure<string>(PersistenceErrors.User.IncorrectUsernameOrPassword);
            }

            string token = _jwtProvider.Generate(user);

            return token;
        }
    }
}
