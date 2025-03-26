using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupTaskUpdateUpdatedDomainEvent(Guid GroupTaskUpdId) : IDomainEvent;
