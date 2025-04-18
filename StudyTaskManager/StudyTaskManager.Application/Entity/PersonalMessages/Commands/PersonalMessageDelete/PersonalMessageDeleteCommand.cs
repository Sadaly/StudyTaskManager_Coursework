using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageDelete;

public sealed record PersonalMessageDeleteCommand(Guid Id) : ICommand;
