using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupTaskStatusCreatedDomainEvent(Guid GroupTaskStatusId) : IDomainEvent;
