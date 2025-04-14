using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Common;


namespace StudyTaskManager.Application.Entity.Generic.Queries.GetAll;

public sealed record GetAllQuery<TEntity>() : IQuery<List<TEntity>> where TEntity : BaseEntity;
