
using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Generic.Commands.DeleteById;

public sealed record DeleteByIdCommand(Guid IdEntity) : ICommand;
