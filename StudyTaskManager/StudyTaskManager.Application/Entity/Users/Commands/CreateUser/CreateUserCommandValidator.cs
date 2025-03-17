using FluentValidation;

namespace StudyTaskManager.Application.Entity.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() 
        {
            RuleFor(x => x.Email).NotEmpty();
			RuleFor(x => x.Email).EmailAddress();
			RuleFor(x => x.UserName).NotEmpty();
			RuleFor(x => x.UserName.Length).GreaterThanOrEqualTo(3).WithMessage("Имя пользователя слишком короткое (<3 символов).");
			RuleFor(x => x.UserName.Length).LessThanOrEqualTo(50).WithMessage("Имя пользователя слишком длинное (>50 символов).");
			RuleFor(x => x.Password).Must(BeAValidString).WithMessage("Строка содержит запрещенные символы ('@', '#', '$', '%').");
			RuleFor(x => x.Password.Length).GreaterThanOrEqualTo(8).WithMessage("Пароль слишком короткий (<8 символов)."); ;
			RuleFor(x => x.Password.Length).LessThanOrEqualTo(100).WithMessage("Пароль слишком длинный (>100 символов)."); ;

		}
		private bool BeAValidString(string value)
		{
			// Запрещенные символы
			var forbiddenChars = new[] { '@', '#', '$', '%' };

			// Проверяем, содержит ли строка запрещенные символы
			return !value.Any(c => forbiddenChars.Contains(c));
		}
	}
}
