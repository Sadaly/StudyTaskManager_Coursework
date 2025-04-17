using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Commands.GroupTaskUpdateUpdate;

public sealed record GroupTaskUpdateUpdateCommand(
    Guid GroupTaskUpdateId,
    string NewContent) : ICommand;
