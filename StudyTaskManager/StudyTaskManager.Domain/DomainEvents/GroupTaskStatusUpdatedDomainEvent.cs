using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupTaskStatusUpdatedDomainEvent(Guid GroupTaskStatusId) : IDomainEvent;
