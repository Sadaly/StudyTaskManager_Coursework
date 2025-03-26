using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record EntityDeletedDomainEvent(BaseEntity BaseEntity, string Type) : IDomainEvent;
public sealed record EntityWithIdDeletedDomainEvent(Guid id, string Type) : IDomainEvent;
