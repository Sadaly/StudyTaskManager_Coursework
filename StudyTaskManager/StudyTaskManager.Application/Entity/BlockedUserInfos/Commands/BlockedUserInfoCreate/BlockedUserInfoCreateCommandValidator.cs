using FluentValidation;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Commands.BlockedUserInfoCreate
{
    public class BlockedUserInfoCreateCommandValidator : AbstractValidator<BlockedUserInfoCreateCommand>
    {
        public BlockedUserInfoCreateCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Reason).NotEmpty();
        }
    }
}
