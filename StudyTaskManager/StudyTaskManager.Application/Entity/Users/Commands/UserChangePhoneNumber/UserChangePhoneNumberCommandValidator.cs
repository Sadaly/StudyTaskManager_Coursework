using FluentValidation;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserChangePhoneNumber
{
    public class UserChangePhoneNumberCommandValidator : AbstractValidator<UserChangePhoneNumberCommand>
    {
        public UserChangePhoneNumberCommandValidator()
        {
            RuleFor(x => x.NewPhoneNumber).Must(phone
                => phone == null
                || (phone.Length >= PhoneNumber.MIN_LENGTH
                && phone.Length <= PhoneNumber.MAX_LENGTH)).WithMessage(DomainErrors.PhoneNumber.InvalidFormat);
        }
    }
}
