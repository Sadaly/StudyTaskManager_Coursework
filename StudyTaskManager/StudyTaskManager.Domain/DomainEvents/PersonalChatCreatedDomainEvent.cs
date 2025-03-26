using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record PersonalChatCreatedDomainEvent(Guid ChatId) : IDomainEvent;
