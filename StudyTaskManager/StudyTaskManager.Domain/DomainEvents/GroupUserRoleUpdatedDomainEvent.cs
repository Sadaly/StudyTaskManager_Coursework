using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupUserRoleUpdatedDomainEvent(Guid UserId, Guid GroupId) : IDomainEvent;
