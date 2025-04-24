using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.GroupInvites.Queries.GroupInviteGetAll;

public sealed record GroupInviteGetAllQuery(
    Expression<Func<GroupInvite, bool>> Perdicate) : IQuery<List<GroupInviteResponse>>;