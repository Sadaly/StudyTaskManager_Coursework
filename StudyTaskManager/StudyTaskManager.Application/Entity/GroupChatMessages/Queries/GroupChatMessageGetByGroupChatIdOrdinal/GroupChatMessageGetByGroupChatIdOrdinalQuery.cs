using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetByGroupChatIdOrdinal;
public sealed record GroupChatMessageGetByGroupChatIdOrdinalQuery(Guid GroupChatId, ulong Ordinal) : IQuery<GroupChatMessageResponse>;
