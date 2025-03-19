using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record LogActionCreatedDomainEvent(Guid LogActionId) : IDomainEvent;
