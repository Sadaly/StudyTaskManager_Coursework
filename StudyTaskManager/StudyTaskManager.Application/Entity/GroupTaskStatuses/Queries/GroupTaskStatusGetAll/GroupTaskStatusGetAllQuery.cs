using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Task;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetAll;

public sealed record GroupTaskStatusGetAllQuery(
	Expression<Func<GroupTaskStatus, bool>>? Predicate) : IQuery<List<GroupTaskStatusRepsonse>>;