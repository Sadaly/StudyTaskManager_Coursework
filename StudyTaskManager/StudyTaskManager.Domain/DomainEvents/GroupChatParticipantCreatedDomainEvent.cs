using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupChatParticipantCreatedDomainEvent(Guid GroupChatId, Guid UserId) : IDomainEvent;
