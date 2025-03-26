using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record PersonalChatMessageAddedDomainEvent(Guid MessageId) : IDomainEvent;
