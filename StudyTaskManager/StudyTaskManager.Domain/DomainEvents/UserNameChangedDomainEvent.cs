using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record UsernameChangedDomainEvent(Guid UserId) : IDomainEvent;
