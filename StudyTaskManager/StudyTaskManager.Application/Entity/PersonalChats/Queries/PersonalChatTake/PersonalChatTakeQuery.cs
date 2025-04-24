using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User.Chat;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatTake;

public sealed record PersonalChatTakeQuery(
    int StartIndex,
    int Count,
    Expression<Func<PersonalChat, bool>>? Perdicate) : IQuery<List<PersonalChatsResponse>>;