using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record LogCreatedDomainEvent(Guid LogId) : IDomainEvent;
