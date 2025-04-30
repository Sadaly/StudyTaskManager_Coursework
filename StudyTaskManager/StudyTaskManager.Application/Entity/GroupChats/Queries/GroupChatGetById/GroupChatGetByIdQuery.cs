using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChats.Queries.GroupChatGetById;

public sealed record GroupChatGetByIdQuery(
    Guid Id) : IQuery<GroupChatResponse>;