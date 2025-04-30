

using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChats.Commands.GroupChatCreate;

public sealed record GroupChatCreateCommand(
    Guid GroupId,
    string Name,
    bool IsPublic
    ) : ICommand<Guid>;