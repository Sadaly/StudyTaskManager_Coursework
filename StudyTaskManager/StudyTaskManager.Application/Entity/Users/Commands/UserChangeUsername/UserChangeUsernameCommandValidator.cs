using FluentValidation;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserChangeUsername
{
    public class UserChangeUsernameCommandValidator : AbstractValidator<UserChangeUsernameCommand>
    {
        public UserChangeUsernameCommandValidator()
		{
			RuleFor(x => x.NewUsername).NotEmpty()
				.MaximumLength(Username.MAX_LENGTH);
		}
    }
}
