using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageDelete;
public sealed record GroupChatMessageDeleteCommand(Guid GroupChatId, ulong Ordinal) : ICommand;
