using System.Linq.Expressions;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Queries.BlockedUserInfoGetAll;
public sealed record BlockedUserInfoGetAllQuery(
    Expression<Func<BlockedUserInfo, bool>>? Predicate) : IQuery<List<BlockedUserInfoResponse>>;
