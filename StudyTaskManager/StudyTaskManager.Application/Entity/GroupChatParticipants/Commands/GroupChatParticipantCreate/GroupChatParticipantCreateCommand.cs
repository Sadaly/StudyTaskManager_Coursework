using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChatParticipants.Commands.GroupChatParticipantCreate;
public sealed record GroupChatParticipantCreateCommand(Guid UserId, Guid GroupChatId) : ICommand;