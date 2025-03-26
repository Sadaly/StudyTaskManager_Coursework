using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupTaskCreatedDomainEvent(Guid GroupTaskId) : IDomainEvent;
