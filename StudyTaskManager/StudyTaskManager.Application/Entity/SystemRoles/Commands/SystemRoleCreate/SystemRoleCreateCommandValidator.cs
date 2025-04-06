using FluentValidation;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleCreate
{
    class SystemRoleCreateCommandValidator : AbstractValidator<SystemRoleCreateCommand>
    {
        public SystemRoleCreateCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MaximumLength(Title.MAX_LENGTH);
        }
    }
}
