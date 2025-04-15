using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Generic.Commands.DeleteById;
using StudyTaskManager.Domain.Abstractions.Repositories.Generic;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Generic.Queries.GetById
{
    class GetByIdQueryHandler<TEntity> : IQueryHandler<GetByIdQuery<TEntity>, TEntity> where TEntity : BaseEntityWithID
    {
        private readonly IRepositoryWithID<TEntity> _repository;

        public GetByIdQueryHandler(IRepositoryWithID<TEntity> repository)
        {
            _repository = repository;
        }
        public async Task<Result<TEntity>> Handle(GetByIdQuery<TEntity> request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
