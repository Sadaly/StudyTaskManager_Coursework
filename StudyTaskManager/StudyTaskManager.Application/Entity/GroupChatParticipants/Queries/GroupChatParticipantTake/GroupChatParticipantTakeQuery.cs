using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Chat;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.GroupChatParticipants.Queries.GroupChatParticipantTake;

public sealed record GroupChatParticipantTakeQuery(
    int StartIndex,
    int Count,
    Expression<Func<GroupChatParticipant, bool>> Perdicate) : IQuery<List<GroupChatParticipantResponse>>;