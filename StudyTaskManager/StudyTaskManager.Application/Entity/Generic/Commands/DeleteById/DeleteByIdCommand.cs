
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Application.Entity.Generic.Commands.DeleteById;

public sealed record DeleteByIdCommand<TEntity>(Guid IdEntity) : ICommand where TEntity : BaseEntityWithID;
