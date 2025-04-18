using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetAll;
public sealed record GroupChatMessageGetAllQuery : IQuery<GroupChatMessageListResponse>;
