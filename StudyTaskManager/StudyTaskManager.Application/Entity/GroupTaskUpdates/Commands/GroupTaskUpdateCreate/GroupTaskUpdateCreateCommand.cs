using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Commands.GroupTaskUpdateCreate;

public sealed record GroupTaskUpdateCreateCommand(
    Guid CreatorId,
    Guid TaskId,
    string Content) : ICommand<Guid>;
