using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupCreate;

public sealed record GroupCreateCommand(
    string Title,
    string? Description,
    Guid CreatorId) : ICommand<Guid>;
