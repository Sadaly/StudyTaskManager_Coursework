using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Commands.GroupTaskStatusCreate;

public sealed record GroupTaskStatusCreateCommand(
    string Title,
    bool CanBeUpdated,
    Guid? GroupId,
    string? Description) : ICommand<Guid>;