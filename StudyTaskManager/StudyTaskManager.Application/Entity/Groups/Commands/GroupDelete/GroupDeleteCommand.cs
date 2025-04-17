using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupDelete;

public sealed record GroupDeleteCommand(Guid Id) : ICommand;