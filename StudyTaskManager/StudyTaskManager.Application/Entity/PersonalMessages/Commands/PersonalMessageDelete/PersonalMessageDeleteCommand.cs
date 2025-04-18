using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageDelete;

public sealed record PersonalMessageDeleteCommand(Guid Id) : ICommand;
