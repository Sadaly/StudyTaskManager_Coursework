using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupChatParticipantLastReadUpdatedDomainEvent(Guid GroupChatId, Guid UserId) : IDomainEvent;
