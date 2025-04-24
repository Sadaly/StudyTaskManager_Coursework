using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Task;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetByGroup;

public sealed record GroupTaskGetAllQuery(Expression<Func<GroupTask, bool>>? Predicate) : IQuery<List<GroupTaskResponse>>;