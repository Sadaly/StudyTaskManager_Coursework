using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Chat;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.GroupChats.Queries.GroupChatTake;

public sealed record GroupChatTakeQuery(
    int StartIndex,
    int Count,
    Expression<Func<GroupChat, bool>>? Perdicate) : IQuery<List<GroupChatResponse>>;
