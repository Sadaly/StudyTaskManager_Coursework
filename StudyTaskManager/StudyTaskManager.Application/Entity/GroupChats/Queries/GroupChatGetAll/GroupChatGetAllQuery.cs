using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Chat;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.GroupChats.Queries.GroupChatGetAll;

public sealed record GroupChatGetAllQuery(
    Expression<Func<GroupChat, bool>>? Predicate) : IQuery<List<GroupChatResponse>>;