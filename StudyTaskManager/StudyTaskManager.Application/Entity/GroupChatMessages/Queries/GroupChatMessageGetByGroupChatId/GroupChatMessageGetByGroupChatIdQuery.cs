using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetByGroupChatId;
public sealed record GroupChatMessageGetByGroupChatIdQuery (Guid GroupChatId) : IQuery<List<GroupChatMessage>>;
