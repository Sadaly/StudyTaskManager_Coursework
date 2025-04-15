using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageCreate;

public sealed record GroupChatMessageCreateCommand(Guid GroupChatId, ulong Ordinal, Guid SenderId, string Content) : ICommand<(Guid, ulong)>;