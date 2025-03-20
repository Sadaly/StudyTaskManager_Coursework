using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupUserLeftDomainEvent(Guid UserId, Guid GroupId) : IDomainEvent;
