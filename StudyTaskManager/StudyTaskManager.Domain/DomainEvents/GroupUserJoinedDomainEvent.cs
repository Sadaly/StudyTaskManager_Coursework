using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupUserJoinedDomainEvent(Guid UserId, Guid GroupId) : IDomainEvent;
