using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupRemoveInvite;

public sealed record GroupRemoveInviteCommand(
    Guid SenderId,
    Guid ReceiverId,
    Guid GroupId) : ICommand;