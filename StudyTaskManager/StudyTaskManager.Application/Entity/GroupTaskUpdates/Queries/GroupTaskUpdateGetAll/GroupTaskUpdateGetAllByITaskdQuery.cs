using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Task;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Queries.GroupTaskUpdateGetAll;

public sealed record GroupTaskUpdateGetAllQuery(
	Expression<Func<GroupTaskUpdate, bool>>? Predicate) : IQuery<List<GroupTaskUpdateResponse>>;