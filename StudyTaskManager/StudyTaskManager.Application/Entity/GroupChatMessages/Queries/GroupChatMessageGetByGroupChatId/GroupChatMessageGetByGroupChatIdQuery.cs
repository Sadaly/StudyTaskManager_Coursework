using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetByGroupChatId;
public sealed record GroupChatMessageGetByGroupChatIdQuery (int StartIndex, int Count, Guid GroupChatId) : IQuery<List<GroupChatMessageResponse>>;
