using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Chat;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.GroupChatParticipants.Queries.GroupChatParticipantGetAll;

public sealed record GroupChatParticipantGetAllQuery(
    Expression<Func<GroupChatParticipant, bool>>? Predicate) : IQuery<List<GroupChatParticipantResponse>>;