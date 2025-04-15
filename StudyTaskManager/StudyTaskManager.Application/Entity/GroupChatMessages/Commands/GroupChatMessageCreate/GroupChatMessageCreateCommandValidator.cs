using FluentValidation;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageCreate
{
    public class GroupChatMessageCreateCommandValidator : AbstractValidator<GroupChatMessageCreateCommand>
	{
		public GroupChatMessageCreateCommandValidator()
		{
			RuleFor(x => x.Content).NotEmpty()
				.MaximumLength(Content.MAX_LENGTH);
		}
	}
}
