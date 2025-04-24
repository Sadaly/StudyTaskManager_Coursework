using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChatParticipants.Commands.GroupChatParticipantDelete;
public sealed record GroupChatParticipantDeleteCommand(Guid UserId, Guid GroupChatId) : ICommand;