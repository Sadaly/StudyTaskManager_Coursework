using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Chat;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageTake;
public sealed record GroupChatMessageTakeQuery(
    int StartIndex, int Count,
    Expression<Func<GroupChatMessage, bool>>? Predicate) : IQuery<List<GroupChatMessageResponse>>;
