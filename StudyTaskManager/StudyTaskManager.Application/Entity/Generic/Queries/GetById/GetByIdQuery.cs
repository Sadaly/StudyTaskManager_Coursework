using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Application.Entity.Generic.Queries.GetById;

public record GetByIdQuery<TEntity>(Guid Id) : IQuery<TEntity> where TEntity : BaseEntityWithID;
