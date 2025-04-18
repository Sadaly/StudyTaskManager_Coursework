using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupRoleTitleUpdatedDomainEvent(Guid RoleId) : IDomainEvent;