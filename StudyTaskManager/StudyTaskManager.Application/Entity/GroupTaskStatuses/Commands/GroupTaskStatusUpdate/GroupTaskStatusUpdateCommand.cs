using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Commands.GroupTaskStatusUpdate;

public sealed record GroupTaskStatusUpdateCommand(
    Guid GroupTaskStatusId,
    string? NewName,
    bool? NewCanBeUpdated,
    string? NewDescription) : ICommand;
