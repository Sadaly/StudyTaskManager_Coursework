using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record UserPhoneNumberChangedDomainEvent(Guid UserId) : IDomainEvent;
