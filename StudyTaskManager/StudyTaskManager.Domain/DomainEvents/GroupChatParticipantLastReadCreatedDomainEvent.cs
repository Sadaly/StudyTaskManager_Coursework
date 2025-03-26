using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupChatParticipantLastReadCreatedDomainEvent(Guid GroupChatId, Guid UserId) : IDomainEvent;
