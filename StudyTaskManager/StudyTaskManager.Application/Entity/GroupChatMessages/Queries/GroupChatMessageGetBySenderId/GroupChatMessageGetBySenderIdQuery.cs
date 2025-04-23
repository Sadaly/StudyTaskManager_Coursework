using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetBySenderId;
public sealed record GroupChatMessageGetBySenderIdQuery(int StartIndex, int Count, Guid SenderId) : IQuery<List<GroupChatMessageResponse>>;