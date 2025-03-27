using FluentValidation;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.User.Commands.UserUpdatePassword
{
	public class UserUpdatePasswordCommandValidator : AbstractValidator<UserUpdatePasswordCommand>
	{
		public UserUpdatePasswordCommandValidator()
		{
			RuleFor(x => x.NewPassword).NotEmpty()
				.MinimumLength(Password.MIN_LENGTH)
				.MaximumLength(Password.MAX_LENGTH);
		}
	}
}
