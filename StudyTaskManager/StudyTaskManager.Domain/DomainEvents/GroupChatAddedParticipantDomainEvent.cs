using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupChatAddedParticipantDomainEvent(Guid GroupChatId) : IDomainEvent;
