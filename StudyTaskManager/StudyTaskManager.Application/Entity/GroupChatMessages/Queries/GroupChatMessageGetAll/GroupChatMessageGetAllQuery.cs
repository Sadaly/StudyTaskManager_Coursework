using System.Linq.Expressions;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetAll;
public sealed record GroupChatMessageGetAllQuery(
    Expression<Func<GroupChatMessage, bool>>? Predicate) : IQuery<GroupChatMessageResponse>;
