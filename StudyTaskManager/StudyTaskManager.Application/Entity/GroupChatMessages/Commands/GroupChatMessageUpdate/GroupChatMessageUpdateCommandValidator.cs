using FluentValidation;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageUpdate
{
	public class GroupChatMessageUpdateCommandValidator : AbstractValidator<GroupChatMessageUpdateCommand>
	{
		public GroupChatMessageUpdateCommandValidator()
		{
			RuleFor(x => x.Content).NotEmpty()
				.MaximumLength(Content.MAX_LENGTH);
		}
	}
}
