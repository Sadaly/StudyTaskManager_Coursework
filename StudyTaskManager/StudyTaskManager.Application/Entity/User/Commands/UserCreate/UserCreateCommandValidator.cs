using FluentValidation;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.CreateUser
{
    public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
    {
        public UserCreateCommandValidator() 
        {
            RuleFor(x => x.Username).NotEmpty()
                .MaximumLength(Username.MAX_LENGTH);

            RuleFor(x => x.Email).NotEmpty().Must(email 
                => email.Split('@').Length == 2);

            RuleFor(x => x.Password).NotEmpty()
                .MinimumLength(Password.MIN_LENGTH)
                .MaximumLength(Password.MAX_LENGTH);

            RuleFor(x => x.PhoneNumber).Must(phone 
                => phone == null 
                || (phone.Length >= PhoneNumber.MIN_LENGTH 
                && phone.Length <= PhoneNumber.MAX_LENGTH));
        }
	}
}
