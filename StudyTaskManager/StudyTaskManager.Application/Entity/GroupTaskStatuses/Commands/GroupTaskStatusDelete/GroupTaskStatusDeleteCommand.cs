using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Commands.GroupTaskStatusDelete;

public sealed record GroupTaskStatusDeleteCommand(Guid Id) : ICommand;