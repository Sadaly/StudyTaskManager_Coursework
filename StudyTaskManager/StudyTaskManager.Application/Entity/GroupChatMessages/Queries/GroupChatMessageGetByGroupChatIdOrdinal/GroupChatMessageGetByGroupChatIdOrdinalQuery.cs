using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetByGroupChatIdOrdinal;
public sealed record GroupChatMessageGetByGroupChatIdOrdinalQuery(Guid GroupChatId, ulong Ordinal) : IQuery<GroupChatMessageResponse>;
