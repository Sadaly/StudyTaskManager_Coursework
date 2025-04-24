using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User.Chat;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatsGetAll;

public sealed record PersonalChatsGetAllQuery(
	Expression<Func<PersonalChat, bool>>? Predicate) : ICommand<List<PersonalChatsResponse>>;
