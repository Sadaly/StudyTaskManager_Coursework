using FluentValidation;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserLogin
{
    public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginCommandValidator() 
        {
            RuleFor(x => x.Email).NotEmpty().Must(email 
                => email.Split('@').Length == 2);

            RuleFor(x => x.Password).NotEmpty()
                .MinimumLength(Password.MIN_LENGTH)
                .MaximumLength(Password.MAX_LENGTH);
        }
	}
}
