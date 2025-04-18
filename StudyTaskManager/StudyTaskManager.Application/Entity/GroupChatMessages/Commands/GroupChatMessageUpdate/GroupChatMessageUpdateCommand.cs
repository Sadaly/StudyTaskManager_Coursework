using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageUpdate;
public sealed record GroupChatMessageUpdateCommand(Guid GroupChatId, ulong Ordinal, string Content) : ICommand<(Guid, ulong)>;
