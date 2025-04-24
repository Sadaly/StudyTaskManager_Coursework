using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Chat;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Queries.GroupChatParticipantLastReadGetAll;
public sealed record GroupChatParticipantLastReadGetAllQuery(
	Expression<Func<GroupChatParticipantLastRead, bool>>? Predicate) : IQuery<List<GroupChatParticipantLastReadResponse>>;