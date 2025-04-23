using System.Linq.Expressions;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Queries.BlockedUserInfoTake;
public sealed record BlockedUserInfoTakeQuery(
    int StartIndex, int Count, 
    Expression<Func<BlockedUserInfo, bool>>? Predicate) : IQuery<List<BlockedUserInfoResponse>>;