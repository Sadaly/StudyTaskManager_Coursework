using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User.Chat;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Queries.PersonalMessageGetAll;

public sealed record PersonalMessageGetAllQuery(
    Expression<Func<PersonalMessage, bool>>? Predicate) : IQuery<List<PersonalMessageResponse>>;