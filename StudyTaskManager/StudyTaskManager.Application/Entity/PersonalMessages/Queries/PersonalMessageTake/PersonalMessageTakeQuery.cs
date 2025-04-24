using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User.Chat;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Queries.PersonalMessageTake;

public sealed record PersonalMessageTakeQuery(
    int StartIndex,
    int Count,
    Expression<Func<PersonalMessage, bool>>? Predicate) : IQuery<List<PersonalMessageResponse>>;